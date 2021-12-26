using Hardwares.Domain;
using Hardwares.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hardwares.MVC.Infrastructure.Mapping;

namespace Hardwares.MVC.Controllers
{
    public class HardwaresController : Controller
    {
        private readonly IRepository<Hardware> _hardwares;
        private readonly ILogger _logger;

        public HardwaresController(IRepository<Hardware> hardwares, ILogger<HardwaresController> logger)
        {
            _hardwares = hardwares;
            _logger = logger;
        }

        // GET: HarwareController
        public async Task<ActionResult> Index()
        {
            try
            {
                var hardwares = await _hardwares.GetAllAsync().ConfigureAwait(false);
                var hardwaresViewModel = hardwares.ToView();
            
                return View(hardwaresViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения списка оборудования {0}", ex.Message);
                throw;
            }
        }

        // GET: HarwareController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HarwareController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HarwareController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HarwareController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HarwareController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HarwareController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HarwareController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
