<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Sidebar.ascx.cs" Inherits="TPC_GRUPO_2A.Controls.Sidebar" %>

<aside class="app-sidebar">
    <div class="sidebar-brand">
        <div class="brand-icon">CC</div>
        <div>
            <strong>CallCenter</strong>
            <span>Gestion de reclamos</span>
        </div>
    </div>

    <nav class="sidebar-nav" aria-label="Navegacion principal">
        <a id="lnkDashboard" runat="server" href="~/" class="sidebar-link">
            <span class="sidebar-icon">H</span>
            <span>Home</span>
        </a>

        <a id="lnkReclamos" runat="server" href="~/Reclamos" class="sidebar-link">
            <span class="sidebar-icon">I</span>
            <span>Incidencias</span>
        </a>

        <a id="lnkClientes" runat="server" href="~/Clientes" class="sidebar-link">
            <span class="sidebar-icon">C</span>
            <span>Clientes</span>
        </a>

        <a id="lnkUsuarios" runat="server" href="~/Usuarios" class="sidebar-link">
            <span class="sidebar-icon">U</span>
            <span>Usuarios</span>
        </a>

        <div class="sidebar-section">Administracion</div>

        <a id="lnkTipos" runat="server" href="~/TiposIncidencias" class="sidebar-link">
            <span class="sidebar-icon">T</span>
            <span>Tipos de incidencia</span>
        </a>

        <a id="lnkPrioridades" runat="server" href="~/Prioridades" class="sidebar-link">
            <span class="sidebar-icon">!</span>
            <span>Prioridades</span>
        </a>
    </nav>
</aside>
