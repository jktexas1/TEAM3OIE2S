<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ZipFiles
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Anonymize &amp; Zip Data</h2>
    <div>
    <h3>
    Anonymize
    </h3>
    <p>
    Please Anonymize all Patient Data: 
        <a href="http://www.dclunie.com/pixelmed/software/webstart/DicomCleaner.html", target="_blank"><button>Anoynimize</button></a>
        <br />
    </p>
    
    </div>
    <div>
    <p />
    Alternatively, you can zip the data here (it is better to anonymize first)
    <h3>Zip the data</h3>
    <IFRAME id="frame1" src=" http://www.ezyzip.com"  runat="server" 
        style="width: 711px; height: 425px"></IFRAME>
    </div>
    
   
   

    <br />
    <br />
   
   

</asp:Content>
