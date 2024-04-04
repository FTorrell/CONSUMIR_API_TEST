namespace CONSUMIR_API_TEST.Models
{
    public class DeviceType
    {
        public int IdDevice { get; set; }

        public string? Description{ get; set; }

        public string? DisplayName { get; set; }

        public virtual ICollection<Device> Productos { get; set; } = new List<Device>();
    }
}
