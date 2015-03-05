using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Framesharp.Presentation.Web.Mvc.Models;

namespace Framesharp.Presentation.Web.Mvc.Extensions
{
    public static class EventListeners
    {
        public static MvcHtmlString EventListenerDropDownList(this HtmlHelper htmlHelper, string name, object listenedControlId, string eventCallbackUrl)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventListeningRequiredHtmlAttributes(new { }, listenedControlId, eventCallbackUrl);

            return htmlHelper.DropDownList(name, new SelectList(new List<object>()), controlHtmlAttributes);
        }
        public static MvcHtmlString EventListenerDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object listenedControlId, string eventCallbackUrl)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventListeningRequiredHtmlAttributes(new { }, listenedControlId, eventCallbackUrl);

            return htmlHelper.DropDownList(name, selectList, controlHtmlAttributes);
        }
        public static MvcHtmlString EventListenerDropDownList(this HtmlHelper htmlHelper, string name, string optionLabel, object listenedControlId, string eventCallbackUrl)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventListeningRequiredHtmlAttributes(new { }, listenedControlId, eventCallbackUrl);

            return htmlHelper.DropDownList(name, new SelectList(new List<object>()), optionLabel, controlHtmlAttributes);
        }
        public static MvcHtmlString EventListenerDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object listenedControlId, string eventCallbackUrl, IDictionary<string, object> htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventListeningRequiredHtmlAttributes(htmlAttributes, listenedControlId, eventCallbackUrl);

            return htmlHelper.DropDownList(name, selectList, controlHtmlAttributes);
        }
        public static MvcHtmlString EventListenerDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object listenedControlId, string eventCallbackUrl, object htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventListeningRequiredHtmlAttributes(htmlAttributes, listenedControlId, eventCallbackUrl);

            return htmlHelper.DropDownList(name, selectList, controlHtmlAttributes);
        }
        public static MvcHtmlString EventListenerDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel, object listenedControlId, string eventCallbackUrl)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventListeningRequiredHtmlAttributes(new { }, listenedControlId, eventCallbackUrl);

            return htmlHelper.DropDownList(name, selectList, controlHtmlAttributes);
        }
        public static MvcHtmlString EventListenerDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel, object listenedControlId, string eventCallbackUrl, IDictionary<string, object> htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventListeningRequiredHtmlAttributes(htmlAttributes, listenedControlId, eventCallbackUrl);

            return htmlHelper.DropDownList(name, selectList, controlHtmlAttributes);
        }
        public static MvcHtmlString EventListenerDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel, object listenedControlId, string eventCallbackUrl, object htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventListeningRequiredHtmlAttributes(htmlAttributes, listenedControlId, eventCallbackUrl);

            return htmlHelper.DropDownList(name, selectList, controlHtmlAttributes);
        }
        public static MvcHtmlString EventListenerDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object listenedControlId, string eventCallbackUrl)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventListeningRequiredHtmlAttributes(new { }, listenedControlId, eventCallbackUrl);

            return htmlHelper.DropDownListFor(expression, selectList, controlHtmlAttributes);
        }
        public static MvcHtmlString EventListenerDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object listenedControlId, string eventCallbackUrl, IDictionary<string, object> htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventListeningRequiredHtmlAttributes(htmlAttributes, listenedControlId, eventCallbackUrl);

            return htmlHelper.DropDownListFor(expression, selectList, controlHtmlAttributes);
        }
        public static MvcHtmlString EventListenerDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object listenedControlId, string eventCallbackUrl, object htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventListeningRequiredHtmlAttributes(htmlAttributes, listenedControlId, eventCallbackUrl);

            return htmlHelper.DropDownListFor(expression, selectList, controlHtmlAttributes);
        }
        public static MvcHtmlString EventListenerDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object listenedControlId, string eventCallbackUrl)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventListeningRequiredHtmlAttributes(new { }, listenedControlId, eventCallbackUrl);

            return htmlHelper.DropDownListFor(expression, selectList, optionLabel, controlHtmlAttributes);
        }
        public static MvcHtmlString EventListenerDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object listenedControlId, string eventCallbackUrl, IDictionary<string, object> htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventListeningRequiredHtmlAttributes(htmlAttributes, listenedControlId, eventCallbackUrl);

            return htmlHelper.DropDownListFor(expression, selectList, optionLabel, controlHtmlAttributes);
        }
        public static MvcHtmlString EventListenerDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object listenedControlId, string eventCallbackUrl, object htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventListeningRequiredHtmlAttributes(htmlAttributes, listenedControlId, eventCallbackUrl);

            return htmlHelper.DropDownListFor(expression, selectList, optionLabel, controlHtmlAttributes);
        }

        public static MvcHtmlString EventListenerCheckBox(this HtmlHelper htmlHelper, string name, object listenedControlId, string eventCallbackUrl)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventListeningRequiredHtmlAttributes(new { }, listenedControlId, eventCallbackUrl);

            return htmlHelper.CheckBox(name, controlHtmlAttributes);
        }
        public static MvcHtmlString EventListenerCheckBox(this HtmlHelper htmlHelper, string name, bool isChecked, object listenedControlId, string eventCallbackUrl)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventListeningRequiredHtmlAttributes(new { }, listenedControlId, eventCallbackUrl);

            return htmlHelper.CheckBox(name, isChecked, controlHtmlAttributes);
        }
        public static MvcHtmlString EventListenerCheckBox(this HtmlHelper htmlHelper, string name, object listenedControlId, string eventCallbackUrl, IDictionary<string, object> htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventListeningRequiredHtmlAttributes(htmlAttributes, listenedControlId, eventCallbackUrl);

            return htmlHelper.CheckBox(name, controlHtmlAttributes);
        }
        public static MvcHtmlString EventListenerCheckBox(this HtmlHelper htmlHelper, string name, object listenedControlId, string eventCallbackUrl, object htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventListeningRequiredHtmlAttributes(htmlAttributes, listenedControlId, eventCallbackUrl);

            return htmlHelper.CheckBox(name, controlHtmlAttributes);
        }
        public static MvcHtmlString EventListenerCheckBox(this HtmlHelper htmlHelper, string name, bool isChecked, object listenedControlId, string eventCallbackUrl, IDictionary<string, object> htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventListeningRequiredHtmlAttributes(htmlAttributes, listenedControlId, eventCallbackUrl);

            return htmlHelper.CheckBox(name, isChecked, controlHtmlAttributes);
        }
        public static MvcHtmlString EventListenerCheckBox(this HtmlHelper htmlHelper, string name, bool isChecked, object listenedControlId, string eventCallbackUrl, object htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventListeningRequiredHtmlAttributes(htmlAttributes, listenedControlId, eventCallbackUrl);

            return htmlHelper.CheckBox(name, isChecked, controlHtmlAttributes);
        }
        public static MvcHtmlString EventListenerCheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, object listenedControlId, string eventCallbackUrl)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventListeningRequiredHtmlAttributes(new { }, listenedControlId, eventCallbackUrl);

            return htmlHelper.CheckBoxFor(expression, controlHtmlAttributes);
        }
        public static MvcHtmlString EventListenerCheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, object listenedControlId, string eventCallbackUrl, object htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventListeningRequiredHtmlAttributes(htmlAttributes, listenedControlId, eventCallbackUrl);

            return htmlHelper.CheckBoxFor(expression, controlHtmlAttributes);
        }
        public static MvcHtmlString EventListenerCheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, object listenedControlId, string eventCallbackUrl, IDictionary<string, object> htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventListeningRequiredHtmlAttributes(htmlAttributes, listenedControlId, eventCallbackUrl);

            return htmlHelper.CheckBoxFor(expression, controlHtmlAttributes);
        }

        private static IDictionary<string, object> IncludeEventListeningRequiredHtmlAttributes(object htmlAttributes, object listenedControlId, string eventCallbackUrl)
        {
            IDictionary<string, object> controlHtmlAttributes = new Dictionary<string, object>();

            foreach (PropertyInfo attribute in htmlAttributes.GetType().GetProperties())
                controlHtmlAttributes.Add(attribute.Name.Replace('_', '-'), attribute.GetValue(htmlAttributes, null));

            controlHtmlAttributes.Add("data-event-listener", string.Empty);
            controlHtmlAttributes.Add("data-listened-control-id", listenedControlId.ToString());
            controlHtmlAttributes.Add("data-event-callback-url", eventCallbackUrl);

            return controlHtmlAttributes;
        }
        private static IDictionary<string, object> IncludeEventListeningRequiredHtmlAttributes(IDictionary<string, object> htmlAttributes, object listenedControlId, string eventCallbackUrl)
        {
            IDictionary<string, object> controlHtmlAttributes = new Dictionary<string, object>();

            foreach (var attribute in htmlAttributes)
                controlHtmlAttributes.Add(attribute.Key.Replace('_', '-'), attribute.Value);

            controlHtmlAttributes.Add("data-event-listener", string.Empty);
            controlHtmlAttributes.Add("data-listened-control-id", listenedControlId.ToString());
            controlHtmlAttributes.Add("data-event-callback-url", eventCallbackUrl);

            return controlHtmlAttributes;
        }
    }

    public static class EventTriggers
    {
        public static MvcHtmlString EventTriggerDropDownList(this HtmlHelper htmlHelper, string name, object targetControlId, string eventCallbackUrl)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventTriggeringRequiredHtmlAttributes(new { }, targetControlId, eventCallbackUrl);

            return htmlHelper.DropDownList(name, new SelectList(new List<object>()), controlHtmlAttributes);
        }
        public static MvcHtmlString EventTriggerDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object targetControlId, string eventCallbackUrl)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventTriggeringRequiredHtmlAttributes(new { }, targetControlId, eventCallbackUrl);

            return htmlHelper.DropDownList(name, selectList, controlHtmlAttributes);
        }
        public static MvcHtmlString EventTriggerDropDownList(this HtmlHelper htmlHelper, string name, string optionLabel, object targetControlId, string eventCallbackUrl)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventTriggeringRequiredHtmlAttributes(new { }, targetControlId, eventCallbackUrl);

            return htmlHelper.DropDownList(name, new SelectList(new List<object>()), optionLabel, controlHtmlAttributes);
        }
        public static MvcHtmlString EventTriggerDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object targetControlId, string eventCallbackUrl, IDictionary<string, object> htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventTriggeringRequiredHtmlAttributes(htmlAttributes, targetControlId, eventCallbackUrl);

            return htmlHelper.DropDownList(name, selectList, controlHtmlAttributes);
        }
        public static MvcHtmlString EventTriggerDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object targetControlId, string eventCallbackUrl, object htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventTriggeringRequiredHtmlAttributes(htmlAttributes, targetControlId, eventCallbackUrl);

            return htmlHelper.DropDownList(name, selectList, controlHtmlAttributes);
        }
        public static MvcHtmlString EventTriggerDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel, object targetControlId, string eventCallbackUrl)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventTriggeringRequiredHtmlAttributes(new { }, targetControlId, eventCallbackUrl);

            return htmlHelper.DropDownList(name, selectList, controlHtmlAttributes);
        }
        public static MvcHtmlString EventTriggerDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel, object targetControlId, string eventCallbackUrl, IDictionary<string, object> htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventTriggeringRequiredHtmlAttributes(htmlAttributes, targetControlId, eventCallbackUrl);

            return htmlHelper.DropDownList(name, selectList, controlHtmlAttributes);
        }
        public static MvcHtmlString EventTriggerDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel, object targetControlId, string eventCallbackUrl, object htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventTriggeringRequiredHtmlAttributes(htmlAttributes, targetControlId, eventCallbackUrl);

            return htmlHelper.DropDownList(name, selectList, controlHtmlAttributes);
        }
        public static MvcHtmlString EventTriggerDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object targetControlId, string eventCallbackUrl)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventTriggeringRequiredHtmlAttributes(new { }, targetControlId, eventCallbackUrl);

            return htmlHelper.DropDownListFor(expression, selectList, controlHtmlAttributes);
        }
        public static MvcHtmlString EventTriggerDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object targetControlId, string eventCallbackUrl, IDictionary<string, object> htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventTriggeringRequiredHtmlAttributes(htmlAttributes, targetControlId, eventCallbackUrl);

            return htmlHelper.DropDownListFor(expression, selectList, controlHtmlAttributes);
        }
        public static MvcHtmlString EventTriggerDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object targetControlId, string eventCallbackUrl, object htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventTriggeringRequiredHtmlAttributes(htmlAttributes, targetControlId, eventCallbackUrl);

            return htmlHelper.DropDownListFor(expression, selectList, controlHtmlAttributes);
        }
        public static MvcHtmlString EventTriggerDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object targetControlId, string eventCallbackUrl)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventTriggeringRequiredHtmlAttributes(new { }, targetControlId, eventCallbackUrl);

            return htmlHelper.DropDownListFor(expression, selectList, optionLabel, controlHtmlAttributes);
        }
        public static MvcHtmlString EventTriggerDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object targetControlId, string eventCallbackUrl, IDictionary<string, object> htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventTriggeringRequiredHtmlAttributes(htmlAttributes, targetControlId, eventCallbackUrl);

            return htmlHelper.DropDownListFor(expression, selectList, optionLabel, controlHtmlAttributes);
        }
        public static MvcHtmlString EventTriggerDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object targetControlId, string eventCallbackUrl, object htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventTriggeringRequiredHtmlAttributes(htmlAttributes, targetControlId, eventCallbackUrl);

            return htmlHelper.DropDownListFor(expression, selectList, optionLabel, controlHtmlAttributes);
        }

        public static MvcHtmlString EventTriggerCheckBox(this HtmlHelper htmlHelper, string name, object targetControlId, string eventCallbackUrl)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventTriggeringRequiredHtmlAttributes(new { }, targetControlId, eventCallbackUrl);

            return htmlHelper.CheckBox(name, controlHtmlAttributes);
        }
        public static MvcHtmlString EventTriggerCheckBox(this HtmlHelper htmlHelper, string name, bool isChecked, object targetControlId, string eventCallbackUrl)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventTriggeringRequiredHtmlAttributes(new { }, targetControlId, eventCallbackUrl);

            return htmlHelper.CheckBox(name, isChecked, controlHtmlAttributes);
        }
        public static MvcHtmlString EventTriggerCheckBox(this HtmlHelper htmlHelper, string name, object targetControlId, string eventCallbackUrl, IDictionary<string, object> htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventTriggeringRequiredHtmlAttributes(htmlAttributes, targetControlId, eventCallbackUrl);

            return htmlHelper.CheckBox(name, controlHtmlAttributes);
        }
        public static MvcHtmlString EventTriggerCheckBox(this HtmlHelper htmlHelper, string name, object targetControlId, string eventCallbackUrl, object htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventTriggeringRequiredHtmlAttributes(htmlAttributes, targetControlId, eventCallbackUrl);

            return htmlHelper.CheckBox(name, controlHtmlAttributes);
        }
        public static MvcHtmlString EventTriggerCheckBox(this HtmlHelper htmlHelper, string name, bool isChecked, object targetControlId, string eventCallbackUrl, IDictionary<string, object> htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventTriggeringRequiredHtmlAttributes(htmlAttributes, targetControlId, eventCallbackUrl);

            return htmlHelper.CheckBox(name, isChecked, controlHtmlAttributes);
        }
        public static MvcHtmlString EventTriggerCheckBox(this HtmlHelper htmlHelper, string name, bool isChecked, object targetControlId, string eventCallbackUrl, object htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventTriggeringRequiredHtmlAttributes(htmlAttributes, targetControlId, eventCallbackUrl);

            return htmlHelper.CheckBox(name, isChecked, controlHtmlAttributes);
        }
        public static MvcHtmlString EventTriggerCheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, object targetControlId, string eventCallbackUrl)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventTriggeringRequiredHtmlAttributes(new { }, targetControlId, eventCallbackUrl);

            return htmlHelper.CheckBoxFor(expression, controlHtmlAttributes);
        }
        public static MvcHtmlString EventTriggerCheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, object targetControlId, string eventCallbackUrl, object htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventTriggeringRequiredHtmlAttributes(htmlAttributes, targetControlId, eventCallbackUrl);

            return htmlHelper.CheckBoxFor(expression, controlHtmlAttributes);
        }
        public static MvcHtmlString EventTriggerCheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, object targetControlId, string eventCallbackUrl, IDictionary<string, object> htmlAttributes)
        {
            IDictionary<string, object> controlHtmlAttributes = IncludeEventTriggeringRequiredHtmlAttributes(htmlAttributes, targetControlId, eventCallbackUrl);

            return htmlHelper.CheckBoxFor(expression, controlHtmlAttributes);
        }

        private static IDictionary<string, object> IncludeEventTriggeringRequiredHtmlAttributes(object htmlAttributes, object targetControlId, string eventCallbackUrl)
        {
            IDictionary<string, object> controlHtmlAttributes = new Dictionary<string, object>();

            foreach (PropertyInfo attribute in htmlAttributes.GetType().GetProperties())
                controlHtmlAttributes.Add(attribute.Name.Replace('_', '-'), attribute.GetValue(htmlAttributes, null));

            controlHtmlAttributes.Add("data-event-trigger", string.Empty);
            controlHtmlAttributes.Add("data-target-control-id", targetControlId.ToString());
            controlHtmlAttributes.Add("data-event-callback-url", eventCallbackUrl);

            return controlHtmlAttributes;
        }
    }

    public static class HtmlExtensions
    {
        /// <summary>
        /// Renders a placeholder container where the operations notifications will be exhibited
        /// </summary>
        /// <param name="helper">The extended html helper instance</param>
        /// <returns>An HTML string containing the markup for the notification placeholder container</returns>
        public static IHtmlString NotificationArea(this HtmlHelper helper)
        {
            string wrapperContainer = "<div class='alert {0}' style='display: {1}' data-notification>";
            string containerIcon = "<i class='fa {0}' style='margin-right: 20px'></i>";
            string closeButton = "<button type='button' class='close' data-close aria-hidden='true'>×</button>";
            string messageContainer = "<span data-message>{0}</span>";

            string message = HttpContext.Current.Response.Headers["OperationResultMessage"];

            // Verifies whether any message should be displayed or just the hidden placeholder
            if (message != null)
            {
                // Verifies if the operation has failed or succeeded
                bool succeededOperation = HttpContext.Current.Response.Headers["OperationResultStatus"] == OperationStatus.Succeeded.ToString();

                // Appends the message to the notification container
                messageContainer = string.Format(messageContainer, message);
                // Inserts the correct css class to the container icon based on the outcome of the operation (success/failure)
                containerIcon = string.Format(containerIcon, succeededOperation ? "fa-check" : "fa-ban");
                // Inserts the correct css class to the wrapper container of the notification based on the outcome of the operation (success/failure)
                wrapperContainer = string.Format(wrapperContainer, succeededOperation ? "alert-success" : "alert-danger", "block");
            }
                // If there is no messages to be displayed at the moment, sets the display of the container to 'none', to keep it hidden
            else
            {
                wrapperContainer = string.Format(wrapperContainer, string.Empty, "none");
                containerIcon = string.Format(containerIcon, string.Empty);
            }

            // builds the full HTML markup for the notification container
            string notificationContainer = string.Format("{0}{1}{2}{3}{4}", wrapperContainer, containerIcon, closeButton, messageContainer, "</div>");
            
            return new HtmlString(notificationContainer);
        }
    }

    public static class HtmlCollectionExtensions
    {
        /// <summary>
        /// Gets the displayname of a property for a given model type
        /// </summary>
        /// <param name="helper">Html helper instance</param>
        /// <param name="modelType">Type of the model</param>
        /// <param name="propertyName">Property whose display information is to be shown</param>
        /// <returns></returns>
        public static string DisplayNameForType<TModel>(this HtmlHelper<TModel> helper, System.Type modelType, string propertyName)
        {
            PropertyInfo property = modelType.GetProperty(propertyName);

            if (property != null)
            {
                object[] attributes = property.GetCustomAttributes(typeof (DisplayAttribute), false);

                if (attributes.Length > 0 && attributes[0] is DisplayAttribute)
                {
                    return (attributes[0] as DisplayAttribute).Name;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the underlying property name for the model. 
        /// </summary>
        /// <typeparam name="TModel">Model type</typeparam>
        /// <param name="helper">HtmlHelper instance</param>
        /// <returns></returns>
        public static IHtmlString PropertyNameForModel<TModel>(this HtmlHelper<TModel> helper)
        {
            return new HtmlString(helper.ViewData.ModelMetadata.PropertyName);
        }

        /// <summary>
        /// Gets the underlying property name for the expression. 
        /// </summary>
        /// <typeparam name="TModel">Model type</typeparam>
        /// <typeparam name="TValue">Expresion</typeparam>
        /// <param name="helper">HtmlHelper instance</param>
        /// <param name="expression">Model binding expression</param>
        /// <returns></returns>
        public static IHtmlString PropertyNameFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            return new HtmlString(ExpressionHelper.GetExpressionText(expression));
        }

        /// <summary>
        /// Displays the short name specified at the Display Attribute, if defined. 
        /// Else it shows the Name or the PropertyName in the last case.
        /// </summary>
        /// <typeparam name="TModel">Tipo do model</typeparam>
        /// <param name="helper">HtmlHelper</param>
        /// <returns></returns>
        public static IHtmlString ShortLabelForModel<TModel>(this HtmlHelper<TModel> helper)
        {
            string fieldIdentity =
                helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(null);

            return CreateLabel(fieldIdentity, helper.ViewContext.ViewData.ModelMetadata);
        }

        /// <summary>
        /// Exibe o nome curto especificado no atributo Display da model
        /// </summary>
        /// <typeparam name="TModel">Tipo do model</typeparam>
        /// <typeparam name="TValue">Valor</typeparam>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="expression">Expressão lambda</param>
        /// <returns></returns>
        public static IHtmlString ShortLabelFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            string fieldIdentity =
                helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(
                ExpressionHelper.GetExpressionText(expression));

            return CreateLabel(fieldIdentity, helper.ViewContext.ViewData.ModelMetadata);
        }

        /// <summary>
        /// Exibe o nome curto especificado no atributo Display da model
        /// </summary>
        /// <typeparam name="TModel">Tipo do model</typeparam>
        /// <param name="helper">HtmlHelper</param>
        /// <returns></returns>
        public static IHtmlString ShortNameForModel<TModel>(this HtmlHelper<TModel> helper)
        {
            ModelMetadata metadata = helper.ViewContext.ViewData.ModelMetadata;

            return new HtmlString(GetShortestDisplayName(metadata));
        }

        /// <summary>
        /// Exibe o nome curto especificado no atributo Display da model
        /// </summary>
        /// <typeparam name="TModel">Tipo do model</typeparam>
        /// <typeparam name="TValue">Valor</typeparam>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="expression">Expressão lambda</param>
        /// <returns></returns>
        public static IHtmlString ShortNameFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            return new HtmlString(GetShortestDisplayName(metadata));
        }

        /// <summary>
        /// Creates an identification scope for rendering controls contained in a collection
        /// </summary>
        /// <typeparam name="TModel">Type of the model to be used</typeparam>
        /// <param name="helper">Html helper instance</param>
        /// <returns></returns>
        public static IDisposable CreateIdentityPrefixScopeForModel<TModel>(this HtmlHelper<TModel> helper)
        {
            // Gets the complete path to describe the class hierarchy in the controls identity like id='Parent_Child_ChildChild_TheCollection_This'
            string fieldId = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(null);

            // Sets a htmlprefixscope for inserting the complete path to the collection before any controls name
            return new HtmlFieldPrefixScope(helper.ViewContext.ViewData.TemplateInfo,
                                            string.Format("{0}", fieldId));
        }

        /// <summary>
        /// Creates an identification scope for rendering controls contained in a collection
        /// </summary>
        /// <typeparam name="TModel">Type of the model to be used</typeparam>
        /// <typeparam name="TValue">Expression type</typeparam>
        /// <param name="helper">Html helper instance</param>
        /// <param name="expression">Lambda expression</param>
        /// <returns></returns>
        public static IDisposable CreateIdentityPrefixScopeFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            // Gets the complete path to describe the class hierarchy in the controls identity like id='Parent_Child_ChildChild_TheCollection_This'
            string fieldId = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));

            // Sets a htmlprefixscope for inserting the complete path to the collection before any controls name
            return new HtmlFieldPrefixScope(helper.ViewContext.ViewData.TemplateInfo,
                                            string.Format("{0}", fieldId));
        }

        /// <summary>
        /// Begins a collection item scope for naming the editor fields
        /// </summary>
        /// <typeparam name="TModel">Type of the model to be used</typeparam>
        /// <param name="helper">Html helper instance</param>
        /// <returns></returns>
        public static IDisposable BeginCollectionItem<TModel>(this HtmlHelper<TModel> helper)
        {
            return BeginCollectionItem(helper, null);
        }

        /// <summary>
        /// Begins a collection item scope for naming the editor fields
        /// </summary>
        /// <typeparam name="TModel">Type of the model to be used</typeparam>
        /// <param name="helper">Html helper instance</param>
        /// <param name="htmlPrefixScopeIdentity">Identity prefix for contained controls</param>
        /// <returns></returns>
        public static IDisposable BeginCollectionItem<TModel>(this HtmlHelper<TModel> helper, string htmlPrefixScopeIdentity)
        {
            htmlPrefixScopeIdentity = htmlPrefixScopeIdentity ?? helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(null);

            string fieldId = ParseFieldIdFromHtmlPrefixScopeIdentity(htmlPrefixScopeIdentity);
            string fieldName = ParseFieldNameFromHtmlPrefixScopeIdentity(htmlPrefixScopeIdentity);

            Guid scopeIdentity = ((ModelBase)helper.ViewContext.ViewData.Model).ModelIdentity;

            helper.ViewContext.Writer.WriteLine(CreateHiddenInput(string.Format("{0}_{1}__ModelIdentity", fieldId, scopeIdentity), string.Format("{0}[{1}].ModelIdentity", fieldName, scopeIdentity.ToString()), scopeIdentity.ToString()));
            helper.ViewContext.Writer.WriteLine(CreateHiddenInput(null, string.Format("{0}.index", fieldName), scopeIdentity.ToString()));

            return new HtmlFieldPrefixScope(helper.ViewContext.ViewData.TemplateInfo, string.Format("{0}[{1}]", fieldName, scopeIdentity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlPrefixScopeIdentity">Identity prefix for contained controls</param>
        /// <returns></returns>
        private static string ParseFieldIdFromHtmlPrefixScopeIdentity(string htmlPrefixScopeIdentity)
        {
            return ExpressionHelper.GetExpressionText(htmlPrefixScopeIdentity).Replace("]", "_").Replace("[", "_").Replace(".", "_");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlPrefixScopeIdentity">Identity prefix for contained controls</param>
        /// <returns></returns>
        private static string ParseFieldNameFromHtmlPrefixScopeIdentity(string htmlPrefixScopeIdentity)
        {
            return htmlPrefixScopeIdentity.Replace("List_", "List[").Replace("_", ".").Replace("..", "].");
        }

        /// <summary>
        /// Define um escopo de nomenclatura para os controles que forem renderizados
        ///  dentro do using
        /// </summary>
        private class HtmlFieldPrefixScope : IDisposable
        {
            private readonly TemplateInfo _templateInfo;
            private readonly string _previousHtmlFieldPrefix;

            public HtmlFieldPrefixScope(TemplateInfo templateInfo, string htmlFieldPrefix)
            {
                _templateInfo = templateInfo;

                _previousHtmlFieldPrefix = templateInfo.HtmlFieldPrefix;
                templateInfo.HtmlFieldPrefix = htmlFieldPrefix;
            }

            public void Dispose()
            {
                _templateInfo.HtmlFieldPrefix = _previousHtmlFieldPrefix;
            }
        }

        /// <summary>
        /// Creates a tagbuilder for building the label control
        /// </summary>
        /// <param name="fieldId">Identity of the control that this label names</param>
        /// <param name="metadata">Molde metadata</param>
        /// <returns></returns>
        private static HtmlString CreateLabel(string fieldId, ModelMetadata metadata)
        {
            // Using string builder to build this control instead of tagbuilder because of backwards compatibily issues
            StringBuilder builder = new StringBuilder("<label ");

            builder.AppendFormat(@"for='{0}'>", fieldId);

            builder.Append(GetShortestDisplayName(metadata));

            builder.Append("</label>");

            return new HtmlString(builder.ToString());
        }

        /// <summary>
        /// Returns the shortest possible name for a model. 
        /// </summary>
        /// <param name="metadata">Model metadata</param>
        /// <returns></returns>
        private static string GetShortestDisplayName(ModelMetadata metadata)
        {
            return
                // Se houver um short name configurado, exibe
                metadata.ShortDisplayName ??
                // Caso contrário exibe o display name
                metadata.DisplayName ??
                // ou o Property name
                metadata.PropertyName;
        }

        /// <summary>
        /// Creates a html string for building the hidden textbox control
        /// </summary>
        /// <param name="fieldId">Identity of the control</param>
        /// <param name="fieldName">Name of the control</param>
        /// <param name="value">Molde metadata</param>
        /// <returns></returns>
        private static HtmlString CreateHiddenInput(string fieldId, string fieldName, string value)
        {
            // Using string builder to build this control instead of tagbuilder because of backwards compatibily issues
            StringBuilder builder = new StringBuilder("<input ");

            if (!String.IsNullOrEmpty(fieldId))
                builder.AppendFormat(@"id='{0}' ", fieldId);

            builder.AppendFormat(@"type='hidden' ");
            builder.AppendFormat(@"name='{0}'", fieldName);
            builder.AppendFormat(@"autocomplete='off' ");
            builder.AppendFormat(@"value='{0}' ", HttpUtility.HtmlEncode(value));

            builder.Append(" />");

            return new HtmlString(builder.ToString());
        }
    }
}