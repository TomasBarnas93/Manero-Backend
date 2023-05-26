namespace Manero_Backend.Models.Interfaces.Services
{
    public interface IEMailService
    {
        public Task SendPasswordResetAsync(string code, long expire, string emailAddress);
    }
}
