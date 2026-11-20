using Microsoft.AspNetCore.Mvc;
using ProjectGym.BusinessLogic.Servies;
using ProjectGym.BusinessLogic.ViewModel.Plans;

namespace ProjectGym.Presentation.Controllers;

public class PlansController(IPlanService plans) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var result = await plans.GetAllAsync(cancellationToken);

        if (!result.IsSuccess)
        {
            TempData["Error"] = result.Error ?? "Cannot load plans.";
            return View(new List<PlanViewModel>());
        }

        return View(result.Value);
    }

    // Plan Details View
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        var result = await plans.GetByIdAsync(id, cancellationToken);

        if (!result.IsSuccess || result.Value is null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(result.Value);
    }

    // Get Edit Plan
    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var result = await plans.GetForUpdateAsync(id, cancellationToken);

        if (!result.IsSuccess || result.Value == null)
        {
            return NotFound();
        }

        return View(result.Value);
    }

    // Post Edit Plan
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
            ModelState.AddModelError(string.Empty, result.Error ?? "Cannot update plan.");
            TempData["Error"] = "Cannot Update Plan.";
            return View(model);
        }

        TempData["Success"] = "Plan Updated Successfully.";
        return RedirectToAction(nameof(Index));
    }

    // Plan Change Status 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleStatus(int id, CancellationToken cancellationToken)
    {
        var result = await plans.ChangeStatusStatusAsync(id, cancellationToken);

        if (!result.IsSuccess)
        {
            TempData["Error"] = result.Error ?? "Cannot change plan status.";
        }
        else
        {
            TempData["Success"] = "Plan status changed successfully!";
        }

        return RedirectToAction(nameof(Index));
    }
}