using System;

namespace PropterHoc
{
    public class MetaStepRegistry : IMetaStepRegistry
    {
        private readonly Dictionary<string, string[]> registered;

        public MetaStepRegistry(IEnumerable<KeyValuePair<string, string[]>> registered)
        {
            this.registered = new Dictionary<string, string[]>(registered);
        }

        public bool TryResolveMetaStep(
            string metaStepName,
            out IReadOnlyList<string> resolvedSteps)
        {
            var resolved = new List<string>();
            resolvedSteps = resolved.AsReadOnly();

            if (!registered.TryGetValue(metaStepName, out var steps))
            {
                return false;
            }

            resolved.AddRange(steps);
            return true;
        }
    }
}
