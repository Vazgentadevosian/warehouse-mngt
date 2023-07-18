using System.Reflection;

namespace WRMNGT.Infrastructure.Abstractions
{
    public interface IAssemblyIndicator
    {
        Assembly GetAssembly(string name);
    }
}
