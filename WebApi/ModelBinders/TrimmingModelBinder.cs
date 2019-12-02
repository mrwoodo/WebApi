using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace WebApi.ModelBinders
{
    //https://vikutech.blogspot.com.au/2018/02/trim-text-in-mvc-core-through-model-binder.html

    public class TrimmingModelBinder : IModelBinder
    {
        private readonly IModelBinder FallbackBinder;

        public TrimmingModelBinder(IModelBinder fallbackBinder)
        {
            FallbackBinder = fallbackBinder ?? throw new ArgumentNullException(nameof(fallbackBinder));
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult != null && valueProviderResult.FirstValue is string str && !string.IsNullOrEmpty(str))
            {
                //Strip out xml tags, prevent XPath / XML injection attacks
                if ((str.Contains("<")) || (str.Contains(">")))
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid character");

                //Trim off spaces
                str = str.Trim();

                bindingContext.Result = ModelBindingResult.Success(str);
                return Task.CompletedTask;
            }

            return FallbackBinder.BindModelAsync(bindingContext);
        }
    }
}
