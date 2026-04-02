//
// StreamHeader.cs: Provides support for reading WavPack audio properties.
//
// Author:
//   Brian Nickel (brian.nickel@gmail.com)
//
// Original Source:
//   wvproperties.cpp from libtunepimp
//
// Copyright (C) 2006-2007 Brian Nickel
// Copyright (C) 2006 by Lukáš Lalinský (Original Implementation)
// Copyright (C) 2004 by Allan Sandfeld Jensen (Original Implementation)
//
// This library is free software; you can redistribute it and/or modify
// it  under the terms of the GNU Lesser General Public License version
// 2.1 as published by the Free Software Foundation.
//
// This library is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307
// USA
//

using System;
using System.Globalization;

namespace TagLib.WavPack
{
	/// <summary>
	///    This struct implements <see cref="IAudioCodec" /> to provide
	///    support for reading WavPack audio properties.
	/// </summary>
	public struct StreamHeader : IAudioCodec, ILosslessAudioCodec, IEquatable<StreamHeader>
	{
		#region Constants

		static readonly uint[] sample_rates = new uint[] {
			6000, 8000, 9600, 11025, 12000, 16000, 22050, 24000,
			32000, 44100, 48000, 64000, 88200, 96000, 192000};

		const int BYTES_STORED = 3;
		const int MONO_FLAG = 4;
		const int SHIFT_LSB = 13;
		const long SHIFT_MASK = (0x1fL << SHIFT_LSB);
		const int SRATE_LSB = 23;
		const long SRATE_MASK = (0xfL << SRATE_LSB);

		/// <summary>
		///    SRATE field value that signals the sample rate is stored in a
		///    subblock rather than the flags table.
		/// </summary>
		const int SRATE_EXTENDED = 15;

		// Subblock metadata IDs (after stripping flag bits).
		const byte ID_CHANNEL_INFO = 0x0d;
		const byte ID_DSD_BLOCK    = 0x0e;
		const byte ID_SAMPLE_RATE  = 0x27; // ID_OPTIONAL_DATA | 0x07

		// Subblock header flag bits.
		const byte SB_LARGE    = 0x80;
		const byte SB_ODD_SIZE = 0x40;

		#endregion



		#region Private Fields

		/// <summary>
		///    Contains the number of bytes in the stream.
		/// </summary>
		readonly long stream_length;

		/// <summary>
		///    Contains the WavPack version.
		/// </summary>
		readonly ushort version;

		/// <summary>
		///    Contains the flags.
		/// </summary>
		readonly uint flags;

		/// <summary>
		///    Contains the sample count.
		/// </summary>
		readonly ulong samples;

		/// <summary>
		///    Contains the extended sample rate from the ID_SAMPLE_RATE
		///    subblock, or 0 if not present.
		/// </summary>
		readonly int extended_sample_rate;

		/// <summary>
		///    Contains the channel count from the ID_CHANNEL_INFO subblock,
		///    or 0 if not present.
		/// </summary>
		readonly int extended_channels;

		/// <summary>
		///    Indicates whether the block contains a DSD subblock.
		/// </summary>
		readonly bool is_dsd;

		#endregion


		#region Public Static Fields

		/// <summary>
		///    The size of a WavPack header.
		/// </summary>
		public const uint Size = 32;

		/// <summary>
		///    The identifier used to recognize a WavPack file.
		/// </summary>
		/// <value>
		///    "wvpk"
		/// </value>
		public static readonly ReadOnlyByteVector FileIdentifier = "wvpk";

		#endregion



		#region Constructors

		/// <summary>
		///    Constructs and initializes a new instance of <see
		///    cref="StreamHeader" /> for a specified header block and
		///    stream length.
		/// </summary>
		/// <param name="data">
		///    A <see cref="ByteVector" /> object containing the stream
		///    header data.
		/// </param>
		/// <param name="streamLength">
		///    A <see cref="long" /> value containing the length of the
		///    WavPack stream in bytes.
		/// </param>
		/// <exception cref="ArgumentNullException">
		///    <paramref name="data" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="CorruptFileException">
		///    <paramref name="data" /> does not begin with <see
		///    cref="FileIdentifier" /> or is less than <see cref="Size"
		///    /> bytes long.
		/// </exception>
		public StreamHeader (ByteVector data, long streamLength)
		{
			if (data == null)
				throw new ArgumentNullException (nameof (data));

			if (!data.StartsWith (FileIdentifier))
				throw new CorruptFileException ("Data does not begin with identifier.");

			if (data.Count < Size)
				throw new CorruptFileException ("Insufficient data in stream header");

			stream_length = streamLength;
			version = data.Mid (8, 2).ToUShort (false);
			flags = data.Mid (24, 4).ToUInt (false);
			samples = data.Mid (12, 4).ToUInt (false);

			// ckSize is the total block size minus 8; cap subblock scanning
			// at the declared block boundary so we never stray into a
			// subsequent block when the caller provides more data than one block.
			uint ck_size = data.Mid (4, 4).ToUInt (false);
			int block_end = (int)Math.Min ((long)ck_size + 8, data.Count);
			int dsdshift;

			ParseSubblocks (data, block_end,
				out extended_sample_rate,
				out extended_channels,
				out is_dsd,
				out dsdshift);

			if (is_dsd) {
				samples <<= 3;
				extended_sample_rate <<= 3 + dsdshift;
			}
		}

