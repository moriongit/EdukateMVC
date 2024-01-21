using Microsoft.AspNetCore.Authentication.OAuth;

namespace Edukate.Helpers
{
    public static class FileExtension
    {
        public static async Task<string> SaveImageAsync(this IFormFile file, string path)
        {
            string extension = Path.GetExtension(file.FileName);
            string filename = Path.GetFileNameWithoutExtension(file.FileName);
            filename = Path.Combine(path, filename + Guid.NewGuid().ToString().Substring(0, 4) + extension);
            using (var fs = File.Create(Path.Combine(PathConstants.RootPath, filename)))
            {
                await file.CopyToAsync(fs);
            }
            return filename;
        }
    }
}
