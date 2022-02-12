using Hardwares.Domain;
using Hardwares.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hardwares.MVC.Infrastructure.Mapping;
using Hardwares.MVC.ViewModels;
using System.Runtime.CompilerServices;

namespace Hardwares.MVC.Controllers
{
    public class HardwaresController : Controller
    {
        private readonly IRepository<Hardware> _hardwaresRepository;
        private readonly ILogger _logger;

        private void LogError(Exception ex, [CallerMemberName] string methodName = null!) => _logger.LogError(ex, "Ошибка выполнения {0}", methodName);

        public HardwaresController(IRepository<Hardware> hardwares, ILogger<HardwaresController> logger)
        {
            _hardwaresRepository = hardwares;
            _logger = logger;
        }

        // GET: HarwareController
        public async Task<IActionResult> Index()
        {
            try
            {
                var hardwares = await _hardwaresRepository.GetAllAsync().ConfigureAwait(false);
                var hardwaresViewModel = hardwares.ToView();
            
                return View(hardwaresViewModel);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        // GET: HarwareController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var hardware = await _hardwaresRepository.GetAsync(id).ConfigureAwait(false);

                if (hardware is null)
                    return NotFound();

                var hardwareViewModel= hardware.ToView();

                return View(hardwareViewModel);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        // GET: HarwareController/Create
        public IActionResult Create() => View("Edit", new HardwareViewModel());

        // GET: HarwareController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var hardware = await _hardwaresRepository.GetAsync(id).ConfigureAwait(false);

                if (hardware is null)
                    return NotFound();

                var hardwareViewModel = hardware.ToView();

                return View(hardwareViewModel);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        // POST: HarwareController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(HardwareViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var hardware = model.FromView()!;

                if (hardware.Id == 0)
                    await _hardwaresRepository.AddAsync(hardware).ConfigureAwait(false);
                else
                    await _hardwaresRepository.UpdateAsync(hardware).ConfigureAwait(false);
                
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        // GET: HarwareController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var hardware = await _hardwaresRepository.GetAsync(id).ConfigureAwait(false);

                if (hardware is null)
                    return NotFound();

                var hardwareViewModel = hardware.ToView();

                return View(hardwareViewModel);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }

        }

        // POST: HarwareController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var hardware = await _hardwaresRepository.GetAsync(id).ConfigureAwait(false);

                if (hardware is null)
                    return NotFound();
                
                await _hardwaresRepository.DeleteAsync(hardware).ConfigureAwait(false); 

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }
    }
}
