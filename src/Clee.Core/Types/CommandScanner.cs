using System;
using System.Linq;
using System.Reflection;

namespace Clee.Types
{
    public class CommandScanner
    {
        public Type[] Scan(Assembly assembly)
        {
            return assembly
                .GetTypes()
                .Where(x => x.IsClass)
                .Where(x => x.IsPublic  || x.IsNestedPublic)
                .Where(x => TypeUtils.IsAssignableToGenericType(x, typeof(ICommand<>)))
                .ToArray();
        }

        public Type[] Scan(Assembly assembly, string namespaceQualifier)
        {
            return Scan(assembly)
                .Where(x => x.Namespace.StartsWith(namespaceQualifier))
                .ToArray();
        }
    }
}