using SimpleInjector;
using System.Linq;

namespace MultiValueDictionarySample.Helpers
{
    public static class ProgramHelper
    {
        private static Container _container;

        public static void Initialize()
        {
            _container = new Container(); 
            RegisterAllTypesWithConvention(); 
            _container.Verify();
        }

        public static TService GetInstance<TService>() where TService : class
        {
            return _container.GetInstance<TService>();
        }

        private static void RegisterAllTypesWithConvention()
        {
            var typesWithInterfaces = typeof(Program).Assembly.GetExportedTypes()
                .Where(t => t.Namespace != null && t.Namespace.StartsWith("MultiValueDictionarySample"))
                .Where(ts => ts.GetInterfaces().Any() && ts.IsClass).ToList();

            var registrations = typesWithInterfaces
                .Where(t => t.GetInterfaces().Length == 1)
                .Select(ti => new { Service = ti.GetInterfaces().Single(), Implementation = ti })
                .Where(r => r.Service.Name == "I" + r.Implementation.Name);
             
            foreach (var reg in registrations)
            {
                _container.Register(reg.Service, reg.Implementation, Lifestyle.Singleton);
            } 
        }
    }
}
