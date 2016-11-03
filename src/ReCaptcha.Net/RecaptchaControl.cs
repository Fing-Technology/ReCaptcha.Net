using System;
using System.ComponentModel;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReCaptcha.Net
{
    [ToolboxData("<{0}:ReCaptchaControl runat=\"server\" />")]
    [Designer(typeof(Design.RecaptchaControlDesigner))]
    public class ReCaptchaControl : WebControl, IValidator
    {
        #region Private Fields

        private const string ReCaptchaHost = "https://www.google.com/recaptcha/api.js";
        private const string RecaptchaResponseField = "g-recaptcha-response";

        private ReCaptchaResponse _reCaptchaResponse;

        #endregion

        #region Public Properties

        [Category("Settings")]
        [Description("The site key from https://www.google.com/recaptcha/admin. Can also be set using ReCaptchaSiteKey in AppSettings.")]
        public string SiteKey { get; set; }

        [Category("Settings")]
        [Description("The secret key from https://www.google.com/recaptcha/admin. Can also be set using ReCaptchaSecretKey in AppSettings.")]
        public string SecretKey { get; set; }

        [Category("Appearence")]
        [DefaultValue("light")]
        [Description("The theme for the reCAPTCHA control. Currently supported values are light and dark")]
        public string Theme { get; set; }

        [Category("Settings")]
        [DefaultValue(false)]
        [Description("Set this to true to stop reCAPTCHA validation. Useful for testing platform. Can also be set using ReCaptchaSkipValidation in AppSettings")]
        public bool SkipReCaptchaValidation { get; set; }

        #endregion

        public ReCaptchaControl()
        {
            SiteKey = ConfigurationManager.AppSettings["ReCaptchaSiteKey"];
            SecretKey = ConfigurationManager.AppSettings["ReCaptchaSecretKey"];
            bool skipRecaptchaValidation;
            if (bool.TryParse(ConfigurationManager.AppSettings["ReCaptchaSkipValidation"], out skipRecaptchaValidation))
            {
                SkipReCaptchaValidation = skipRecaptchaValidation;
            }

        }

        #region Overriden Methods

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (string.IsNullOrEmpty(SiteKey) || string.IsNullOrEmpty(SecretKey)) {
                throw new ApplicationException("reCAPTCHA needs to be configured with a site & secret key.");
            }
            Page.Validators.Add(this);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (SkipReCaptchaValidation)
                writer.WriteLine("reCAPTCHA validation is skipped. Set SkipReCaptchaValidation property to false to enable validation.");
            else RenderContents(writer);
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            output.AddAttribute(HtmlTextWriterAttribute.Src, ReCaptchaHost);
            output.RenderBeginTag(HtmlTextWriterTag.Script);
            output.RenderEndTag();

            output.AddAttribute(HtmlTextWriterAttribute.Class, "g-recaptcha");
            output.AddAttribute("data-sitekey", SiteKey);
            if (!string.IsNullOrEmpty(Theme))
                output.AddAttribute("data-theme", Theme);
            output.RenderBeginTag(HtmlTextWriterTag.Div);
            output.RenderEndTag();

            output.RenderBeginTag(HtmlTextWriterTag.Noscript);
            output.Indent++;
            output.RenderBeginTag(HtmlTextWriterTag.Div);
            output.Indent++;
            output.AddAttribute(HtmlTextWriterAttribute.Style, "width: 302px; height: 422px; position: relative;");
            output.RenderBeginTag(HtmlTextWriterTag.Div);
            output.Indent++;
            output.AddAttribute(HtmlTextWriterAttribute.Style, "width: 302px; height: 422px; position: absolute;");
            output.RenderBeginTag(HtmlTextWriterTag.Div);
            output.Indent++;
            output.AddAttribute(HtmlTextWriterAttribute.Src, "https://www.google.com/recaptcha/api/fallback?k=" + SiteKey);
            output.AddAttribute("frameborder", "0");
            output.AddAttribute("scrolling", "no");
            output.AddAttribute(HtmlTextWriterAttribute.Style, "width: 302px; height:422px; border-style: none;");
            output.RenderBeginTag(HtmlTextWriterTag.Iframe);
            output.RenderEndTag();
            output.Indent--;
            output.RenderEndTag();
            output.Indent--;
            output.RenderEndTag();
            output.AddAttribute(HtmlTextWriterAttribute.Style, "width: 300px; height: 60px; border-style: none; bottom: 12px; left: 25px; margin: 0px; padding: 0px; right: 25px; background: #f9f9f9; border: 1px solid #c1c1c1; border-radius: 3px;");
            output.RenderBeginTag(HtmlTextWriterTag.Div);
            output.Indent++;
            output.AddAttribute(HtmlTextWriterAttribute.Id, "g-recaptcha-response");
            output.AddAttribute(HtmlTextWriterAttribute.Name, "g-recaptcha-response");
            output.AddAttribute(HtmlTextWriterAttribute.Class, "g-recaptcha-response");
            output.AddAttribute(HtmlTextWriterAttribute.Style, "width: 250px; height: 40px; border: 1px solid #c1c1c1; margin: 10px 25px; padding: 0px; resize: none;");
            output.RenderBeginTag(HtmlTextWriterTag.Textarea);
            output.RenderEndTag();
            output.Indent--;
            output.RenderEndTag();
            output.Indent--;
            output.RenderEndTag();
            output.Indent--;
            output.RenderEndTag();
        }

        #endregion

        #region IValidator Members


        [Localizable(true)]
        [DefaultValue("The verification words are incorrect.")]
        public string ErrorMessage { get; set; }

        [Browsable(false)]
        public bool IsValid
        {
            get { return _reCaptchaResponse != null && _reCaptchaResponse.Success;  }
            set { }
        }

        public void Validate()
        {
            if (SkipReCaptchaValidation) {
                _reCaptchaResponse = new ReCaptchaResponse { Success = true};
            } else {
                ReCaptchaValidator validator = new ReCaptchaValidator
                {
                    SecretKey = SecretKey,
                    Response = Context.Request.Form[RecaptchaResponseField],
                    RemoteIp = Page.Request.UserHostAddress
                };
                _reCaptchaResponse = validator.Validate();
            }
        }

        #endregion
    }
}