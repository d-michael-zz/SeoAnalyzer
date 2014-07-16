<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainForm.aspx.cs" Inherits="SeoAnalyzer.MainForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SEO Analyzer</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Insert URL or text."></asp:Label>
            <br />
            <asp:TextBox ID="Input" runat="server" Height="16px" Rows="2" Width="280px"></asp:TextBox>
            <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                <asp:ListItem Selected="True">Text</asp:ListItem>
                <asp:ListItem>Url</asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div>
            <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                <asp:ListItem Selected="True">Filter stop words</asp:ListItem>
                <asp:ListItem Selected="True">Each word count</asp:ListItem>
                <asp:ListItem Selected="True">Words in meta tags</asp:ListItem>
                <asp:ListItem Selected="True">External links count</asp:ListItem>
            </asp:CheckBoxList>
            <asp:Button ID="StartBtn" runat="server" Text="Start" OnClick="StartBtn_Click" />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Each word count"></asp:Label>
            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" OnSorting="GridView1_Sorting">
            </asp:GridView>
            <asp:Label ID="Label3" runat="server" Text="Words in meta tags"></asp:Label>
            <asp:GridView ID="GridView2" runat="server"></asp:GridView>
            <asp:Label ID="Label4" runat="server" Text="Number of links in text: "></asp:Label>
        </div>
    </form>
</body>
</html>
