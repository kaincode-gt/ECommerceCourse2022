using Microsoft.AspNetCore.Components.Forms;

namespace ECommerceCourse2022.Service;

public interface IFileUpload
{
    Task<string> UploadFile(IBrowserFile file);
    bool DeleteFile(string filePath);
}
