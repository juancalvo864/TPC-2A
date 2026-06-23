<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="TPC_GRUPO_2A.Clientes" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="d-flex justify-content-between align-items-center mb-3">
        <h2>Clientes</h2>
        </div>
        <div class="d-flex gap-2 mb-3">
            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select" style="max-width:160px;">
                <asp:ListItem Value="activo" Text="Activos" Selected="True" />
                <asp:ListItem Value="inactivo" Text="Inactivos" />
                <asp:ListItem Value="todos" Text="Todos" />
            </asp:DropDownList>
            <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por nombre/email " style="max-width:300px;" />
            <asp:Button ID="btnFiltrar" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary" OnClick="btnFiltrar_Click"/>
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-outline-danger" OnClick="btnLimpiar_Click"/>
        </div>
         <div class="card shadow-sm">
             <div class="card-body">
                 <asp:GridView ID="dgvClientes" runat="server" CssClass="table table-bordered table-hover"
                     AutoGenerateColumns="false" DataKeyNames="Id" OnRowCommand="dgvClientes_RowCommand">
                     <Columns>
                         <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                         <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                         <asp:BoundField DataField="Email" HeaderText="Email" />
                         <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                         <asp:BoundField DataField="Identificacion" HeaderText="Dni" />
                         <asp:TemplateField HeaderText="Fecha Alta">
                             <ItemTemplate>
                                 <%# Eval("FechaAlta", "{0:dd/MM/yyyy}") %>
                             </ItemTemplate>
                         </asp:TemplateField>
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
             </div>
         </div>
        <br />
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-success" OnClick="btnAgregar_Click" />
    </main>
</asp:Content>