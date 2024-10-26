namespace Epal.Application.Interfaces;

public interface IVerificationService
{
     Task SendVerificationCode(string email);
     bool Verify(string email, int verificationCode);
}