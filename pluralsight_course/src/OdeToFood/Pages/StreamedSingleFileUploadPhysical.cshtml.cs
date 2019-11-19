using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using OdeToFood.Utils;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OdeToFood.Pages
{
    public class StreamedSingleFileUploadPhysicalModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        private readonly long _fileSizeLimit;
        private readonly string[] _permittedExtensions = { ".png", ".jpeg", ".jpg" };
        private readonly string _targetFilePath;

        // Add your Computer Vision subscription key and endpoint to your environment variables.
        private readonly string _subscriptionKey;
        private readonly string _endpoint;

        // the OCR method endpoint
        private readonly string _uriBase;

        public StreamedSingleFileUploadPhysicalModel(IConfiguration config, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
            // To save physical files to a path provided by configuration:
            _targetFilePath = config.GetValue<string>("StoredFilesPath");

            _subscriptionKey = config.GetValue<string>("SubscriptionKey");
            _endpoint = config.GetValue<string>("EndPoint");
            _uriBase = _endpoint + "vision/v2.1/ocr";
        }

        [BindProperty]
        public BufferedSingleFileUploadPhysical FileUpload { get; set; }

        public string Result { get; private set; }

        public void OnGet()
        {
        }

        /// <summary>
        /// Upload Example
        /// https://docs.microsoft.com/pt-br/aspnet/core/mvc/models/file-uploads?view=aspnetcore-3.0#upload-small-files-with-buffered-model-binding-to-physical-storage        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostUploadAsync()
        {
            if (!ModelState.IsValid)
            {
                Result = "Please correct the form.";
                return Page();
            }

            var formFileContent =
                await FileHelpers.ProcessFormFile<BufferedSingleFileUploadPhysical>(
                    FileUpload.FormFile, ModelState, _permittedExtensions, _fileSizeLimit);

            if (!ModelState.IsValid)
            {
                Result = "Please correct the form.";

                return Page();
            }

            var temporaryName = WebUtility.HtmlEncode(FileUpload.FormFile.FileName);
            var filePath = Path.Combine(_targetFilePath, temporaryName);

            using (var fileStream = System.IO.File.Create(filePath))
            {
                await fileStream.WriteAsync(formFileContent);
            }

            var message = await MakeOCRRequest(filePath);

            return RedirectToPage("./Index", new { message });
        }

        /// <summary>
        /// Gets the text visible in the specified image file by using
        /// the Computer Vision REST API.
        /// 
        /// Example: https://docs.microsoft.com/pt-br/azure/cognitive-services/computer-vision/quickstarts/csharp-print-text
        /// </summary>
        /// <param name="imageFilePath">The image file with printed text.</param>
        private async Task<string> MakeOCRRequest(string imageFilePath)
        {
            try
            {
                string _requestParameters = "language=unk&detectOrientation=true";
                string _uri = _uriBase + "?" + _requestParameters;

                var request = new HttpRequestMessage(HttpMethod.Post, _uri);
                request.Headers.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);

                var client = _clientFactory.CreateClient();

                // Request headers.
                //client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);

                // Request parameters. 
                // The language parameter doesn't specify a language, so the 
                // method detects it automatically.
                // The detectOrientation parameter is set to true, so the method detects and
                // and corrects text orientation before detecting text.
                //string requestParameters = "language=unk&detectOrientation=true";

                // Assemble the URI for the REST API method.
                //string uri = _uriBase + "?" + requestParameters;

                HttpResponseMessage response;

                // Read the contents of the specified local image
                // into a byte array.
                byte[] byteData = GetImageAsByteArray(imageFilePath);

                // Add the byte array as an octet stream to the request body.
                using (ByteArrayContent content = new ByteArrayContent(byteData))
                {
                    // This example uses the "application/octet-stream" content type.
                    // The other content types you can use are "application/json"
                    // and "multipart/form-data".
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    request.Content = content;
                    // Asynchronously call the REST API method.

                    response = await client.SendAsync(request);
                    //response = await client.PostAsync(uri, content);
                }

                // Asynchronously get the JSON response.
                string contentString = await response.Content.ReadAsStringAsync();

                // Display the JSON response.
                return JToken.Parse(contentString).ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
                return e.Message.ToString();
            }
        }

        /// <summary>
        /// Returns the contents of the specified file as a byte array.
        /// </summary>
        /// <param name="imageFilePath">The image file to read.</param>
        /// <returns>The byte array of the image data.</returns>
        private static byte[] GetImageAsByteArray(string imageFilePath)
        {
            // Open a read-only file stream for the specified file.
            using FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            // Read the file's contents into a byte array.
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }
    }

    public class BufferedSingleFileUploadPhysical
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }

        [Display(Name = "Note")]
        [StringLength(50, MinimumLength = 0)]
        public string Note { get; set; }
    }
}