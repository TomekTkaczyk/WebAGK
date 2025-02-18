using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Users.Core.Exceptions;
internal class EmailNotChanged() : WebAGKException(
	"EmailMessage has not been changed.", StatusCodes.Status400BadRequest) { }
