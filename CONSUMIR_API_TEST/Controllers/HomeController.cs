using CONSUMIR_API_TEST.Models;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using System.Text;


using Newtonsoft.Json;

using CONSUMIR_API_TEST.Servicios;

namespace CONSUMIR_API_TEST.Controllers
{
    public class HomeController : Controller
    {
        private IServicio_API _servicioApi;

        public HomeController(IServicio_API servicioApi)
        {
            _servicioApi = servicioApi;
        }

        public async Task<IActionResult> Index()
        {
            List<Device> lista = await _servicioApi.ListaDevice();
            return View(lista);
        }

        // Llamaremos a este controlador cuando queramos visualizar la pagina de DeviceType
        public async Task<IActionResult> IndexDeviceType()
        {
            List<DeviceType> lista = await _servicioApi.ListaDeviceType();
            return View(lista);
        }


        //VA A CONTROLAR LAS FUNCIONES DE GUARDAR O EDITAR
        public async Task<IActionResult> Device(int idDevice)
        {

            Device modelo_device = new Device();
            ViewBag.Accion = "Nuevo Device";

            if (idDevice != 0)
            {
                ViewBag.Accion = "Editar Device";
                modelo_device = await _servicioApi.ObtenerDevice(idDevice);               
            }

            return View(modelo_device);
        }

        public async Task<IActionResult> DeviceType(int idDevice)
        {

            DeviceType modelo_device = new DeviceType();
            ViewBag.Accion = "Nuevo Device";

            if (idDevice != 0)
            {
                ViewBag.Accion = "Editar Device Type";
                modelo_device = await _servicioApi.ObtenerDeviceType(idDevice);
            }

            return View(modelo_device);
        }

        //VA A CONTROLAR LAS FUNCIONES DE GUARDAR O EDITAR
        public async Task<IActionResult> GuardarCambiosDeviceType(int idDeviceType)
        {

            DeviceType modelo_devicetype = new DeviceType();

            ViewBag.Accion = "Nuevo Device Type";

            if (idDeviceType != 0)
            {

                ViewBag.Accion = "Editar Device Type";
                modelo_devicetype = await _servicioApi.ObtenerDeviceType(idDeviceType);
            }

            return View(modelo_devicetype);
        }
        [HttpPost]
        public async Task<IActionResult> GuardarCambiosDevice(Device ob_Device)
        {

            bool respuesta;

            if (ob_Device.IdDevice == 0)
            {
                respuesta = await _servicioApi.GuardarDevice(ob_Device);
            }
            else
            {
                respuesta = await _servicioApi.EditarDevice(ob_Device);
            }


            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();

        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambiosDeviceType(DeviceType ob_devicetype)
        {

            bool respuesta;

            if (ob_devicetype.IdDevice == 0)
            {
                respuesta = await _servicioApi.GuardarDeviceType(ob_devicetype);
            }
            else
            {
                respuesta = await _servicioApi.EditarDeviceType(ob_devicetype);
            }

            if (respuesta)
                return RedirectToAction("IndexDeviceType");
            else
                return NoContent();

        }

        [HttpGet]
        public async Task<IActionResult> EliminarDevice(int idDevice)
        {

            var respuesta = await _servicioApi.EliminarDevice(idDevice);

            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> EliminarDeviceAutorizacion(int idDevice)
        {
            Device modelo_device = new Device();

            ViewBag.Accion = "Eliminar Device";
            modelo_device = await _servicioApi.ObtenerDevice(idDevice);
           
            return View(modelo_device);
        }

        public async Task<IActionResult> EliminarDeviceTypeAutorizacion(int idDevice)
        {
            DeviceType modelo_device = new DeviceType();

            ViewBag.Accion = "Eliminar Device Type";
            modelo_device = await _servicioApi.ObtenerDeviceType(idDevice);

            return View(modelo_device);
        }

        [HttpGet]
        public async Task<IActionResult> EliminarDeviceType(int idDeviceType)
        {

            var respuesta = await _servicioApi.EliminarDeviceType(idDeviceType);

            if (respuesta)
                return RedirectToAction("IndexDeviceType");
            else
                return NoContent();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}