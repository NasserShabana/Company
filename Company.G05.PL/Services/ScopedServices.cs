
namespace Company.G05.PL.Services
{
    public class ScopedServices : IScopedServices
    {
        public Guid Guid { get ; set; }

        public ScopedServices() {
        Guid= Guid.NewGuid();
        }
        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
