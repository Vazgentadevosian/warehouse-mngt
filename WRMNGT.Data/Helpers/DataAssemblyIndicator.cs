using System.Reflection;

namespace WRMNGT.Data.Helpers
{
    public static class DataAssemblyIndicator
    {
        public static Assembly GetAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}
