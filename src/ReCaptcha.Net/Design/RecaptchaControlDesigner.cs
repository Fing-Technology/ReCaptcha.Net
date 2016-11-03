using System.ComponentModel.Design;
using System.Web.UI.Design;

namespace ReCaptcha.Net.Design
{
    public class RecaptchaControlDesigner : ControlDesigner
    {
        public override string GetDesignTimeHtml()
        {
            return CreatePlaceHolderDesignTimeHtml("reCAPTCHA Validator");
        }

        public override bool AllowResize
        {
            get
            {
                return false;
            }
        }

        // Return a custom ActionList collection
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                DesignerActionListCollection actionLists = new DesignerActionListCollection();
                actionLists.AddRange(base.ActionLists);

                // Add a custom DesignerActionList
                actionLists.Add(new ActionList(this));
                return actionLists;
            }
        }

        public class ActionList : DesignerActionList
        {
            // Constructor
            public ActionList(RecaptchaControlDesigner parent) : base(parent.Component)
            {
            }

            // Create the ActionItem collection and add one command
            public override DesignerActionItemCollection GetSortedActionItems()
            {
                //TODO: I can't get this to open up automatically (
                DesignerActionItemCollection items = new DesignerActionItemCollection
                {
                    new DesignerActionHeaderItem("API Key"),
                    new DesignerActionTextItem("To use reCAPTCHA, you need an API key from https://www.google.com/recaptcha/admin", "")
                };

                return items;
            }
        }
    }
}
