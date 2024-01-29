/*using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Reflection.Metadata.Ecma335;
using BlazorBootstrap;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using CalcFraction.Data.Service;
using CalcFraction.Data.ViewModel;
using NPOI.SS.Formula.Functions;

namespace CalcFraction.Pages
{
    public class UploadView : ComponentBase
    {
        [Inject] IWebHostEnvironment WebHostEnvironment { get; set; }
        [Inject] IJSRuntime Js { get; set; }

        [Inject]
        protected IWebHostEnvironment HostingEnv { get; set; }
        [Inject]
        protected ZooService ExcelService { get; set; }

        protected List<ExcelViewModel> ExcelModel = new List<ExcelViewModel>();
        protected List<ExcelViewModel> ListNewUser = new List<ExcelViewModel>();


        public bool flag;
        public bool flagForProcessing;
        private List<IBrowserFile> loadedFiles = new();
        private long maxFileSize = 1024 * 1024 * 15;
        private int maxAllowedFiles = 1;
        private bool isLoading;
        private List<string> newFiles = new List<string>();
        private string fileNameExcel;
        public int numberOfInputFiles = 1;
        public Dictionary<string, double> fileUploadProgress = new();
        public ProgressBar progressBar;
        public string Error { get; set; }



        *//*public async Task OnFileChanged(InputFileChangeEventArgs e, int index)
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

                    await SaveFile(buffer, file.Name);
                }
            }
        }

        void UpdateProgress(double percentage, string fileName)
        {
            if (fileUploadProgress.ContainsKey(fileName))
            {
                fileUploadProgress[fileName] = percentage;
                JsInput(percentage);
                progressBar.IncreaseWidth(percentage);
                progressBar.SetLabel($"{progressBar.GetWidth()}%");
            }
            else
            {
                fileUploadProgress.Add(fileName, percentage);
                JsInput(percentage);
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
            *//*string strPercent = percent.ToString();*//*
            string strPercent = Convert.ToInt32(percent).ToString();
            Js.InvokeVoidAsync("updating", strPercent);
        }*//*

        //другой код


        public async Task SaveFiles(InputFileChangeEventArgs e)
        {
            Error = "";
            isLoading = true;
            loadedFiles.Clear();
            flag = false;
            flagForProcessing = false;
            var pathFolder = Path.Combine(WebHostEnvironment.WebRootPath, "Excel");

            if (Directory.Exists(pathFolder))
            {
                //    Console.WriteLine("\nПапка 'Excel' уже существует или была успешно создана.\n");
            }
            else
            {
                Directory.CreateDirectory(pathFolder);
            }

            foreach (var file in e.GetMultipleFiles(maxAllowedFiles))
            {
                loadedFiles.Add(file);
                var trustedFileNameForFileStorage = Guid.NewGuid().ToString();
                var fileExtension = Path.GetExtension(file.Name);
                var fileName = trustedFileNameForFileStorage + fileExtension;
                var path = Path.Combine(pathFolder, fileName);

                fileNameExcel = path;

                using (var fs = new FileStream(path, FileMode.Create))
                {
                    await file.OpenReadStream(maxFileSize).CopyToAsync(fs);
                }
                newFiles.Add(path);
            }
            isLoading = false;

            if (fileNameExcel.Substring(fileNameExcel.Length - 4) != "xlsx")
            {
                File.Delete(fileNameExcel);
                return;
            }

            ListNewUser.Clear();

            ExcelModel = ExcelService.GetAll();

            using (FileStream file = new FileStream(fileNameExcel, FileMode.Open, FileAccess.Read))
            {
                IWorkbook workbook = new XSSFWorkbook(file);

                for (var sheetiter = 0; sheetiter < workbook.NumberOfSheets; sheetiter++)
                {
                    ISheet sheet = workbook.GetSheetAt(sheetiter);
                    IRow firstRow = sheet.GetRow(sheet.FirstRowNum);

                    var firstCellCheck = firstRow.GetCell(CellReference.ConvertColStringToIndex("A"));
                    var secondCellCheck = firstRow.GetCell(CellReference.ConvertColStringToIndex("B"));
                    var thirdCellCheck = firstRow.GetCell(CellReference.ConvertColStringToIndex("C"));
                    var fourCellCheck = firstRow.GetCell(CellReference.ConvertColStringToIndex("D"));
                    var fiveCellCheck = firstRow.GetCell(CellReference.ConvertColStringToIndex("E"));
                    string firstCell = null;
                    string secondCell = null;
                    string thirdCell = null;
                    string fourCell = null;
                    string fiveCell = null;
                    if (firstCellCheck != null && secondCellCheck != null && thirdCellCheck != null && fourCellCheck != null && fiveCellCheck != null)
                    {
                        firstCell = firstCellCheck.ToString();
                        secondCell = secondCellCheck.ToString();
                        thirdCell = thirdCellCheck.ToString();
                        fourCell = fourCellCheck.ToString();
                        fiveCell = fiveCellCheck.ToString();
                    }
                    if (firstCell != "Artikelnummer" || secondCell != "Name" || thirdCell != "Anzahl" || fourCell != "Preis" || fiveCell != "Gesamt")
                    {
                        File.Delete(fileNameExcel);
                        ListNewUser.Clear();
                        return;
                    }
                    *//*int percent = 39 / 100;*//*
                    for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                    {
                        ExcelViewModel userViewModel = new ExcelViewModel();
                        IRow row = sheet.GetRow(i);
                        if (row == null) { continue; }
                        string Artikelnummer = null;
                        string Name = null;
                        int Anzahl = 0;
                        int Preis = 0;
                        int Gesamt = 0;

                        string columnLetter = "A";
                        int columnIndex = CellReference.ConvertColStringToIndex(columnLetter);
                        if (row.GetCell(columnIndex) != null)
                        {
                            try
                            {
                                Artikelnummer = row.GetCell(columnIndex).StringCellValue;
                                userViewModel.Artikelnummer = Artikelnummer;
                            }
                            catch (Exception)
                            {
                                Error += "Столбец Artikelnummer содержит неправильный тип данных. ";
                            }
                        }
                        else
                        {
                            Error += "Столбец Artikelnummer пуст. \n";
                        }
                        

                        columnLetter = "B";
                        columnIndex = CellReference.ConvertColStringToIndex(columnLetter);
                        if (row.GetCell(columnIndex) != null)
                        {
                            try
                            {
                                Name = row.GetCell(columnIndex).StringCellValue;
                                userViewModel.Name = Name;
                            }  
                            catch(Exception)
                            {
                                Error += "Столбец Name содержит неправильный тип данных. \n";
                            }
                        }
                        else
                        {
                            Error += "Столбец Name пуст. \n";
                        }
                        

                        columnLetter = "C";
                        columnIndex = CellReference.ConvertColStringToIndex(columnLetter);
                        if (row.GetCell(columnIndex) != null)
                        {
                            try
                            {
                                Anzahl = (int)row.GetCell(columnIndex).NumericCellValue;
                                userViewModel.Anzahl = Anzahl;
                            }
                            catch(Exception)
                            {
                                Error += "Столбец Anzahl содержит неправильный тип данных. \n";
                            }
                        }
                        else
                        {
                            Error += "Столбец Anzahl пуст. \n";
                        }
                        

                        columnLetter = "D";
                        columnIndex = CellReference.ConvertColStringToIndex(columnLetter);
                        if (row.GetCell(columnIndex) != null)
                        {
                            try
                            {
                                Preis = (int)row.GetCell(columnIndex).NumericCellValue;
                                userViewModel.Preis = Preis;
                            }
                            catch (Exception)
                            {
                                Error += "Столбец Preis содержит неправильный тип данных. \n";
                            }
                        }
                        else
                        {
                            Error += "Столбец Preis пуст. ";
                        }
                        

                        columnLetter = "E";
                        columnIndex = CellReference.ConvertColStringToIndex(columnLetter);
                        if (row.GetCell(columnIndex) != null)
                        {
                            try
                            {
                                Gesamt = (int)row.GetCell(columnIndex).NumericCellValue;
                                userViewModel.Gesamt = Gesamt;
                            }
                            catch(Exception)
                            {
                                Error += "Столбец Gesamt содержит неправильный тип данных. \n";
                            }
                        }
                        else
                        {
                            Error += "Столбец Gesamt пуст. \n";
                        }

                        ExcelService.Create(userViewModel);
                        *//*percent += percent;
                        progressBar.IncreaseWidth(percent);
                        progressBar.SetLabel($"{progressBar.GetWidth()}%");*/
                        /*foreach (var item in ListNewUser)
                        {
                            var rezult = ExcelService.Create(item);
                        }*//*

                    }
                }
            }

        }

        public void LoadFiles()
        {
            Js.InvokeVoidAsync("clickInputFile");
        }
        *//*public void ProcessExcelFile()
        {
            foreach (var item in ListNewUser)
            {
                var rezult = ExcelService.Create(item);
            }
        }*//*
    }
}
*/