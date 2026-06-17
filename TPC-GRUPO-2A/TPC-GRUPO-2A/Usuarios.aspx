<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="TPC_GRUPO_2A.Usuarios" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2>Pagina de Usuarios</h2>
        <asp:GridView
            ID="dgvUsuarios"
            runat="server"
            CssClass="table table-bordered table-hover"
            AutoGenerateColumns="false"
            DataKeyNames="Id">
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
            </Columns>
        </asp:GridView>
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-success" OnClick="btnAgregar_Click" />
    </main>
</asp:Content>
