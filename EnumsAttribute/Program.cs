using System;
using EnumHelper;

namespace EnumsAttribute
{
    class Program
    {
        static void Main(string[] args)
        {
            var helper = new Helper();

            Console.WriteLine("Строковое представление значений перечисления, (у значений установлен аттрибут Description):");
            Console.WriteLine(helper.GetDisplayValue(DocType.FirstValue));
            Console.WriteLine(helper.GetDisplayValue(DocType.SecondValue));
            Console.WriteLine(helper.GetDisplayValue(DocType.ThirdValue));
            Console.WriteLine();

            Console.WriteLine("Строковое представление значения перечисления, (значение не имеет аттрибута):");
            Console.WriteLine(helper.GetDisplayValue(DocType.FourthValue));
            Console.WriteLine();

            var firstSearch = "Второе значение";
            var secondSearch = "ThirdValue";
            var thirdSearch = "AAAAA";

            Console.WriteLine($"Значение перечисления (поиск по аттрибуту значения, строка поиска: \"{firstSearch}\"):");           
            Console.WriteLine(helper.GetValueBySearchString<DocType>(firstSearch));
            Console.WriteLine();

            Console.WriteLine($"Значение перечисления (поиск по имени значения, строка поиска: \"{secondSearch}\"):");
            Console.WriteLine(helper.GetValueBySearchString<DocType>(secondSearch));
            Console.WriteLine();
           
            Console.WriteLine($"Ищем несуществующее значение, ReturnDefaultValue == true (строка поиска: \"{thirdSearch}\"):");
            helper.ReturnDefaultValue = true;
            Console.WriteLine(helper.GetValueBySearchString<DocType>(thirdSearch));
            Console.WriteLine();

            try
            {
                Console.WriteLine($"Ищем несуществующее значение, ReturnDefaultValue == false (строка поиска: \"{thirdSearch}\"):");
                helper.ReturnDefaultValue = false;
                Console.WriteLine(helper.GetValueBySearchString<DocType>(thirdSearch));
                Console.WriteLine();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception message:");
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
    public enum DocType : byte
    {
        /// <summary>
        /// Ноль считается как не инициализированное значение
        /// </summary>
        UndefinedValue = 0,

        [Description("Первое значение")]
        FirstValue,

        [Description("Второе значение")]
        SecondValue,

        [Description("Третье значение")]
        ThirdValue,

        FourthValue
    }
}
