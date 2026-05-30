<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPC_GRUPO_2A._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="dgvClientes" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered" DataKeyNames="Id">
       
 <%--       <Columns>
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
            <asp:CommandField ShowSelectButton="true" HeaderText="Seleccionar" />
        </Columns>--%>
    </asp:GridView>
</asp:Content>
