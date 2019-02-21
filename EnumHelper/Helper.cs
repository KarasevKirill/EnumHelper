using System;

namespace EnumHelper
{
    /// <summary>
    /// Позволяет получать строку из аттрибута [Description()] Enum
    /// или получить значение Enum по значению Description
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Указывает, необходимо ли вернуть 0, если значение перечисление
        /// не было найдено при поиске по строке
        /// </summary>
        public bool ReturnDefaultValue { get; set; }

        /// <summary>
        /// Извлекает аттрибут из значения Enum, если аттрибут отсутствует, то
        /// вернет null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        private T GetAttribute<T>(Enum value) where T : Attribute
        {
            var memberInfo = value.GetType().GetMember(value.ToString());

            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);

            return attributes.Length > 0 ? (T)attributes[0] : null;
        }

        /// <summary>
        /// Возвращает строковое представление значения Enum
        /// Если имеется аттрибут Description, то вернется его значение
        /// Если аттрибут отсутствует, то вернется строка, содержащая имя значения
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetDisplayValue(Enum value)
        {
            var attribute = GetAttribute<DescriptionAttribute>(value);

            return attribute == null ? value.ToString() : attribute.Description;
        }

        /// <summary>
        /// Возвращает значение Enum, у которого значение аттрибута Description совпадает с
        /// переданной строкой поиска.
        /// Если значение аттрибута не найдено, то возвращает значение, имя которого совпадает
        /// со строкой поиска.
        /// Если совпадений нет, то либо вернется 0, либо бросается ArgumentException, в
        /// зависимости от свойства ReturnDefaultValue
        /// Актуально для C# версий до 7.3, в 7.3 и выше можно реализовать проще
        /// </summary>
        /// <typeparam name="E">Enum, по значения которого осуществляется поиск</typeparam>
        /// <param name="description">Значение аттрибута Description, по которому ведется поиск значения Enum</param>
        /// <returns></returns>
        public E GetValueBySearchString<E>(string searchString) where E : struct, IConvertible
        {
            var currentType = typeof(E);

            if (!currentType.IsEnum)
                throw new ArgumentException("Этому методу для работы необходим Enum");

            var values = Enum.GetValues(currentType);

            foreach (var currentValue in values)
            {
                string currentDescription = GetDisplayValue((Enum)currentValue);

                if (currentDescription == searchString)
                    return (E)currentValue;
            }

            foreach (var currentValue in values)
            {
                string valueName = currentValue.ToString();

                if (valueName == searchString)
                    return (E)currentValue;
            }

            if (ReturnDefaultValue)
                return default(E);

            throw new ArgumentException("Ни одно значение перечисления не имеет аттрибут Description или имя, совпадающие с искомой строкой");
        }
    }
}

