<%@ Page Title="Prioridades" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Prioridades.aspx.cs" Inherits="TPC_GRUPO_2A.Prioridades" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="d-flex justify-content-between align-items-center mb-3">
            <h2>Prioridades</h2>
        </div>
        <div class="d-flex gap-2 mb-3">
            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select" style="max-width:160px;">
                <asp:ListItem Value="activo" Text="Activos" />
                <asp:ListItem Value="inactivo" Text="Inactivos" />
                <asp:ListItem Value="todos" Text="Todos" />
            </asp:DropDownList>
            <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por nombre" style="max-width:300px;" />
            <asp:Button ID="btnFiltrar" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary" OnClick="btnFiltrar_Click"/>
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-outline-danger" OnClick="btnLimpiar_Click"/>
        </div>
         <div class="card shadow-sm">
             <div class="card-body">
                 <asp:GridView ID="dgvPrioridades" runat="server" CssClass="table table-bordered table-hover"
                     AutoGenerateColumns="false" DataKeyNames="Id" OnRowCommand="dgvPrioridades_RowCommand">
                     <Columns>
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
                                     CommandName="Editar" CommandArgument='<%# Eval("Id") %>' />
                                 <asp:Button ID="btnBaja" runat="server"
                                     Text='<%# (bool)Eval("Activo") ? "Dar de baja" : "Activar" %>'
                                     CssClass='<%# (bool)Eval("Activo") ? "btn btn-danger btn-sm" : "btn btn-success btn-sm" %>'
                                     CommandName="Baja" CommandArgument='<%# Eval("Id") %>'
                                     OnClientClick='<%# (bool)Eval("Activo") ? "return swConfirmarClick(\"¿Querés dar de baja esta prioridad?\", this);" : "return swConfirmarClick(\"¿Querés activar esta prioridad?\", this);" %>' />
                             </ItemTemplate>
                         </asp:TemplateField>
                     </Columns>
                 </asp:GridView>
             </div>
         </div>
        <br />
            <asp:Button ID="btnNuevo" runat="server" Text="Agregar" CssClass="btn btn-success" OnClick="btnNuevo_Click" />
    </main>
</asp:Content>