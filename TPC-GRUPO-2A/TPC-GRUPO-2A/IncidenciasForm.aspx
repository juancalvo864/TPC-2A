<%@ Page Title="Nueva incidencia" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IncidenciasForm.aspx.cs" Inherits="TPC_GRUPO_2A.IncidenciasForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="incident-form-shell">
        <div class="incident-form-head">
            <div>
                <h2 class="mb-1">Carga de incidencia</h2>
                <p class="text-muted mb-0">Registra el reclamo y el sistema se encarga del alta, asignación y estado inicial.</p>
            </div>
        </div>

        <asp:ValidationSummary ID="vsErrores" runat="server" CssClass="alert alert-danger" HeaderText="Revisa los siguientes campos:" ValidationGroup="Incidencia" />

        <div class="card shadow-sm">
            <div class="card-body">
                <div class="row g-3">
                    <div class="col-md-6">
                        <label class="form-label">Cliente</label>
                        <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-select" />
                        <asp:RequiredFieldValidator ID="rfvCliente" runat="server" ControlToValidate="ddlCliente" InitialValue="" ErrorMessage="Debés seleccionar un cliente." CssClass="text-danger small" Display="Dynamic" ValidationGroup="Incidencia" />
                    </div>
                    <div class="col-md-3">
                        <label class="form-label">Tipo de incidencia</label>
                        <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-select" />
                        <asp:RequiredFieldValidator ID="rfvTipo" runat="server" ControlToValidate="ddlTipo" InitialValue="" ErrorMessage="Debés seleccionar un tipo de incidencia." CssClass="text-danger small" Display="Dynamic" ValidationGroup="Incidencia" />
                    </div>
                    <div class="col-md-3">
                        <label class="form-label">Prioridad</label>
                        <asp:DropDownList ID="ddlPrioridad" runat="server" CssClass="form-select" />
                        <asp:RequiredFieldValidator ID="rfvPrioridad" runat="server" ControlToValidate="ddlPrioridad" InitialValue="" ErrorMessage="Debés seleccionar una prioridad." CssClass="text-danger small" Display="Dynamic" ValidationGroup="Incidencia" />
                    </div>
                    <div class="col-12">
                        <label class="form-label">Problematica</label>
                        <asp:TextBox ID="txtProblematica" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" placeholder="Describí claramente el reclamo del cliente..." />
                        <asp:RequiredFieldValidator ID="rfvProblematica" runat="server" ControlToValidate="txtProblematica" ErrorMessage="Debes ingresar la problematica." CssClass="text-danger small" Display="Dynamic" ValidationGroup="Incidencia" />
                    </div>
                </div>
            </div>
        </div>

        <div class="d-flex gap-2 mt-3">
            <asp:Button ID="btnGuardar" runat="server" Text="Registrar incidencia" CssClass="btn btn-success" ValidationGroup="Incidencia" OnClick="btnGuardar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary" CausesValidation="false" OnClick="btnCancelar_Click" />
        </div>
    </main>
</asp:Content>
