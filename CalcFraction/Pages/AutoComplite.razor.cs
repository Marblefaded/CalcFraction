using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;

namespace CalcFraction.Pages
{
    public class AutoComplitePageViewe : ComponentBase
    {
        [Inject] IJSRuntime Js { get; set; }

        private DotNetObjectReference<AutoComplitePageViewe>? _dotNetObjectReference;
        private string? _inputString;

        public string? _xAxis;
        public string? _yAxis;

        public string Show = "none";

        public List<string> DataBaseStrings = new List<string>()
        {
            "яблоко", "груша", "апельсин", "ананас", "фрукт", "Вова", "Иван", "Влад", "мандарин"
        };

        public List<string> ListOfTips = new List<string>();

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _dotNetObjectReference = DotNetObjectReference.Create(this);
                /*oldInputString = _inputString;*/
                /*oldChanges[0] = oldInputString;*/
            }
        }

        public string InputString
        {
            get => _inputString;

            set
            {
                _inputString = value;
                ShowTip();
            }
        }
        public string oldInput { get; set; }
        public string solWord { get; set; } 
        public char[] newChanges { get; set; }
        public char[] oldChanges { get; set; }
        public bool flag = false;
        public int positionA { get; set; }
        public int positionB { get; set; }
        public bool LastSimbolCheck(string str)
        {
            if (str.EndsWith(' '))
            {
                return true;
            }
            return false;
        }

        public void Between(char[] newChanges, char[] oldChanges)
        {
            if (oldChanges != newChanges)
            {
                
                if (oldChanges == null)
                {
                    positionB = newChanges.Length - 1;
                    solWord = new string(newChanges);
                }
                else
                {
                    /*CheckWord();*/
                    int minLength = Math.Min(newChanges.Length, oldChanges.Length);

                    for (int i = 0; i <= minLength; i++)
                    {
                        if ((oldChanges.Length) != i)
                        {
                            if (newChanges[i] != oldChanges[i])
                            {
                                positionB = i;

                                break;
                            }
                        }
                        else
                        {
                            positionB = i;
                        }
                    }
                    var newChangesReverse = newChanges.Reverse().ToArray();
                    for (int i = 0; i < positionB; i++)
                    {
                        if (newChangesReverse[i] == ' ' || newChangesReverse[i] == '\n' || i != newChanges.Length)
                        {
                            positionA = i;
                        }
                        else
                        {
                            Console.WriteLine("Error");
                        }
                    }

                }

            }
        }

        public void CheckWord()
        {
            oldInput = oldInput.Replace("\n", " ");
            var arrOldInput = oldInput.Split(' ');
            var arrNewInput = ClearString().Split(" ");
            if(arrOldInput.Length == arrNewInput.Length)
            {
                var min = Math.Min(arrOldInput.Length, arrNewInput.Length);
                for (int i = 0; i < min; i++)
                {
                    if (arrOldInput[i] != arrNewInput[i])
                    {
                        solWord = arrNewInput[i];
                        /*flag = false;*/
                    }
                    else
                    {

                        solWord = arrNewInput[arrNewInput.Length - 1];
                        /*flag = false;*/
                    }
                }
            }
            
            
        }

        public string TakeLastWord()
        {
            var wordList = ClearString().Split(' ');
            return wordList[wordList.Count() - 1];
        }

        public string ClearString()
        {
            var clearString = _inputString.Replace("\n", " ");
            return clearString;
        }

        public void CheckSimilar(string str)//проверка на совпадение startswith
        {

            ListOfTips = DataBaseStrings.Where(x => x.StartsWith(str.Trim())).ToList();
        }

        public void ShowTip()//получение координат
        {
            Js.InvokeVoidAsync("AutoCompliteJsFunctions.ShowTip", _dotNetObjectReference);
        }

        [JSInvokable]
        public void MakeCoordinate(int x, int y)
        {

            _xAxis = $"{x}px";
            _yAxis = $"{y}px";
            Show = "block";

            flag = false;
            if (LastSimbolCheck(_inputString))
            {
                Show = "none";
                flag = true;
            }
            newChanges = _inputString.ToArray();
            if (oldChanges != null)
            {
                if (oldChanges.Length > newChanges.Length)
                {
                    oldChanges = newChanges;
                    oldInput = _inputString;
                    Show = "none";
                }
                else
                {
                    Between(newChanges, oldChanges);
                    oldChanges = newChanges;
                    oldInput = _inputString;
                    string str = "";
                    var placeA = positionA;
                    while (placeA <= positionB)//тут нужно поменять логику
                    {
                        str += newChanges[placeA];
                        placeA++;
                    }

                    CheckSimilar(str);//Сюда надо определенную слово
                    CheckForShowTip();
                    StateHasChanged();
                }
            }
            else
            {
                Between(newChanges, oldChanges);
                oldChanges = newChanges;
                oldInput = _inputString;
                string str = "";
                var placeA = positionA;
                while (placeA <= positionB)//из массива символов извлекаем от а до б слово и ищем
                {
                    str += newChanges[placeA];
                    placeA++;
                }
                CheckSimilar(str);//Сюда надо определенную слово
                CheckForShowTip();
                StateHasChanged();
            }
        }

        public void CheckForShowTip()
        {
            /*|| flag == true*/
            if (ListOfTips.Count == 0 || ListOfTips.Count == DataBaseStrings.Count)
            {
                Show = "none";
            }
            else
            {
                Show = "block";
                
            }
        }
        public void Wording(string addWord, string str)
        {
            addWord = addWord.Trim();
            var arrAddWord = addWord.ToArray();
            var listAddWord = arrAddWord.ToList();
            var arrStr = str.ToArray();

            for (int i = arrAddWord.Length-1; i < arrStr.Length; i++)
            {
                listAddWord.Add(arrStr[i]);
            }
            arrAddWord = listAddWord.ToArray();
            oldInput = new string(arrAddWord);
        }


        public void InputChoise(string str)
        {
            Show = "none";
            flag = true;
            if (_inputString.Split(" ").Count() == 1)
            {
                str += " ";
                InputString = str;

                ReturnToTextArea();
                return;
            }
            /*var addWord = "";
            var indexA = positionA;
            while (indexA <= positionB)
            {
                addWord += newChanges[indexA];
                indexA++;
            }
            Wording(addWord, str);
*/
            char[] letters = newChanges;
            List<char> charList = new List<char>(letters);
            charList.RemoveRange(positionA, positionB - positionA + 1);

            letters = charList.ToArray();

            if (newChanges[positionA] == ' ' || newChanges[positionA] == '\n')
            {
                str = str.Insert(0, $"{newChanges[positionA]}");
            }
            char[] wordArray = str.ToCharArray();
            /*char[] wordArray = oldInput.ToCharArray();*/
            char[] result = new char[letters.Length + wordArray.Length];

            Array.Copy(letters, result, positionA);
            Array.Copy(wordArray, 0, result, positionA, wordArray.Length);
            Array.Copy(letters, positionA, result, positionA + wordArray.Length, letters.Length - positionA);
            /*result[result.Length-1] = ' ';*/
            
            InputString = new string(result) + " ";
            

            ReturnToTextArea();
            StateHasChanged();
        }
        public void ReturnToTextArea()
        {
            Js.InvokeVoidAsync("AutoCompliteJsFunctions.SetFocusToTextarea");
        }
        public void Dispose()
        {
            _dotNetObjectReference?.Dispose();
        }
    }
}