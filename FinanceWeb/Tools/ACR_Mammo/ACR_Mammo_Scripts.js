    function getEventTarget(e) {
        e = e || window.event;
        return e.target || e.srcElement;
    }

var ul = document.getElementById('MainContent_ListPermissionLocations');
var img_src = '<img class="RedMinus" onclick="remove();" src="/Images/RedMinus.png" height="10px" width="10px">'

function addlocation(event) {
    var ul = document.getElementById("MainContent_UserLocations");
    var assigned_locations = ul.getElementsByTagName("li");
    var location_array = [];


    for (var i = 0; i < assigned_locations.length; ++i) {
        // do something with items[i], which is a <li> element
        console.log(assigned_locations[i].innerHTML.replace(img_src, "").replace(/^\d{1,10} - /, ""))

        location_array[i] = assigned_locations[i].innerHTML.replace(img_src, "").replace(/^\d{1,10} - /, "");
    }

    var target = getEventTarget(event);
    var selection = $(target).closest('tr').find('.locationcss').text().replace(img_src, "").replace(/^\d{1,10} - /, "")
    console.log("Selection: " + selection);
    console.log("Array: " + location_array);

    if ($.inArray(selection, location_array) == -1 && $.inArray("All Locations", location_array)) {
        $('ul#MainContent_UserLocations').append("<li class=\"no_padding\"><img class=\"RedMinus\" onClick=\"remove();\" src=\"/Images/RedMinus.png\" height=\"10px\" width=\"10px\"/>" + $(target).closest('tr').find('.locationcss').text().replace(img_src, "") + "</li>");
        $('#MainContent_Locations_Field').val(function (i, val) { if (val == '') { return $(target).closest('tr').find('.locationcss').text().replace(img_src, "").replace(/^\d{1,10} - /, "") } else { return val + '|' + $(target).closest('tr').find('.locationcss').text().replace(img_src, "").replace(/^\d{1,10} - /, ""); } });
    }

};

$(document).ready(function () {
    //if ($('#MainContent_Locations_Field').val() != "") {
    //    console.log("On Page Load: " + $('#MainContent_Locations_Field').val());
    //    $('#MainContent_Locations_Field').val().split('|').forEach(function (i, e, array) {
    //        var target = getEventTarget(event);
    //        console.log(i + " -- " + e);
    //        $('ul#MainContent_UserLocations').append("<li>" + img_src + i + "</li>");
    //    })
    //}
    $("#AddRemoveTechs").click(function () { alert("hello!"); }) // Just making sure we imported the js
});


function remove() {  // Account for  the removed term being the front/middle + back + only one in list
    $(event.target).closest("li").remove();
    console.log("Remove : " + $('#MainContent_Locations_Field').val().indexOf($(event.target).closest('li').text().replace(/^\d{1,10} - /, "")));
    if ($('#MainContent_Locations_Field').val().indexOf($(event.target).closest('li').text() + '|') >= 0) {
        $('#MainContent_Locations_Field').val($('#MainContent_Locations_Field').val().replace($(event.target).closest('li').text() + '|', ''));
    }
    else if ($('#MainContent_Locations_Field').val().indexOf('|' + $(event.target).closest('li').text()) >= 0) {
        $('#MainContent_Locations_Field').val($('#MainContent_Locations_Field').val().replace('|' + $(event.target).closest('li').text(), ''));
    }
    else if ($('#MainContent_Locations_Field').val().indexOf($(event.target).closest('li').text()) >= 0) {
        $('#MainContent_Locations_Field').val($('#MainContent_Locations_Field').val().replace($(event.target).closest('li').text(), ''));
    }
}

