//
// Tag.cs: This abstract class provides generic access to standard tag
// features. All tag types will extend this class.
//
// Author:
//   Brian Nickel (brian.nickel@gmail.com)
//
// Original Source:
//   tag.cpp from TagLib
//
// Copyright (C) 2005-2007 Brian Nickel
// Copyright (C) 2003 Scott Wheeler
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
using System.Collections.Generic;
using System.Globalization;

namespace TagLib;

/// <summary>
///    Indicates the tag types used by a file.
/// </summary>
[Flags]
public enum TagTypes : uint
{
	/// <summary>
	///    No tag types.
	/// </summary>
	None = 0x00000000,

	/// <summary>
	///    Xiph's Vorbis Comment
	/// </summary>
	Xiph = 0x00000001,

	/// <summary>
	///    ID3v1 Tag
	/// </summary>
	Id3v1 = 0x00000002,

	/// <summary>
	///    ID3v2 Tag
	/// </summary>
	Id3v2 = 0x00000004,

	/// <summary>
	///    APE Tag
	/// </summary>
	Ape = 0x00000008,

	/// <summary>
	///    Apple's ILST Tag Format
	/// </summary>
	Apple = 0x00000010,

	/// <summary>
	///    ASF Tag
	/// </summary>
	Asf = 0x00000020,

	/// <summary>
	///    Standard RIFF INFO List Tag
	/// </summary>
	RiffInfo = 0x00000040,

	/// <summary>
	///    RIFF Movie ID List Tag
	/// </summary>
	MovieId = 0x00000080,

	/// <summary>
	///    DivX Tag
	/// </summary>
	DivX = 0x00000100,

	/// <summary>
	///    FLAC Metadata Blocks Tag
	/// </summary>
	FlacMetadata = 0x00000200,

	/// <summary>
	///    TIFF IFD Tag
	/// </summary>
	TiffIFD = 0x00000400,

	/// <summary>
	///    XMP Tag
	/// </summary>
	XMP = 0x00000800,

	/// <summary>
	///    Jpeg Comment Tag
	/// </summary>
	JpegComment = 0x00001000,

	/// <summary>
	///    Gif Comment Tag
	/// </summary>
	GifComment = 0x00002000,

	/// <summary>
	///    native PNG keywords
	/// </summary>
	Png = 0x00004000,

	/// <summary>
	/// IPTC-IIM tag
	/// </summary>
	IPTCIIM = 0x00008000,

	/// <summary>
	///    Audible Metadata Blocks Tag
	/// </summary>
	AudibleMetadata = 0x00010000,

	/// <summary>
	/// Matroska native tag
	/// </summary>
	Matroska = 0x00020000,

	/// <summary>
	///    All tag types.
	/// </summary>
	AllTags = 0xFFFFFFFF
}

/// <summary>
///    This abstract class provides generic access to standard tag
///    features. All tag types will extend this class.
/// </summary>
/// <remarks>
///    Because not every tag type supports the same features, it may be
///    useful to check that the value is stored by re-reading the
///    property after it is stored.
/// </remarks>
public abstract class Tag
{
	/// <summary>
	///    Gets the tag types contained in the current instance.
	/// </summary>
	/// <value>
	///    A bitwise combined <see cref="TagLib.TagTypes" />
	///    containing the tag types contained in the current
	///    instance.
	/// </value>
	/// <remarks>
	///    For a standard tag, the value should be intuitive. For
	///    example, <see cref="TagLib.Id3v2.Tag" /> objects have a
	///    value of <see cref="TagLib.TagTypes.Id3v2" />. However,
	///    for tags of type <see cref="TagLib.CombinedTag" /> may
	///    contain multiple or no types.
	/// </remarks>
	public abstract TagTypes TagTypes { get; }

