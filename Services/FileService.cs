
namespace FinalProjectAnimalShop.Services;

public class FileService
{
    private readonly string _uploadPath;

    public FileService(IWebHostEnvironment environment)
    {
        _uploadPath = Path.Combine(environment.WebRootPath, "img");
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        var fileName = Path.GetFileNameWithoutExtension(file.FileName);
        var extension = Path.GetExtension(file.FileName);
        var uniqueFileName = $"{fileName}_{DateTime.Now.Ticks}{extension}";
        var filePath = Path.Combine(_uploadPath, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return $"/img/{uniqueFileName}";
    }

    public void DeleteFile(string? relativeFilePath)
    {
        if (!string.IsNullOrEmpty(relativeFilePath) && relativeFilePath != "/img/NoImage.jpg")
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativeFilePath.TrimStart('/'));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
