<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register TagPrefix="ReCaptcha" Namespace="ReCaptcha.Net" Assembly="ReCaptcha.Net" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblResult" runat="server" />
        <ReCaptcha:ReCaptchaControl id="reCaptchaControl" runat="server"/>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
    </div>
    </form>
</body>
</html>
