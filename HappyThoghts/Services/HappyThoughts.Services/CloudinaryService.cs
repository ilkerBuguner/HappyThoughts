namespace HappyThoughts.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinaryUtility;

        public CloudinaryService(Cloudinary cloudinaryUtility)
        {
            this.cloudinaryUtility = cloudinaryUtility;
        }

        public async Task<string> UploadPhotoAsync(IFormFile file, string fileName)
        {
            byte[] destinationData;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                destinationData = memoryStream.ToArray();
            }

            UploadResult uploadResult = null;

            using (var memoryStream = new MemoryStream(destinationData))
            {
                ImageUploadParams uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(fileName, memoryStream),
                };

                uploadResult = await this.cloudinaryUtility.UploadAsync(uploadParams);
            }

            return uploadResult?.SecureUri.AbsoluteUri;
        }

    }
}
