using Gymy.BusinessLogic.Common;
using Microsoft.AspNetCore.Mvc;
using ProjectGym.BusinessLogic.Servies;
using ProjectGym.BusinessLogic.ViewModel.Members;

namespace ProjectGym.Presentation.Controllers
{
    public class MembersController(IMemberService members) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var result = await members.GetAllAsync(cancellationToken);

            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Error ?? "Cannot load members.";
                return View(new List<MemberIndexViewModel>());
            }

            return View(result.Value);
        }

        // Add Member View
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Add Member
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMemberViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Result result = await members.CreateAsync(model, cancellationToken);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Error ?? "Cannot create member.");
                TempData["Error"] = "Cannot Create Member.";
                return View(model);
            }

            TempData["Success"] = "Member Created Successfully.";
            return RedirectToAction(nameof(Index));
        }

        // Member Details
        public async Task<IActionResult> MemberDetails(int id, CancellationToken cancellationToken)
        {
            var result = await members.GetByIdAsync(id, cancellationToken);

            if (!result.IsSuccess || result.Value == null)
            {
                return NotFound();
            }

            return View(result.Value);
        }

        // Get Member Health Record
        [HttpGet]
        public async Task<IActionResult> MemberHealthRecord(int id, CancellationToken cancellationToken)
        {
            var result = await members.GetHealthRecordAsync(id, cancellationToken);

            if (!result.IsSuccess || result.Value == null)
            {
                return NotFound();
            }

            return View(result.Value);
        }

        // Get Edit Member View
        [HttpGet]
        public async Task<IActionResult> EditMember(int id, CancellationToken cancellationToken)
        {
            var result = await members.GetForUpdateAsync(id, cancellationToken);

            if (!result.IsSuccess || result.Value == null)
            {
                return NotFound();
            }

            return View(result.Value);
        }

        // Post Edit Member
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMember([FromRoute] int id, EditMemberViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await members.UpdateAsync(id, model, cancellationToken);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Error ?? "Cannot update member.");
                TempData["Error"] = "Cannot Update Member.";
                return View(model);
            }

            TempData["Success"] = "Member Updated Successfully.";
            return RedirectToAction(nameof(Index));
        }

        // Get Delete Member View
        [HttpGet]
        public async Task<IActionResult> DeleteMember(int id, CancellationToken cancellationToken)
        {
            var result = await members.GetForDeleteAsync(id, cancellationToken);

            if (!result.IsSuccess || result.Value == null)
            {
                return NotFound();
            }

            ViewBag.MemberId = id;
            return View(result.Value);
        }

        // Post Delete Member View
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            var result = await members.DeleteAsync(id, cancellationToken);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Error ?? "Cannot delete member.");
                TempData["Error"] = "Cannot Delete Member.";
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = "Member Deleted Successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
