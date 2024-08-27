<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Test Page Ronin.aspx.vb" Inherits="FinanceWeb.Test_Page_Ronin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<!DOCTYPE html>

<html>
<head>
</head>
<script>

    // Show a locator on the map
    //
    function ShowLocation(postal) {
        // Get the geographic location
        var url = "http://maps.google.com/maps/api/geocode/json?address=" + postal + "&sensor=false";
        sendRequest(url, DisplayAddress);
    }

    function DisplayAddress(url, response) {
        alert(response);
    }

    // Make a REST based request
    function sendRequest(url, callback) {
        var req = createXMLHTTPObject();
        if (!req)
            return;
        req.open("GET", url, true);
        req.setRequestHeader('User-Agent', 'XMLHTTP/1.0');

        req.onreadystatechange = function () {
            if (req.readyState != 4) return;

            if (req.status != 200 && req.status != 206 && req.status != 304) {
                alert(req.status + " " + url);
                return;
            }
            callback(url, req.responseText);
        }

        if (req.readyState == 4)
            return;
        req.send(null);
    }

    // Create object for making REST based request
    function createXMLHTTPObject() {
        if (navigator.appName == "Microsoft Internet Explorer") {
            http = new ActiveXObject("Microsoft.XMLHTTP");
        } else {
            http = new XMLHttpRequest();
        }
        return http;
    }
</script>
<body onload="ShowLocation( '30354' )">
</body>
</html>
