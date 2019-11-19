using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.FileProviders;
using System.Net.Mime;

namespace OdeToFood.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IFileProvider _fileProvider;

        public IndexModel(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public IDirectoryContents PhysicalFiles { get; private set; }
        public string Message { get; private set; }

        public void OnGet(string message)
        {
            Message = message;
            PhysicalFiles = _fileProvider.GetDirectoryContents(string.Empty);
        }

        public IActionResult OnGetDownloadPhysical(string fileName)
        {
            var downloadFile = _fileProvider.GetFileInfo(fileName);
            return PhysicalFile(downloadFile.PhysicalPath, MediaTypeNames.Application.Octet, fileName);
        }
    }
}
