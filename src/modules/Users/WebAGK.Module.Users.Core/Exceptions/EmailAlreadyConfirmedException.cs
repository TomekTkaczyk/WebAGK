using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Users.Core.Exceptions;
internal class EmailAlreadyConfirmedException() : WebAGKException("EmailMessage is already confirmed.", StatusCodes.Status400BadRequest) { }
