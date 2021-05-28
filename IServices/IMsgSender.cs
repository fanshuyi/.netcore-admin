using System.Threading.Tasks;

namespace IServices
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }

    //public interface ISmsSender
    //{
    //    Task<SendSmsResponse> ContractAgree(string PhoneNumber);
    //    Task<SendSmsResponse> UserLogin(string PhoneNumber, string TemplateParam);
    //}
}