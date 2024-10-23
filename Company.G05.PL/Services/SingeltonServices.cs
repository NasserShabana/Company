namespace Company.G05.PL.Services
{
    public class SingeltonServices : ISingeltonServices
    {
        public Guid Guid { get; set; }

        public SingeltonServices()
        {
            Guid = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
