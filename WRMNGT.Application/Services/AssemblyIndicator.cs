using System.Reflection;
using WRMNGT.Infrastructure.Abstractions;
using WRMNGT.Data.Helpers;
using WRMNGT.Domain.Helpers;
using WRMNGT.Application.Helpers;

using static WRMNGT.Domain.Constants.AssemblyConstants;

namespace WRMNGT.Application.Services
{
    public class AssemblyIndicator : IAssemblyIndicator
    {
        public Assembly GetAssembly(string name)
        {
            return name switch
            {
                DomainName => DomainAssemblyIndicator.GetAssembly(),
                DataName => DataAssemblyIndicator.GetAssembly(),
                ApplicationName => ApplicationAssemblyIndicator.GetAssembly(),

                _ => throw new NotSupportedException($"Can not indicate assembly with name={name} "),
            };
        }
    }
}
