using FluentValidation;
using WebAGK.Module.Agents.Core.Entities;

namespace WebAGK.Module.Agents.Core.Validators;

public class AgentValidator : AbstractValidator<Agent> {
    public AgentValidator() {
        RuleFor(agent => agent.FirstName).NotEmpty();
        RuleFor(agent => (agent.PersonalId == null) && (agent.TaxId == null)).Equal(false);
    }
}