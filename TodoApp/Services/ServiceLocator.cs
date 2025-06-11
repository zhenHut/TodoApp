using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Services
{
    public class ServiceLocator
    {
        #region Fields
        private static readonly Dictionary<Type, object> _services = new();

        #endregion

        #region Methods
        public static void Register<T>(T implementation)
        {
            _services[typeof(T)] = implementation!;
        }

        public static T GET<T>()
        {
            return (T) _services[typeof(T)];
        }

        #endregion
    }
}
