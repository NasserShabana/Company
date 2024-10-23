namespace Company.G05.PL.Services
{
    public class TransientServices : ITransientServices
    {
        public Guid Guid { get; set; }

        public TransientServices()
        {
            Guid = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
