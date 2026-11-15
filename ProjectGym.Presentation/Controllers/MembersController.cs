using Microsoft.AspNetCore.Mvc;
using ProjectGym.BusinessLogic.Comman;
using ProjectGym.BusinessLogic.Servies;
using ProjectGym.BusinessLogic.ViewModel.Members;

namespace ProjectGym.Presentation.Controllers
{
    public class MembersController(IMemberService members) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var items = await members.GetAllAsync(cancellationToken);
            return View(items);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            //var items = await members.AddAsync();
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
                ModelState.AddModelError(string.Empty, result.Message);
                TempData["Error"] = "Cannot Create Member.";
                return View(model);
            }
            //var items = await members.AddAsync();
            TempData["Success"] = "Member Created Successfully.";
            return RedirectToAction(nameof(Index));
        }
        // Member Details
        public async Task<IActionResult> MemberDetails(int id, CancellationToken cancellationToken)
        {
            var item = await members.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> MemberHealthRecord(int id, CancellationToken cancellationToken)
        {
            var item = await members.GetHealthRecordAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // Edit Member
        [HttpGet]
        public async Task<IActionResult> EditMember(int id, CancellationToken cancellationToken)
        {
            var item = await members.GetForUpdateAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
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
                ModelState.AddModelError(string.Empty, result.Message);
                TempData["Error"] = "Cannot Update Member.";
                return View(model);
            }
            TempData["Success"] = "Member Updated Successfully.";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> DeleteMember(int id, CancellationToken cancellationToken)
        {
            var item = await members.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound();
            }
            ViewBag.MemberId = id;
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            var result = await members.DeleteAsync(id, cancellationToken);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                TempData["Error"] = "Cannot Delete Member.";
                return RedirectToAction(nameof(Index));
            }
            TempData["Success"] = "Member Deleted Successfully.";
            return RedirectToAction(nameof(Index));
        }

    }
}
