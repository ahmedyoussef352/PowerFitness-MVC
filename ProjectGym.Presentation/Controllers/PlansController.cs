using Microsoft.AspNetCore.Mvc;
using ProjectGym.BusinessLogic.Servies;
using ProjectGym.BusinessLogic.ViewModel.Plans;

namespace ProjectGym.Presentation.Controllers;

public class PlansController(IPlanService plans) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var items = await plans.GetAllAsync(cancellationToken);
        return View(items.ToList());
    }
    public async Task<IActionResult> Details(int id)
    {

        if (id <= 0)
        {
            return NotFound();
        }
        var plan = await plans.GetByIdAsync(id);

        if (plan is null)
        {
            return RedirectToAction(nameof(Index));

        }
        return View(plan);
    }
    // Edit Member
    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var item = await plans.GetForUpdateAsync(id, cancellationToken);
        if (item == null)
        {
            return NotFound();
        }
        return View(item);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromRoute] int id, EditPlanViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var result = await plans.UpdateAsync(id, model, cancellationToken);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            TempData["Error"] = "Cannot Update Member.";
            return View(model);
        }
        TempData["Success"] = "Member Updated Successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleStatus(int id, CancellationToken cancellationToken)
    {
        var result = await plans.ChangeStatusStatusAsync(id, cancellationToken);

        if (!result.IsSuccess)
        {
            TempData["Error"] = result.Message;
        }
        else
        {
            TempData["Success"] = result.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}