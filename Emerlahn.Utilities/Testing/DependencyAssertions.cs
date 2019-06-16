using System;
using System.Linq;
using System.Reflection;

namespace Emerlahn.Utilities.Testing
{
    public static class DependencyAssertions
    {
        public static void AssertReferencesAllowed(this Assembly assembly, Func<string, bool> isAllowed)
        {
            assembly.GetReferencedAssemblies().Select(x => x.Name).Where(n => !isAllowed(n)).ShouldBeEmpty();
        }

        public static void AssertReferencesDisallowed(this Assembly assembly, Func<string, bool> isntAllowed)
        {
            assembly.GetReferencedAssemblies().Select(x => x.Name).Where(n => isntAllowed(n)).ShouldBeEmpty();
        }
    }
}