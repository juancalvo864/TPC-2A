<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="TPC_GRUPO_2A.Usuarios" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="d-flex justify-content-between align-items-center mb-3">
        <h2>Usuarios</h2>
         </div>
        <div class="d-flex gap-2 mb-3">
            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select" Style="max-width: 160px;">
                <asp:ListItem Value="activo" Text="Activos" Selected="True" />
                <asp:ListItem Value="inactivo" Text="Inactivos" />
                <asp:ListItem Value="todos" Text="Todos" />
            </asp:DropDownList>
            <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por nombre o email " Style="max-width: 300px;" />
            <asp:Button ID="btnFiltrar" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary" OnClick="btnFiltrar_Click" />
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-outline-danger" OnClick="btnLimpiar_Click" />
        </div>
        <div class="card shadow-sm">
            <div class="card-body">
                <asp:GridView
                    ID="dgvUsuarios"
                    runat="server"
                    CssClass="table table-bordered table-hover"
                    AutoGenerateColumns="false"
                    DataKeyNames="Id"
                    OnRowCommand="dgvUsuarios_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Login" HeaderText="Login" />
                        <asp:TemplateField HeaderText="Rol">
                            <ItemTemplate>
                                <%# Eval("Rol.Nombre") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <asp:CheckBox
                                    ID="chkActivo"
                                    runat="server"
                                    Checked='<%# Eval("Activo") %>'
                                    AutoPostBack="true"
                                    OnCheckedChanged="chkActivo_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button
                                    ID="btnEditar"
                                    runat="server"
                                    Text="Editar"
                                    CssClass="btn btn-warning btn-sm"
                                    CommandName="Editar"
                                    CommandArgument='<%# Eval("Id") %>' />
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
