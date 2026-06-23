<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClientesForm.aspx.cs" Inherits="TPC_GRUPO_2A.ClientesForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="mb-4">Alta de Cliente</h2>
        <asp:ValidationSummary 
            ID="vsErrores" 
            runat="server"
            CssClass="alert alert-danger"
            HeaderText="Revisá los siguientes campos:"
            ValidationGroup="Cliente" />

        <div class="card shadow-sm">
            <div class="card-body">

                <div class="row">

                    <div class="col-md-6 mb-3">
                        <label class="form-label">Nombre</label>
                        <asp:TextBox 
                            ID="txtNombre"
                            runat="server" 
                            CssClass="form-control" 
                            MaxLength="50">

                        </asp:TextBox>
                        <asp:RequiredFieldValidator 
                            ID="rfvNombre" runat="server" 
                            ControlToValidate="txtNombre" 
                            ErrorMessage="El nombre es obligatorio." 
                            CssClass="text-danger small" 
                            Display="Dynamic" 
                            ValidationGroup="Cliente"
                            />
                        <asp:RegularExpressionValidator
                            ID="revNombre"
                            runat="server"
                            ControlToValidate="txtNombre"
                            ErrorMessage="El nombre solo puede contener letras y espacios."
                            CssClass="text-danger small"
                            Display="Dynamic"
                            ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$"
                            ValidationGroup="Cliente" />
                    </div>

                    <div class="col-md-6 mb-3">
                        <label class="form-label">Apellido</label>
                        <asp:TextBox 
                            ID="txtApellido" 
                            runat="server" 
                            CssClass="form-control"
                            MaxLength="50">
                        </asp:TextBox>

                        <asp:RequiredFieldValidator 
                            ID="rfvApellido" 
                            runat="server"
                            ControlToValidate="txtApellido"
                            ErrorMessage="El apellido es obligatorio."
                            CssClass="text-danger small"
                            Display="Dynamic"
                            ValidationGroup="Cliente" />

                        <asp:RegularExpressionValidator
                            ID="revApellido"
                            runat="server"
                            ControlToValidate="txtApellido"
                            ErrorMessage="El apellido solo puede contener letras y espacios."
                            CssClass="text-danger small"
                            Display="Dynamic"
                            ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$"
                            ValidationGroup="Cliente" />
                    </div>

                    <div class="col-md-6 mb-3">
                        <label class="form-label">Email</label>
                        <asp:TextBox 
                            ID="txtEmail" 
                            runat="server" 
                            CssClass="form-control" 
                            TextMode="Email"
                            MaxLength="100">
                        </asp:TextBox>

                        <asp:RequiredFieldValidator 
                            ID="rfvEmail" 
                            runat="server"
                            ControlToValidate="txtEmail"
                            ErrorMessage="El email es obligatorio."
                            CssClass="text-danger small"
                            Display="Dynamic"
                            ValidationGroup="Cliente" />

                        <asp:RegularExpressionValidator
                            ID="revEmail"
                            runat="server"
                            ControlToValidate="txtEmail"
                            ErrorMessage="Ingresá un email válido."
                            CssClass="text-danger small"
                            Display="Dynamic"
                            ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                            ValidationGroup="Cliente" />
                    </div>

                    <div class="col-md-6 mb-3">
                        <label class="form-label">Teléfono</label>
                        <asp:TextBox 
                            ID="txtTelefono" 
                            runat="server" 
                            CssClass="form-control"
                            MaxLength="20"
                            placeholder="Ej: 1123456789">
                        </asp:TextBox>

                        <asp:RequiredFieldValidator 
                            ID="rfvTelefono" 
                            runat="server"
                            ControlToValidate="txtTelefono"
                            ErrorMessage="El teléfono es obligatorio."
                            CssClass="text-danger small"
                            Display="Dynamic"
                            ValidationGroup="Cliente" />

                        <asp:RegularExpressionValidator
                            ID="revTelefono"
                            runat="server"
                            ControlToValidate="txtTelefono"
                            ErrorMessage="El teléfono solo puede contener números, espacios, +, - o paréntesis."
                            CssClass="text-danger small"
                            Display="Dynamic"
                            ValidationExpression="^[0-9+\-\s()]{6,20}$"
                            ValidationGroup="Cliente" />
                    </div>

                    <div class="col-md-6 mb-3">
                        <label class="form-label">Identificación</label>
                         <asp:TextBox 
                            ID="txtIdentificacion" 
                            runat="server" 
                            CssClass="form-control"
                            MaxLength="20"
                            placeholder="Ej: DNI / CUIT">
                        </asp:TextBox>

                        <asp:RequiredFieldValidator 
                            ID="rfvIdentificacion" 
                            runat="server"
                            ControlToValidate="txtIdentificacion"
                            ErrorMessage="La identificación es obligatoria."
                            CssClass="text-danger small"
                            Display="Dynamic"
                            ValidationGroup="Cliente" />

                        <asp:RegularExpressionValidator
                            ID="revIdentificacion"
                            runat="server"
                            ControlToValidate="txtIdentificacion"
                            ErrorMessage="La identificación solo puede contener números."
                            CssClass="text-danger small"
                            Display="Dynamic"
                            ValidationExpression="^[0-9]{7,11}$"
                            ValidationGroup="Cliente" />
                    </div>

                    <div class="col-md-6 mb-3">
                        <label class="form-label">Fecha de Alta</label>
                        <asp:TextBox 
                            ID="txtFechaAlta" 
                            runat="server" 
                            CssClass="form-control" 
                            TextMode="Date">
                        </asp:TextBox>

                        <asp:RequiredFieldValidator 
                            ID="rfvFechaAlta" 
                            runat="server"
                            ControlToValidate="txtFechaAlta"
                            ErrorMessage="La fecha de alta es obligatoria."
                            CssClass="text-danger small"
                            Display="Dynamic"
                            ValidationGroup="Cliente" />
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


            </div>
        </div>
        <br />
                <div class="d-flex gap-2">
                    <asp:Button 
                        ID="btnGuardar" 
                        runat="server" 
                        Text="Guardar"
                        CssClass="btn btn-success" 
                        OnClick="btnGuardar_Click"
                        ValidationGroup="Cliente" />

                    <asp:Button 
                        ID="btnCancelar" 
                        runat="server" 
                        Text="Cancelar"
                        CssClass="btn btn-secondary" 
                        OnClick="btnCancelar_Click"
                        CausesValidation="false" />
                </div>
    </div>
</asp:Content>
