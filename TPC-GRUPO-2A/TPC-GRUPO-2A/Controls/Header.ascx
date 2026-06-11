<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="TPC_GRUPO_2A.Controls.Header"%>

<header class="app-topbar">
    <div class="topbar-search">
        <input class="form-control"
            type="search"
            placeholder="Buscar cliente, reclamo o telefono..."
            aria-label="Buscar">
    </div>

     <div class="topbar-user">
        <a href="Perfil.aspx" class="text-muted small text-decoration-none">
        <div class="user-avatar">
            <asp:Label ID="lblIniciales" runat="server" />
        </div>
        </a>
            <div>
                <strong><asp:Label ID="lblNombre" runat="server" /></strong>
                <span><asp:Label ID="lblRol" runat="server" /></span>
            </div>
    </div>
</header>