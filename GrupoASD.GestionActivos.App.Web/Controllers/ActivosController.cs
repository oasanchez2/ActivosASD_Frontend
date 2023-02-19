using GrupoASD.GestionActivos.App.Servicios.Entities;
using GrupoASD.GestionActivos.App.Servicios.Facade;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrupoASD.GestionActivos.App.Web.Controllers
{
    public class ActivosController : Controller
    {
        private readonly ILogger _logger;
        private readonly IAsdGestionActivosApi _asdGestionActivosApi;

        public ActivosController(ILogger<ActivosController> logger, IAsdGestionActivosApi asdGestionActivosApi)
        {
            _logger = logger;
            _asdGestionActivosApi = asdGestionActivosApi;
        }

        // GET: ActivosController
        public async Task<IActionResult> Index()
        {
            try
            {
                var resultadoActivos = await _asdGestionActivosApi.ActivosObtenerAsync();

                if (resultadoActivos.HttpStatus == System.Net.HttpStatusCode.OK)
                {
                    var activos = JsonConvert.DeserializeObject<IEnumerable<ActivosModel>>(resultadoActivos.JsonResultado);
                    return View(activos.ToList());
                }
                else
                {
                    JObject resultadoObjet = JObject.Parse(resultadoActivos.JsonResultado);
                    ViewBag.ErrorMessage = "Estamos Presentando inconvientes. Intente mas tarde. " + resultadoObjet["mensaje"].ToString();
                    return View("ErrorCritico");
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(0, "Exception. {0}", ex.Message);
                ViewBag.ErrorMessage = "Estamos Presentando inconvientes. Intente mas tarde. ";
                return View("ErrorCritico");
            }
        }

        // GET: ActivosController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var resultadoActivo = await _asdGestionActivosApi.ActivosObtenerPorIdAsync(id);
                if (resultadoActivo.HttpStatus == System.Net.HttpStatusCode.OK)
                {
                    var activo = JsonConvert.DeserializeObject<ActivosModel>(resultadoActivo.JsonResultado);
                    return View(activo);
                }
                else
                {
                    JObject resultadoObjet = JObject.Parse(resultadoActivo.JsonResultado);
                    ViewBag.ErrorMessage = "Estamos Presentando inconvientes. Intente mas tarde. " + resultadoObjet["mensaje"].ToString();
                    return View("ErrorCritico");
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(0, "Exception. {0}", ex.Message);
                ViewBag.ErrorMessage = "Estamos Presentando inconvientes. Intente mas tarde. ";
                return View("ErrorCritico");
            }
        }

        // GET: ActivosController/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                await CargarMaestroListaDesplegable();

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(0, "Exception. {0}", ex.Message);
                ViewBag.ErrorMessage = "Estamos Presentando inconvientes. Intente mas tarde. ";
                return View("ErrorCritico");
            }
        }

        // POST: ActivosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActivosModel model)
        {
            await CargarMaestroListaDesplegable();
            try
            {
                if (ModelState.IsValid)
                {
                    RespuestaApi resultado = await _asdGestionActivosApi.ActivosInsertar(model);
                    if (resultado.HttpStatus == System.Net.HttpStatusCode.OK)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        JObject resultadoObjet = JObject.Parse(resultado.JsonResultado);
                        ModelState.AddModelError("", "Algo no salio bien. " + resultadoObjet["mensaje"].ToString());
                        return View(model);
                    }

                }
                return View(model);

            }
            catch (Exception ex)
            {
                _logger.LogCritical(0, "Exception. {0}", ex.Message);
                ModelState.AddModelError("", "Estamos Presentando inconvientes. Intente mas tarde. ");
                return View(model);
            }
        }

        // GET: ActivosController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var resultadoActivo = await _asdGestionActivosApi.ActivosObtenerPorIdAsync(id);
                if (resultadoActivo.HttpStatus == System.Net.HttpStatusCode.OK)
                {
                    var activo = JsonConvert.DeserializeObject<ActivosModel>(resultadoActivo.JsonResultado);
                    if (activo == null)
                    {
                        return NotFound();
                    }
                    ActivoUpdateModel model = new ActivoUpdateModel
                    {
                        IdActivo = activo.IdActivo,
                        Serial = activo.Serial,
                        FechaBaja = activo.FechaBaja
                    };

                    return View(model);
                }
                else
                {
                    JObject resultadoObjet = JObject.Parse(resultadoActivo.JsonResultado);
                    ViewBag.ErrorMessage = "Estamos Presentando inconvientes. Intente mas tarde. " + resultadoObjet["mensaje"].ToString();
                    return View("ErrorCritico");
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(0, "Exception. {0}", ex.Message);
                ViewBag.ErrorMessage = "Estamos Presentando inconvientes. Intente mas tarde. ";
                return View("ErrorCritico");
            }
        }

        // POST: ActivosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ActivoUpdateModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RespuestaApi resultado = await _asdGestionActivosApi.ActivosUpdate(model);
                    if (resultado.HttpStatus == System.Net.HttpStatusCode.OK)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        JObject resultadoObjet = JObject.Parse(resultado.JsonResultado);
                        ModelState.AddModelError("", "Algo no salio bien. " + resultadoObjet["mensaje"].ToString());
                        return View(model);
                    }
                }
                return View(model);
           
            }
            catch(Exception ex)
            {
                _logger.LogCritical(0, "Exception. {0}", ex.Message);
                ViewBag.ErrorMessage = "Estamos Presentando inconvientes. Intente mas tarde. ";
                return View("ErrorCritico");
            }
        }

        #region Metodos Privados
        /// <summary>
        /// Consulta en las apis los datos para llenar las listas de los tipos y estados de un activos
        /// </summary>
        /// <returns></returns>
        private async Task CargarMaestroListaDesplegable()
        {
            try
            {
                var listaTiposActivo = await CargarListaTiposDeActivos();
                var listtaEstadosActivo = await CargarListaEstadosDeActivos();
                ViewData["IdEstadoActual"] = new SelectList(listtaEstadosActivo, "IdEstado", "NombreEstado");
                ViewData["IdTipoActivo"] = new SelectList(listaTiposActivo, "IdTipoActivo", "Nombre");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al Cargar listas desplegables. " + ex.Message);
            }
        }

        /// <summary>
        /// Consulta el api y trae el listado de los tipo de un activo
        /// </summary>
        /// <returns></returns>
        private async Task<List<TipoActivoModel>> CargarListaTiposDeActivos()
        {
            List<TipoActivoModel> lista = new List<TipoActivoModel>();
            try
            {
                RespuestaApi respuesta = await _asdGestionActivosApi.TiposActivosObtenerAsync();
                if (respuesta.HttpStatus == System.Net.HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<List<TipoActivoModel>>(respuesta.JsonResultado);
                }
                else
                {
                    JObject resultadoObjet = JObject.Parse(respuesta.JsonResultado);
                    _logger.LogCritical(0, "Error en api. {0}", resultadoObjet["mensaje"].ToString());
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(0, "Exception. {0}", ex.Message);
            }
            return lista;
        }

        /// <summary>
        /// Consulta el api y trae el listado de los estados de un activo
        /// </summary>
        /// <returns></returns>
        private async Task<List<EstadosActivosModel>> CargarListaEstadosDeActivos()
        {
            List<EstadosActivosModel> lista = new List<EstadosActivosModel>();
            try
            {
                RespuestaApi respuesta = await _asdGestionActivosApi.EstadosActivosObtenerAsync();
                if (respuesta.HttpStatus == System.Net.HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<List<EstadosActivosModel>>(respuesta.JsonResultado);
                }
                else
                {
                    JObject resultadoObjet = JObject.Parse(respuesta.JsonResultado);
                    _logger.LogCritical(0, "Error en api. {0}", resultadoObjet["mensaje"].ToString());
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(0, "Exception. {0}", ex.Message);
            }
            return lista;
        }
        #endregion
    }
}
