using WebAGK.Shared.Infrastructure.Exceptions;
using WebAGK.Shared.Infrastructure.ValueObject;

namespace InfrastructureTests.ValueObjects;

public class EmailTests {
    [Theory]
    [InlineData("email@address.com")]
    [InlineData("email@address.com.pl")]
    [InlineData("email@address.pl")]
    [InlineData("email.email@address.com")]
    [InlineData("email.email@address.com.pl")]
    [InlineData("email.email@address.pl")]
    public void Valid_EmailAddress_should_be_successful(string validEmailAddress) {
        Email _email = validEmailAddress;
        Assert.NotNull(_email);
        Assert.Equal(validEmailAddress, _email);
    }

    [Theory]
    [InlineData("email@address.c")]
    [InlineData("email@.com")]
    [InlineData("email@com")]
    [InlineData("email@")]
    [InlineData("email(at)address.com")]
    [InlineData("")]
    [InlineData(" ")]
    void Invalid_EmailAddress_should_throw_an_exception(string invalidPersonalId) {

        Assert.Throws<InvalidEmailException>(() => {
            Email _email = invalidPersonalId;
        });
    }
}