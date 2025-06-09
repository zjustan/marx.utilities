using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Marx.Utilities
{
    public static class ReflectionExtensions 
    {
        /// <summary>
        /// Creates a dynamic method to retrieve the value of a field using reflection.
        /// </summary>
        /// <param name="field">The field information from which the getter is created.</param>
        /// <returns>A function that takes an object instance and returns the value of the specified field. If the field is static, the instance parameter can be null.</returns>
        public static Func<object, object> CreateGetter(this FieldInfo field) {
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

        /// <summary>
        /// Creates a dynamic method to set the value of a field using reflection.
        /// </summary>
        /// <param name="field">The field information from which the setter is created.</param>
        /// <returns>A function that takes an object instance and a value, and sets the specified field's value. If the field is static, the instance parameter can be null.</returns>
        public static Action<object, object> CreateSetter(this FieldInfo field) {
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


        /// <summary>
        /// Creates a dynamic method to retrieve the value of a property using reflection.
        /// </summary>
        /// <param name="property">The property information from which the getter is created.</param>
        /// <returns>A function that takes an object instance and returns the value of the specified property. If the property is static, the instance parameter can be null.</returns>
        /// <exception cref="ArgumentException">Thrown if the property does not have a getter method.</exception>
        public static Func<object, object> CreateGetter(this PropertyInfo property) {
            MethodInfo getter = property.GetGetMethod(true);
            if (getter == null)
                throw new ArgumentException("Property does not have a getter.");

            return (object instance) =>
            {
                object value = getter.Invoke(instance, null);
                return property.PropertyType.IsValueType ? Convert.ChangeType(value, typeof(object)) : value;
            };
        }

        /// <summary>
        /// Creates a dynamic method to set the value of a field using reflection.
        /// </summary>
        /// <param name="field">The field information from which the setter is created.</param>
        /// <returns>An action that takes an object instance and a value to set the specified field. If the field is static, the instance parameter can be null.</returns>
        public static Action<object, object> CreateSetter(this PropertyInfo property) {
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