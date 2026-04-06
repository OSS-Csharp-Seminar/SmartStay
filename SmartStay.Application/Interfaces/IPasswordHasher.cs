namespace SmartStay.Application.Interfaces;

public interface IPasswordHasher<T>
{
    T Hash(string password);
    bool Validate(string password, T hashedPassword);
}