using System.Collections.Generic;

namespace GameSystem.Initialization
{
    internal class InitGroup
    {
        internal string groupName { get; private set; }

        internal int Count => queue.Count;

        private List<InitUnit> queue = new();

        internal void AddUnit(InitUnit unit)
        {
            queue.Add(unit);
        }
        internal bool GetNextUnit(out InitUnit unit)
        {
            if (queue.Count == 0)
            {
                unit = null;
                return false;
            }

            unit = queue[0];
            queue.Remove(unit);
            return true;
        }

        internal InitGroup(string name)
        {
            groupName = name;
        }
    }
}