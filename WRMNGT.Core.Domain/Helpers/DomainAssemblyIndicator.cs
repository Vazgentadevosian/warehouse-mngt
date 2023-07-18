using System.Reflection;

namespace WRMNGT.Domain.Helpers
{
    public static class DomainAssemblyIndicator
    {
        public static Assembly GetAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}
