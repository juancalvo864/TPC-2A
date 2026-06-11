<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="TPC_GRUPO_2A.Perfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
            <h2 class="mb-4">Mi perfil</h2>
            <div class="card shadow-sm">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Nombre</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Enabled="false" />
                        </div>
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Apellido</label>
                            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" Enabled="false" />
                        </div>
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Enabled="false" />
                        </div>
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Rol</label>
                            <asp:TextBox ID="txtRol" runat="server" CssClass="form-control" Enabled="false" />
                       
                            </div>
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Fecha de creación</label>
                            <asp:TextBox ID="txtFechaCreacion" runat="server" CssClass="form-control" Enabled="false" />
                        </div>
                    </div>
                    <asp:Panel ID="pnlPassword" runat="server" Visible="false">
                        <hr />
                        <h5 class="mb-3">Cambiar contraseña</h5>
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label class="form-label">Nueva contraseña</label>
                                <asp:TextBox ID="txtNuevaPassword" runat="server" CssClass="form-control" TextMode="Password" />
                            </div>
                            <div class="col-md-6 mb-3">
                                <label class="form-label">Confirmar contraseña</label>
                                <asp:TextBox ID="txtConfirmarPassword" runat="server" CssClass="form-control" TextMode="Password" />
                            </div>
                        </div>
                    </asp:Panel>

                    <hr />
                    <div class="d-flex gap-2">
                        <asp:Button ID="btnEditar" runat="server" Text="Editar perfil" CssClass="btn btn-primary" OnClick="btnEditar_Click"/>
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar cambios" CssClass="btn btn-success" Visible="false" OnClick="btnGuardar_Click"/>
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" Visible="false" OnClick="btnCancelar_Click"/>
                    </div>
                </div>
            </div>
        </div>

</asp:Content>
