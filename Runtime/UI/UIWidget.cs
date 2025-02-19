using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace Marx.Utilities 
{
    public class UIWidget : MonoBehaviour
    {
        public new RectTransform transform => (RectTransform)base.transform;

        private readonly List<Binding> bindings = new();

        private class Binding
        {
            public Action<object> visualHandler;
            public Func<object> valueGetter;
            public object nullValue;
        }

        public void Bind<T>(Func<T> valueProperty, Action<T> visualProperty, T nullValue)
        {

            Binding binding = new()
            {
                nullValue = nullValue,
                visualHandler = (x) => visualProperty((T)x),
                valueGetter = () => valueProperty(),
            };

            RefreshBinding(binding);
            bindings.Add(binding);
        }

        public void Refresh()
        {
            bindings.ForEach(RefreshBinding);
        }

        private void RefreshBinding(Binding binding)
        {

            object value;
            try { value = binding.valueGetter.Invoke(); }
            catch (NullReferenceException) { value = null; }

            if (Equals(value, null))
                value = binding.nullValue;

            binding.visualHandler(value);
        }

        private MemberExpression GetMemberExpression(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Lambda:
                    return GetMemberExpression((expression as LambdaExpression).Body);
                case ExpressionType.MemberAccess:
                    return (expression as MemberExpression);
                default:
                    throw new Exception();
            }
        }

    }
}
