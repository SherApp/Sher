namespace Sher.SharedKernel.Options
{
    public class PasswordHashingOptions
    {
        public int Iterations { get; set; } = 2;
        public int DegreeOfParallelism { get; set; } = 4;
        public int MemorySize { get; set; } = 1024 * 1024;
    }
}