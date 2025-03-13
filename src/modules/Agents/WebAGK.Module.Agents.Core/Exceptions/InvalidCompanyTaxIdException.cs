using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Agents.Core.Exceptions;

public class InvalidCompanyTaxIdException() : WebAgkException(
    "The company requires a TaxId number.", 
    StatusCodes.Status400BadRequest);