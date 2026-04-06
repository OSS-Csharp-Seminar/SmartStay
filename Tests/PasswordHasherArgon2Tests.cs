using FluentAssertions;
using SmartStay.Application.Util;

namespace Tests;

public class PasswordHasherArgon2Tests
{
    private readonly PasswordHasherArgon2 _hasher = new PasswordHasherArgon2();
    
    [Test]
    public void ShouldReturnNonEmptyString()
    {
        var result = _hasher.Hash("testpassword121234");
        
        result.Should().NotBeNullOrEmpty();
    }

    [Test]
    public void ShouldReturnDifferentStrings()
    {
        var result1 = _hasher.Hash("testpassword121234");
        var result2 = _hasher.Hash("testpassword121234");
        
        result1.Should().NotBe(result2);
    }

    [Theory]
    [TestCase("testpassword121234")]
    [TestCase("12234398")]
    [TestCase("1##$%)@)#(($2234namesurnamedog")]
    [TestCase("1#asdasdasdadsasdasdasdasdasdasdasdasd`#$%)@)#(($2234namesurnamedog")]
    public void ShouldReturnTrueForCorrectInput(string password)
    {
        var hashed= _hasher.Hash(password);
        var result = _hasher.Validate(password,hashed);

        result.Should().BeTrue();
    }
    
    [Test]
    public void ShouldReturnFalseForFalseInput()
    {
        var password= "testpassword121234";
        var hashed= _hasher.Hash(password);
        var result = _hasher.Validate(password,"faalse password124");

        result.Should().BeFalse();
    } 
}