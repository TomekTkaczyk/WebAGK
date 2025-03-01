using WebAGK.Shared.Infrastructure.Exceptions;
using WebAGK.Shared.Infrastructure.ValueObjects;
using TaxId = WebAGK.Shared.Infrastructure.ValueObjects.TaxId;

namespace InfrastructureTests.ValueObjects;

public class PersonalIdTests {
    [Theory]
    [InlineData("68102910014")]
    public void Valid_PersonaId_should_be_successful(string validPersonalId) {
        PersonalId _personalId = validPersonalId;
        Assert.NotNull(_personalId);
        Assert.Equal(validPersonalId, _personalId);
    }

    [Theory]
    [InlineData("68102910015")]
    public void Invalid_PersonalId_should_throw_an_exception(string invalidPersonalId) {

        Assert.Throws<InvalidTaxIdException>(() => {
            TaxId _taxId = invalidPersonalId;
        });
    }
}