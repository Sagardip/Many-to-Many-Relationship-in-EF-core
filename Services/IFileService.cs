using Microsoft.AspNetCore.Http;

namespace one.Services
{
    public interface IFileService
    {
        string Upload(IFormFile file);
    }
}