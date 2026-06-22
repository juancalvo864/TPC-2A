<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecuperarPassword.aspx.cs" Inherits="TPC_GRUPO_2A.RecuperarPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Recuperar contraseña - CallCenter</title>
    <link runat="server" href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <link runat="server" href="~/Content/bootstrap-icons/bootstrap-icons.min.css" rel="stylesheet" />
    <link runat="server" href="~/Content/Site.css" rel="stylesheet" />
    <link runat="server" href="~/Content/pages css/loginStyle.css" rel="stylesheet" />
</head>
<body>
    <form runat="server">
        <div class="login-wrapper">
            <div class="auth-container">
                <div class="auth-card" style="max-width: 460px;">
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
                        <div class="login-title">Recuperar contraseña</div>
                        <div class="login-subtitle">Ingresá tu email y te enviaremos las instrucciones</div>

                        <asp:Panel ID="pnlFormulario" runat="server">
                            <div class="mb-3">
                                <label class="form-label">Email</label>
                                <div class="input-group">
                                    <span class="input-group-text">
                                        <i class="bi bi-envelope"></i>
                                    </span>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"
                                        TextMode="Email" placeholder="Ingresá tu email" />
                                </div>
                            </div>
                            <asp:Button ID="btnEnviar" runat="server" Text="Enviar" CssClass="btn-login" OnClick="btnEnviar_Click"/>
                        </asp:Panel>
                        <asp:Panel ID="pnlConfirmacion" runat="server" Visible="false">
                            <div class="alert alert-success mt-3">
                                <i class="bi bi-check-circle me-2"></i>
                                Si existe una cuenta asociada a ese email, vas a recibir un mensaje con tu nueva contraseña en breve.
                           
                            </div>
                        </asp:Panel>
                        <div class="mt-3 text-center" style="font-size: 13px;">
                            <a href="<%= ResolveUrl("~/Login.aspx") %>" class="text-muted text-decoration-none">
                                <i class="bi bi-arrow-left me-1"></i>Volver al inicio de sesión
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="<%= ResolveUrl("~/Scripts/bootstrap.bundle.min.js") %>"></script>
</body>
</html>
