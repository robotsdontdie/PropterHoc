namespace PropterHoc
{
    public interface IMetaStepRegistry
    {
        bool TryResolveMetaStep(string metaStepName, out IReadOnlyList<string> resolvedSteps);
    }
}