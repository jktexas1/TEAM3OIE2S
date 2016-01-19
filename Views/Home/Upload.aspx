<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TEAM3OIE2S.Models.UploadModel>" %>
<%--<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %> --%>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Upload
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
    <h2>
    Please Anonymize and Compress Data: <a href= "<%: Url.Action("ZipFiles", "Home")%>"> <button>Anonymize and Compress</button></a>
    </h2>
    </div>
    <form id="form1" action="" method="post" enctype="multipart/form-data" runat="server">

  <%-- <% using (Html.BeginForm())
      {%> --%>
        <%: Html.ValidationSummary(true)%>

            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.originalID)%>
            </div>
            <div class="editor-field">
                 <%: Html.DropDownListFor(m => m.originalID, Model.OriginalIDs, "- Select -", new { @class = "form-control" }) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.dateOfSurgery)%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.dateOfSurgery)%>
                <%: Html.ValidationMessageFor(model => model.dateOfSurgery)%>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.endographBrand)%>
            </div>
            <div class="editor-field">
              <%: Html.DropDownListFor(m => m.endographBrand, Model.EndographBrands, "- Select -", new { @class = "form-control" }) %>
            </div>
            <table>
            <tr>
            <td></td>
            <td>Diameter</td>
            <td>Length</td>
            </tr>
            <tr>
            <td>Endograph Body</td>
            <td>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.endographDiameter)%>
                <%: Html.ValidationMessageFor(model => model.endographDiameter)%>
            </div>
            </td>
            <td>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.endographLength)%>
                <%: Html.ValidationMessageFor(model => model.endographLength)%>
            </div>
            </td>
            </tr>
            <tr>
            <td>Unilateral leg</td>
            <td>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.unilateralDiameter)%>
                <%: Html.ValidationMessageFor(model => model.unilateralDiameter)%>
            </div>
            </td>
            <td>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.unlateralLength)%>
                <%: Html.ValidationMessageFor(model => model.unlateralLength)%>
            </div>
            </td>
            </tr>

            <tr>
            <td>Contralateral leg</td>
            <td>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.contralateralDiameter)%>
                <%: Html.ValidationMessageFor(model => model.contralateralDiameter)%>
            </div>
            </td>
            <td>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.contralateralLength)%>
                <%: Html.ValidationMessageFor(model => model.contralateralLength)%>
            </div>
            </td>
            </tr>

            </table>
          <%-- <% } %> --%>
    <h2>Upload</h2>
    <input type="file" name="file" />
    <input type="submit" value="Submit"  />
    </form>

</asp:Content>
