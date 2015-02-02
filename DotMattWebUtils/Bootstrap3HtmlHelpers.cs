using System;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Web.Mvc.Html;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace DotMattLibrary.Web
{
    /// <summary>
    /// A set of Razor Html Helpers for Bootstrap 3.
    /// By Matt Slay
    /// </summary>
    public static class Bootstrap3Helpers
    {

        //---------------------------------------------------------------------------------------------------------
        public static IHtmlString BootstrapEditorFor<TModel, TProperty>(
            this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression,
            string labelCaption = ""
        )
        {
            string control = helper.EditorFor<TModel, TProperty>(expression)
                                   .ToString();

            return BootstrapFieldBase(helper, expression, control, labelCaption);
        }

        //---------------------------------------------------------------------------------------------------------
        public static IHtmlString BootstrapTextBoxFor<TModel, TProperty>(
            this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression,
            string labelCaption = ""
        )
        {
            string control = helper.TextBoxFor<TModel, TProperty>(expression, new { @class = "form-control" })
                                   .ToString();

            return BootstrapFieldBase(helper, expression, control, labelCaption);
        }


        //---------------------------------------------------------------------------------------------------------
        public static IHtmlString BootstrapDropDownListFor<TModel, TProperty>(
            this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression,
            IEnumerable<SelectListItem> selectList,
            string labelCaption = ""
        )
        {
            string control = helper.DropDownListFor(expression, selectList, new { @class = "form-control" }).ToString();

            return BootstrapFieldBase(helper, expression, control, labelCaption);
        }

        //---------------------------------------------------------------------------------------------------------
        public static IHtmlString BootstrapFieldBase<TModel, TProperty>(
            this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression,
            string control = "",
            string labelCaption = ""
        )
        {
            if (String.IsNullOrEmpty(labelCaption))
                labelCaption = Parse(helper, expression).LabelCaption;

            string label = helper.LabelFor<TModel, TProperty>(expression, labelCaption).ToString();

            var sb = new StringBuilder();
            sb.AppendLine("<div class=\"form-group\">");
            sb.AppendLine(label);
            sb.AppendLine(control);
            sb.Append("</div>");

            return new HtmlString(sb.ToString());
        }

        //---------------------------------------------------------------------------------------------------------
        public static IHtmlString BootstrapSpanContainer(string html, int spanWidth)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("<div class=\"col-md-{0}\">", spanWidth.ToString());
            sb.AppendLine(html);
            sb.AppendLine("</div>");

            return new HtmlString(sb.ToString());
        }

        //---------------------------------------------------------------------------------------------------------
        public static IHtmlString BootstrapCheckBox<TModel, TProperty>(
            this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression,
            string labelCaption = null
        )
        {
            var propObj = Parse(helper, expression, labelCaption);
            string checkedString = propObj.PropertyValue.Equals("1") ? "checked = '1'" : "";

            var sb = new StringBuilder();
            sb.AppendLine("<div class='form-group'>");
            //sb.AppendLine("  <div class='controls'>");
            sb.AppendFormat("  <label for=\"{0}_{1}\">", propObj.ModelType, propObj.ModelProperty);
            sb.AppendFormat("    <input name=\"{0}[{1}]\" type=\"hidden\" value=\"{2}\" />", propObj.ModelType, propObj.ModelProperty, propObj.PropertyValue);
            sb.AppendFormat("    <input id=\"{0}_{1}\" name=\"{0}[{1}]\" type=\"checkbox\" value=\"{2}\" {3}/>", propObj.ModelType, propObj.ModelProperty, propObj.PropertyValue, checkedString);
            sb.AppendFormat("  {0}</label>", propObj.LabelCaption);
            //sb.AppendLine("  </div>");
            sb.AppendLine("</div>");

            return new HtmlString(sb.ToString());
        }

        //---------------------------------------------------------------------------------------------------------
        public static HelperProps Parse<TModel, TProperty>(
                this HtmlHelper<TModel> helper,
                Expression<Func<TModel, TProperty>> expression,
                string labelCaption = null)
        {
            // Learned these tricks from: http://blogs.planetcloud.co.uk/mygreatdiscovery/post/Creating-tooltips-using-data-annotations-in-ASPNET-MVC.aspx

            string modelType = (typeof(TModel)).Name;  // Read model type as a string
            string modelProperty = ExpressionHelper.GetExpressionText(expression); // Read property name as a string

            // These are here only to show how you can pluck out this info from the ViewContext...
            //var currentControllerName = (string)helper.ViewContext.RouteData.Values["controller"];
            //var currentActionName = (string)helper.ViewContext.RouteData.Values["action"];

            if (labelCaption == null) // If caption is not passed, look for one on the data annotations of the class. 
            {                         // If not specified there in DisplayName, then use the property name as the caption. 

                // Look for [Display(Name = "Display text:")] on the model annotations
                var metaData = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
                labelCaption = metaData.DisplayName;

                if (labelCaption == null) // If not present on model annotations, derive from passed model property.
                {
                    labelCaption = metaData.PropertyName ?? modelProperty;
                    if (labelCaption.Contains("."))
                    {
                        string[] words = Regex.Split(labelCaption, @"\W+");
                        labelCaption = words[words.Length - 1];
                    }
                    labelCaption = labelCaption.Replace('_', ' ');
                    labelCaption = char.ToUpper(labelCaption[0]) + labelCaption.Substring(1);
                }
            }

            // Grab model from view. See: http://stackoverflow.com/questions/12091942/custom-html-helper-with-linq-expression-in-mvc-3

            TModel model = (TModel)helper.ViewContext.ViewData.ModelMetadata.Model;
            // Invoke model property via expression
            TProperty propertyValue = expression.Compile().Invoke(model);

            HelperProps propObj = new HelperProps();
            propObj.ModelType = modelType;
            propObj.ModelProperty = modelProperty;
            propObj.LabelCaption = labelCaption;


            var type = (propertyValue != null) ? propertyValue.GetType().ToString() : "null";
            switch (type)
            {
                case "System.Boolean":
                    propObj.PropertyValue = propertyValue.ToString().Equals("True") ? "1" : "0";
                    break;
                //case "System.String":
                //    propObj.PropertyValue = propertyValue.ToString();
                //    break;
                case "null":
                    propObj.PropertyValue = "";
                    break;
                default:
                    propObj.PropertyValue = propertyValue.ToString();
                    break;
            }

            return propObj;
        }
    }


    public class HelperProps
    {
        public string ModelType { get; set; }
        public string ModelProperty { get; set; }
        public string LabelCaption { get; set; }
        public string PropertyValue { get; set; }
    }

    public static class ExtensionMethods
    {
    
        public static IHtmlString DivWrapper(this IHtmlString Content, string Classes)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<div class=\"" + Classes + "\">");
            sb.AppendLine(Content.ToString());
            sb.Append("</div>");

            return new HtmlString(sb.ToString());

        }

        public static IHtmlString PlaceHolderText(this IHtmlString Content, string PlaceHolderText)
        {
            // This content was rendered into the control in the BootstrapTextBoxFor() method so we could do a Replace on it here.
            //string placeHolderStub = "placeholder=\" \"";
            // This is the replacement text, which will have the placeholder text filled in.
            string placeholderHtml = String.Format("placeholder=\"{0}\"", PlaceHolderText);

            string html = Content.ToString().Replace("<input ", "<input " + placeholderHtml);

            var htmlstring = new HtmlString(html);
            return new HtmlString(html);

        }

    
    }
}