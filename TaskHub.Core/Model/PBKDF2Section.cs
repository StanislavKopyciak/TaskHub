namespace TaskHub.Core.Model
{
    public class PBKDF2Section
    {
        public int IterationCount { get; set; }
        public int SaltSize { get; set; }
        public int KeySize { get; set; }
    }

}
