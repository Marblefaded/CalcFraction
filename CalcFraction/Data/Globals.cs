using NPOI.SS.Formula.Functions;

namespace CalcFraction.Data
{
    public class Globals
    {
        public static List<string> AnimalsDictionary => InitAnimals();

        public static List<string> InitAnimals()
        {
            var dictionary = new List<string>
            {
                { "Кабан" },
                { "Хомяк" },
                { "Снегирь" },
                { "Декоративная крыса" },
                { "Кролик" },
                { "Шиншилла" },
                { "Бородатая агама" },
                { "Морская свинка" }
            };
            return dictionary;
        }
        

        


    }
}
