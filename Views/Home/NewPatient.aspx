<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TEAM3OIE2S.Models.Patient>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Patient
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Patient</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        
            <div class="editor-label">
                <%: Html.LabelFor(model => model.originalID) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.originalID) %>
                <%: Html.ValidationMessageFor(model => model.originalID) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.sex) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.sex) %>
                <%: Html.ValidationMessageFor(model => model.sex) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.age) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.age) %>
                <%: Html.ValidationMessageFor(model => model.age) %>
            </div>
           
            <%--
            <div class="editor-label">
                <%: Html.LabelFor(model => model.dateOfSurgery) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.dateOfSurgery)%>
                <%: Html.ValidationMessageFor(model => model.dateOfSurgery) %>
            </div>
             --%>
            <p>
                <input type="submit" value="Create" />
            </p>

    <% } %>

</asp:Content>

