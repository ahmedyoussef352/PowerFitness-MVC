using ProjectGym.BusinessLogic.Comman;
using ProjectGym.BusinessLogic.ViewModel.Members;
using ProjectGym.BusinessLogic.ViewModel.Trainers;
using ProjectGym.DataAccess.Entities;
using ProjectGym.DataAccess.Entities.ValueObjects;
using ProjectGym.DataAccess.Repositories;

namespace ProjectGym.BusinessLogic.Servies;

public class TrainerService(ITrainerRepo trainerRepo) : ITrainerService
{
    public async Task<IEnumerable<TrainerIndexViewModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        var trainers = await trainerRepo.GetAllAsync(cancellationToken);

        return trainers.Select(t => new TrainerIndexViewModel
        {
            Id = t.Id,
            Name = t.Name,
            Email = t.Email,
            Phone = t.Phone,
            Specialties = t.Speciality.ToString()
        });
    }
    public async Task<Result> CreateAsync(CreateTrainerViewModel model, CancellationToken cancellationToken = default)
    {
        var email = model.Email.Trim().ToLower();
        var name = model.Name.Trim().ToLower();
        var phone = model.Phone.Trim();

        if (await trainerRepo.IsEmailTakenAsync(email: email, cancellationToken: cancellationToken))
        {
            return Result.Failure("A Trainer with this email already exists.");
        }

        if (await trainerRepo.IsPhoneTakenAsync(phone: phone, cancellationToken: cancellationToken))
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
            HireDate = DateOnly.FromDateTime(DateTime.Now),

            Address = new Address
            {
                BuildingNumber = model.BuildingNumber,
                City = model.City,
                Street = model.Street
            },

        };
        await trainerRepo.AddAsync(trainer, cancellationToken);
        await trainerRepo.SaveChangesAsync(cancellationToken);

        return Result.Success("Trainer added successfully!");
    }

    public async Task<TrainerDetailsViewModel?> GetDetailsById(int id, CancellationToken cancellationToken = default)
    {
        var trainer = await trainerRepo.GetByIdAsync(
            id: id,
            cancellationToken: cancellationToken
            );

        if (trainer is null)
        {
            return null;
        }
        var today = DateOnly.FromDateTime(DateTime.Now);

        return new TrainerDetailsViewModel
        {
            Id = trainer.Id,
            Name = trainer.Name,
            Email = trainer.Email,
            Phone = trainer.Phone,
            DateOfBirth = trainer.DateOfBirth.ToShortDateString(),
            Address = $"{trainer.Address.BuildingNumber} - {trainer.Address.Street} - {trainer.Address.City}",
            Specialties = trainer.Speciality.ToString(),


        };
    }
    // Get Data To Edit
    public async Task<EditTrainerViewModel?> GetForUpdateAsync(int id, CancellationToken cancellationToken)
    {
        var trainer = await trainerRepo.GetByIdAsync(id, cancellationToken);

        if (trainer is null)
        {
            return null;
        }

        return new EditTrainerViewModel
        {
            Name = trainer.Name,
            Email = trainer.Email,
            Phone = trainer.Phone,
            BuildingNumber = trainer.Address.BuildingNumber,
            Street = trainer.Address.Street,
            City = trainer.Address.City,
            Specialties = trainer.Speciality,



        };
    }
    // Post Update
    public async Task<Result> UpdateAsync(int id, EditTrainerViewModel model, CancellationToken cancellationToken)
    {
        var trainer = await trainerRepo.GetByIdAsync(id, cancellationToken);
        if (trainer is null)
        {
            return Result.Failure("Trainer not found.");
        }
        if (trainer.Name != model.Name)
            return Result.Failure("Name cannot be changed");

        var email = model.Email.Trim().ToLowerInvariant();
        var phone = model.Phone;
        if (await trainerRepo.IsEmailTakenAsync(email, excludeId: id))
        {
            return Result.Failure("A Trainer with this email already exists.");
        }
        if (await trainerRepo.IsPhoneTakenAsync(phone, excludeId: id))
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

        await trainerRepo.SaveChangesAsync(cancellationToken);
        return Result.Success("Member updated Successfully!");
    }

    //// Delete Member 
    public async Task<Result> DeleteAsync(int id,
        CancellationToken cancellationToken)
    {
        var trainer = await trainerRepo.GetDetailsByIdAsync(id,
            includes: m => m.Sessions,
           cancellationToken: cancellationToken);


        if (trainer is null)
            return Result.Failure("Trainer not found");

        if (await trainerRepo.
        HasUpCommingSessionAsync(id, cancellationToken: cancellationToken)
        )
            return Result.Failure("Trainer Not Found");

        await trainerRepo.SoftDeleteAsync(trainer, cancellationToken);
        await trainerRepo.SaveChangesAsync(cancellationToken);
        return Result.Success("Trainer Deleted Successfully!");

    }


}
