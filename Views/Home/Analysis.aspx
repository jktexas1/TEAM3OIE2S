<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TEAM3OIE2S.Models.AnalysisModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Analysis
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Analysis</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.originalID) %>
            </div>
            <div class="editor-field">
                <%: Html.DropDownListFor(model => model.originalID, Model.OriginalIDs, "- Select - ", new { @class = "form-control", @onchange = "this.form.submit()" })%>
                <%: Html.ValidationMessageFor(model => model.originalID) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.studyAndSeries) %>
            </div>
            <div class="editor-field">
                 <%: Html.DropDownListFor(model => model.studyAndSeries, Model.StudySeries, "- Select - ", new { @class = "form-control" })%>
                <%: Html.ValidationMessageFor(model => model.studyAndSeries) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.ROI_begin) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.ROI_begin) %>
                <%: Html.ValidationMessageFor(model => model.ROI_begin) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.iliacBifurcation) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.iliacBifurcation) %>
                <%: Html.ValidationMessageFor(model => model.iliacBifurcation) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.comment) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.comment) %>
                <%: Html.ValidationMessageFor(model => model.comment) %>
            </div>
            
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

