using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Marx.Utilities
{
    public static class ReflectionExtensions 
    {
        
        public static Func<object, object> CreateGetter(this FieldInfo field)
        {
            string methodName = field.ReflectedType.FullName + ".get_" + field.Name;
            DynamicMethod setterMethod = new DynamicMethod(methodName, typeof(object), new Type[1] { typeof(object) }, true);
            ILGenerator gen = setterMethod.GetILGenerator();
            if (field.IsStatic)
            {
                gen.Emit(OpCodes.Ldsfld, field);
            }
            else
            {
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Castclass, field.DeclaringType);
                gen.Emit(OpCodes.Ldfld, field);
            }
            gen.Emit(OpCodes.Ret);
            return (Func<object, object>)setterMethod.CreateDelegate(typeof(Func<object, object>));
        }
        
        
        public static Action<object, object> CreateSetter(this FieldInfo field)
        {
            string methodName = field.ReflectedType.FullName+".set_"+field.Name;
            DynamicMethod setterMethod = new DynamicMethod(methodName, null, new Type[2]{typeof(object),typeof(object)},true);
            ILGenerator gen = setterMethod.GetILGenerator();

            if (field.IsStatic)
            {
                gen.Emit(OpCodes.Ldarg_1);
                if (field.FieldType.IsValueType)
                    gen.Emit(OpCodes.Unbox_Any, field.FieldType);
                gen.Emit(OpCodes.Stsfld, field);
            }
            else
            {
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Castclass, field.DeclaringType);
                gen.Emit(OpCodes.Ldarg_1);
                if (field.FieldType.IsValueType)
                    gen.Emit(OpCodes.Unbox_Any, field.FieldType);
                gen.Emit(OpCodes.Stfld, field);
            }
            gen.Emit(OpCodes.Ret);
            return (Action<object, object>)setterMethod.CreateDelegate(typeof(Action<object, object>));
        }
        
        
        public static Func<object, object> CreateGetter(this PropertyInfo property)
        {
            MethodInfo getter = property.GetGetMethod(true);
            if (getter == null)
                throw new ArgumentException("Property does not have a getter.");

            return (object instance) =>
            {
                object value = getter.Invoke(instance, null);
                return property.PropertyType.IsValueType ? Convert.ChangeType(value, typeof(object)) : value;
            };
        }

        public static Action<object, object> CreateSetter(this PropertyInfo property)
        {
            MethodInfo setter = property.GetSetMethod(true);
            if (setter == null)
                throw new ArgumentException("Property does not have a setter.");

            return (object instance, object value) =>
            {
                if (property.PropertyType.IsValueType && value != null)
                    value = Convert.ChangeType(value, property.PropertyType);
                setter.Invoke(instance, new object[] { value });
            };
        }
    }
}