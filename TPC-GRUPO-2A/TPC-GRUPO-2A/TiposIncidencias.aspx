<%@ Page Title="TiposIncidencias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TiposIncidencias.aspx.cs" Inherits="TPC_GRUPO_2A.TiposIncidencias" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="d-flex justify-content-between align-items-center mb-3">
            <h2>Tipos de Incidencias</h2>
            <asp:Button ID="btnNuevo" runat="server" Text="+ Nuevo" CssClass="btn btn-primary" />
        </div>

        <asp:GridView ID="dgvTipos" runat="server" CssClass="table table-bordered table-hover"
            AutoGenerateColumns="false" DataKeyNames="Id">
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
                        <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-warning btn-sm" />
                        <asp:Button ID="btnBaja" runat="server" Text="Dar de baja" CssClass="btn btn-danger btn-sm" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </main>
</asp:Content>