<%@ Page Title="Acerca del Proyecto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="TPC_GRUPO_2A.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %></h2>

        <h3>Sistema de Gestión de Reclamos para Call Center</h3>

        <p>
            CallCenter es una aplicación web desarrollada para la administración de clientes
            y la gestión integral de reclamos e incidencias.
        </p>

        <p>
            El sistema permite registrar clientes, crear reclamos, asignarlos a usuarios,
            realizar su seguimiento mediante distintos estados y administrar la información
            según perfiles de acceso específicos: Administrador, Supervisor y Telefonista.
        </p>
    </main>
</asp:Content>