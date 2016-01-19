<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<TEAM3OIE2S.Models.Audit>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Audit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Audit</h2>

    <table>
        <tr>
            
            <th>
                auditID
            </th>
            <th>
                userID
            </th>
            <th>
                userName
            </th>
            <th>
                date
            </th>
            <th>
                DBtable
            </th>
            <th>
                attribute
            </th>
            <th>
                access
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
         
            <td>
                <%: item.auditID %>
            </td>
            <td>
                <%: item.userID %>
            </td>
            <td>
                <%: item.userName %>
            </td>
            <td>
                <%: String.Format("{0:g}", item.date) %>
            </td>
            <td>
                <%: item.DBtable %>
            </td>
            <td>
                <%: item.attribute %>
            </td>
            <td>
                <%: item.access %>
            </td>
        </tr>
    
    <% } %>

    </table>



</asp:Content>

