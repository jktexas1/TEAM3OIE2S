<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<TEAM3OIE2S.Models.TestimonialView>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Testimonial
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Testimonial</h2>
   
    <form id="form1" action="" method="post" enctype="multipart/form-data" runat="server">
     <% if (Session["surgeonID"] != null)
           { %>
                <label>Add a testimonial: </label>
                <%= Html.TextBox("Add") %>
                <input type="submit" name="Add" value="Add" />
            
        <% } %>
        <p />
        <%= Html.TextBox("Search") %>
        <input type="submit" name="Search" value="Search" />
    
    <p />
    <table>
        <tr>
            <th>
                Name
            </th>
            <th>
                Testimonial
            </th>
            <th>
                Date
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%: item.firstname %>
            </td>
            <td>
                <%: item.content %>
            </td>
            <td>
                <%: String.Format("{0:g}", item.date) %>
            </td>
        </tr>
    
    <% } %>

    </table>
        
    </form>
</asp:Content>

