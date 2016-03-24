<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;" border="1">
        <th colspan="2" style="text-align:center;">
            My Library App
            <br />
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </th>
        <tr>
            <td style="vertical-align:top;">
                <div style="font-weight:bold;">Users</div><br />
                <asp:GridView ID="gvUsers" runat="server" DataKeyNames="Id, CardNumber" EmptyDataText="No users found." AutoGenerateSelectButton="True" OnSelectedIndexChanged="gvUsers_SelectedIndexChanged">
                    <SelectedRowStyle BackColor="#FFCC66" />                    
                </asp:GridView>
            </td>
            <td style="vertical-align:top;">
                <div style="font-weight:bold;">Find Library Books:</div><br />
                <asp:TextBox ID="tbSearch" runat="server"></asp:TextBox>
                <asp:DropDownList ID="ddlSearchtype" runat="server">
                    <asp:ListItem Value="0">ISBN</asp:ListItem>
                    <asp:ListItem Value="1">Title</asp:ListItem>
                    <asp:ListItem Value="2">Author</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnFindBooks" runat="server" Text="Search" OnClick="btnFindBooks_Click" />
                <asp:GridView ID="gvBooks" runat="server" DataKeyNames="Id, IsAvailable" EmptyDataText="No books found." OnSelectedIndexChanged="gvBooks_SelectedIndexChanged">                
                    <Columns>
                        <asp:TemplateField HeaderText="First Name">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbCheckOut" CommandName="Select" runat="server">Check Out</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <div style="font-weight:bold;">User's Books</div><br />
                <asp:GridView ID="gvCheckedOut" runat="server" DataKeyNames="Id, LinkId" EmptyDataText="No checked out books found." OnSelectedIndexChanged="gvCheckedOut_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderText="First Name">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbCheckIn" CommandName="Select" runat="server">Check In</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
            <td>

            </td>
        </tr>
    </table>
</asp:Content>

