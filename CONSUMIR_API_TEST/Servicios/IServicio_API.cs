using CONSUMIR_API_TEST.Models;

namespace CONSUMIR_API_TEST.Servicios
{
    public interface IServicio_API
    {
        Task<List<Device>> ListaDevice();
        Task<List<DeviceType>> ListaDeviceType();
        Task<Device> ObtenerDevice(int IdDevice);
        Task<DeviceType> ObtenerDeviceType(int IdDeviceType);
        Task<bool> GuardarDevice(Device objeto);
        Task<bool> GuardarDeviceType(DeviceType objeto);
        Task<bool> EditarDevice(Device objeto);
        Task<bool> EditarDeviceType(DeviceType objeto);
        Task<bool> EliminarDevice(int IdDevice);
        Task<bool> EliminarDeviceType(int IdDeviceType);


    }
}
