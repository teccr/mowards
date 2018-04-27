using System;
using System.Collections.Generic;
using Mowards.ViewModels;

namespace Mowards.Services
{
    public class ViewModelFactory
    {
        private static Dictionary<string, ViewModelBase> Instances = 
            new Dictionary<string, ViewModelBase>();

        public static T GetInstance<T>() where T : ViewModelBase
        {
            var targetType = typeof(T).Name;
            T instance = null;
            if(Instances.ContainsKey(targetType))
            {
                instance = (T)Instances[targetType];
            }
            else 
            {
                instance = Activator.CreateInstance<T>();
                Instances.Add( targetType, instance );
            }
            return instance;
        }

        public static void DeleteInstance<T>() where T : ViewModelBase
        {
            var targetType = typeof(T).Name;
            if (Instances.ContainsKey(targetType))
            {
                Instances.Remove(targetType);
            }
        }

        public static void DeleteAllInstances()
        {
            if(Instances != null && Instances.Count > 0)
            {
                Instances.Clear();
                Instances = null;
            }
        }
    }
}
