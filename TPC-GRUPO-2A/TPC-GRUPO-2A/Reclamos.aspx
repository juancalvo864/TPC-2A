<%@ Page Title="Reclamos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reclamos.aspx.cs" Inherits="TPC_GRUPO_2A.Reclamos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <main>
        <div class="d-flex justify-content-between align-items-center mb-3">
            <h2>Reclamos</h2>
        </div>

        <div class="card shadow-sm">
            <div class="card-body">
                <asp:GridView ID="dgvReclamos" runat="server" CssClass="table table-bordered table-hover"
                    AutoGenerateColumns="false" DataKeyNames="Id">
                    <Columns>
                        <asp:BoundField DataField="NroReclamo" HeaderText="Nro. Reclamo" />
                        <asp:TemplateField HeaderText="Cliente">
                            <ItemTemplate>
                                <%# Eval("Cliente.Nombre") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo">
                            <ItemTemplate>
                                <%# Eval("TipoIncidencia.Nombre") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prioridad">
                            <ItemTemplate>
                                <%# Eval("Prioridad.Nombre") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <%# Eval("EstadoActual.Nombre") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Asignado a">
                            <ItemTemplate>
                                <%# Eval("UsuarioAsignado.Nombre") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha Alta">
                            <ItemTemplate>
                                <%# Eval("FechaAlta", "{0:dd/MM/yyyy}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button ID="btnVer" runat="server" Text="Ver detalle" CssClass="btn btn-outline-primary btn-sm"
                                    CommandName="Ver" CommandArgument='<%# Eval("Id") %>'/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </main>

</asp:Content>