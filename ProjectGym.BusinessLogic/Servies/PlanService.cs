using ProjectGym.BusinessLogic.Comman;
using ProjectGym.BusinessLogic.ViewModel.Plans;

namespace ProjectGym.BusinessLogic.Servies
{
    public class PlanService(IPlanRepository planRepository) : IPlanService
    {
        public async Task<IEnumerable<PlanViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var plans = await planRepository.GetAllAsync(cancellationToken);

            return plans.Select(p => new PlanViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                DurationDays = p.DurationDays,
                IsActive = p.IsActive
            });
        }

        public Task<PlanViewModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var plan = planRepository.GetByIdAsync(id, cancellationToken);

            return plan.ContinueWith(t =>
            {
                var p = t.Result;
                if (p is null)
                {
                    return null!;
                }
                return new PlanViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    DurationDays = p.DurationDays,
                    IsActive = p.IsActive
                };
            }, cancellationToken);
        }
        // Get Data To Edit
        public async Task<EditPlanViewModel?> GetForUpdateAsync(int id, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(id, cancellationToken);

            if (plan is null)
            {
                return null;
            }

            return new EditPlanViewModel
            {
                PlanName = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price


            };
        }
        // Post Update
        public async Task<Result> UpdateAsync(int id, EditPlanViewModel model, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(id, cancellationToken);
            if (plan is null)
            {
                return Result.Failure("plan not found.");
            }
            if (plan.Name != model.PlanName)
                return Result.Failure("Name cannot be changed");

            plan.Description = model.Description;
            plan.DurationDays = model.DurationDays;
            plan.Price = model.Price;

            await planRepository.SaveChangesAsync(cancellationToken);
            return Result.Success("Plan updated Successfully!");
        }

        public async Task<Result> ChangeStatusStatusAsync(int id, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(id, cancellationToken);
            if (plan is null)
            {
                return Result.Failure("Plan not found.");
            }

            plan.IsActive = !plan.IsActive;

            await planRepository.SaveChangesAsync(cancellationToken);

            string statusMessage = plan.IsActive ? "Activate" : "Deactivate";
            return Result.Success($"Plan {statusMessage} successfully!");
        }
    }
}
