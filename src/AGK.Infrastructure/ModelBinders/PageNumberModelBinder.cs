using AGK.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AGK.Infrastructure.ModelBinders;
public class PageNumberModelBinder : IModelBinder
{
	public Task BindModelAsync(ModelBindingContext bindingContext) {
		var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

		if(valueProviderResult == ValueProviderResult.None) {
			bindingContext.Result = ModelBindingResult.Success(new PageNumber(1));
			return Task.CompletedTask;
		}

		if(int.TryParse(valueProviderResult.FirstValue, out var value) && value >= 1) {
			bindingContext.Result = ModelBindingResult.Success(new PageNumber(value));
		}
		else {
			bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid page number.");
			bindingContext.Result = ModelBindingResult.Failed();
		}

		return Task.CompletedTask;
	}
}
