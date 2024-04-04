using CONSUMIR_API_TEST.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace CONSUMIR_API_TEST.Servicios
{
    public class Servicio_API : IServicio_API
    {
        private static string _usuario;
        private static string _clave;
        private static string _baseUrl;
        private static string _token;

        public Servicio_API()
        {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _usuario = builder.GetSection("ApiSetting:usuario").Value;
            _clave = builder.GetSection("ApiSetting:clave").Value;
            _baseUrl = builder.GetSection("ApiSetting:baseUrl").Value;
        }

        //USAR REFERENCIAS 
        public async Task Autenticar()
        {

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var credenciales = new Credencial() { usuario = _usuario, clave = _clave };

            var content = new StringContent(JsonConvert.SerializeObject(credenciales), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync("api/Autenticacion/Validar", content);
            var json_respuesta = await response.Content.ReadAsStringAsync();

            var resultado = JsonConvert.DeserializeObject<ResultadoCredencial>(json_respuesta);
            _token = resultado.token;
        }

        public async Task<List<Device>> ListaDevice()
        {
            List<Device> listadevices = new List<Device>();

            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync("api/Device/Lista");

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApiDevice>(json_respuesta);
                listadevices = resultado.response;
            }

            return listadevices;
        }

        public async Task<List<DeviceType>> ListaDeviceType()
        {
            List<DeviceType> listadevicetypes = new List<DeviceType>();

            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync("api/DeviceType/Lista");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApiDeviceType>(json_respuesta);
                listadevicetypes = resultado.response;
            }

            return listadevicetypes;
        }
        public async Task<Device> ObtenerDevice(int idDevice)
        {
            Device objeto = new Device();

            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync($"api/Device/Obtener/{idDevice}");

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApiUnoDevice>(json_respuesta);
                objeto = resultado.response;
            }

            return objeto;
        }

        public async Task<DeviceType> ObtenerDeviceType(int idDeviceType)
        {
            DeviceType objeto = new DeviceType();

            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync($"api/DeviceType/Obtener/{idDeviceType}");

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApiUnoDeviceType>(json_respuesta);
                objeto = resultado.response;
            }

            return objeto;
        }

        public async Task<bool> GuardarDevice(Device objeto)
        {
            bool respuesta = false;

            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("api/Device/Guardar/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> GuardarDeviceType(DeviceType objeto)
        {
            bool respuesta = false;

            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("api/DeviceType/Guardar/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> EditarDevice(Device objeto)
        {
            bool respuesta = false;

            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync("api/Device/Editar/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> EditarDeviceType(DeviceType objeto)
        {
            bool respuesta = false;

            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync("api/DeviceType/Editar/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> EliminarDevice(int idDevice)
        {
            bool respuesta = false;

            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await cliente.DeleteAsync($"api/Device/Eliminar/{idDevice}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> EliminarDeviceType(int idDeviceType)
        {
            bool respuesta = false;

            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await cliente.DeleteAsync($"api/DeviceType/Eliminar/{idDeviceType}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }
    }
}
