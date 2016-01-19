<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Contact
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <form id="form1" runat="server">

    <h2>Contact Us</h2>
<p>
    <table style="width:100%;">
        <tr>
            <td style="width: 604px; color: #000000;">
                Contact Form:<br />
                <br />
                Name:
                <asp:TextBox ID="TextBox1" runat="server" Width="212px"></asp:TextBox>
                <br />
                <br />
                Email:
                <asp:TextBox ID="TextBox2" runat="server" Width="215px"></asp:TextBox>
                <br />
                <br />
                Comment:<br />
&nbsp;<asp:TextBox ID="TextBox3" runat="server" Height="120px" Width="257px"></asp:TextBox>
            </td>
           <%-- <td style="width: 113px">
                &nbsp;</td> --%>
            <td>
                Contact Information:<br />
                <br />
                We are located at 4800 Calhoun Dr, Houston, 77004<br />
                <br />
                Phone: 713-948-4800<br />
                <br />
                Email: <a href="mailto:team3oie2s@gmail.com">team3oie2s@gmail.com</a>
                <br />
                <br />
                <br />
                <br />
            </td>
        </tr>
       
    </table>
</p>

</form>

</asp:Content>
