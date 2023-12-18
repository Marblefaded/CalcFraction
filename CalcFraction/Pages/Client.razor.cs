using Microsoft.AspNetCore.Components;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using NPOI.Util;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using CalcFraction.Data.ViewModel;

namespace CalcFraction.Pages
{
    public class ClientView : ComponentBase
    {
        [Inject] IWebHostEnvironment WebHostEnvironment { get; set; }
        [Inject] IJSRuntime Js { get; set; }
        public int numberOfInputFiles = 1;
        public Dictionary<string, double> fileUploadProgress = new();
        public ProgressBar progressBar;
        public List<ClientViewModel> clientViewModels { get; set; } = new();
        public string uuid { get; set; }
        public string phoneNumber { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string street { get; set; }
        public string zip { get; set; }
        public string area { get; set; }
        public string cvv { get; set; }
        public string cardNumber { get; set; }
        public string Error { get; set; }
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
                var readFile = file.OpenReadStream();

                var fileSize = file.Size;
                var buffer = new byte[fileSize];
                long maxsize = 512000;
                long totalBytesRead = 0;
                await file.OpenReadStream(maxsize).ReadAsync(buffer);
                var fileContent = System.Text.Encoding.UTF8.GetString(buffer);

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
                ReadJson(fileContent);
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

        public void ReadJson(string stringJson)
        {
            ClientViewModel model = new ClientViewModel();//rss["result"].AsJEnumerable
            JObject rss = JObject.Parse(stringJson);
            int n = 0;
            while (true)
            {
                if (rss["result"].AsJEnumerable<JToken> != null)
                {
                    if (rss["result"][n].AsJEnumerable<JToken> != null)
                    {
                        if (rss["result"][n]["username"].AsJEnumerable<JToken> != null)
                        {
                            model.username = (string)rss["result"][n]["username"];
                        }
                        else
                        {
                            break;
                        }
                        if (rss["result"][n]["password"].AsJEnumerable<JToken> != null)
                        {
                            model.password = (string)rss["result"][n]["password"];
                        }
                        else 
                        { 
                            break; 
                        }
                        if (rss["result"][n]["phoneNumber"].AsJEnumerable<JToken> != null)
                        {
                            model.phoneNumber = (string)rss["result"][n]["phoneNumber"];
                        }
                        else
                        {
                            break;
                        }
                        if (rss["result"][n]["uuid"].AsJEnumerable<JToken> != null)
                        {
                            model.uuid = (string)rss["result"][n]["uuid"];
                        }
                        else
                        {
                            break;
                        }

                        if (rss["result"][n]["location"].AsJEnumerable<JToken> != null)
                        {
                            model.street = (string)rss["result"][n]["location"]["street"];

                            model.zip = (string)rss["result"][n]["location"]["zip"];

                            if(rss["result"][n]["job"].AsJEnumerable<JToken> != null)
                            {
                                if(rss["result"][n]["job"]["area"].AsJEnumerable<JToken> != null)
                                {
                                    model.area = (string)rss["result"][n]["job"]["area"];

                                    if(rss["result"][n]["creditCard"].AsJEnumerable<JToken> != null)
                                    {
                                        if (rss["result"][n]["creditCard"]["number"].AsJEnumerable<JToken> != null)
                                        {
                                            model.cardNumber = (string)rss["result"][n]["creditCard"]["number"];
                                        }
                                        else
                                        {
                                            break;
                                        }
                                        if(rss["result"][n]["creditCard"]["cvv"].AsJEnumerable <JToken> != null)
                                        {
                                            model.cvv = (string)rss["result"][n]["creditCard"]["cvv"];
                                        }
                                        else 
                                        { 
                                            break; 
                                        }
                                    }
                                    else 
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    else 
                    { 
                        break; 
                    }
                    

                    clientViewModels.Add(model);
                    StateHasChanged();
                    n++;
                }
                else 
                { 
                    break; 
                }
            }

        }
    }
}

