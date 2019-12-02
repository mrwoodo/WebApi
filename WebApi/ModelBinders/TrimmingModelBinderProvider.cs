using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Logging;
using System;

namespace WebApi.ModelBinders
{
    public class TrimmingModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (!context.Metadata.IsComplexType && context.Metadata.ModelType == typeof(string))
            {
                var loggerFactory = (ILoggerFactory)context.Services.GetService(typeof(ILoggerFactory));

                return new TrimmingModelBinder(new SimpleTypeModelBinder(context.Metadata.ModelType, loggerFactory));
            }

            return null;
        }
    }
}
