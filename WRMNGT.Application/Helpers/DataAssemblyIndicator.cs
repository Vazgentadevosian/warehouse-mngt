using System.Reflection;

namespace WRMNGT.Application.Helpers
{
    public static class ApplicationAssemblyIndicator
    {
        public static Assembly GetAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}
