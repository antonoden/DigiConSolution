var current_active_playlist;

function playlistClicked(playlistid, viewclientid) {

    $.ajax({
        url: "Home/Playlist",
        type: "GET",
        cache: false,
        data: { playlistId: playlistid, viewclientId: viewclientid }
    }).done(function (result) {
        $('#playlist').html(result);
    });

    $("#playlist-" + playlistid).addClass("active");
    if (current_active_playlist != playlistid)
    {
        $("#playlist-" + current_active_playlist).removeClass('active');
        current_active_playlist = playlistid;
    }
}

var current_active_viewclient;

function viewclientClicked(id) {
    $.ajax({
        url: "Home/Viewclient",
        type: "GET",
        cache: false,
        data: { viewclientid: id }
    }).done(function (result) {
        $('#viewclient').html(result);
    });

    $("#viewclient-" + id).addClass("active");
    if (current_active_viewclient != id) {
        $("#viewclient-" + current_active_viewclient).removeClass('active');
        current_active_viewclient = id;
    }
}

var current_active_slide;

function slideClicked(slideid, playlistid)
{
    $.ajax({
        url: "Home/Slide",
        type: "GET",
        cache: false,
        data: { playlistId: playlistid, slideId: slideid }
    }).done(function (result) {
        $('#slide').html(result);
    });

    $("#slide-" + slideid).addClass("active");
    if (current_active_slide != slideid)
    {
        $("#slide-" + current_active_slide).removeClass('active');
        current_active_slide = slideid;
    }
}