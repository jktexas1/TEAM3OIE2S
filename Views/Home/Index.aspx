<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: ViewData["Message"] %></h2>
    <p>
        The TEAM3 Online International EVAR System (TEAM3OIE2S) is global <b>anonymized</b> database of X-ray computed tomography (CT) data. This system allows surgeons to anonymize and upload CT data from pre- and post-operative EVAR stint patients. The data can be used by computational scientists to perform various analyses, which will be used by researchers to determine efficacy of EVAR procedures.
    </p>
</asp:Content>
