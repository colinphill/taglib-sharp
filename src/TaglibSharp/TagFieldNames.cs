//
// TagFieldNames.cs: Standard field name constants for use with Tag.GetField,
// Tag.SetField, and Tag.GetAllFields.
//
// Copyright (C) 2024 taglib-sharp contributors
//
// This library is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License version
// 2.1 as published by the Free Software Foundation.
//

namespace TagLib
{
	/// <summary>
	///    Contains string constants for the standardized field names used with
	///    <see cref="Tag.GetField" />, <see cref="Tag.SetField" />, and
	///    <see cref="Tag.GetAllFields" />.
	/// </summary>
	/// <remarks>
	///    These names correspond 1:1 to the properties on <see cref="Tag" />.
	///    Each tag type internally maps them to its native representation
	///    (e.g. "ARTIST" in Vorbis comments, "TPE1" in ID3v2). Fields not in
	///    this list may still be set and retrieved on tag types that support
	///    arbitrary keys (Vorbis, APEv2, ID3v2 TXXX frames, ASF descriptors).
	/// </remarks>
	public static class TagFieldNames
	{
		/// <summary>Title of the track.</summary>
		public const string Title = "Title";

		/// <summary>Title used for sorting.</summary>
		public const string TitleSort = "TitleSort";

		/// <summary>Subtitle or description of the track.</summary>
		public const string Subtitle = "Subtitle";

		/// <summary>Long description (e.g. podcast episode description).</summary>
		public const string Description = "Description";

		/// <summary>Performing artists (may be multi-valued).</summary>
		public const string Performers = "Performers";

		/// <summary>Performing artists used for sorting (may be multi-valued).</summary>
		public const string PerformersSort = "PerformersSort";

		/// <summary>Role/instrument for each performer (parallel to Performers).</summary>
		public const string PerformersRole = "PerformersRole";

		/// <summary>Album artists / band (may be multi-valued).</summary>
		public const string AlbumArtists = "AlbumArtists";

		/// <summary>Album artists used for sorting (may be multi-valued).</summary>
		public const string AlbumArtistsSort = "AlbumArtistsSort";

		/// <summary>Composers (may be multi-valued).</summary>
		public const string Composers = "Composers";

		/// <summary>Composers used for sorting (may be multi-valued).</summary>
		public const string ComposersSort = "ComposersSort";

		/// <summary>Album name.</summary>
		public const string Album = "Album";

		/// <summary>Album name used for sorting.</summary>
		public const string AlbumSort = "AlbumSort";

		/// <summary>Comment / annotation.</summary>
		public const string Comment = "Comment";

		/// <summary>Genres (may be multi-valued).</summary>
		public const string Genres = "Genres";

		/// <summary>Release year (numeric, stored as decimal string).</summary>
		public const string Year = "Year";

		/// <summary>Track number within the album (1-based).</summary>
		public const string Track = "Track";

		/// <summary>Total number of tracks on the album.</summary>
		public const string TrackCount = "TrackCount";

		/// <summary>Disc number within the release.</summary>
		public const string Disc = "Disc";

		/// <summary>Total number of discs in the release.</summary>
		public const string DiscCount = "DiscCount";

		/// <summary>Lyrics or script of the track.</summary>
		public const string Lyrics = "Lyrics";

		/// <summary>Grouping / content group.</summary>
		public const string Grouping = "Grouping";

		/// <summary>Beats per minute (numeric, stored as decimal string).</summary>
		public const string BeatsPerMinute = "BeatsPerMinute";

		/// <summary>Conductor.</summary>
		public const string Conductor = "Conductor";

		/// <summary>Copyright message.</summary>
		public const string Copyright = "Copyright";

		/// <summary>Publisher / record label.</summary>
		public const string Publisher = "Publisher";

		/// <summary>ISRC (International Standard Recording Code).</summary>
		public const string ISRC = "ISRC";

		/// <summary>Remixer / interpreted/remixed-by.</summary>
		public const string RemixedBy = "RemixedBy";

		/// <summary>Initial musical key (e.g. "Cm", "F#").</summary>
		public const string InitialKey = "InitialKey";

		/// <summary>Nominal track length as a string.</summary>
		public const string Length = "Length";

		/// <summary>Date the tag was written (ISO 8601 format).</summary>
		public const string DateTagged = "DateTagged";

		/// <summary>MusicBrainz Artist ID.</summary>
		public const string MusicBrainzArtistId = "MusicBrainzArtistId";

		/// <summary>MusicBrainz Release Group ID.</summary>
		public const string MusicBrainzReleaseGroupId = "MusicBrainzReleaseGroupId";

		/// <summary>MusicBrainz Release ID (Album ID).</summary>
		public const string MusicBrainzReleaseId = "MusicBrainzReleaseId";

		/// <summary>MusicBrainz Release Artist ID (Album Artist ID).</summary>
		public const string MusicBrainzReleaseArtistId = "MusicBrainzReleaseArtistId";

		/// <summary>MusicBrainz Release Track ID.</summary>
		public const string MusicBrainzTrackId = "MusicBrainzTrackId";

		/// <summary>MusicBrainz Recording ID (Track ID).</summary>
		public const string MusicBrainzRecordingId = "MusicBrainzRecordingId";

		/// <summary>MusicBrainz Work ID.</summary>
		public const string MusicBrainzWorkId = "MusicBrainzWorkId";

		/// <summary>MusicBrainz Disc ID.</summary>
		public const string MusicBrainzDiscId = "MusicBrainzDiscId";

		/// <summary>MusicIP PUID.</summary>
		public const string MusicIpId = "MusicIpId";

		/// <summary>Amazon ASIN.</summary>
		public const string AmazonId = "AmazonId";

		/// <summary>MusicBrainz Release Status (e.g. "Official", "Bootleg").</summary>
		public const string MusicBrainzReleaseStatus = "MusicBrainzReleaseStatus";

		/// <summary>MusicBrainz Release Type (e.g. "Album", "Single").</summary>
		public const string MusicBrainzReleaseType = "MusicBrainzReleaseType";

		/// <summary>MusicBrainz Release Country (ISO 3166-1 alpha-2).</summary>
		public const string MusicBrainzReleaseCountry = "MusicBrainzReleaseCountry";
	}
}
