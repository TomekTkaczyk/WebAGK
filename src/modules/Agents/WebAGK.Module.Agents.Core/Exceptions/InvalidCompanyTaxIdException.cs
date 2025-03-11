using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Agents.Core.Exceptions;

public class InvalidCompanyTaxIdException() : WebAGKException(
    "The company requires a TaxId number.", 
    StatusCodes.Status400BadRequest);