//
// IsoAudioSampleEntryAlac.cs: Provides an implementation of an Apple Lossless
// (ALAC) audio sample entry box and support for reading ALAC stream properties.
//
// Author:
//   Colin Hill
//
// Copyright (C) 2024 Colin Hill
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

namespace TagLib.Mpeg4
{
	/// <summary>
	///    This class extends <see cref="IsoAudioSampleEntry" /> and implements
	///    <see cref="ILosslessAudioCodec" /> to provide an implementation of an
	///    Apple Lossless Audio Codec (ALAC) sample entry box, including support
	///    for reading stream properties from the codec-specific <c>alac</c>
	///    child box.
	/// </summary>
	/// <remarks>
	///    <para>An ALAC sample entry box has box type <c>alac</c> and contains
	///    a child box, also typed <c>alac</c>, that carries the
	///    <c>ALACSpecificConfig</c> defined in the Apple Lossless open-source
	///    specification. The config layout (after the FullBox version/flags
	///    field) is:</para>
	///    <list type="table">
	///      <item><term>frameLength</term><description>4 bytes — samples per frame</description></item>
	///      <item><term>compatibleVersion</term><description>1 byte</description></item>
	///      <item><term>bitDepth</term><description>1 byte — bits per sample</description></item>
	///      <item><term>riceHistoryMult</term><description>1 byte</description></item>
	///      <item><term>riceInitialHistory</term><description>1 byte</description></item>
	///      <item><term>riceLimit</term><description>1 byte</description></item>
	///      <item><term>numChannels</term><description>1 byte</description></item>
	///      <item><term>maxRun</term><description>2 bytes</description></item>
	///      <item><term>maxFrameBytes</term><description>4 bytes</description></item>
	///      <item><term>avgBitRate</term><description>4 bytes — bits per second</description></item>
	///      <item><term>sampleRate</term><description>4 bytes — Hz</description></item>
	///    </list>
	/// </remarks>
	public class IsoAudioSampleEntryAlac : IsoAudioSampleEntry, ILosslessAudioCodec
	{
		#region Private Fields

		/// <summary>
		///    Contains the number of bits per sample read from the
		///    ALACSpecificConfig.
		/// </summary>
		readonly int bits_per_sample;

		/// <summary>
		///    Contains the number of channels read from the ALACSpecificConfig.
		/// </summary>
		readonly int alac_channels;

		/// <summary>
		///    Contains the average bit rate in bits per second read from the
		///    ALACSpecificConfig.
		/// </summary>
		readonly int avg_bit_rate;

		/// <summary>
		///    Contains the sample rate in Hz read from the ALACSpecificConfig.
		/// </summary>
		readonly int alac_sample_rate;

		/// <summary>
		///    Indicates whether the ALACSpecificConfig was successfully parsed.
		/// </summary>
		readonly bool alac_config_valid;

		#endregion



		#region Constructors

		/// <summary>
		///    Constructs and initializes a new instance of <see
		///    cref="IsoAudioSampleEntryAlac" /> with a provided header and
		///    handler by reading the contents from a specified file.
		/// </summary>
		/// <param name="header">
		///    A <see cref="BoxHeader" /> object containing the header
		///    to use for the new instance.
		/// </param>
		/// <param name="file">
		///    A <see cref="TagLib.File" /> object to read the contents
		///    of the box from.
		/// </param>
		/// <param name="handler">
		///    A <see cref="IsoHandlerBox" /> object containing the
		///    handler that applies to the new instance.
		/// </param>
		/// <exception cref="ArgumentNullException">
		///    <paramref name="file" /> is <see langword="null" />.
		/// </exception>
		public IsoAudioSampleEntryAlac (BoxHeader header, TagLib.File file, IsoHandlerBox handler)
			: base (header, file, handler)
		{
			// The alac child box is a FullBox whose Data contains the
			// ALACSpecificConfig. Layout of Data (relative to box data start):
			//   [0..3]  version (1 byte) + flags (3 bytes)  — FullBox fields
			//   [4..7]  frameLength        uint32
			//   [8]     compatibleVersion  uint8
			//   [9]     bitDepth           uint8
			//   [10]    riceHistoryMult    uint8
			//   [11]    riceInitialHistory uint8
			//   [12]    riceLimit          uint8
			//   [13]    numChannels        uint8
			//   [14..15] maxRun            uint16
			//   [16..19] maxFrameBytes     uint32
			//   [20..23] avgBitRate        uint32  (bits/sec)
			//   [24..27] sampleRate        uint32  (Hz)
			// Minimum expected data size: 28 bytes.
			const int MinConfigSize = 28;

			// "alac" as a string literal avoids ambiguity with the inherited BoxType property.
			var alacBox = GetChildRecursively (new ReadOnlyByteVector ("alac"));
			if (alacBox?.Data != null && alacBox.Data.Count >= MinConfigSize) {
				var d = alacBox.Data;
				bits_per_sample = d[9];
				alac_channels = d[13];
				avg_bit_rate = (int)d.Mid (20, 4).ToUInt ();
				alac_sample_rate = (int)d.Mid (24, 4).ToUInt ();
				alac_config_valid = true;
			}
		}

		#endregion



		#region IAudioCodec Overrides

		/// <summary>
		///    Gets a text description of the media represented by the current
		///    instance.
		/// </summary>
		/// <value>
		///    A <see cref="string" /> containing "Apple Lossless Audio (ALAC)".
		/// </value>
		public override string Description {
			get { return "Apple Lossless Audio (ALAC)"; }
		}

		/// <summary>
		///    Gets the average bitrate of the audio represented by the current
		///    instance.
		/// </summary>
		/// <value>
		///    The average bitrate in kilobits per second from the
		///    ALACSpecificConfig, or the value from the base
		///    <see cref="IsoAudioSampleEntry" /> if the config was not
		///    available.
		/// </value>
		public override int AudioBitrate {
			get {
				if (alac_config_valid)
					return avg_bit_rate / 1000;
				return base.AudioBitrate;
			}
		}

		/// <summary>
		///    Gets the sample rate of the audio represented by the current
		///    instance.
		/// </summary>
		/// <value>
		///    The sample rate in Hz from the ALACSpecificConfig, or the value
		///    from the base <see cref="IsoAudioSampleEntry" /> if the config
		///    was not available.
		/// </value>
		public override int AudioSampleRate {
			get {
				if (alac_config_valid)
					return alac_sample_rate;
				return base.AudioSampleRate;
			}
		}

		/// <summary>
		///    Gets the number of channels in the audio represented by the
		///    current instance.
		/// </summary>
		/// <value>
		///    The channel count from the ALACSpecificConfig, or the value from
		///    the base <see cref="IsoAudioSampleEntry" /> if the config was
		///    not available.
		/// </value>
		public override int AudioChannels {
			get {
				if (alac_config_valid)
					return alac_channels;
				return base.AudioChannels;
			}
		}

		#endregion



		#region ILosslessAudioCodec

		/// <summary>
		///    Gets the number of bits per sample in the audio represented by
		///    the current instance.
		/// </summary>
		/// <value>
		///    A <see cref="int" /> value containing the number of bits per
		///    sample read from the ALACSpecificConfig, or 0 if the config was
		///    not available.
		/// </value>
		public int BitsPerSample {
			get { return alac_config_valid ? bits_per_sample : 0; }
		}

		#endregion
	}
}