		#endregion



		#region Public Properties

		/// <summary>
		///    Gets the duration of the media represented by the current
		///    instance.
		/// </summary>
		/// <value>
		///    A <see cref="TimeSpan" /> containing the duration of the
		///    media represented by the current instance.
		/// </value>
		public TimeSpan Duration {
			get {
				return AudioSampleRate > 0 ?
					TimeSpan.FromSeconds (samples / (double)AudioSampleRate + 0.5) : TimeSpan.Zero;
			}
		}

		/// <summary>
		///    Gets the types of media represented by the current
		///    instance.
		/// </summary>
		/// <value>
		///    Always <see cref="MediaTypes.Audio" />.
		/// </value>
		public MediaTypes MediaTypes {
			get { return MediaTypes.Audio; }
		}

		/// <summary>
		///    Gets a text description of the media represented by the
		///    current instance.
		/// </summary>
		/// <value>
		///    A <see cref="string" /> object containing a description
		///    of the media represented by the current instance.
		/// </value>
		public string Description {
			get {
				return string.Format (CultureInfo.InvariantCulture,
					is_dsd ? "WavPack Version {0} Audio (DSD)" : "WavPack Version {0} Audio",
					Version);
			}
		}

		/// <summary>
		///    Gets the bitrate of the audio represented by the current
		///    instance.
		/// </summary>
		/// <value>
		///    A <see cref="int" /> value containing a bitrate of the
		///    audio represented by the current instance.
		/// </value>
		public int AudioBitrate {
			get {
				return (int)(Duration > TimeSpan.Zero ? ((stream_length * 8L) / Duration.TotalSeconds) / 1000 : 0);
			}
		}

		/// <summary>
		///    Gets the sample rate of the audio represented by the
		///    current instance.
		/// </summary>
		/// <value>
		///    A <see cref="int" /> value containing the sample rate of
		///    the audio represented by the current instance.
		/// </value>
		public int AudioSampleRate {
			get {
				int index = (int)((flags & SRATE_MASK) >> SRATE_LSB);
				return index == SRATE_EXTENDED ? extended_sample_rate : (int)sample_rates[index];
			}
		}

		/// <summary>
		///    Gets the number of channels in the audio represented by
		///    the current instance.
		/// </summary>
		/// <value>
		///    A <see cref="int" /> value containing the number of
		///    channels in the audio represented by the current
		///    instance.
		/// </value>
		public int AudioChannels {
			get {
				if (extended_channels > 0)
					return extended_channels;
				return ((flags & MONO_FLAG) != 0) ? 1 : 2;
			}
		}

		/// <summary>
		///    Gets the WavPack version of the audio represented by the
		///    current instance.
		/// </summary>
		/// <value>
		///    A <see cref="int" /> value containing the WavPack version
		///    of the audio represented by the current instance.
		/// </value>
		public int Version {
			get { return version; }
		}

		/// <summary>
		///    Gets the number of bits per sample in the audio
		///    represented by the current instance.
		/// </summary>
		/// <value>
		///    A <see cref="int" /> value containing the number of bits
		///    per sample in the audio represented by the current
		///    instance.
		/// </value>
		public int BitsPerSample {
			get {
				return is_dsd ? 1 : (int)(((flags & BYTES_STORED) + 1) * 8 - ((flags & SHIFT_MASK) >> SHIFT_LSB));
			}
		}

		/// <summary>
		///    Gets whether the audio represented by the current instance is
		///    DSD (Direct Stream Digital) encoded.
		/// </summary>
		/// <value>
		///    <see langword="true" /> if a DSD subblock was found in the first
		///    WavPack block; otherwise <see langword="false" />.
		/// </value>
		public bool IsDsd {
			get { return is_dsd; }
		}

		#endregion



		#region Private Static Methods

