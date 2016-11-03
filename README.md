# ReCaptcha.Net ASP.NET Plugin

[![Build Status](https://travis-ci.org/janssenr/ReCaptcha.Net.svg?branch=master)](https://travis-ci.org/janssenr/ReCaptcha.Net)

## Description

The reCAPTCHA ASP.NET Library provides a simple way to place a [CAPTCHA](http://www.google.com/recaptcha/) on your ASP.NET website, helping you stop bots from abusing it. The library wraps the [reCAPTCHA API](https://developers.google.com/recaptcha/intro). You can use the library from any .NET language including C# and Visual Basic .NET.

## Usage

After you've signed up for your API keys, below are basic instructions for installing reCAPTCHA on your site with ASP.NET:

Add a reference on your website to ReCaptcha.Net.dll: On the Visual Studio Website menu, choose Add Reference and then click the .NET tab in the dialog box. Select the ReCaptcha.Net.dll component from the list of .NET components and then click OK. If you don't see the component, click the Browse tab and look for the assembly file on your hard drive.
Insert the reCAPTCHA control into the form you wish to protect by adding the following code snippets:

At the top of the aspx page, insert this:

```
<%@ Register TagPrefix="ReCaptcha" Namespace="ReCaptcha.Net" Assembly="ReCaptcha.Net" %>
```

Then insert the reCAPTCHA control inside of the <<form runat="server">> tag:

```
<ReCaptcha:ReCaptchaControl id="reCaptchaControl" runat="server" SiteKey="your_site_key" ServerKey="your_server_key" />
```

You will need to substitute your site and server key into SiteKey and ServerKey respectively.
Make sure you use ASP.NET validation to validate your form (you should check Page.IsValid on submission).

The following is a "Hello World" with reCAPTCHA.

```
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
        <ReCaptcha:ReCaptchaControl id="reCaptchaControl" runat="server" SiteKey="your_site_key" ServerKey="your_server_key" />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
    </div>
    </form>
</body>
</html>
```