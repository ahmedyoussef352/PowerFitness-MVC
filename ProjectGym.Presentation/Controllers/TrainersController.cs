using Microsoft.AspNetCore.Mvc;
using ProjectGym.BusinessLogic.Comman;
using ProjectGym.BusinessLogic.Servies;
using ProjectGym.BusinessLogic.ViewModel.Members;
using ProjectGym.BusinessLogic.ViewModel.Trainers;

namespace ProjectGym.Presentation.Controllers;

public class TrainersController(ITrainerService trainers) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var items = await trainers.GetAllAsync(cancellationToken);
        return View(items);
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        //var items = await members.AddAsync();
        return View();
    }
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
            ModelState.AddModelError(string.Empty, result.Message);
            TempData["Error"] = "Cannot Create Member.";
            return View(model);
        }
        //var items = await members.AddAsync();
        TempData["Success"] = "Member Created Successfully.";
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var trainer = await trainers.GetDetailsById(id, cancellationToken);
        if (trainer == null)
        {
            return NotFound();
        }
        return View(trainer);
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var trainer = await trainers.GetForUpdateAsync(id, cancellationToken);
        if (trainer == null)
        {
            return NotFound();
        }
        return View(trainer);
    }
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
            ModelState.AddModelError(string.Empty, result.Message);
            TempData["Error"] = result.Message;
            return View(model);
        }
        //var items = await members.AddAsync();
        TempData["Success"] = "Trainer Update Successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var item = await trainers.GetDetailsById(id, cancellationToken);
        if (item == null)
        {
            return NotFound();
        }
        ViewBag.TrainerId = id;
        return View(item);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var result = await trainers.DeleteAsync(id, cancellationToken);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            TempData["Error"] = "Cannot Delete Trainer.";
            return RedirectToAction(nameof(Index));
        }
        TempData["Success"] = "Trainer Deleted Successfully.";
        return RedirectToAction(nameof(Index));
    }
}
