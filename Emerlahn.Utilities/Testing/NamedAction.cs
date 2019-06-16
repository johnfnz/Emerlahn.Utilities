using System;

namespace Emerlahn.Utilities.Testing
{
    public class NamedAction
    {
        public string Name { get; }
        public Action Action { get; }

        public NamedAction(string name, Action action)
        {
            Name = name;
            Action = action;
        }
    }
}