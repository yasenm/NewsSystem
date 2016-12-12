using System;
using System.Linq.Expressions;

namespace NewsSystem.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static object GetPropValue(this object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static object GetPropValue<T>(this object src, Expression<Func<T>> propertyLambda)
        {
            return src.GetType().GetProperty(src.GetPropertyName(propertyLambda)).GetValue(src, null);
        }

        public static string GetPropertyName<T>(this object src, Expression<Func<T>> propertyLambda)
        {
            var me = propertyLambda.Body as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            return me.Member.Name;
        }
    }
}
