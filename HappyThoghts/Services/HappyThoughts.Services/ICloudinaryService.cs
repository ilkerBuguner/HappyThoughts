namespace HappyThoughts.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<string> UploadPhotoAsync(IFormFile file, string fileName);
    }
}
