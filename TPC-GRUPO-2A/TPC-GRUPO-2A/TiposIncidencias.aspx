<%@ Page Title="TiposIncidencias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TiposIncidencias.aspx.cs" Inherits="TPC_GRUPO_2A.TiposIncidencias" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="d-flex justify-content-between align-items-center mb-3">
            <h2>Tipos de Incidencias</h2>
            <asp:Button ID="btnNuevo" runat="server" Text="+ Nuevo" CssClass="btn btn-primary" OnClick="btnNuevo_Click"/>
        </div>
        <div class="d-flex gap-2 mb-3">
            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select" style="max-width:160px;">
                <asp:ListItem Value="activo" Text="Activos" Selected="True" />
                <asp:ListItem Value="inactivo" Text="Inactivos" />
                <asp:ListItem Value="todos" Text="Todos" />
            </asp:DropDownList>
            <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por nombre/descripción" style="max-width:300px;" />
            <asp:Button ID="btnFiltrar" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary" OnClick="btnFiltrar_Click"/>
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-outline-danger" OnClick="btnLimpiar_Click"/>
        </div>
        <asp:Panel ID="pnlFormulario" runat="server" Visible="false" CssClass="card p-3 mb-4">
             <asp:Label ID="lblTituloPanel" runat="server" Text="Nuevo Tipo de Incidencia" CssClass="h5 mb-3 d-block" />
            <div class="mb-3">
                <label class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingresá el nombre" />
            </div>
            <div class="mb-3">
                <label class="form-label">Descripción</label>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" placeholder="Ingresá una descripción (opcional)" />
            </div>
            <div class="d-flex gap-2">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click"/>
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click"/>
            </div>
        </asp:Panel>

        <asp:GridView ID="dgvTipos" runat="server" CssClass="table table-bordered table-hover"
            AutoGenerateColumns="false" DataKeyNames="Id" OnRowCommand="dgvTipos_RowCommand">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                <asp:TemplateField HeaderText="Estado">
                    <ItemTemplate>
                        <%# (bool)Eval("Activo") ? "Activo" : "Inactivo" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-warning btn-sm"
                            CommandName="Editar" CommandArgument='<%# Eval("Id") %>' />
                        <asp:Button ID="btnBaja" runat="server"
                            Text='<%# (bool)Eval("Activo") ? "Dar de baja" : "Activar" %>'
                            CssClass='<%# (bool)Eval("Activo") ? "btn btn-danger btn-sm" : "btn btn-success btn-sm" %>'
                            CommandName="Baja" CommandArgument='<%# Eval("Id") %>'
                            OnClientClick='<%# (bool)Eval("Activo") ? "return confirm(\"¿Estás seguro que querés dar de baja este registro?\");" : "return confirm(\"¿Estás seguro que querés activar este registro?\");" %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </main>
</asp:Content>