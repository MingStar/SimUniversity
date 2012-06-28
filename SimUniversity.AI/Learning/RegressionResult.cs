namespace MingStar.SimUniversity.AI.Learning
{
    public sealed class RegressionResult
    {
        public RegressionResult(TerminationReason terminationReason, double[] constants, double errorValue,
                                int evaluationCount)
        {
            TerminationReason = terminationReason;
            Constants = constants;
            ErrorValue = errorValue;
            EvaluationCount = evaluationCount;
        }

        public TerminationReason TerminationReason { get; private set; }
        public double[] Constants { get; private set; }
        public double ErrorValue { get; private set; }
        public int EvaluationCount { get; private set; }
    }

    public enum TerminationReason
    {
        MaxFunctionEvaluations,
        Converged,
        Unspecified
    }
}