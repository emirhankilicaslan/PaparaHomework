using PaparaHomeworkAPI.Entities;

namespace PaparaHomeworkAPI.Services
{
    public interface ILoginService
    {
        User Authenticate(string username, string password);
    }
}