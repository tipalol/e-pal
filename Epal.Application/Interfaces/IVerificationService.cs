namespace Epal.Application.Interfaces;

public interface IVerificationService
{
     public Task SendVerificationCodeAsync(string email);
     public bool Verify(string email, int verificationCode);
}