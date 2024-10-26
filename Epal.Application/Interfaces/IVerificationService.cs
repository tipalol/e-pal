namespace Epal.Application.Interfaces;

public interface IVerificationService
{
     Task SendVerificationCodeAsync(string email);
     bool Verify(string email, int verificationCode);
}