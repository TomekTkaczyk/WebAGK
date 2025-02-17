using Microsoft.AspNetCore.Mvc;

namespace WebAGK.Shared.Infrastructure.Api;
public class ProducesDefaultContentTypeAttribute(params string[] additionalContentTypes) 
	: ProducesAttribute("application/json", additionalContentTypes)
{
}
