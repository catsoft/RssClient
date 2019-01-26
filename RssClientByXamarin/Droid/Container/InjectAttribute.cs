using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Shared;

namespace Droid.Container
{
    public class InjectAttribute : Attribute
    {
        public InjectAttribute()
        {
            
        }
    }

    public static class InjectExtension
    {
        public static void Inject(this object obj)
        {
            var type = obj.GetType();
            var items = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            var fieldInfos = items.Where(w => w.GetCustomAttribute<InjectAttribute>() != null).ToList();
            foreach (var fieldInfo in fieldInfos)
            {
                var fieldType = fieldInfo.FieldType;
                fieldInfo.SetValue(obj, App.Container.Resolve(fieldType));
            }
        }
    }
}