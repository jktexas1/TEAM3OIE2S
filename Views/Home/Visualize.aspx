<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Visualize
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Visualize</h2>
    NOTE: Java is required to run the DICOM viewer.
    <div />
    To view a DICOM Series, select File > Import > Image Sequence and select the directory containing the images.
    <div />
    For slice navigation buttons (play/pause/next/previous, etc.), click the <font color="#B40404"><b> >> </b></font> button on the right.
    <div />
    
    <%--<script src="~/Content/example.js" type="text/javascript" runat="server"></script> --%>
    <%--><applet code='ij.ImageJApplet.class' archive='~/Content/signed-ij-1.45q.jar' width="750" height="550" security="all-permissions"></applet> --%>

    <script src="http://www.java.com/js/deployJava.js" type="text/javascript" ></script>
    <script type="text/javascript" >
        var attributes = {
          code: 'ij.ImageJApplet',
          archive: 'http://cs4351.cs.uh.edu/TEAM3OIE2S/Content/signed-ij-1.45q.jar',
          width: 750, height: 550
         };
         var parameters = { fontSize: 16, java_arguments: "-Xmx512m" };
         var version = '1.6';
         deployJava.runApplet(attributes, parameters, version); 
    </script> 

</asp:Content>
