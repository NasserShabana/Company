namespace Company.G05.PL.Services
{
    public interface IScopedServices
    {
        public Guid Guid { get; set; }
        public string GetGuid();
    }
}
