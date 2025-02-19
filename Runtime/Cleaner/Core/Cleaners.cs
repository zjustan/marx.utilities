using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Marx.Utilities
{

    public static class Cleaners
    {
        public static List<ICleaner> cleaners = new List<ICleaner>();

        public static void Clean(IEnumerable<object> objs)
        {
            CreateCleaners();

            foreach (var obj in objs)
            {
                Clean(obj);
            }
        }

        public static void Clean(object obj)
        {
            if (obj == null)
                return;

            CreateCleaners();

            try
            {
                foreach (ICleaner c in cleaners)
                {
                    if (c.TryClean(obj))
                        return;
                }
            }
            catch (Exception e)
            {
                Debug.LogError("error occoured while cleaning object");
                Debug.LogException(e);
            }
        }

        private static void CreateCleaners()
        {
            if (cleaners.Count > 0)
                return;

            Type interfaceType = typeof(ICleaner);
            foreach (var type in Assembly.GetAssembly(typeof(ICleaner)).GetTypes().Where(
                x => !x.IsAbstract && interfaceType.IsAssignableFrom(x)))
            {
                cleaners.Add((ICleaner)Activator.CreateInstance(type));
            }

            cleaners = cleaners.OrderByDescending(x => x.Priority).ToList();
        }
    }

}