	/// <summary>
	///    Gets and sets the title for the media described by the
	///    current instance.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> object containing the title for
	///    the media described by the current instance or <see
	///    langword="null" /> if no value is present.
	/// </value>
	/// <remarks>
	///    The title is most commonly the name of the song or
	///    episode or a movie title. For example, "Daydream
	///    Believer" (a song by the Monkies), "Space Seed" (an
	///    episode of Star Trek), or "Harold and Kumar Go To White
	///    Castle" (a movie).
	/// </remarks>
	public virtual string Title {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets the sort name for the title of the media
	///    described by the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> object containing the sort name for
	///    the title of the media described by the current instance or <see
	///    langword="null" /> if no value is present.
	/// </value>
	/// <remarks>
	///    Possibly used to sort compilations, or episodic content.
	/// </remarks>
	public virtual string TitleSort {
		get { return null; }
		set { }
	}


	/// <summary>
	///    Gets and sets a short description, one-liner.
	///    It represents the tagline of the Video/music.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> containing the subtitle
	///    the media represented by the current instance
	///    or an empty array if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field gives a nice/short precision to
	///    the title, which is typically below the title on the
	///    front cover of a media.
	///    For example, for "Back to the future", this would be
	///    "It's About Time".
	///    </para>
	/// </remarks>
	public virtual string Subtitle {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets a short description of the media.
	///    For a music, this could be the comment that the artist
	///    made of its artwork. For a video, this should be a
	///    short summary of the story/plot, but a spoiler. This
	///    should give the impression of what to expect in the
	///    media.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> containing the subtitle
	///    the media represented by the current instance
	///    or an empty array if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This is especially relevant for a movie.
	///    For example, for "Back to the Future 2", this could be
	///    "After visiting 2015, Marty McFly must repeat his visit
	///    to 1955 to prevent disastrous changes to 1985...without
	///    interfering with his first trip".
	///    </para>
	/// </remarks>
	public virtual string Description {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets the performers or artists who performed in
	///    the media described by the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="T:string[]" /> containing the performers or
	///    artists who performed in the media described by the
	///    current instance or an empty array if no value is
	///    present.
	/// </value>
	/// <remarks>
	///    <para>This field is most commonly called "Artists" in
	///    Audio media, or "Actor" in Video media, and should be
	///    used to represent each artist/actor appearing in the
	///    media. It can be simple in the form of "The Beatles"
	///    or more complicated in the form of "John Lennon,
	///    Paul McCartney, George Harrison, Pete Best", depending
	///    on the preferences of the listener/spectator
	///    and the degree to which they organize their media
	///    collection.</para>
	///    <para>As the preference of the user may vary,
	///    applications should not try to limit the user in what
	///    choice they may make.</para>
	/// </remarks>
	public virtual string[] Performers {
		get { return new string[] { }; }
		set { }
	}

	/// <summary>
	///    Gets and sets the sort names of the performers or artists
	///    who performed in the media described by the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="T:string[]" /> containing the sort names for
	///    the performers or artists who performed in the media
	///    described by the current instance, or an empty array if
	///    no value is present.
	/// </value>
	/// <remarks>
	///    <para>This is used to provide more control over how tracks
	///    are sorted. Typical uses are to skip common prefixes or
	///    sort by last name. For example, "The Beatles" might be
	///    sorted as "Beatles, The".
	///    </para>
	/// </remarks>
	public virtual string[] PerformersSort {
		get { return new string[] { }; }
		set { }
	}


	/// <summary>
	///    Gets and sets the Charaters for a video media, or
	///    instruments played for music media.
	///    This should match the <see cref="Performers"/> array (for
	///    each person correspond one/more role). Several roles for
	///    the same artist/actor can be made up with semicolons.
	///    For example, "Marty McFly; Marty McFly Jr.; Marlene McFly".
	/// </summary>
	/// <remarks>
	///    <para> This is typically usefull for movies, although the
	///    instrument played by each artist in a music may be of
	///    relevance.
	///    </para>
	///    <para>It is highly important to match each role to the
	///    performers. This means that a role may be <see
	///    langword="null"/> to keep the match between a
	///    Performers[i] and PerformersRole[i].
	///    </para>
	/// </remarks>
	public virtual string[] PerformersRole {
		get { return new string[] { }; }
		set { }
	}


	/// <summary>
	///    Gets and sets the band or artist who is credited in the
	///    creation of the entire album or collection containing the
	///    media described by the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="T:string[]" /> containing the band or artist
	///    who is credited in the creation of the entire album or
	///    collection containing the media described by the current
	///    instance or an empty array if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field is typically optional but aids in the
	///    sorting of compilations or albums with multiple artists.
	///    For example, if an album has several artists, sorting by
	///    artist will split up the album and sorting by album will
	///    split up albums by the same artist. Having a single album
	///    artist for an entire album will solve this
	///    problem.</para>
	///    <para>As this value is to be used as a sorting key, it
	///    should be used with less variation than <see
	///    cref="Performers" />. Where performers can be broken into
	///    muliple artist it is best to stick with a single band
	///    name. For example, "The Beatles".</para>
	/// </remarks>
	public virtual string[] AlbumArtists {
		get { return new string[] { }; }
		set { }
	}

	/// <summary>
	///    Gets and sets the sort names for the band or artist who
	///    is credited in the creation of the entire album or
	///    collection containing the media described by the
	///    current instance.
	/// </summary>
	/// <value>
	///    A <see cref="T:string[]" /> containing the sort names
	///    for the band or artist who is credited in the creation
	///    of the entire album or collection containing the media
	///    described by the current instance or an empty array if
	///    no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field is typically optional but aids in the
	///    sorting of compilations or albums with multiple artists.
	///    For example, if an album has several artists, sorting by
	///    artist will split up the album and sorting by album will
	///    split up albums by the same artist. Having a single album
	///    artist for an entire album will solve this
	///    problem.</para>
	///    <para>As this value is to be used as a sorting key, it
	///    should be used with less variation than <see
	///    cref="Performers" />. Where performers can be broken into
	///    muliple artist it is best to stick with a single band
	///    name. For example, "Beatles, The".</para>
	/// </remarks>
	public virtual string[] AlbumArtistsSort {
		get { return new string[] { }; }
		set { }
	}

	/// <summary>
	///    Gets and sets the composers of the media represented by
	///    the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="T:string[]" /> containing the composers of the
	///    media represented by the current instance or an empty
	///    array if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field represents the composers, song writers,
	///    script writers, or persons who claim authorship of the
	///    media.</para>
	/// </remarks>
	public virtual string[] Composers {
		get { return new string[] { }; }
		set { }
	}

	/// <summary>
	///    Gets and sets the sort names for the composers of the
	///    media represented by the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="T:string[]" /> containing the sort names
	///    for the composers of the media represented by the
	///    current instance or an empty array if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field is typically optional but aids in the
	///    sorting of compilations or albums with multiple Composers.
	///    </para>
	///    <para>As this value is to be used as a sorting key, it
	///    should be used with less variation than <see
	///    cref="Composers" />. Where performers can be broken into
	///    muliple artist it is best to stick with a single composer.
	///    For example, "McCartney, Paul".</para>
	/// </remarks>
	public virtual string[] ComposersSort {
		get { return new string[] { }; }
		set { }
	}

	/// <summary>
	///    Gets and sets the album of the media represented by the
	///    current instance. For a video media, this represent the
	///    collection the video belongs to.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> object containing the album of
	///    the media represented by the current instance or <see
	///    langword="null" /> if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field represents the name of the album the
	///    media belongs to. In the case of a boxed set, it should
	///    be the name of the entire set rather than the individual
	///    disc. In case of a Serie, this should be name of the serie,
	///    rather than the season of a serie.</para>
	///    <para>For example, "Rubber Soul" (an album by the
	///    Beatles), "The Sopranos: Complete First Season" (a boxed
	///    set of TV episodes), "Back To The Future" (a
	///    serie of movies/sequels), or "Game of Thrones" (a serie
	///    with several seasons).</para>
	/// </remarks>
	public virtual string Album {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets the sort names for the Album Title of the
	///    media represented by the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> containing the sort names
	///    for the Album Title of the media represented by the
	///    current instance or an empty array if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field is typically optional but aids in the
	///    sorting of compilations or albums with Similar Titles.
	///    </para>
	/// </remarks>
	public virtual string AlbumSort {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets a user comment on the media represented by
	///    the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> object containing user comments
	///    on the media represented by the current instance or <see
	///    langword="null" /> if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field should be used to store user notes and
	///    comments. There is no constraint on what text can be
	///    stored here, but it should not contain program
	///    information.</para>
	///    <para>Because this field contains notes that the user
	///    might think of while listening to the media, it may be
	///    useful for an application to make this field easily
	///    accessible, perhaps even including it in the main
	///    interface.</para>
	/// </remarks>
	public virtual string Comment {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets the genres of the media represented by the
	///    current instance.
	/// </summary>
	/// <value>
	///    A <see cref="T:string[]" /> containing the genres of the
	///    media represented by the current instance or an empty
	///    array if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field represents genres that apply to the song,
	///    album or video. This is often used for filtering media.
	///    </para>
	///    <para>A list of common audio genres as popularized by
	///    ID3v1, are stored in <see cref="Genres.Audio" />.
	///    Additionally, <see cref="Genres.Video" /> contains video
	///    genres as used by DivX.</para>
	/// </remarks>
	public virtual string[] Genres {
		get { return new string[] { }; }
		set { }
	}

	/// <summary>
	///    Gets and sets the year that the media represented by the
	///    current instance was recorded.
	/// </summary>
	/// <value>
	///    A <see cref="uint" /> containing the year that the media
	///    represented by the current instance was created or zero
	///    if no value is present.
	/// </value>
	/// <remarks>
	///    <para>Years greater than 9999 cannot be stored by most
	///    tagging formats and will be cleared if a higher value is
	///    set.</para>
	///    <para>Some tagging formats store higher precision dates
	///    which will be truncated when this property is set. Format
	///    specific implementations are necessary access the higher
	///    precision values.</para>
	/// </remarks>
	public virtual uint Year {
		get { return 0; }
		set { }
	}

	/// <summary>
	///    Gets and sets the position of the media represented by
	///    the current instance in its containing album, or season
	///    (for series).
	/// </summary>
	/// <value>
	///    A <see cref="uint" /> containing the position of the
	///    media represented by the current instance in its
	///    containing album or zero if not specified.
	/// </value>
	/// <remarks>
	///    <para>This value should be the same as is listed on the
	///    album cover and no more than <see cref="TrackCount"
	///    /> if <see cref="TrackCount" /> is non-zero.</para>
	///    <para>Most tagging formats store this as a string. To
	///    help sorting, a two-digit zero-filled value is used
	///    in the resulting tag.</para>
	///    <para>For a serie, this property represents the episode
	///    in a season of the serie.
	///    </para>
	/// </remarks>
	public virtual uint Track {
		get { return 0; }
		set { }
	}

	/// <summary>
	///    Gets and sets the number of tracks in the album, or the
	///    number of episodes in a serie, of the media represented
	///    by the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="uint" /> containing the number of tracks in
	///    the album, or the number of episodes in a serie, of the
	///    media represented by the current instance or zero if not
	///    specified.
	/// </value>
	/// <remarks>
	///    <para>If non-zero, this value should be at least equal to
	///    <see cref="Track" />. If <see cref="Track" /> is zero,
	///    this value should also be zero.</para>
	/// </remarks>
	public virtual uint TrackCount {
		get { return 0; }
		set { }
	}

	/// <summary>
	///    Gets and sets the number of the disc containing the media
	///    represented by the current instance in the boxed set. For
	///    a serie, this represents the season number.
	/// </summary>
	/// <value>
	///    A <see cref="uint" /> containing the number of the disc
	///    or season of the media represented by the current instance
	///    in the boxed set.
	/// </value>
	/// <remarks>
	///    <para>This value should be the same as is number that
	///    appears on the disc. For example, if the disc is the
	///    first of three, the value should be <c>1</c>. It should
	///    be no more than <see cref="DiscCount" /> if <see
	///    cref="DiscCount" /> is non-zero.</para>
	/// </remarks>
	public virtual uint Disc {
		get { return 0; }
		set { }
	}

	/// <summary>
	///    Gets and sets the number of discs or seasons in the
	///    boxed set containing the media represented by the
	///    current instance.
	/// </summary>
	/// <value>
	///    A <see cref="uint" /> containing the number of discs
	///    or seasons in the boxed set containing the media
	///    represented by the current instance or zero if not
	///    specified.
	/// </value>
	/// <remarks>
	///    <para>If non-zero, this value should be at least equal to
	///    <see cref="Disc" />. If <see cref="Disc" /> is zero,
	///    this value should also be zero.</para>
	/// </remarks>
	public virtual uint DiscCount {
		get { return 0; }
		set { }
	}

	/// <summary>
	///    Gets and sets the lyrics or script of the media
	///    represented by the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> object containing the lyrics or
	///    script of the media represented by the current instance
	///    or <see langword="null" /> if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field contains a plain text representation of
	///    the lyrics or scripts with line breaks and whitespace
	///    being the only formatting marks.</para>
	///    <para>Some formats support more advances lyrics, like
	///    synchronized lyrics, but those must be accessed using
	///    format specific implementations.</para>
	/// </remarks>
	public virtual string Lyrics {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets the grouping on the album which the media
	///    in the current instance belongs to.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> object containing the grouping on
	///    the album which the media in the current instance belongs
	///    to or <see langword="null" /> if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field contains a non-physical grouping to
	///    which the track belongs. In classical music, this could
	///    be a movement. It could also be parts of a series like
	///    "Introduction", "Closing Remarks", etc.</para>
	/// </remarks>
	public virtual string Grouping {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets the number of beats per minute in the audio
	///    of the media represented by the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="uint" /> containing the number of beats per
	///    minute in the audio of the media represented by the
	///    current instance, or zero if not specified.
	/// </value>
	/// <remarks>
	///    <para>This field is useful for DJ's who are trying to
	///    match songs. It should be calculated from the audio or
	///    pulled from a database.</para>
	/// </remarks>
	public virtual uint BeatsPerMinute {
		get { return 0; }
		set { }
	}

	/// <summary>
	///    Gets and sets the conductor or director of the media
	///    represented by the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> object containing the conductor
	///    or director of the media represented by the current
	///    instance or <see langword="null" /> if no value present.
	/// </value>
	/// <remarks>
	///    <para>This field is most useful for organizing classical
	///    music and movies.</para>
	/// </remarks>
	public virtual string Conductor {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets the copyright information for the media
	///    represented by the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> object containing the copyright
	///    information for the media represented by the current
	///    instance or <see langword="null" /> if no value present.
	/// </value>
	/// <remarks>
	///    <para>This field should be used for storing copyright
	///    information. It may be useful to show this information
	///    somewhere in the program while the media is
	///    playing.</para>
	///    <para>Players should not support editing this field, but
	///    media creation tools should definitely allow
	///    modification.</para>
	/// </remarks>
	public virtual string Copyright {
		get { return null; }
		set { }
	}


	/// <summary>
	///    Gets and sets the date at which the tag has been written.
	/// </summary>
	/// <value>
	///    A nullable <see cref="DateTime" /> object containing the
	///    date at which the tag has been written, or <see
	///    langword="null" /> if no value present.
	/// </value>
	public virtual DateTime? DateTagged {
		get { return null; }
		set { }
	}


	/// <summary>
	///    Gets and sets the MusicBrainz Artist ID of the media represented by
	///    the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> containing the MusicBrainz ArtistID of the
	///    media represented by the current instance or an empty
	///    array if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field represents the MusicBrainz ArtistID, and is used
	///    to uniquely identify a particular Artist of the track.</para>
	/// </remarks>
	public virtual string MusicBrainzArtistId {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets the MusicBrainz Release Group ID of the media represented by
	///    the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> containing the MusicBrainz ReleaseGroupID of the
	///    media represented by the current instance or an empty
	///    array if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field represents the MusicBrainz ReleaseGroupID, and is used
	///    to uniquely identify a particular Release Group to which this track belongs.</para>
	/// </remarks>
	public virtual string MusicBrainzReleaseGroupId {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets the MusicBrainz Release ID of the media represented by
	///    the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> containing the MusicBrainz ReleaseID of the
	///    media represented by the current instance or an empty
	///    array if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field represents the MusicBrainz ReleaseID, and is used
	///    to uniquely identify a particular Release to which this track belongs.</para>
	/// </remarks>
	public virtual string MusicBrainzReleaseId {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets the MusicBrainz Release Artist ID of the media represented by
	///    the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> containing the MusicBrainz ReleaseArtistID of the
	///    media represented by the current instance or an empty
	///    array if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field represents the MusicBrainz Release ArtistID, and is used
	///    to uniquely identify a particular Album Artist credited with the Album.</para>
	/// </remarks>
	public virtual string MusicBrainzReleaseArtistId {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets the MusicBrainz Track ID of the media represented by
	///    the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> containing the MusicBrainz TrackID of the
	///    media represented by the current instance or an empty
	///    array if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field represents the MusicBrainz TrackID, and is used
	///    to uniquely identify a particular track.</para>
	/// </remarks>
	public virtual string MusicBrainzTrackId {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets the MusicBrainz Recording ID of the media represented by
	///    the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> containing the MusicBrainz RecordingID of the
	///    media represented by the current instance or an empty
	///    array if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field represents the MusicBrainz RecordingID, and is used
	///    to uniquely identify a particular recording.</para>
	/// </remarks>
	public virtual string MusicBrainzRecordingId {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets the MusicBrainz Work ID of the media represented by
	///    the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> containing the MusicBrainz WorkID of the
	///    media represented by the current instance or an empty
	///    array if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field represents the MusicBrainz WorkID, and is used
	///    to uniquely identify a particular work.</para>
	/// </remarks>
	public virtual string MusicBrainzWorkId {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets the MusicBrainz Disc ID of the media represented by
	///    the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> containing the MusicBrainz DiscID of the
	///    media represented by the current instance or an empty
	///    array if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field represents the MusicBrainz DiscID, and is used
	///    to uniquely identify the particular Released Media associated with
	///    this track.</para>
	/// </remarks>
	public virtual string MusicBrainzDiscId {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets the MusicIP PUID of the media represented by
	///    the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> containing the MusicIP PUID of the
	///    media represented by the current instance or an empty
	///    array if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field represents the MusicIP PUID, and is an acoustic
	///    fingerprint identifier.  It Identifies what this track "Sounds Like".</para>
	/// </remarks>
	public virtual string MusicIpId {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets the Amazon ID of the media represented by
	///    the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> containing the AmazonID of the
	///    media represented by the current instance or an empty
	///    array if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field represents the AmazonID, and is used
	///    to identify the particular track or album in the Amazon Catalog.</para>
	/// </remarks>
	public virtual string AmazonId {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets the MusicBrainz Release Status of the media represented by
	///    the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> containing the MusicBrainz ReleaseStatus of the
	///    media represented by the current instance or an empty
	///    array if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field represents the MusicBrainz ReleaseStatus, and is used
	///    to describes how 'official' a Release is.  Common Status are: Official, Promotion,
	///    Bootleg, Pseudo-release.</para>
	/// </remarks>
	public virtual string MusicBrainzReleaseStatus {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets the MusicBrainz Release Type of the media represented by
	///    the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> containing the MusicBrainz ReleaseType of the
	///    media represented by the current instance or an empty
	///    array if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field represents the MusicBrainz ReleaseType, that describes
	///    what kind of release a Release is..  Common Status are: Single, Album,
	///    EP, Compilation, Soundtrack, SpokenWord, Interview, Audiobook, Live, Remix,
	///    and Other.  Careful thought must be given when using this field to decide if
	///    a particular track "Is a Compilation".</para>
	/// </remarks>
	public virtual string MusicBrainzReleaseType {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets the MusicBrainz Release Country of the media represented by
	///    the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> containing the MusicBrainz ReleaseCountry of the
	///    media represented by the current instance or an empty
	///    array if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field represents the MusicBrainz ReleaseCountry, that describes
	///    the country in which an album was released.  Note that the ReleaseCountry
	///    of an album is not necessarily the country in which it was produced. The
	///    label itself will typically be more relevant. eg, a release on "Foo Records UK"
	///    that has "Made in Austria" printed on it, will likely be a UK release.</para>
	/// </remarks>
	public virtual string MusicBrainzReleaseCountry {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets the ReplayGain track gain in dB.
	/// </summary>
	/// <value>
	///    A <see cref="bool" /> value in dB for the track gain as
	///    per the ReplayGain specification.
	/// </value>
	public virtual double ReplayGainTrackGain {
		get { return double.NaN; }
		set { }
	}

	/// <summary>
	///    Gets and sets the ReplayGain track peak sample.
	/// </summary>
	/// <value>
	///    A <see cref="bool" /> value for the track peak as per the
	///    ReplayGain specification.
	/// </value>
	public virtual double ReplayGainTrackPeak {
		get { return double.NaN; }
		set { }
	}

	/// <summary>
	///    Gets and sets the ReplayGain album gain in dB.
	/// </summary>
	/// <value>
	///    A <see cref="bool" /> value in dB for the album gain as
	///    per the ReplayGain specification.
	/// </value>
	public virtual double ReplayGainAlbumGain {
		get { return double.NaN; }
		set { }
	}

	/// <summary>
	///    Gets and sets the ReplayGain album peak sample.
	/// </summary>
	/// <value>
	///    A <see cref="bool" /> value for the album peak as per the
	///    ReplayGain specification.
	/// </value>
	public virtual double ReplayGainAlbumPeak {
		get { return double.NaN; }
		set { }
	}

	/// <summary>
	///    Gets and sets the initial key of the song.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> value for the initial key
	///    of the song.
	/// </value>
	public virtual string InitialKey {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets the remixer of the song.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> value for the remixer
	///    of the song.
	/// </value>
	public virtual string RemixedBy {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets the publisher of the song.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> value for the publisher
	///    of the song.
	/// </value>
	public virtual string Publisher {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets the ISRC (International Standard Recording Code) of the song.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> value containing the ISRC of the song.
	/// </value>
	public virtual string ISRC {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets the Length of the media represented
	///    by the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="string" /> object containing the length of
	///    the media represented by the current instance or <see
	///    langword="null" /> if no value is present.
	/// </value>
	/// <remarks>
	///    <para>This field represents the label or length of the album the
	///    media belongs to. </para>
	///    <para>For example, "00:15:00".</para>
	/// </remarks>
	public virtual string Length {
		get { return null; }
		set { }
	}

	/// <summary>
	///    Gets and sets a collection of pictures associated with
	///    the media represented by the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="T:IPicture[]" /> containing a collection of
	///    pictures associated with the media represented by the
	///    current instance or an empty array if none are present.
	/// </value>
	/// <remarks>
	///    <para>Typically, this value is used to store an album
	///    cover or icon to use for the file, but it is capable of
	///    holding any type of image, including pictures of the
	///    band, the recording studio, the concert, etc.</para>
	/// </remarks>
	public virtual IPicture[] Pictures {
		get { return new IPicture[] { }; }
		set { }
	}

	/// <summary>
	///    Gets and sets the same value as <see cref="Performers"
	///    />.
	/// </summary>
	/// <value>
	///    The same value as <see cref="Performers" />.
	/// </value>
	/// <remarks>
	///    This property exists to aleviate confusion. Use <see
	///    cref="Performers" /> for track artists and <see
	///    cref="AlbumArtists" /> for album artists.
	/// </remarks>
	[Obsolete ("For album artists use AlbumArtists. For track artists, use Performers")]
	public virtual string[] Artists {
		get { return Performers; }
		set { Performers = value; }
	}

	/// <summary>
	///    Gets the same value as <see cref="FirstPerformer" />.
	/// </summary>
	/// <value>
	///    The same value as <see cref="FirstPerformer" />.
	/// </value>
	/// <remarks>
	///    This property exists to aleviate confusion. Use <see
	///    cref="FirstPerformer" /> for track artists and <see
	///    cref="FirstAlbumArtist" /> for album artists.
	/// </remarks>
	[Obsolete ("For album artists use FirstAlbumArtist. For track artists, use FirstPerformer")]
	public string FirstArtist {
		get { return FirstPerformer; }
	}

	/// <summary>
	///    Gets the first value contained in <see
	///    cref="AlbumArtists" />.
	/// </summary>
	/// <value>
	///    The first <see cref="string" /> object in <see
	///    cref="AlbumArtists" />, or <see langword="null" /> is it
	///    contains no values.
	/// </value>
	/// <remarks>
	///    This property is provided for convenience. Use <see
	///    cref="AlbumArtists" /> to set the value.
	/// </remarks>
	public string FirstAlbumArtist {
		get { return FirstInGroup (AlbumArtists); }
	}

	/// <summary>
	///    Gets the first value contained in <see
	///    cref="AlbumArtistsSort" />.
	/// </summary>
	/// <value>
	///    The first <see cref="string" /> object in <see
	///    cref="AlbumArtistsSort" />, or <see langword="null" /> is it
	///    contains no values.
	/// </value>
	/// <remarks>
	///    This property is provided for convenience. Use <see
	///    cref="AlbumArtistsSort" /> to set the value.
	/// </remarks>
	public string FirstAlbumArtistSort {
		get { return FirstInGroup (AlbumArtistsSort); }
	}

	/// <summary>
	///    Gets the first value contained in <see
	///    cref="Performers" />.
	/// </summary>
	/// <value>
	///    The first <see cref="string" /> object in <see
	///    cref="Performers" />, or <see langword="null" /> is it
	///    contains no values.
	/// </value>
	/// <remarks>
	///    This property is provided for convenience. Use <see
	///    cref="Performers" /> to set the value.
	/// </remarks>
	public string FirstPerformer {
		get { return FirstInGroup (Performers); }
	}

	/// <summary>
	///    Gets the first value contained in <see
	///    cref="PerformersSort" />.
	/// </summary>
	/// <value>
	///    The first <see cref="string" /> object in <see
	///    cref="PerformersSort" />, or <see langword="null" /> is it
	///    contains no values.
	/// </value>
	/// <remarks>
	///    This property is provided for convenience. Use <see
	///    cref="PerformersSort" /> to set the value.
	/// </remarks>
	public string FirstPerformerSort {
		get { return FirstInGroup (PerformersSort); }
	}

	/// <summary>
	///    Gets the first value contained in <see
	///    cref="ComposersSort" />.
	/// </summary>
	/// <value>
	///    The first <see cref="string" /> object in <see
	///    cref="ComposersSort" />, or <see langword="null" /> is it
	///    contains no values.
	/// </value>
	/// <remarks>
	///    This property is provided for convenience. Use <see
	///    cref="ComposersSort" /> to set the value.
	/// </remarks>
	public string FirstComposerSort {
		get { return FirstInGroup (ComposersSort); }
	}

	/// <summary>
	///    Gets the first value contained in <see
	///    cref="Composers" />.
	/// </summary>
	/// <value>
	///    The first <see cref="string" /> object in <see
	///    cref="Composers" />, or <see langword="null" /> is it
	///    contains no values.
	/// </value>
	/// <remarks>
	///    This property is provided for convenience. Use <see
	///    cref="Composers" /> to set the value.
	/// </remarks>
	public string FirstComposer {
		get { return FirstInGroup (Composers); }
	}

	/// <summary>
	///    Gets the first value contained in <see cref="Genres" />.
	/// </summary>
	/// <value>
	///    The first <see cref="string" /> object in <see
	///    cref="Genres" />, or <see langword="null" /> is it
	///    contains no values.
	/// </value>
	/// <remarks>
	///    This property is provided for convenience. Use <see
	///    cref="Genres" /> to set the value.
	/// </remarks>
	public string FirstGenre {
		get { return FirstInGroup (Genres); }
	}

	/// <summary>
	///    Gets the same value as <see cref="JoinedPerformers" />.
	/// </summary>
	/// <value>
	///    The same value as <see cref="JoinedPerformers" />.
	/// </value>
	/// <remarks>
	///    This property exists to aleviate confusion. Use <see
	///    cref="JoinedPerformers" /> for track artists and <see
	///    cref="JoinedAlbumArtists" /> for album artists.
	/// </remarks>
	[Obsolete ("For album artists use JoinedAlbumArtists. For track artists, use JoinedPerformers")]
	public string JoinedArtists {
		get { return JoinedPerformers; }
	}

	/// <summary>
	///    Gets a semicolon separated string containing the values
	///    in <see cref="AlbumArtists" />.
	/// </summary>
	/// <value>
	///    A semicolon separated <see cref="string" /> object
	///    containing the values in <see cref="AlbumArtists" />.
	/// </value>
	/// <remarks>
	///    This property is provided for convenience. Use <see
	///    cref="AlbumArtists" /> to set the value.
	/// </remarks>
	public string JoinedAlbumArtists {
		get { return JoinGroup (AlbumArtists); }
	}

	/// <summary>
	///    Gets a semicolon separated string containing the values
	///    in <see cref="Performers" />.
	/// </summary>
	/// <value>
	///    A semicolon separated <see cref="string" /> object
	///    containing the values in <see cref="Performers" />.
	/// </value>
	/// <remarks>
	///    This property is provided for convenience. Use <see
	///    cref="Performers" /> to set the value.
	/// </remarks>
	public string JoinedPerformers {
		get { return JoinGroup (Performers); }
	}

	/// <summary>
	///    Gets a semicolon separated string containing the values
	///    in <see cref="PerformersSort" />.
	/// </summary>
	/// <value>
	///    A semicolon separated <see cref="string" /> object
	///    containing the values in <see cref="PerformersSort" />.
	/// </value>
	/// <remarks>
	///    This property is provided for convenience. Use <see
	///    cref="PerformersSort" /> to set the value.
	/// </remarks>
	public string JoinedPerformersSort {
		get { return JoinGroup (PerformersSort); }
	}

	/// <summary>
	///    Gets a semicolon separated string containing the values
	///    in <see cref="Composers" />.
	/// </summary>
	/// <value>
	///    A semicolon separated <see cref="string" /> object
	///    containing the values in <see cref="Composers" />.
	/// </value>
	/// <remarks>
	///    This property is provided for convenience. Use <see
	///    cref="Composers" /> to set the value.
	/// </remarks>
	public string JoinedComposers {
		get { return JoinGroup (Composers); }
	}

	/// <summary>
	///    Gets a semicolon separated string containing the values
	///    in <see cref="Genres" />.
	/// </summary>
	/// <value>
	///    A semicolon separated <see cref="string" /> object
	///    containing the values in <see cref="Genres" />.
	/// </value>
	/// <remarks>
	///    This property is provided for convenience. Use <see
	///    cref="Genres" /> to set the value.
	/// </remarks>
	public string JoinedGenres {
		get { return JoinGroup (Genres); }
	}

	/// <summary>
	///    Gets the first string in an array.
	/// </summary>
	/// <param name="group">
	///    A <see cref="T:string[]" /> to get the first string from.
	/// </param>
	/// <returns>
	///    The first <see cref="string" /> object contained in
	///    <paramref name="group" />, or <see langword="null" /> if
	///    the array is <see langword="null" /> or empty.
	/// </returns>
	static string FirstInGroup (string[] group)
	{
		return group == null || group.Length == 0 ?
			null : group[0];
	}

	/// <summary>
	///    Joins a array of strings into a single, semicolon
	///    separated, string.
	/// </summary>
	/// <param name="group">
	///    A <see cref="T:string[]" /> containing values to combine.
	/// </param>
	/// <returns>
	///    A semicolon separated <see cref="string" /> object
	///    containing the values from <paramref name="group" />.
	/// </returns>
	static string JoinGroup (string[] group)
	{
		if (group == null || group.Length == 0)
			return null;

		return string.Join ("; ", group);
	}

	/// <summary>
	///    Gets whether or not the current instance is empty.
	/// </summary>
	/// <value>
	///    <see langword="true" /> if the current instance does not
	///    any values. Otherwise <see langword="false" />.
	/// </value>
	/// <remarks>
	///    In the default implementation, this checks the values
	///    supported by <see cref="Tag" />, but it may be extended
	///    by child classes to support other values.
	/// </remarks>
	public virtual bool IsEmpty {
		get {
			return IsNullOrLikeEmpty (Title) &&
			IsNullOrLikeEmpty (Grouping) &&
			IsNullOrLikeEmpty (AlbumArtists) &&
			IsNullOrLikeEmpty (Performers) &&
			IsNullOrLikeEmpty (Composers) &&
			IsNullOrLikeEmpty (Conductor) &&
			IsNullOrLikeEmpty (Copyright) &&
			IsNullOrLikeEmpty (Album) &&
			IsNullOrLikeEmpty (Comment) &&
			IsNullOrLikeEmpty (Genres) &&
			Year == 0 &&
			BeatsPerMinute == 0 &&
			Track == 0 &&
			TrackCount == 0 &&
			Disc == 0 &&
			DiscCount == 0;
		}
	}

	/// <summary>
	///    Clears the values stored in the current instance.
	/// </summary>
	/// <remarks>
	///    The clearing procedure is format specific and should
	///    clear all values.
	/// </remarks>
	public abstract void Clear ();



	/// <summary>
	///    Set the Tags that represent the Tagger software
	///    (TagLib#) itself.
	/// </summary>
	/// <remarks>
	///    This is typically a method to call just before
	///    saving a tag.
	/// </remarks>
	public void SetInfoTag ()
	{
		DateTagged = DateTime.Now;
	}


	/// <summary>
	///    Copies all standard values from one tag to another,
	///    optionally overwriting existing values.
	/// </summary>
	/// <param name="source">
	///    A <see cref="Tag" /> object containing the source tag to
	///    copy the values from.
	/// </param>
	/// <param name="target">
	///    A <see cref="Tag" /> object containing the target tag to
	///    copy values to.
	/// </param>
	/// <param name="overwrite">
	///    A <see cref="bool" /> specifying whether or not to copy
	///    values over existing one.
	/// </param>
	/// <remarks>
	///    <para>This method only copies the most basic values,
	///    those contained in this class, between tags. To copy
	///    format specific tags, or additional details, additional
	///    implementations need to be applied. For example, copying
	///    from one <see cref="TagLib.Id3v2.Tag" /> to another:
	///    <c>foreach (TagLib.Id3v2.Frame frame in old_tag)
	///    new_tag.AddFrame (frame);</c></para>
	/// </remarks>
	/// <exception cref="ArgumentNullException">
	///    <paramref name="source" /> or <paramref name="target" />
	///    is <see langword="null" />.
	/// </exception>
	[Obsolete ("Use Tag.CopyTo(Tag,bool)")]
	public static void Duplicate (Tag source, Tag target,
								  bool overwrite)
	{
		if (source == null)
			throw new ArgumentNullException (nameof (source));

		if (target == null)
			throw new ArgumentNullException (nameof (target));

		source.CopyTo (target, overwrite);
	}

	/// <summary>
	///    Copies the values from the current instance to another
	///    <see cref="TagLib.Tag" />, optionally overwriting
	///    existing values.
	/// </summary>
	/// <param name="target">
	///    A <see cref="Tag" /> object containing the target tag to
	///    copy values to.
	/// </param>
	/// <param name="overwrite">
	///    A <see cref="bool" /> specifying whether or not to copy
	///    values over existing one.
	/// </param>
	/// <remarks>
	///    <para>This method only copies the most basic values when
	///    copying between different tag formats, however, if
	///    <paramref name="target" /> is of the same type as the
	///    current instance, more advanced copying may be done.
	///    For example, <see cref="TagLib.Id3v2.Tag" /> will copy
	///    all of its frames to another tag.</para>
	/// </remarks>
	/// <exception cref="ArgumentNullException">
	///    <paramref name="target" /> is <see langword="null" />.
	/// </exception>
	public virtual void CopyTo (Tag target, bool overwrite)
	{
		if (target == null)
			throw new ArgumentNullException (nameof (target));

		if (overwrite || IsNullOrLikeEmpty (target.Title))
			target.Title = Title;

		if (overwrite || IsNullOrLikeEmpty (target.Subtitle))
			target.Subtitle = Subtitle;

		if (overwrite || IsNullOrLikeEmpty (target.Description))
			target.Description = Description;

		if (overwrite || IsNullOrLikeEmpty (target.AlbumArtists))
			target.AlbumArtists = AlbumArtists;

		if (overwrite || IsNullOrLikeEmpty (target.Performers))
			target.Performers = Performers;

		if (overwrite || IsNullOrLikeEmpty (target.PerformersRole))
			target.PerformersRole = PerformersRole;

		if (overwrite || IsNullOrLikeEmpty (target.Composers))
			target.Composers = Composers;

		if (overwrite || IsNullOrLikeEmpty (target.Album))
			target.Album = Album;

		if (overwrite || IsNullOrLikeEmpty (target.Comment))
			target.Comment = Comment;

		if (overwrite || IsNullOrLikeEmpty (target.Genres))
			target.Genres = Genres;

		if (overwrite || target.Year == 0)
			target.Year = Year;

		if (overwrite || target.Track == 0)
			target.Track = Track;

		if (overwrite || target.TrackCount == 0)
			target.TrackCount = TrackCount;

		if (overwrite || target.Disc == 0)
			target.Disc = Disc;

		if (overwrite || target.DiscCount == 0)
			target.DiscCount = DiscCount;

		if (overwrite || target.BeatsPerMinute == 0)
			target.BeatsPerMinute = BeatsPerMinute;

		if (overwrite || IsNullOrLikeEmpty (target.InitialKey))
			target.InitialKey = InitialKey;

		if (overwrite || IsNullOrLikeEmpty (target.Publisher))
			target.Publisher = Publisher;

		if (overwrite || IsNullOrLikeEmpty (target.ISRC))
			target.ISRC = ISRC;

		if (overwrite || IsNullOrLikeEmpty (target.RemixedBy))
			target.RemixedBy = RemixedBy;

		if (overwrite || IsNullOrLikeEmpty (target.Grouping))
			target.Grouping = Grouping;

		if (overwrite || IsNullOrLikeEmpty (target.Conductor))
			target.Conductor = Conductor;

		if (overwrite || IsNullOrLikeEmpty (target.Copyright))
			target.Copyright = Copyright;

		if (overwrite || target.DateTagged == null)
			target.DateTagged = DateTagged;

		if (overwrite || target.Pictures == null || target.Pictures.Length == 0)
			target.Pictures = Pictures;
	}

	/// <summary>
	///    Returns all values for a named field as a string array.
	/// </summary>
	/// <param name="name">
	///    The field name. Use constants from <see cref="TagFieldNames" /> for
	///    standard fields. Tag types that support custom fields (Vorbis, APEv2,
	///    ID3v2, ASF) also accept format-native keys for non-standard fields.
	/// </param>
	/// <returns>
	///    A <see cref="T:string[]" /> containing the values, or an empty array
	///    if the field is absent or empty.
	/// </returns>
	public virtual string[] GetField (string name)
	{
		string s;
		string[] a;
		uint u;

		switch (name) {
		case TagFieldNames.Title:
			s = Title; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.TitleSort:
			s = TitleSort; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.Subtitle:
			s = Subtitle; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.Description:
			s = Description; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.Performers:
			a = Performers; return a != null && a.Length > 0 ? a : Array.Empty<string> ();
		case TagFieldNames.PerformersSort:
			a = PerformersSort; return a != null && a.Length > 0 ? a : Array.Empty<string> ();
		case TagFieldNames.PerformersRole:
			a = PerformersRole; return a != null && a.Length > 0 ? a : Array.Empty<string> ();
		case TagFieldNames.AlbumArtists:
			a = AlbumArtists; return a != null && a.Length > 0 ? a : Array.Empty<string> ();
		case TagFieldNames.AlbumArtistsSort:
			a = AlbumArtistsSort; return a != null && a.Length > 0 ? a : Array.Empty<string> ();
		case TagFieldNames.Composers:
			a = Composers; return a != null && a.Length > 0 ? a : Array.Empty<string> ();
		case TagFieldNames.ComposersSort:
			a = ComposersSort; return a != null && a.Length > 0 ? a : Array.Empty<string> ();
		case TagFieldNames.Album:
			s = Album; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.AlbumSort:
			s = AlbumSort; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.Comment:
			s = Comment; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.Genres:
			a = Genres; return a != null && a.Length > 0 ? a : Array.Empty<string> ();
		case TagFieldNames.Year:
			u = Year; return u > 0 ? new[] { u.ToString (CultureInfo.InvariantCulture) } : Array.Empty<string> ();
		case TagFieldNames.Track:
			u = Track; return u > 0 ? new[] { u.ToString (CultureInfo.InvariantCulture) } : Array.Empty<string> ();
		case TagFieldNames.TrackCount:
			u = TrackCount; return u > 0 ? new[] { u.ToString (CultureInfo.InvariantCulture) } : Array.Empty<string> ();
		case TagFieldNames.Disc:
			u = Disc; return u > 0 ? new[] { u.ToString (CultureInfo.InvariantCulture) } : Array.Empty<string> ();
		case TagFieldNames.DiscCount:
			u = DiscCount; return u > 0 ? new[] { u.ToString (CultureInfo.InvariantCulture) } : Array.Empty<string> ();
		case TagFieldNames.Lyrics:
			s = Lyrics; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.Grouping:
			s = Grouping; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.BeatsPerMinute:
			u = BeatsPerMinute; return u > 0 ? new[] { u.ToString (CultureInfo.InvariantCulture) } : Array.Empty<string> ();
		case TagFieldNames.Conductor:
			s = Conductor; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.Copyright:
			s = Copyright; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.Publisher:
			s = Publisher; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.ISRC:
			s = ISRC; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.RemixedBy:
			s = RemixedBy; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.InitialKey:
			s = InitialKey; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.Length:
			s = Length; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.DateTagged: {
			var dt = DateTagged;
			return dt != null ? new[] { dt.Value.ToString ("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture) } : Array.Empty<string> ();
		}
		case TagFieldNames.MusicBrainzArtistId:
			s = MusicBrainzArtistId; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.MusicBrainzReleaseGroupId:
			s = MusicBrainzReleaseGroupId; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.MusicBrainzReleaseId:
			s = MusicBrainzReleaseId; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.MusicBrainzReleaseArtistId:
			s = MusicBrainzReleaseArtistId; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.MusicBrainzTrackId:
			s = MusicBrainzTrackId; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.MusicBrainzRecordingId:
			s = MusicBrainzRecordingId; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.MusicBrainzWorkId:
			s = MusicBrainzWorkId; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.MusicBrainzDiscId:
			s = MusicBrainzDiscId; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.MusicIpId:
			s = MusicIpId; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.AmazonId:
			s = AmazonId; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.MusicBrainzReleaseStatus:
			s = MusicBrainzReleaseStatus; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.MusicBrainzReleaseType:
			s = MusicBrainzReleaseType; return s != null ? new[] { s } : Array.Empty<string> ();
		case TagFieldNames.MusicBrainzReleaseCountry:
			s = MusicBrainzReleaseCountry; return s != null ? new[] { s } : Array.Empty<string> ();
		default:
			return Array.Empty<string> ();
		}
	}

	/// <summary>
	///    Sets the named field to the specified values.
	/// </summary>
	/// <param name="name">
	///    The field name. Use constants from <see cref="TagFieldNames" /> for
	///    standard fields. Tag types that support custom fields also accept
	///    format-native keys for non-standard fields.
	/// </param>
	/// <param name="values">
	///    The values to store. Pass an empty array or <see langword="null" />
	///    to clear the field.
	/// </param>
	public virtual void SetField (string name, params string[] values)
	{
		string first = values != null && values.Length > 0 ? values[0] : null;

		switch (name) {
		case TagFieldNames.Title:         Title = first; break;
		case TagFieldNames.TitleSort:     TitleSort = first; break;
		case TagFieldNames.Subtitle:      Subtitle = first; break;
		case TagFieldNames.Description:   Description = first; break;
		case TagFieldNames.Performers:    Performers = values ?? Array.Empty<string> (); break;
		case TagFieldNames.PerformersSort: PerformersSort = values ?? Array.Empty<string> (); break;
		case TagFieldNames.PerformersRole: PerformersRole = values ?? Array.Empty<string> (); break;
		case TagFieldNames.AlbumArtists:  AlbumArtists = values ?? Array.Empty<string> (); break;
		case TagFieldNames.AlbumArtistsSort: AlbumArtistsSort = values ?? Array.Empty<string> (); break;
		case TagFieldNames.Composers:     Composers = values ?? Array.Empty<string> (); break;
		case TagFieldNames.ComposersSort: ComposersSort = values ?? Array.Empty<string> (); break;
		case TagFieldNames.Album:         Album = first; break;
		case TagFieldNames.AlbumSort:     AlbumSort = first; break;
		case TagFieldNames.Comment:       Comment = first; break;
		case TagFieldNames.Genres:        Genres = values ?? Array.Empty<string> (); break;
		case TagFieldNames.Year:
			Year = first != null && uint.TryParse (first, NumberStyles.None, CultureInfo.InvariantCulture, out uint yr) ? yr : 0;
			break;
		case TagFieldNames.Track:
			Track = first != null && uint.TryParse (first, NumberStyles.None, CultureInfo.InvariantCulture, out uint tr) ? tr : 0;
			break;
		case TagFieldNames.TrackCount:
			TrackCount = first != null && uint.TryParse (first, NumberStyles.None, CultureInfo.InvariantCulture, out uint trc) ? trc : 0;
			break;
		case TagFieldNames.Disc:
			Disc = first != null && uint.TryParse (first, NumberStyles.None, CultureInfo.InvariantCulture, out uint dc) ? dc : 0;
			break;
		case TagFieldNames.DiscCount:
			DiscCount = first != null && uint.TryParse (first, NumberStyles.None, CultureInfo.InvariantCulture, out uint dcc) ? dcc : 0;
			break;
		case TagFieldNames.Lyrics:        Lyrics = first; break;
		case TagFieldNames.Grouping:      Grouping = first; break;
		case TagFieldNames.BeatsPerMinute:
			BeatsPerMinute = first != null && uint.TryParse (first, NumberStyles.None, CultureInfo.InvariantCulture, out uint bpm) ? bpm : 0;
			break;
		case TagFieldNames.Conductor:     Conductor = first; break;
		case TagFieldNames.Copyright:     Copyright = first; break;
		case TagFieldNames.Publisher:     Publisher = first; break;
		case TagFieldNames.ISRC:          ISRC = first; break;
		case TagFieldNames.RemixedBy:     RemixedBy = first; break;
		case TagFieldNames.InitialKey:    InitialKey = first; break;
		case TagFieldNames.Length:        Length = first; break;
		case TagFieldNames.DateTagged:
			DateTagged = first != null && DateTime.TryParse (first, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt) ? dt : (DateTime?)null;
			break;
		case TagFieldNames.MusicBrainzArtistId:         MusicBrainzArtistId = first; break;
		case TagFieldNames.MusicBrainzReleaseGroupId:   MusicBrainzReleaseGroupId = first; break;
		case TagFieldNames.MusicBrainzReleaseId:        MusicBrainzReleaseId = first; break;
		case TagFieldNames.MusicBrainzReleaseArtistId:  MusicBrainzReleaseArtistId = first; break;
		case TagFieldNames.MusicBrainzTrackId:          MusicBrainzTrackId = first; break;
		case TagFieldNames.MusicBrainzRecordingId:      MusicBrainzRecordingId = first; break;
		case TagFieldNames.MusicBrainzWorkId:           MusicBrainzWorkId = first; break;
		case TagFieldNames.MusicBrainzDiscId:           MusicBrainzDiscId = first; break;
		case TagFieldNames.MusicIpId:                   MusicIpId = first; break;
		case TagFieldNames.AmazonId:                    AmazonId = first; break;
		case TagFieldNames.MusicBrainzReleaseStatus:    MusicBrainzReleaseStatus = first; break;
		case TagFieldNames.MusicBrainzReleaseType:      MusicBrainzReleaseType = first; break;
		case TagFieldNames.MusicBrainzReleaseCountry:   MusicBrainzReleaseCountry = first; break;
		// Unknown names are ignored in the base implementation.
		}
	}

	/// <summary>
	///    Removes the named field, clearing all its values.
	/// </summary>
	/// <param name="name">
	///    The field name. Uses the same naming convention as
	///    <see cref="GetField" /> and <see cref="SetField" />.
	/// </param>
	public virtual void RemoveField (string name)
	{
		SetField (name);
	}

	/// <summary>
	///    Enumerates all fields that have at least one non-empty value.
	/// </summary>
	/// <returns>
	///    A sequence of <see cref="T:System.Collections.Generic.KeyValuePair`2" /> pairs
	///    where the key is a field name (see <see cref="TagFieldNames" />) and
	///    the value is a non-empty string array. For tag types that support
	///    custom fields the sequence also includes format-native fields that
	///    have no standard name equivalent.
	/// </returns>
	public virtual IEnumerable<KeyValuePair<string, string[]>> GetAllFields ()
	{
		string[] allNames = {
			TagFieldNames.Title, TagFieldNames.TitleSort, TagFieldNames.Subtitle,
			TagFieldNames.Description, TagFieldNames.Performers, TagFieldNames.PerformersSort,
			TagFieldNames.PerformersRole, TagFieldNames.AlbumArtists, TagFieldNames.AlbumArtistsSort,
			TagFieldNames.Composers, TagFieldNames.ComposersSort, TagFieldNames.Album,
			TagFieldNames.AlbumSort, TagFieldNames.Comment, TagFieldNames.Genres,
			TagFieldNames.Year, TagFieldNames.Track, TagFieldNames.TrackCount,
			TagFieldNames.Disc, TagFieldNames.DiscCount, TagFieldNames.Lyrics,
			TagFieldNames.Grouping, TagFieldNames.BeatsPerMinute, TagFieldNames.Conductor,
			TagFieldNames.Copyright, TagFieldNames.Publisher, TagFieldNames.ISRC,
			TagFieldNames.RemixedBy, TagFieldNames.InitialKey, TagFieldNames.Length,
			TagFieldNames.DateTagged,
			TagFieldNames.MusicBrainzArtistId, TagFieldNames.MusicBrainzReleaseGroupId,
			TagFieldNames.MusicBrainzReleaseId, TagFieldNames.MusicBrainzReleaseArtistId,
			TagFieldNames.MusicBrainzTrackId, TagFieldNames.MusicBrainzRecordingId,
			TagFieldNames.MusicBrainzWorkId, TagFieldNames.MusicBrainzDiscId,
			TagFieldNames.MusicIpId, TagFieldNames.AmazonId,
			TagFieldNames.MusicBrainzReleaseStatus, TagFieldNames.MusicBrainzReleaseType,
			TagFieldNames.MusicBrainzReleaseCountry,
		};

		foreach (string fieldName in allNames) {
			string[] values = GetField (fieldName);
			if (values.Length > 0)
				yield return new KeyValuePair<string, string[]> (fieldName, values);
		}
	}

	/// <summary>
	///    Checks if a <see cref="string" /> is <see langword="null"
	///    /> or contains only whitespace characters.
	/// </summary>
	/// <param name="value">
	///    A <see cref="string" /> object to check.
	/// </param>
	/// <returns>
	///    <see langword="true" /> if the string is <see
	///    langword="null" /> or contains only whitespace
	///    characters. Otherwise <see langword="false" />.
	/// </returns>
	static bool IsNullOrLikeEmpty (string value)
	{
		return value == null || value.Trim ().Length == 0;
	}

	/// <summary>
	///    Checks if all the strings in the array return <see
	///    langword="true" /> with <see
	///    cref="IsNullOrLikeEmpty(string)" /> or if the array is
	///    <see langword="null" /> or is empty.
	/// </summary>
	/// <param name="value">
	///    A <see cref="T:string[]" /> to check the contents of.
	/// </param>
	/// <returns>
	///    <see langword="true" /> if the array is <see
	///    langword="null" /> or empty, or all elements return <see
	///    langword="true" /> for <see
	///    cref="IsNullOrLikeEmpty(string)" />. Otherwise <see
	///    langword="false" />.
	/// </returns>
	static bool IsNullOrLikeEmpty (string[] value)
	{
		if (value == null)
			return true;

		foreach (string s in value)
			if (!IsNullOrLikeEmpty (s))
				return false;

		return true;
	}
}
