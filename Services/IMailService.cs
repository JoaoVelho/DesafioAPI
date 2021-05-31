using System.Threading.Tasks;
using DesafioAPI.Models;

namespace DesafioAPI.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}