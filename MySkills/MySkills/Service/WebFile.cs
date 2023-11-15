﻿using SixLabors.ImageSharp.Formats.Jpeg;
using System.Security.Cryptography;
using System.Text;

namespace MySkills.Service
{
    public class WebFile
    {
        const string FOLDER_PREFIX = "./wwwroot";

        public string GetWebFileName(string filename)
        {
            string dir = GetWebFileFolder(filename);

            CreateFolder(FOLDER_PREFIX + dir);

            return dir + "/" + Path.GetFileNameWithoutExtension(filename) + ".jpg";
        }

        public string GetWebFileFolder(string filename)
        {
            MD5 md5hash = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(filename);
            byte[] hashBytes = md5hash.ComputeHash(inputBytes);

            string hash = Convert.ToHexString(hashBytes);

            return "/images/" + hash[..2] + "/" +
                   hash[..4];
        }

        public void CreateFolder(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        public async Task UploadAndResizeImage(Stream fileStream, string filename, int newWidth, int newHeight)
        {
            using (Image image = await Image.LoadAsync(fileStream))
            {
                int aspectWidth = newWidth;
                int aspectHeight = newHeight;

                if (image.Width / (image.Height / (float)newHeight) > newWidth)
                    aspectHeight = (int)(image.Height / (image.Width / (float)newWidth));
                else
                    aspectWidth = (int)(image.Width / (image.Height / (float)newHeight));

                image.Mutate(x => x.Resize(aspectWidth, aspectHeight, KnownResamplers.Lanczos3));

                await image.SaveAsJpegAsync(FOLDER_PREFIX + filename, new JpegEncoder() { Quality = 75 });
            }
        }
    }
}