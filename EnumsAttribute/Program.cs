using System;
using EnumHelper;

namespace EnumsAttribute
{
    class Program
    {
        static void Main(string[] args)
        {
            var helper = new Helper();

            var first = helper.GetDescription(DocType.FirstValue);
            var second = helper.GetDescription(DocType.SecondValue);
            var third = helper.GetDescription(DocType.ThirdValue);

            Console.WriteLine(first);
            Console.WriteLine(second);
            Console.WriteLine(third);

            Console.ReadKey();
        }
    }

    public enum DocType : byte
    {
        [Description("Первое значение")]
        FirstValue,

        [Description("Второе значение")]
        SecondValue,

        [Description("Третье значение")]
        ThirdValue
    }
}
