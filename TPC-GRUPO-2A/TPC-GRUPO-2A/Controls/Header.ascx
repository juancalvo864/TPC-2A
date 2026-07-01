<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="TPC_GRUPO_2A.Controls.Header"%>

<header class="app-topbar">

     <div class="topbar-user" style="margin-left:auto">
        <a href="Perfil.aspx" class="text-muted small text-decoration-none"/>
        <div class="dropdown">
            <a class="text-muted small text-decoration-none dropdown-toggle d-flex justify-content-between align-items-center gap-2" href="Perfil.aspx" role="button" id="dropdownMenuLink" data-bs-toggle="dropdown" aria-expanded="false">
             <div class="user-avatar">
                 <asp:Image ID="imgPerfil"
                     ImageUrl="~/Images/ProfileImages/default.png"
                     style="height:32px; width:32px; border-radius: 50%;"
                     runat="server" />
             </div>
             <div>
                <strong><asp:Label ID="lblNombre" runat="server" /></strong>
                <span><asp:Label ID="lblRol" runat="server" /></span>
             </div>
            </a>

            <ul class="dropdown-menu" aria-labelledby="dropdownMenuLink">
            <li><a class="dropdown-item" href="Perfil.aspx">Perfil</a></li>
            <li>
                <asp:LinkButton 
                    ID="btnCerrarSesion"
                    runat="server"
                    CssClass="dropdown-item text-danger"
                    OnClick="btnCerrarSesion_Click">
                    Cerrar sesión 
                    <i class="bi bi-box-arrow-right me-2"></i>
                </asp:LinkButton>
            </li>
            </ul>
        </div>
    </div>
</header>