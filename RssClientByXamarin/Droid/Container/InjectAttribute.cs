using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Droid.Screens.Base;
using Droid.Screens.Navigation;
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
        private static List<Type> _stopList = new List<Type>()
        {
            // TODO разделить на android ios
            typeof(InjectActivity),
            typeof(InjectFragment)
        };
        
        public static void Inject(this object obj, bool searchInDeep = false)
        {
            var type = obj.GetType();

            var items = new List<FieldInfo>();

            if (!searchInDeep)
            {
                items = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).ToList();
            }
            else
            {
                while (type != null && !_stopList.Contains(type))
                {
                    items = items.Concat(type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)).ToList();
                
                    type = type.BaseType;
                }                
            }

            foreach (var fieldInfo in items.Where(w => w.GetCustomAttribute<InjectAttribute>() != null))
            {
                var fieldType = fieldInfo.FieldType;
                fieldInfo.SetValue(obj, App.Container.Resolve(fieldType));
            }
        }
    }
}