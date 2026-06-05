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
    <style>
        .login-wrapper {
            min-height: 100vh;
            background: #f5f7fb;
            display: flex;
            align-items: center;
            justify-content: center;
        }
        .login-card {
            background: #fff;
            border-radius: 14px;
            border: 1px solid #e5e7eb;
            padding: 40px 36px;
            width: 100%;
            max-width: 420px;
            box-shadow: 0 4px 24px rgba(0,0,0,0.07);
        }
        .login-brand {
            display: flex;
            align-items: center;
            gap: 12px;
            margin-bottom: 28px;
        }
        .login-brand-icon {
            width: 46px;
            height: 46px;
            border-radius: 10px;
            background: #0b2a4a;
            display: flex;
            align-items: center;
            justify-content: center;
            color: #fff;
            font-size: 22px;
        }
        .login-brand strong {
            display: block;
            font-size: 17px;
            color: #1f2937;
            line-height: 1.1;
        }
        .login-brand span {
            display: block;
            color: #6b7280;
            font-size: 13px;
            margin-top: 2px;
        }
        .login-title {
            font-size: 22px;
            font-weight: 700;
            color: #1f2937;
            margin-bottom: 6px;
        }
        .login-subtitle {
            color: #6b7280;
            font-size: 14px;
            margin-bottom: 28px;
        }
        .form-label {
            font-size: 13px;
            font-weight: 600;
            color: #374151;
        }
        .btn-login {
            background: #0d6efd;
            color: #fff;
            border: none;
            border-radius: 8px;
            padding: 11px;
            font-size: 15px;
            font-weight: 600;
            width: 100%;
            margin-top: 8px;
        }
        .btn-login:hover {
            background: #0b5ed7;
            color: #fff;
        }
        .error-msg {
            background: #fef2f2;
            border: 1px solid #fecaca;
            color: #dc2626;
            border-radius: 8px;
            padding: 10px 14px;
            font-size: 13px;
            margin-bottom: 18px;
        }
    </style>
</head>
<body>
    <form runat="server">
        <div class="login-wrapper">
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
                    <label class="form-label">Usuario</label>
                    <asp:TextBox ID="txtLogin" runat="server" CssClass="form-control" placeholder="Ingresá tu usuario" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Contraseña</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="••••••••" />
                </div>

                <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn-login" OnClick="btnIngresar_Click"/>
            </div>
        </div>
    </form>
    <script src="<%= ResolveUrl("~/Scripts/bootstrap.bundle.min.js") %>"></script>
</body>
</html>
