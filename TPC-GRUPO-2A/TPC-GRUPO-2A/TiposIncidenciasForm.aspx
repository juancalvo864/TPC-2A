<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TiposIncidenciasForm.aspx.cs" Inherits="TPC_GRUPO_2A.TiposIncidenciasForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">
        <div class="row justify-content-center">
            <div class="col-md-8">

                <div class="card shadow">
                    <div class="card-header">
                        <h3 class="mb-0">Tipo de Incidencia</h3>
                    </div>

                    <div class="card-body">

                        <div class="mb-3">
                            <label for="txtNombre" class="form-label">Nombre</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <label for="txtDescripcion" class="form-label">Descripción</label>
                            <asp:TextBox ID="txtDescripcion"
                                runat="server"
                                CssClass="form-control"
                                TextMode="MultiLine"
                                Rows="4">
                            </asp:TextBox>
                        </div>

                        <div class="form-check mb-3">
                            <asp:CheckBox ID="chkActivo" runat="server" Text="Activo" />
                        </div>

                                                <div class="d-flex gap-2">
                            <asp:Button ID="btnGuardar"
                                runat="server"
                                Text="Guardar"
                                CssClass="btn btn-primary"
                                OnClick="btnGuardar_Click" />

                            <asp:Button ID="btnCancelar"
                                runat="server"
                                Text="Cancelar"
                                CssClass="btn btn-secondary"
                                PostBackUrl="~/TiposIncidencias.aspx" />
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
