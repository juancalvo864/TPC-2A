<%@ Page Title="Nuevo Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UsuariosForm.aspx.cs" Inherits="TPC_GRUPO_2A.UsuariosForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="mb-4">
            <asp:Label ID="lblTitulo" runat="server" Text="Alta de Usuario" />
        </h2>

        <asp:ValidationSummary
            ID="vsErrores"
            runat="server"
            CssClass="alert alert-danger"
            HeaderText="Revisa los siguientes campos:"
            ValidationGroup="Usuario" />

        <div class="card shadow-sm">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="El nombre es obligatorio." CssClass="text-danger small" Display="Dynamic" ValidationGroup="Usuario" />
                    </div>

                    <div class="col-md-6 mb-3">
                        <label class="form-label">Apellido</label>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ControlToValidate="txtApellido" ErrorMessage="El apellido es obligatorio." CssClass="text-danger small" Display="Dynamic" ValidationGroup="Usuario" />
                    </div>

                    <div class="col-md-6 mb-3">
                        <label class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="El email es obligatorio." CssClass="text-danger small" Display="Dynamic" ValidationGroup="Usuario" />
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Ingresa un email valido." CssClass="text-danger small" Display="Dynamic" ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" ValidationGroup="Usuario" />
                    </div>

                    <div class="col-md-6 mb-3">
                        <label class="form-label">Contrasena</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="La contrasena es obligatoria." CssClass="text-danger small" Display="Dynamic" ValidationGroup="Usuario" />
                        <asp:Label ID="lblPasswordAyuda" runat="server" CssClass="form-text text-muted" />
                    </div>

                    <div class="col-md-6 mb-3">
                        <label class="form-label">Rol</label>
                        <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-select"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvRol" runat="server" ControlToValidate="ddlRol" InitialValue="" ErrorMessage="Selecciona un rol." CssClass="text-danger small" Display="Dynamic" ValidationGroup="Usuario" />
                    </div>

                    <div class="col-md-6 mb-3">
                        <label class="form-label">Estado</label>
                        <asp:DropDownList ID="ddlActivo" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Activo" Value="true" Selected="True" />
                            <asp:ListItem Text="Inactivo" Value="false" />
                        </asp:DropDownList>
                    </div>
                </div>

            </div>
        </div>
        <br />
                <div class="d-flex gap-2">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" ValidationGroup="Usuario" />
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" OnClientClick="swConfirmar('Esta accionn no se puede deshacer.', this); return false;" CausesValidation="false" Visible="false" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" CausesValidation="false" />
                </div>
    </div>
</asp:Content>
