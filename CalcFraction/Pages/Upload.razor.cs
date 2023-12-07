using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Reflection.Metadata.Ecma335;
using BlazorBootstrap;

namespace CalcFraction.Pages
{
    public class UploadView : ComponentBase
    {
        [Inject] IWebHostEnvironment WebHostEnvironment { get; set; }
        [Inject] IJSRuntime Js { get; set; }
        public int numberOfInputFiles = 1;
        public Dictionary<string, double> fileUploadProgress = new();
        public ProgressBar progressBar;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                
            }
        }

        public string GetInputFileStyle(int index)
        {
            return index == numberOfInputFiles - 1 ? "" : "display: none";
        }

        public async Task OnFileChanged(InputFileChangeEventArgs e, int index)
        {
            numberOfInputFiles++;

            foreach (var file in e.GetMultipleFiles())
            {
                var fileSize = file.Size;
                var buffer = new byte[fileSize];
                long totalBytesRead = 0;

                using var stream = file.OpenReadStream(maxAllowedSize: long.MaxValue);
                while (true)
                {
                    var read = await stream.ReadAsync(buffer);
                    if (read == 0)
                        break;

                    totalBytesRead += read;
                    double percentage = (double)totalBytesRead / fileSize * 100;
                    UpdateProgress(percentage, file.Name);

                    /*await SaveFile(buffer, file.Name);*/
                }
            }
        }

        void UpdateProgress(double percentage, string fileName)
        {
            if (fileUploadProgress.ContainsKey(fileName))
            {
                fileUploadProgress[fileName] = percentage;
                /*JsInput(percentage);*/
                progressBar.IncreaseWidth(percentage);
                progressBar.SetLabel($"{progressBar.GetWidth()}%");
            }
            else
            {
                fileUploadProgress.Add(fileName, percentage);
                /*JsInput(percentage);*/
                progressBar.IncreaseWidth(percentage);
                progressBar.SetLabel($"{progressBar.GetWidth()}%");
            }

        }

        async Task SaveFile(byte[] buffer, string fileName)
        {
            var wwwRootPath = Path.Combine(WebHostEnvironment.WebRootPath, fileName);
            await File.WriteAllBytesAsync(wwwRootPath, buffer);
        }
        public void JsInput(double percent)
        {
            /*string strPercent = percent.ToString();*/
            string strPercent = Convert.ToInt32(percent).ToString();
            Js.InvokeVoidAsync("updating", strPercent);
        }
    }
}
