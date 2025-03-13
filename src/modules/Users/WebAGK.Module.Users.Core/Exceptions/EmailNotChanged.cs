using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Users.Core.Exceptions;
internal class EmailNotChanged() : WebAgkException(
	"EmailMessage has not been changed.", StatusCodes.Status400BadRequest) { }
