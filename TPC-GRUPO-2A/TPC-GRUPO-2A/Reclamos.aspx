<%@ Page Title="Incidencias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reclamos.aspx.cs" Inherits="TPC_GRUPO_2A.Reclamos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main class="incident-workspace">
        <section class="card shadow-sm border-0">
            <div class="card-body p-4">
                <div class="row align-items-center g-3">
                    <div class="col-lg">
                        <div class="text-uppercase text-primary small fw-semibold mb-2">Mesa operativa</div>
                        <h2 class="mb-2">Panel de incidencias</h2>
                        <p class="text-muted mb-0">Una vista compacta para seguir reclamos, prioridades, estados y asignaciones sin salir del flujo.</p>
                    </div>
                    <div class="col-lg-auto">
                        <asp:Button ID="btnNuevaIncidencia" runat="server" Text="Cargar incidencia" CssClass="btn btn-primary" OnClick="btnNuevaIncidencia_Click" />
                    </div>
                </div>
            </div>
        </section>

        <section class="row g-3 incident-summary">
            <div class="col-12 col-md-6 col-xl-3">
                <div class="card border-primary shadow-sm h-100">
                    <div class="card-body">
                        <span class="text-uppercase small fw-semibold text-primary">Abiertas</span>
                        <div class="display-6 fw-bold text-primary mb-1">
                            <asp:Literal ID="litAbiertas" runat="server" />
                        </div>
                        <p class="text-muted mb-0 small">Reclamos recien ingresados o pendientes de atencion.</p>
                    </div>
                </div>
            </div>
            <div class="col-12 col-md-6 col-xl-3">
                <div class="card border-warning shadow-sm h-100">
                    <div class="card-body">
                        <span class="text-uppercase small fw-semibold text-warning">En analisis</span>
                        <div class="display-6 fw-bold text-warning mb-1">
                            <asp:Literal ID="litAnalisis" runat="server" />
                        </div>
                        <p class="text-muted mb-0 small">Casos en gestion activa o con trabajo en curso.</p>
                    </div>
                </div>
            </div>
            <div class="col-12 col-md-6 col-xl-3">
                <div class="card border-success shadow-sm h-100">
                    <div class="card-body">
                        <span class="text-uppercase small fw-semibold text-success">Resueltas</span>
                        <div class="display-6 fw-bold text-success mb-1">
                            <asp:Literal ID="litResueltas" runat="server" />
                        </div>
                        <p class="text-muted mb-0 small">Incidencias respondidas, pendientes de cierre final.</p>
                    </div>
                </div>
            </div>
            <div class="col-12 col-md-6 col-xl-3">
                <div class="card border-secondary shadow-sm h-100">
                    <div class="card-body">
                        <span class="text-uppercase small fw-semibold text-secondary">Cerradas</span>
                        <div class="display-6 fw-bold text-secondary mb-1">
                            <asp:Literal ID="litCerradas" runat="server" />
                        </div>
                        <p class="text-muted mb-0 small">Casos finalizados con comentario de cierre registrado.</p>
                    </div>
                </div>
            </div>
        </section>

        <section class="incident-toolbar card shadow-sm">
            <div class="card-body">
                <div class="row g-3 align-items-end">
                    <div class="col-lg-5">
                        <label class="form-label">Busqueda</label>
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Cliente, reclamo o detalle" />
                    </div>
                    <div class="col-lg-3">
                        <label class="form-label">Estado</label>
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select" />
                    </div>
                    <div class="col-lg-2">
                        <label class="form-label">Prioridad</label>
                        <asp:DropDownList ID="ddlPrioridad" runat="server" CssClass="form-select" />
                    </div>
                    <div class="col-lg-2">
                        <div class="incident-toolbar-actions">
                            <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary" OnClick="btnFiltrar_Click" />
                            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary" OnClick="btnLimpiar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <section class="card shadow-sm">
            <div class="card-body p-0">
                <asp:Panel ID="pnlSinResultados" runat="server" Visible="false" CssClass="incident-empty">
                    <h5 class="mb-2">No hay incidencias para mostrar</h5>
                    <p class="text-muted mb-0">Ajusta los filtros o registra una nueva incidencia para comenzar.</p>
                </asp:Panel>

                <asp:GridView ID="dgvIncidencias" runat="server"
                    CssClass="table table-hover align-middle mb-0 incident-table"
                    AutoGenerateColumns="false"
                    DataKeyNames="Id"
                    OnRowCommand="dgvIncidencias_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Reclamo">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkAbrir" runat="server" CssClass="fw-semibold text-decoration-none"
                                    CommandName="Abrir" CommandArgument='<%# Eval("Id") %>'>
                                    <%# Eval("NroReclamo") %>
                                </asp:LinkButton>
                                <div class="small text-muted mt-1"><%# ResumirTexto(Eval("DescripcionProblematica")) %></div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <span class='badge rounded-pill <%# ObtenerClaseEstado(Eval("EstadoActual.Nombre")) %>'><%# Eval("EstadoActual.Nombre") %></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cliente">
                            <ItemTemplate>
                                <div class="fw-semibold"><%# ObtenerNombreCliente(Eval("Cliente.Nombre"), Eval("Cliente.Apellido")) %></div>
                                <div class="small text-muted"><%# Eval("Cliente.Email") %></div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo">
                            <ItemTemplate>
                                <span class="badge text-bg-light border"><%# Eval("TipoIncidencia.Nombre") %></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prioridad">
                            <ItemTemplate>
                                <span class='badge rounded-pill <%# ObtenerClasePrioridad(Eval("Prioridad.Nombre")) %>'><%# Eval("Prioridad.Nombre") %></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Asignado">
                            <ItemTemplate>
                                <%# ObtenerNombreUsuario(Eval("UsuarioAsignado.Nombre"), Eval("UsuarioAsignado.Apellido")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Alta">
                            <ItemTemplate>
                                <div><%# Eval("FechaAlta", "{0:dd/MM/yyyy}") %></div>
                                <div class="small text-muted"><%# Eval("FechaAlta", "{0:HH:mm}") %></div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </section>
    </main>

    <div class="modal fade" id="incidentModal" tabindex="-1" aria-hidden="true" runat="server" clientidmode="Static">
        <div class="modal-dialog modal-xl modal-dialog-scrollable">
            <div class="modal-content">
                <div class="modal-header">
                    <div>
                        <h5 class="modal-title mb-0">Incidencia</h5>
                        <small class="text-muted">Edicion y seguimiento del reclamo seleccionado.</small>
                    </div>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="hdnIncidenteId" runat="server" />

                    <div class="row g-3 mb-3">
                        <div class="col-md-4">
                            <label class="form-label">Nro. reclamo</label>
                            <asp:TextBox ID="txtNroReclamo" runat="server" CssClass="form-control" Enabled="false" />
                        </div>
                        <div class="col-md-4">
                            <label class="form-label">Cliente</label>
                            <asp:TextBox ID="txtClienteDetalle" runat="server" CssClass="form-control" Enabled="false" />
                        </div>
                        <div class="col-md-4">
                            <label class="form-label">Estado actual</label>
                            <asp:TextBox ID="txtEstadoActual" runat="server" CssClass="form-control" Enabled="false" />
                        </div>
                        <div class="col-md-4">
                            <label class="form-label">Tipo</label>
                            <asp:DropDownList ID="ddlTipoDetalle" runat="server" CssClass="form-select" />
                        </div>
                        <div class="col-md-4">
                            <label class="form-label">Prioridad</label>
                            <asp:DropDownList ID="ddlPrioridadDetalle" runat="server" CssClass="form-select" />
                        </div>
                        <div class="col-md-4">
                            <asp:Panel ID="pnlAsignacion" runat="server">
                                <label class="form-label">Asignado a</label>
                                <asp:DropDownList ID="ddlAsignadoDetalle" runat="server" CssClass="form-select" />
                            </asp:Panel>
                        </div>
                        <div class="col-12">
                            <label class="form-label">Problemática</label>
                            <asp:TextBox ID="txtProblematicaDetalle" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                        </div>
                    </div>

                    <div class="d-flex justify-content-end gap-2 mb-4">
                        <asp:Button ID="btnGuardarCambios" runat="server" Text="Guardar cambios" CssClass="btn btn-primary" OnClick="btnGuardarCambios_Click" />
                        <asp:Button ID="btnReasignar" runat="server" Text="Reasignar" CssClass="btn btn-outline-primary" OnClick="btnReasignar_Click" />
                    </div>

                    <div class="card border-0 bg-light-subtle mb-3">
                        <div class="card-body">
                            <h6 class="mb-3">Agregar comentario</h6>
                            <div class="row g-3 align-items-end">
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtNuevoComentario" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Escribi una observacion, novedad o respuesta del cliente..." />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnAgregarComentario" runat="server" Text="Agregar" CssClass="btn btn-success w-100" OnClick="btnAgregarComentario_Click" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="table-responsive">
                        <asp:GridView ID="dgvComentarios" runat="server"
                            CssClass="table table-sm table-striped table-bordered align-middle mb-0"
                            AutoGenerateColumns="false" EmptyDataText="Todavia no hay comentarios cargados.">
                            <Columns>
                                <asp:TemplateField HeaderText="Fecha">
                                    <ItemTemplate>
                                        <%# Eval("FechaComentario", "{0:dd/MM/yyyy HH:mm}") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Usuario">
                                    <ItemTemplate>
                                        <%# ObtenerNombreUsuario(Eval("Usuario.Nombre"), Eval("Usuario.Apellido")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Comentario" HeaderText="Comentario" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
