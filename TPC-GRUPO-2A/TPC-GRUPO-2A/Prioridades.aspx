<%@ Page Title="Prioridades" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Prioridades.aspx.cs" Inherits="TPC_GRUPO_2A.Prioridades" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2>Página de Prioridades</h2>
        <p> Módulo en desarrollo.</p>
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
                                CommandName="Editar" CommandArgument='<%# Eval("Id") %>' />
                            <asp:Button ID="btnBaja" runat="server"
                                Text='<%# (bool)Eval("Activo") ? "Dar de baja" : "Activar" %>'
                                CssClass='<%# (bool)Eval("Activo") ? "btn btn-danger btn-sm" : "btn btn-success btn-sm" %>'
                                CommandName="Baja" CommandArgument='<%# Eval("Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
    </main>
</asp:Content>