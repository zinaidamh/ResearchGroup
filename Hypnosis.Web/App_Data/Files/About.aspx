<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Contents.aspx.cs" Inherits="Dynamic.Contents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=MVHughugHead%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
      
        <asp:Literal ID="content_container" runat="server"></asp:Literal>
     
 
</asp:Content> 
