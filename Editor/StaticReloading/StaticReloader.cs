using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace Marx.Utilities.Editor
{
	/// <summary>
	/// This class is responsible for resetting all static field and properties before starting play mode.
	/// This class only has purpose when Reload Domain is disabled.
	/// To do this go to 'Project Settings' > 'Editor' > 'Enter Play Mode Settings'.
	/// Make sure 'Enter Play Mode Options' is enabled and 'Reload Domain' is disabled. 
	/// If 'Enter Play Mode Options' is disabled, a domain reload will happen regardless of whether 'Reload Domain' is disabled or enabled.
	/// </summary>
	[InitializeOnLoad]
	public static class StaticReloader
	{
		static StaticReloader()
		{
			EditorApplication.playModeStateChanged -= LogPlayModeState;
			EditorApplication.playModeStateChanged += LogPlayModeState;
		}

		private static void LogPlayModeState(PlayModeStateChange state)
		{
			if (state == PlayModeStateChange.ExitingEditMode)
				ReloadStaticFields();
		}

		private static void ReloadStaticFields()
		{
			foreach (Type item in typeof(StaticReloadAttribute).Assembly.GetReferencingAssemblies().SelectMany(x => x.GetTypes()))
			{
				foreach (FieldInfo field in item.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
				{
					if (field.GetCustomAttribute<StaticReloadAttribute>() == null) continue;
					object value = CreateDefault(field.FieldType);
					field.SetValue(null,value);
				}

				foreach (PropertyInfo property in item.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
				{
					if (property.GetCustomAttribute<StaticReloadAttribute>() == null) continue;
					object value = CreateDefault(property.PropertyType);
					property.SetValue(null,value);
				}
			}
		}

		private static object CreateDefault(Type type)
		{
			if(type.GetConstructor(Type.EmptyTypes) != null && !type.IsAbstract)
			{
				return Activator.CreateInstance(type);
			}

			return null;
		}
	}
}
