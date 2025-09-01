using Microsoft.AspNetCore.Http;

namespace RegistroLegal.Core.Aplications.Helpers
{
    public class UploadFile
    {
        public static string Upload<T>(IFormFile? file, T id, string folderName, bool editMode = false, string? imagePath = "") 
        {
            if (editMode && file == null) 
            {
                return imagePath ?? "";
            }

            if (file == null) 
            {
                return string.Empty;    
            
            }

            string basePAth = $"/Image/{folderName}/{id}";

            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{basePAth}");

            if (!Directory.Exists(path)) 
            {
                Directory.CreateDirectory(path);
            }

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string FullFile = Path.Combine(path, fileName);

            using (var stream = new FileStream(FullFile, FileMode.Create)) 
            {
                file.CopyTo(stream);
            }

            //Eliminar archivos viejos (Solo en modo edicion).
            if (editMode && !string.IsNullOrWhiteSpace(imagePath)) 
            {
                string[] oldImagPath = imagePath.Split('/');
                string oldFileName = oldImagPath[^1];
                string completeOldPath = Path.Combine(path, oldFileName);

                if (File.Exists(completeOldPath)) 
                {
                    File.Delete(completeOldPath);
                }                  
            }
                return $"{basePAth}/{fileName}";
        }

        public static bool Delete<T>(T id, string folderName) 
        {
            string basePAth = $"/Image/{folderName}/{id}";

            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{basePAth}");

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                return true;
            }          
                return false;  
        }
    }
}
