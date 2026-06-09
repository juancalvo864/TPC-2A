<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClientesForm.aspx.cs" Inherits="TPC_GRUPO_2A.ClientesForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="mb-4">Alta de Cliente</h2>

        <div class="card shadow-sm">
            <div class="card-body">

                <div class="row">

                    <div class="col-md-6 mb-3">
                        <label class="form-label">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-6 mb-3">
                        <label class="form-label">Apellido</label>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-6 mb-3">
                        <label class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                    </div>

                    <div class="col-md-6 mb-3">
                        <label class="form-label">Teléfono</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-6 mb-3">
                        <label class="form-label">Identificación</label>
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-6 mb-3">
                        <label class="form-label">Fecha de Alta</label>
                        <asp:TextBox ID="txtFechaAlta" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>

                    <asp:Panel ID="pnlActivo" runat="server" Visible="true">
                    <div class="col-md-6 mb-3">
                        <label class="form-label d-block">Estado</label>
                        <div class="form-check">
                            <asp:CheckBox ID="chkActivo" runat="server" Checked="true" />
                            <label class="form-check-label ms-2" for="chkActivo">
                                Activo
                            </label>
                        </div>
                    </div>
                    </asp:Panel>

                </div>

                <hr />

                <div class="d-flex gap-2">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar"
                        CssClass="btn btn-primary" OnClick="btnGuardar_Click"/>

                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                        CssClass="btn btn-secondary" OnClick="btnCancelar_Click"/>
                </div>

            </div>
        </div>
    </div>
</asp:Content>

<%--        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Identificacion { get; set; }
        public DateTime FechaAlta { get; set; }
        public bool Activo { get; set; }--%>