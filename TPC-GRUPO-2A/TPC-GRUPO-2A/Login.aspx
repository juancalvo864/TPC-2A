<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TPC_GRUPO_2A.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Iniciar sesión - CallCenter</title>

    <link runat="server" href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <link runat="server" href="~/Content/bootstrap-icons/bootstrap-icons.min.css" rel="stylesheet" />
    <link runat="server" href="~/Content/Site.css" rel="stylesheet" />
    <link runat="server" href="~/Content/pages css/loginStyle.css" rel="stylesheet" />

</head>

<body>
    <form runat="server">
        <div class="login-wrapper">
            <div class="auth-container">
                <div class="auth-card">
                    <div class="row g-0">

                        <div class="col-md-6 d-none d-md-flex">
                            <div class="auth-brand-panel w-100 d-flex flex-column">
                                <div>
                                    <div class="brand-icon-large">
                                        <i class="bi bi-headset"></i>
                                    </div>

                                    <div class="brand-title">CallCenter</div>

                                    <p class="brand-description">
                                        Plataforma para gestionar reclamos, clientes, operadores,
                                        prioridades y seguimiento de casos desde un único lugar.
                                    </p>
                                </div>

                                <div class="feature-list">
                                    <div class="feature-item">
                                        <span class="feature-icon">
                                            <i class="bi bi-check-lg"></i>
                                        </span>
                                        <span>Gestión centralizada de reclamos</span>
                                    </div>

                                    <div class="feature-item">
                                        <span class="feature-icon">
                                            <i class="bi bi-check-lg"></i>
                                        </span>
                                        <span>Control de usuarios, roles y permisos</span>
                                    </div>

                                    <div class="feature-item">
                                        <span class="feature-icon">
                                            <i class="bi bi-check-lg"></i>
                                        </span>
                                        <span>Seguimiento del estado de cada caso</span>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="login-card">

                                <div class="login-brand">
                                    <div class="login-brand-icon">
                                        <i class="bi bi-headset"></i>
                                    </div>
                                    <div>
                                        <strong>CallCenter</strong>
                                        <span>Sistema de reclamos</span>
                                    </div>
                                </div>

                                <div class="login-title">Iniciar sesión</div>
                                <div class="login-subtitle">Ingresá tus credenciales para continuar</div>

                                <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="error-msg">
                                    <i class="bi bi-exclamation-circle me-1"></i>
                                    <asp:Literal ID="litError" runat="server" />
                                </asp:Panel>

                                <div class="mb-3">
                                    <label class="form-label">Email</label>

                                    <div class="input-group">
                                        <span class="input-group-text">
                                            <i class="bi bi-envelope"></i>
                                        </span>

                                        <asp:TextBox 
                                            ID="txtLogin" 
                                            runat="server" 
                                            CssClass="form-control" 
                                            TextMode="Email" 
                                            placeholder="Ingresá tu email" />
                                    </div>
                                </div>

                                <div class="mb-2">
                                    <label class="form-label">Contraseña</label>

                                    <div class="input-group">
                                        <span class="input-group-text">
                                            <i class="bi bi-lock"></i>
                                        </span>

                                        <asp:TextBox 
                                            ID="txtPassword" 
                                            runat="server" 
                                            CssClass="form-control" 
                                            TextMode="Password" 
                                            placeholder="••••••••" />
                                    </div>
                                </div>

                                <div class="auth-links">
                                    <span></span>
                                    <a href="<%= ResolveUrl("~/RecuperarPassword.aspx") %>">
                                        ¿Olvidaste tu contraseña?
                                    </a>
                                </div>

                                <asp:Button 
                                    ID="btnIngresar" 
                                    runat="server" 
                                    Text="Ingresar" 
                                    CssClass="btn-login" 
                                    OnClick="btnIngresar_Click" />

                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </form>

    <script src="<%= ResolveUrl("~/Scripts/bootstrap.bundle.min.js") %>"></script>
</body>
</html>