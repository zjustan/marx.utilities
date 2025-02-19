using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Marx.Utilities
{
	/// <summary>
	/// This class is responsible for resetting all static field and properties before starting play mode.
	/// This class only has purpose when Reload Domain is disabled.
	/// To do this go to 'Project Settings' > 'Editor' > 'Enter Play Mode Settings'.
	/// Make sure 'Enter Play Mode Options' is enabled and 'Reload Domain' is disabled. 
	/// If 'Enter Play Mode Options' is disabled, a domain reload will happen regardless of whether 'Reload Domain' is disabled or enabled.
	/// </summary>
	[InitializeOnLoadAttribute]
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
			//FIX: there should be a reverse search to get all the types / assemblies 
			// this will not cause a problem for not bc we are only using one asmdef
			foreach (Type item in typeof(StaticReloadAttribute).Assembly.GetTypes())
			{
				foreach (FieldInfo field in item.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
				{
					if (field.IsStatic && field.GetCustomAttribute<StaticReloadAttribute>() != null)
					{
						field.SetValue(null, null);
						Debug.Log($"resetted: {item.Name}.{field.Name}");
					}
				}

				foreach (PropertyInfo property in item.GetProperties().Where(x => x.GetCustomAttribute<StaticReloadAttribute>() != null))
				{
					property.SetValue(null, null);
					Debug.Log($"resetted: {item.Name}.{property.Name}");
				}
			}
		}
	}
}
