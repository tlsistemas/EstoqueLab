using EstoqueLab.UI.Helpers;
using EstoqueLab.UI.Models;
using EstoqueLab.Uteis.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace EstoqueLab.UI.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly IApiService _api;
        private readonly ILogger<CategoriaController> _logger;

        public CategoriaController(IApiService api, 
            ILogger<CategoriaController> logger)
        {
            _api = api;
            _logger = logger;
        }

        public async Task<ActionResult> IndexAsync()
        {
            var model = new List<Categoria>();
            var result = await _api.Get(Methods.Categoria);
            if(!ReferenceEquals(result.Data, null))
            {
                model = JsonConvert.DeserializeObject<List<Categoria>>(result.Data.ToString());
            }
            return View(model);
        }

        public async Task<ActionResult> DetailsAsync(string key)
        {
            var model = new List<Categoria>();
            var param = "?Key=" + key;
            var result = await _api.Get(Methods.Categoria + param);
            if (!ReferenceEquals(result.Data, null))
            {
                model = JsonConvert.DeserializeObject<List<Categoria>>(result.Data.ToString());
            }
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Categoria model)
        {
            try
            {
                var json = JsonConvert.SerializeObject(model);
                var result = await _api.Post(Methods.Categoria, json);
                if (!ReferenceEquals(result.Data, null))
                {
                    TempData["sucesso_categoria_lista"] = "Categoria cadastrada com sucesso!";
                    return RedirectToAction("Index", "Categoria");
                }
                else
                {
                    TempData["erro_categoria_lista"] = "Categoria não cadastrada!";
                    return RedirectToAction("Index", "Categoria");
                }                
            }
            catch
            {
                TempData["erro_categoria_lista"] = "Categoria não cadastrada!";
                return RedirectToAction("Index", "Categoria");
            }
        }

        public async Task<ActionResult> EditAsync(string key)
        {
            var model = new List<Categoria>();
            var param = "?Key=" + key;
            var result = await _api.Get(Methods.Categoria + param);
            if (!ReferenceEquals(result.Data, null))
            {
                model = JsonConvert.DeserializeObject<List<Categoria>>(result.Data.ToString());
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(Categoria model)
        {
            try
            {
                var json = JsonConvert.SerializeObject(model);
                var result = await _api.Put(Methods.Categoria, json);
                if (!ReferenceEquals(result.Data, null))
                {
                    TempData["sucesso_categoria_lista"] = "Categoria atualizado com sucesso!";
                    return RedirectToAction("Index", "Categoria");
                }
                else
                {
                    TempData["erro_categoria_lista"] = "Categoria não atualizado!";
                    return RedirectToAction("Index", "Categoria");
                }
            }
            catch
            {
                TempData["erro_categoria_lista"] = "Categoria não atualizado!";
                return RedirectToAction("Index", "Categoria");
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}
