using CustomExpressionVisitor;
using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helpers
{
    public static class  CopyProperties
    {
        public static void CopyPropertiesTo(this IEntity copyFrom, object copyTo)
        {
            var expectedType = copyFrom.GetType();
            var allProperties = expectedType.GetProperties();
            foreach (var prop in allProperties)
            {
                var propertyName = ((CustomAttributeMapper)Attribute.GetCustomAttribute(prop, typeof(CustomAttributeMapper))).MapTo;
                if (copyTo.GetType().GetProperty(propertyName) != null)
                    copyTo.GetType().GetProperty(propertyName).SetValue(copyTo, expectedType.GetProperty(prop.Name).GetValue(copyFrom, null));
                else
                    throw new InvalidCastException();
            }
        }
    }
}
