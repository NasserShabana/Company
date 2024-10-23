namespace Company.G05.PL.Services
{
    public interface ITransientServices
    {
        public Guid Guid { get; set; }
        public string GetGuid();
    }
}
