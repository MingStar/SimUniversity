namespace MingStar.SimUniversity.AI.Learning
{
    public sealed class SimplexConstant
    {
        // The value of the constant
        public SimplexConstant(double value, double initialPerturbation)
        {
            Value = value;
            InitialPerturbationScale = initialPerturbation;
        }

        public double Value { get; set; }
        // The size of the initial perturbation
        public double InitialPerturbationScale { get; set; }
    }
}