		/// <summary>
		///    Scans WavPack subblocks in <paramref name="data" /> from offset
		///    <see cref="Size" /> up to <paramref name="blockEnd" />, extracting
		///    extended sample rate, channel count, and DSD flag.
		/// </summary>
		/// <param name="data">
		///    The raw block data including the 32-byte block header.
		/// </param>
		/// <param name="blockEnd">
		///    The byte offset at which the current block ends within
		///    <paramref name="data" />.
		/// </param>
		/// <param name="extSampleRate">
		///    On return, the sample rate from the ID_SAMPLE_RATE subblock,
		///    or 0 if not present.
		/// </param>
		/// <param name="extChannels">
		///    On return, the channel count from the ID_CHANNEL_INFO subblock,
		///    or 0 if not present.
		/// </param>
		/// <param name="isDsd">
		///    On return, <see langword="true" /> if an ID_DSD_BLOCK subblock
		///    was found.
		/// <param name="dsdShift">
		///    On return, the DSD shift value from the ID_DSD_BLOCK subblock,
		///    or 0 if not present.
		/// </param>
		/// </param>
		static void ParseSubblocks (ByteVector data, int blockEnd,
			out int extSampleRate, out int extChannels, out bool isDsd, out int dsdShift)
		{
			extSampleRate = 0;
			extChannels = 0;
			isDsd = false;
			dsdShift = 0;

			int offset = (int)Size; // subblocks begin immediately after the header

			while (offset + 2 <= blockEnd) {
				byte idByte   = data[offset++];
				byte sizeByte = data[offset++];

				// The stored size is a word count; disk bytes = word_count << 1.
				int diskBytes = sizeByte << 1;

				if ((idByte & SB_LARGE) != 0) {
					// Large subblock: two more size bytes extend the word count.
					if (offset + 2 > blockEnd)
						break;
					diskBytes |= data[offset++] << 9;
					diskBytes |= data[offset++] << 17;
					idByte = (byte)(idByte & ~SB_LARGE);
				}

				// ODD_SIZE means the final byte on disk is padding; actual data
				// is one byte shorter than the disk-aligned count.
				int dataBytes = diskBytes;
				if ((idByte & SB_ODD_SIZE) != 0) {
					idByte = (byte)(idByte & ~SB_ODD_SIZE);
					dataBytes--;
				}

				if (offset + diskBytes > blockEnd)
					break;

				switch (idByte) {
				case ID_SAMPLE_RATE:
					// 3-byte little-endian sample rate.
					if (dataBytes >= 3)
						extSampleRate = data[offset] | (data[offset + 1] << 8) | (data[offset + 2] << 16);
					break;

				case ID_CHANNEL_INFO:
					// Byte 0 is the channel count (covers up to 255 channels).
					if (dataBytes >= 1)
						extChannels = data[offset];
					break;

				case ID_DSD_BLOCK:
					isDsd = true;
					if (dataBytes >= 1)
						dsdShift = data[offset] & 0x1f; // shift is stored in the low 5 bits
					break;
				}

				offset += diskBytes;
			}
		}

		#endregion



		#region IEquatable

		/// <summary>
		///    Generates a hash code for the current instance.
		/// </summary>
		/// <returns>
		///    A <see cref="int" /> value containing the hash code for
		///    the current instance.
		/// </returns>
		public override int GetHashCode ()
		{
			unchecked {
				return (int)(flags ^ samples ^ version);
			}
		}

		/// <summary>
		///    Checks whether or not the current instance is equal to
		///    another object.
		/// </summary>
		/// <param name="other">
		///    A <see cref="object" /> to compare to the current
		///    instance.
		/// </param>
		/// <returns>
		///    A <see cref="bool" /> value indicating whether or not the
		///    current instance is equal to <paramref name="other" />.
		/// </returns>
		/// <seealso cref="M:System.IEquatable`1.Equals" />
		public override bool Equals (object other)
		{
			if (!(other is StreamHeader))
				return false;

			return Equals ((StreamHeader)other);
		}

		/// <summary>
		///    Checks whether or not the current instance is equal to
		///    another instance of <see cref="StreamHeader" />.
		/// </summary>
		/// <param name="other">
		///    A <see cref="StreamHeader" /> object to compare to the
		///    current instance.
		/// </param>
		/// <returns>
		///    A <see cref="bool" /> value indicating whether or not the
		///    current instance is equal to <paramref name="other" />.
		/// </returns>
		/// <seealso cref="M:System.IEquatable`1.Equals" />
		public bool Equals (StreamHeader other)
		{
			return flags == other.flags &&
				samples == other.samples &&
				version == other.version;
		}

		/// <summary>
		///    Gets whether or not two instances of <see
		///    cref="StreamHeader" /> are equal to eachother.
		/// </summary>
		/// <param name="first">
		///    The first <see cref="StreamHeader" /> object to compare.
		/// </param>
		/// <param name="second">
		///    The second <see cref="StreamHeader" /> object to compare.
		/// </param>
		/// <returns>
		///    <see langword="true" /> if <paramref name="first" /> is
		///    equal to <paramref name="second" />. Otherwise, <see
		///    langword="false" />.
		/// </returns>
		public static bool operator == (StreamHeader first, StreamHeader second)
		{
			return first.Equals (second);
		}

		/// <summary>
		///    Gets whether or not two instances of <see
		///    cref="StreamHeader" /> are unequal to eachother.
		/// </summary>
		/// <param name="first">
		///    The first <see cref="StreamHeader" /> object to compare.
		/// </param>
		/// <param name="second">
		///    The second <see cref="StreamHeader" /> object to compare.
		/// </param>
		/// <returns>
		///    <see langword="true" /> if <paramref name="first" /> is
		///    unequal to <paramref name="second" />. Otherwise, <see
		///    langword="false" />.
		/// </returns>
		public static bool operator != (StreamHeader first, StreamHeader second)
		{
			return !first.Equals (second);
		}

		#endregion
	}
}
