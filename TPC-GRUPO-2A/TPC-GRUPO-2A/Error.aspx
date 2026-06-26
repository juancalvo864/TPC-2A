<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="TPC_GRUPO_2A.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Error - CallCenter</title>
    <link runat="server" href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <link runat="server" href="~/Content/bootstrap-icons/bootstrap-icons.min.css" rel="stylesheet" />
    <link runat="server" href="~/Content/Site.css" rel="stylesheet" />
    <style>
        body { background-color: #f8f9fa; }
        .error-container {
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
        }
        .error-card {
            background: #fff;
            border-radius: 12px;
            box-shadow: 0 4px 24px rgba(0,0,0,.08);
            padding: 3rem 2.5rem;
            max-width: 480px;
            width: 100%;
            text-align: center;
        }
        .error-icon { font-size: 4rem; color: #dc3545; margin-bottom: 1rem; }
        .error-code { font-size: 5rem; font-weight: 700; color: #dee2e6; line-height: 1; }
    </style>
</head>
<body>
    <form runat="server">
        <div class="error-container">
            <div class="error-card">
                <div class="error-icon"><i class="bi bi-exclamation-triangle-fill"></i></div>
                <div class="error-code">500</div>
                <h2 class="mt-2 mb-1">Ocurrio un error inesperado</h2>
                <p class="text-muted mb-4">Lo sentimos, algo salio mal. Por favor intenta de nuevo o volve al inicio.</p>
                <a href="~/" runat="server" class="btn btn-primary px-4">
                    <i class="bi bi-house me-2"></i>Volver al inicio
                </a>
            </div>
        </div>
    </form>
    <script src="<%= ResolveUrl("~/Scripts/bootstrap.bundle.min.js") %>"></script>
</body>
</html>
