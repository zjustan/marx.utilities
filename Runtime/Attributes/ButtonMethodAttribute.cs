﻿// ---------------------------------------------------------------------------- 
// Author: Kaynn, Yeo Wen Qin
// https://github.com/Kaynn-Cahya
// Date:   26/02/2019
// ----------------------------------------------------------------------------

using JetBrains.Annotations;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Marx.Utilities {

    [AttributeUsage(AttributeTargets.Method)]
    [MeansImplicitUse]
    public class ButtonMethodAttribute : PropertyAttribute {
        public readonly ButtonMethodDrawOrder DrawOrder;

        public readonly string Name = null;

        public ButtonMethodAttribute(ButtonMethodDrawOrder drawOrder = ButtonMethodDrawOrder.AfterInspector) => DrawOrder = drawOrder;
        public ButtonMethodAttribute(string name, ButtonMethodDrawOrder drawOrder = ButtonMethodDrawOrder.AfterInspector) {
            DrawOrder = drawOrder;
            Name = name;
        }

    }

    public enum ButtonMethodDrawOrder {
        BeforeInspector,
        AfterInspector
    }
}

#if UNITY_EDITOR
namespace Marx.Editor {

    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UnityEditor;
    using Marx.Utilities;

    public class ButtonMethodHandler {
        public readonly List<(MethodInfo Method, string Name, ButtonMethodDrawOrder Order)> TargetMethods;
        public int Amount => TargetMethods?.Count ?? 0;

        private readonly Object _target;

        public ButtonMethodHandler(Object target) {
            _target = target;

            var type = target.GetType();
            var bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            var members = type.GetMembers(bindings).Where(IsButtonMethod);

            foreach (var member in members) {
                var method = member as MethodInfo;
                if (method == null) continue;

                if (IsValidMember(method, member)) {
                    var attribute = (ButtonMethodAttribute)Attribute.GetCustomAttribute(method, typeof(ButtonMethodAttribute));
                    if (TargetMethods == null) TargetMethods = new List<(MethodInfo, string, ButtonMethodDrawOrder)>();
                    TargetMethods.Add((method, attribute.Name ?? method.Name.SplitCamelCase(), attribute.DrawOrder));
                }
            }
        }

        public void OnBeforeInspectorGUI() {
            if (TargetMethods == null) return;

            bool anyDrawn = false;
            foreach (var method in TargetMethods) {
                if (method.Order != ButtonMethodDrawOrder.BeforeInspector) continue;

                anyDrawn = true;
                if (GUILayout.Button(method.Name)) InvokeMethod(_target, method.Method);
            }

            if (anyDrawn) EditorGUILayout.Space();
        }

        public void OnAfterInspectorGUI() {
            if (TargetMethods == null) return;
            bool anyDrawn = false;

            foreach (var method in TargetMethods) {
                if (method.Order != ButtonMethodDrawOrder.AfterInspector) continue;

                if (!anyDrawn) {
                    EditorGUILayout.Space();
                    anyDrawn = true;
                }

                if (GUILayout.Button(method.Name)) InvokeMethod(_target, method.Method);
            }
        }

        public void Invoke(MethodInfo method) => InvokeMethod(_target, method);


        private void InvokeMethod(Object target, MethodInfo method) {
            var result = method.Invoke(target, null);

            if (result != null) {
                var message = $"{result} \nResult of Method '{method.Name}' invocation on object {target.name}";
                Debug.Log(message, target);
            }
        }

        private bool IsButtonMethod(MemberInfo memberInfo) {
            return Attribute.IsDefined(memberInfo, typeof(ButtonMethodAttribute));
        }

        private bool IsValidMember(MethodInfo method, MemberInfo member) {
            if (method == null) {
                Debug.LogWarning(
                    $"Property <color=brown>{member.Name}</color>.Reason: Member is not a method but has EditorButtonAttribute!");
                return false;
            }

            if (method.GetParameters().Length > 0) {
                Debug.LogWarning(
                    $"Method <color=brown>{method.Name}</color>.Reason: Methods with parameters is not supported by EditorButtonAttribute!");
                return false;
            }

            return true;
        }
    }
}
#endif