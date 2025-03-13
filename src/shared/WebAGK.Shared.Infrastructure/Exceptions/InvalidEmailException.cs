using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Shared.Infrastructure.Exceptions;

public class InvalidEmailException(object email) : WebAgkException("Cannot set: {email} as email.", StatusCodes.Status400BadRequest) {
    public object EmailId { get; } = email;
}
