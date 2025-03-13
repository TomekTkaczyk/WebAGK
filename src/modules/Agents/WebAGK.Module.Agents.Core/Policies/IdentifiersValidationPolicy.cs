using WebAGK.Module.Agents.Core.Entities;
using WebAGK.Module.Agents.Core.Exceptions;

namespace WebAGK.Module.Agents.Core.Policies;

public class IdentifiersValidationPolicy {
	public static void Validate(Agent agent)
	{
		if(agent.IsCompany && agent.TaxId is null) {
			throw new InvalidCompanyTaxIdException();
		}

		if(agent.PersonalId is null && agent.TaxId is null) {
			throw new InvalidIdentifierException();
		}


	}
}