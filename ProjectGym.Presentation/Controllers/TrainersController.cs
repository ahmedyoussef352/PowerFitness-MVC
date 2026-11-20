using Gymy.BusinessLogic.Common;
using Microsoft.AspNetCore.Mvc;
using ProjectGym.BusinessLogic.Servies;
using ProjectGym.BusinessLogic.ViewModel.Members;
using ProjectGym.BusinessLogic.ViewModel.Trainers;

namespace ProjectGym.Presentation.Controllers;

public class TrainersController(ITrainerService trainers) : Controller
{
    // Trainers View
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var result = await trainers.GetAllAsync(cancellationToken);

        if (!result.IsSuccess)
        {
            TempData["Error"] = result.Error ?? "Cannot load trainers.";
            return View(new List<TrainerIndexViewModel>());
        }

        return View(result.Value);
    }

    // Add Trainer View
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // Post Add Trainer View
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateTrainerViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        Result result = await trainers.CreateAsync(model, cancellationToken);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Cannot create trainer.");
            TempData["Error"] = "Cannot Create Trainer.";
            return View(model);
        }

        TempData["Success"] = "Trainer Created Successfully.";
        return RedirectToAction(nameof(Index));
    }

    // Details Trainer
    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var result = await trainers.GetDetailsByIdAsync(id, cancellationToken);

        if (!result.IsSuccess || result.Value == null)
        {
            return NotFound();
        }

        return View(result.Value);
    }

    // Get Edit Trainer View
    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var result = await trainers.GetForUpdateAsync(id, cancellationToken);

        if (!result.IsSuccess || result.Value == null)
        {
            return NotFound();
        }

        return View(result.Value);
    }

    // Post Edit Trainer
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromRoute] int id, EditTrainerViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        Result result = await trainers.UpdateAsync(id: id, model: model, cancellationToken: cancellationToken);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Cannot update trainer.");
            TempData["Error"] = result.Error ?? "Cannot Update Trainer.";
            return View(model);
        }

        TempData["Success"] = "Trainer Updated Successfully.";
        return RedirectToAction(nameof(Index));
    }

    // Get Delete Trainer View
    [HttpGet]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await trainers.GetDetailsByIdAsync(id, cancellationToken);

        if (!result.IsSuccess || result.Value == null)
        {
            return NotFound();
        }

        ViewBag.TrainerId = id;
        return View(result.Value);
    }

    // Post Delete Trainer View
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var result = await trainers.DeleteAsync(id, cancellationToken);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Cannot delete trainer.");
            TempData["Error"] = "Cannot Delete Trainer.";
            return RedirectToAction(nameof(Index));
        }

        TempData["Success"] = "Trainer Deleted Successfully.";
        return RedirectToAction(nameof(Index));
    }
}