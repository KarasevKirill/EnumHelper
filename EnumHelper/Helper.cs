using System;

namespace EnumHelper
{
    public class Helper
    {
         private T GetAttribute<T>(Enum value) where T : Attribute
        {
            var memberInfo = value.GetType().GetMember(value.ToString());

            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);

            return attributes.Length > 0 ? (T)attributes[0] : null;
        }

        public string GetDescription(Enum value)
        {
            var attribute = GetAttribute<DescriptionAttribute>(value);

            return attribute == null ? "" : attribute.Description;
        }
    }
}

