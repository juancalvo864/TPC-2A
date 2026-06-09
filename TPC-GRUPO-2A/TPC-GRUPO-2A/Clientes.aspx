<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="TPC_GRUPO_2A.Clientes" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2>Página de Clientes</h2>
         <asp:GridView ID="dgvClientes" runat="server" CssClass="table table-bordered table-hover"
            AutoGenerateColumns="false" DataKeyNames="Id" OnRowCommand="dgvClientes_RowCommand">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                <asp:BoundField DataField="Identificacion" HeaderText="Identificación" />
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
                            CommandName="Baja" CommandArgument='<%# Eval("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-success" OnClick="btnAgregar_Click" />
    </main>
</asp:Content>