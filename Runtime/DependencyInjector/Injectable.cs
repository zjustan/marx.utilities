using System;
using System.Collections.Generic;
using System.Reflection;
using Object = UnityEngine.Object;

namespace Marx.Utilities
{
    public class Injectable
    {
        public readonly List<InjectionField> InjectionPoints = new();
    }

    public class InjectionField
    {
        public readonly Type FieldType;
        public readonly Action<object, object> Setter;

        public InjectionField(Type fieldType, Action<object, object> setter)
        {
            this.FieldType = fieldType;
            this.Setter = setter;
        }

        public InjectionField(FieldInfo field)
        {
            FieldType = field.FieldType;
            Setter = field.CreateSetter();
        }

        public InjectionField(PropertyInfo property)
        {
           FieldType = property.PropertyType;
           Setter = property.CreateSetter();

        }
    }
}