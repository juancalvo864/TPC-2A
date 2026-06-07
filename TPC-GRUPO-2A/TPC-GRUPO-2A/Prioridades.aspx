<%@ Page Title="Prioridades" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Prioridades.aspx.cs" Inherits="TPC_GRUPO_2A.Prioridades" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="d-flex justify-content-between align-items-center mb-3">
            <h2>Prioridades</h2>
            <asp:Button ID="btnNuevo" runat="server" Text="+ Nuevo" CssClass="btn btn-primary" OnClick="btnNuevo_Click"/>
        </div>
        <asp:Panel ID="pnlFormulario" runat="server" Visible="false" CssClass="card p-3 mb-4">
            <asp:Label ID="lblTituloPanel" runat="server" Text="Nueva Prioridad" CssClass="h5 mb-3 d-block" />
            <div class="mb-3">
                <label class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingresá el nombre" />
            </div>
            <div class="mb-3">
                <label class="form-label">Nivel</label>
                <asp:TextBox ID="txtNivel" runat="server" CssClass="form-control" placeholder="Ingresá el nivel" TextMode="Number" />
            </div>
            <div class="d-flex gap-2">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click"/>
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click"/>
            </div>
        </asp:Panel>
        <asp:GridView ID="dgvPrioridades" runat="server" CssClass="table table-bordered table-hover"
            AutoGenerateColumns="false" DataKeyNames="Id">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Nivel" HeaderText="Nivel" />
                <asp:TemplateField HeaderText="Estado">
                    <ItemTemplate>
                        <%# (bool)Eval("Activo") ? "Activo" : "Inactivo" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-warning btn-sm"
                            CommandName="Editar" OnClick="btnEditar_Click" CommandArgument='<%# Eval("Id") %>' />
                        <asp:Button ID="btnBaja" runat="server" OnClick="btnBaja_Click"
                            Text='<%# (bool)Eval("Activo") ? "Dar de baja" : "Activar" %>'
                            CssClass='<%# (bool)Eval("Activo") ? "btn btn-danger btn-sm" : "btn btn-success btn-sm" %>'
                            CommandName="Baja" CommandArgument='<%# Eval("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </main>
</asp:Content>