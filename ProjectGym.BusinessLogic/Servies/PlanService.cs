using Gymy.BusinessLogic.Common;
using ProjectGym.BusinessLogic.ViewModel.Plans;

namespace ProjectGym.BusinessLogic.Servies
{
    public class PlanService(IPlanRepository planRepository) : IPlanService
    {
        public async Task<Result<IReadOnlyList<PlanViewModel>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var plans = await planRepository.GetAllAsync(cancellationToken);

            var planList = plans.Select(p => new PlanViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                DurationDays = p.DurationDays,
                IsActive = p.IsActive
            }).ToList();

            return Result<IReadOnlyList<PlanViewModel>>.Success(planList);
        }

        public async Task<Result<PlanViewModel>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var plan = await planRepository.GetByIdAsync(id, cancellationToken);

            if (plan is null)
            {
                return Result<PlanViewModel>.Failure("Plan not found.");
            }

            var viewModel = new PlanViewModel
            {
                Id = plan.Id,
                Name = plan.Name,
                Price = plan.Price,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                IsActive = plan.IsActive
            };

            return Result<PlanViewModel>.Success(viewModel);
        }

        // Get Data To Edit
        public async Task<Result<EditPlanViewModel>> GetForUpdateAsync(int id, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(id, cancellationToken);

            if (plan is null)
            {
                return Result<EditPlanViewModel>.Failure("Plan not found.");
            }

            var viewModel = new EditPlanViewModel
            {
                PlanName = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price
            };

            return Result<EditPlanViewModel>.Success(viewModel);
        }

        // Post Update
        public async Task<Result> UpdateAsync(int id, EditPlanViewModel model, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(id, cancellationToken);
            if (plan is null)
            {
                return Result.Failure("Plan not found.");
            }

            if (plan.Name != model.PlanName)
                return Result.Failure("Name cannot be changed");

            plan.Description = model.Description;
            plan.DurationDays = model.DurationDays;
            plan.Price = model.Price;

            await planRepository.SaveChangesAsync(cancellationToken);
            return Result.Success();
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

            return Result.Success();
        }
    }
}
