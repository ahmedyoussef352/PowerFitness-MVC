using Gymy.BusinessLogic.Common;
using ProjectGym.BusinessLogic.Comman;
using ProjectGym.BusinessLogic.ViewModel.Members;
using ProjectGym.BusinessLogic.ViewModel.Trainers;
using ProjectGym.DataAccess.Entities;
using ProjectGym.DataAccess.Entities.ValueObjects;
using ProjectGym.DataAccess.Repositories;

namespace ProjectGym.BusinessLogic.Servies;

public class TrainerService(IUnitOfWork unitOfWork, IDateTimeProvider clock) : ITrainerService
{
    public async Task<Result<IReadOnlyList<TrainerIndexViewModel>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var trainers = await unitOfWork.Trainers.GetAllAsync(cancellationToken);

        var trainerList = trainers.Select(t => new TrainerIndexViewModel
        {
            Id = t.Id,
            Name = t.Name,
            Email = t.Email,
            Phone = t.Phone,
            Specialties = t.Speciality.ToString()
        }).ToList();

        return Result<IReadOnlyList<TrainerIndexViewModel>>.Success(trainerList);
    }

    public async Task<Result> CreateAsync(CreateTrainerViewModel model, CancellationToken cancellationToken = default)
    {
        var email = model.Email.Trim().ToLower();
        var name = model.Name.Trim().ToLower();
        var phone = model.Phone.Trim();

        if (await unitOfWork.Trainers.IsEmailTakenAsync(email: email, cancellationToken: cancellationToken))
        {
            return Result.Failure("A Trainer with this email already exists.");
        }

        if (await unitOfWork.Trainers.IsPhoneTakenAsync(phone: phone, cancellationToken: cancellationToken))
        {
            return Result.Failure("A Trainer with this phone number already exists.");
        }

        if (model.Gender is null)
        {
            return Result.Failure("Invalid gender selection.");
        }

        var trainer = new Trainer
        {
            Name = name,
            Email = email,
            Phone = phone,
            Gender = model.Gender.Value,
            Speciality = model.Specialties!.Value,
            DateOfBirth = model.DateOfBirth,
            HireDate = DateOnly.FromDateTime(clock.UtcNow),
            Address = new Address
            {
                BuildingNumber = model.BuildingNumber,
                City = model.City,
                Street = model.Street
            },
        };

        await unitOfWork.Trainers.AddAsync(trainer, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<TrainerDetailsViewModel>> GetDetailsByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var trainer = await unitOfWork.Trainers.GetByIdAsync(
            id: id,
            cancellationToken: cancellationToken
        );

        if (trainer is null)
        {
            return Result<TrainerDetailsViewModel>.Failure("Trainer not found.");
        }

        var viewModel = new TrainerDetailsViewModel
        {
            Id = trainer.Id,
            Name = trainer.Name,
            Email = trainer.Email,
            Phone = trainer.Phone,
            DateOfBirth = trainer.DateOfBirth.ToShortDateString(),
            Address = $"{trainer.Address.BuildingNumber} - {trainer.Address.Street} - {trainer.Address.City}",
            Specialties = trainer.Speciality.ToString(),
        };

        return Result<TrainerDetailsViewModel>.Success(viewModel);
    }

    // Get Data To Edit
    public async Task<Result<EditTrainerViewModel>> GetForUpdateAsync(int id, CancellationToken cancellationToken)
    {
        var trainer = await unitOfWork.Trainers.GetByIdAsync(id, cancellationToken);

        if (trainer is null)
        {
            return Result<EditTrainerViewModel>.Failure("Trainer not found.");
        }

        var viewModel = new EditTrainerViewModel
        {
            Name = trainer.Name,
            Email = trainer.Email,
            Phone = trainer.Phone,
            BuildingNumber = trainer.Address.BuildingNumber,
            Street = trainer.Address.Street,
            City = trainer.Address.City,
            Specialties = trainer.Speciality,
        };

        return Result<EditTrainerViewModel>.Success(viewModel);
    }

    // Post Update
    public async Task<Result> UpdateAsync(int id, EditTrainerViewModel model, CancellationToken cancellationToken)
    {
        var trainer = await unitOfWork.Trainers.GetByIdAsync(id, cancellationToken);
        if (trainer is null)
        {
            return Result.Failure("Trainer not found.");
        }

        if (trainer.Name != model.Name)
            return Result.Failure("Name cannot be changed");

        var email = model.Email.Trim().ToLowerInvariant();
        var phone = model.Phone;

        if (await unitOfWork.Trainers.IsEmailTakenAsync(email, excludeId: id))
        {
            return Result.Failure("A Trainer with this email already exists.");
        }
        if (await unitOfWork.Trainers.IsPhoneTakenAsync(phone, excludeId: id))
        {
            return Result.Failure("A Trainer with this phone number already exists.");
        }

        trainer.Email = email;
        trainer.Phone = phone;
        trainer.Speciality = model.Specialties!.Value;

        trainer.Address = new Address
        {
            BuildingNumber = model.BuildingNumber,
            City = model.City.Trim(),
            Street = model.Street.Trim()
        };

        await unitOfWork.CommitAsync(cancellationToken);
        return Result.Success();
    }

    // Delete Trainer 
    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var trainer = await unitOfWork.Trainers.GetDetailsByIdAsync(id,
            includes: m => m.Sessions,
            cancellationToken: cancellationToken);

        if (trainer is null)
            return Result.Failure("Trainer not found");

        if (await unitOfWork.Trainers.HasUpCommingSessionAsync(id, cancellationToken: cancellationToken))
            return Result.Failure("Trainer has upcoming sessions and cannot be deleted");

        await unitOfWork.Trainers.SoftDeleteAsync(trainer, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
