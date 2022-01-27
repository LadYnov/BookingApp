using System.Threading.Tasks;

namespace WebApp.Services;

public interface IPaymentService
{
    Task<bool> SendPaymentInformation();
}