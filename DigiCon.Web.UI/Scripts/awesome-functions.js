
/*******************************************************/
/*

********************************************************/





/***********************************************************/
/*

**************************************************************/

/* functions to be activated when a viewclient in 'onewindowsSolution' has been clicked */


function viewclientClicked(viewclientId) {
    $("#playlist").empty();
    $("#slide").empty();

    // ajax call viewclient
    $.ajax({
        url: "Home/Viewclient",
        type: "GET",
        cache: false,
        data: { viewclientid: viewclientId }
    }).done(function (result) {
        $('#viewclient').html(result);
    });

    // ajax call add playlist
    $.ajax({
        url: "Home/AddPlaylist",
        type: "GET",
        cache: false,
        data: { viewclientId: viewclientId }
    }).done(function (result) {
        $('#addPlaylistTab').html(result);
    });

    $.ajax({
        url: "Home/CreateAndAddPlaylist",
        type: "GET",
        cache: false,
        data: { viewclientId: viewclientId }
    }).done(function (result) {
        $('#addCreatePlaylistTab').html(result);
    });

    $("#viewclient-" + viewclientId).addClass("active");
    if (current_active_viewclient != viewclientId) {
        $("#viewclient-" + current_active_viewclient).removeClass('active');
        current_active_viewclient = viewclientId;
    }
}

/*********************************************************/
/*

***********************************************************/



/************************************************/
/*

**********************************************/

/* functions used in add playlist to viewclient- modal */
function addPlaylistToViewclientButtonClicked(viewclientid)
{
    $("#addPlaylistModal").modal("show");
}

function addPlaylistModalClose()
{
    $("#addPlaylistModal").modal("toggle");
}
/************************************************/
/*

**********************************************/

/* functions used in editmodal for playlists */
function editPlaylistButtonClicked(playlistid) {
    
    $.ajax({
        url: "Home/EditPlaylist",
        type: "GET",
        cache: false,
        data: { playlistId: playlistid }
    }).done(function (result) {
        $('#editPlaylistModalContent').html(result);
    });

    $("#editPlaylistModal").modal("show");

    
}
function editPlaylistModalClose() {
    $("#editPlaylistModal").modal("toggle");
}
/***********************************************/
/*

**********************************************/

/* functions used in editmodal for viewclient */


function editViewclientModalClose() {
    $("#editViewclientModal").modal("toggle");
}

function editViewclientModalSave() {
    editViewclientModalClose();
}

/************************************************/
/*

***************************************************/


/***********************************************/
/*

**********************************************/



/***************************************************/
/*

****************************************************/


/* function is used to change playorder of slides and playlists */
function stepOrder(sel) {
    var changedobj, direction;
    var lastobj = "";
    var currobj = {};
    var stopval, startval,currvalue;
    $(sel).each(function () {
        changedobj = $(this); //Element is now jqobject and not html
    });
    var mOld = parseInt(changedobj.attr("oldvalue")), mNew = parseInt(changedobj.val());
    console.log("Old is and then new is :"+ mOld + mNew);
    changedobj.val("0");
    var size = parseInt($(changedobj).attr("max"));
        if (mNew > mOld) {
            direction = 1; //Steps to right
            stopval = mNew + 1;
            startval = mOld + 1;
        } else if (mNew < mOld) {
            direction = -1 //Steps to left
            stopval = mOld;
            startval = mNew;
        }
       var allTxtb = $("input[id$=_Playorder]");
        for (var i = startval; i != stopval ; i++) {
            allTxtb.each(function () {
                if (($(this).val() == i) && ($(this).attr("name")!=lastobj)) {
                    currobj = this;
                    currvalue = i;   
                }
            });
            if ((currvalue != 0) && (typeof currobj !== 'undefined')) {
                console.log("Change value of " + $(currobj).attr("name"));
                var newval = parseInt($(currobj).val()) - (direction);               
                $(currobj).attr("oldvalue", $(currobj).val());
                $(currobj).val(newval);
                lastobj = $(currobj).attr("name");               
            }
     }
     $(changedobj).attr("oldvalue", mNew);
     $(changedobj).val(mNew);
    }

function saveOld(sel) {
    $(sel).each(function () {
        $(this).attr("oldvalue", $(this).val());
        console.log("Old val is: " + $(this).val());
    });

}


