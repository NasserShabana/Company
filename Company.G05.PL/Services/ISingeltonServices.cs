namespace Company.G05.PL.Services
{
    public interface ISingeltonServices
    {
        public Guid Guid { get; set; }
        public string GetGuid();
    }
}
