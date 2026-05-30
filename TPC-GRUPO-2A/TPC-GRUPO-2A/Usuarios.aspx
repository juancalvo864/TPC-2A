<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="TPC_GRUPO_2A.Usuarios" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2>Página de Usuarios</h2>
<asp:GridView ID="dgvUsuarios" runat="server" CssClass="table"></asp:GridView>
    </main>
</asp:Content>