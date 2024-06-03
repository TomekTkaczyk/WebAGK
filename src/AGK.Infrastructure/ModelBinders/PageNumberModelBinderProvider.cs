using AGK.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace AGK.Infrastructure.ModelBinders;
public class PageNumberModelBinderProvider : IModelBinderProvider
{
	public IModelBinder GetBinder(ModelBinderProviderContext context) {
		if(context.Metadata.ModelType == typeof(PageNumber)) {
			return new BinderTypeModelBinder(typeof(PageNumberModelBinder));
		}
		return null;
	}
}
