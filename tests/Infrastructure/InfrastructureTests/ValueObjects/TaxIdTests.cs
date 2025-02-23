using WebAGK.Shared.Infrastructure.Exceptions;
using WebAGK.Shared.Infrastructure.ValueObject;

namespace InfrastructureTests.ValueObjects;

public class TaxIdTests {
    [Theory]
    [InlineData("6941297604")]
    [InlineData("PL6941297604")]
    public void Valid_TaxId_should_be_successful(string validTaxId) {
        TaxId _taxId = validTaxId;
        Assert.NotNull(_taxId);
        Assert.Equal(validTaxId, _taxId);
    }

    [Theory]
    [InlineData("6941297605")]
    [InlineData("PL6941297605")]
    [InlineData("DE6941297604")]
    public void Invalid_TaxId_should_throw_an_exception(string invalidTaxId) {

        Assert.Throws<InvalidTaxIdException>(() => {
            TaxId _taxId = invalidTaxId;
        });
    }
}