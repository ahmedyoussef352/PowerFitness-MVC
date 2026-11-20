

using AutoMapper;
using Gymy.BusinessLogic.Common;
using ProjectGym.BusinessLogic.Comman;
using ProjectGym.BusinessLogic.Servies;
using ProjectGym.BusinessLogic.ViewModel.HeathRecord;
using ProjectGym.BusinessLogic.ViewModel.Members;
using ProjectGym.DataAccess.Entities;
using ProjectGym.DataAccess.Entities.ValueObjects;
using ProjectGym.DataAccess.Enums;
using ProjectGym.DataAccess.Repositories;

public class MemberService(IUnitOfWork unitOfWork,
    IMapper mapper,
    IDateTimeProvider clock) : IMemberService
{
    public async Task<Result> CreateAsync(CreateMemberViewModel model, CancellationToken cancellationToken = default)
    {
        var email = model.Email.Trim().ToLower();
        var name = model.Name.Trim().ToLower();
        var phone = model.Phone.Trim();

        if (await unitOfWork.Members.IsEmailTakenAsync(email: email, cancellationToken: cancellationToken))
        {
            return Result.Failure("A member with this email already exists.", nameof(model.Email));
        }

        if (await unitOfWork.Members.IsPhoneTakenAsync(phone: phone, cancellationToken: cancellationToken))
        {
            return Result.Failure("A member with this phone number already exists.", nameof(model.Phone));
        }

        if (model.Gender is null)
        {
            return Result.Failure("Invalid gender selection.", nameof(model.Gender));
        }

        if (model.HealthRecord.BloodType is null)
        {
            return Result.Failure("Invalid blood type selection.");
        }

        var member = mapper.Map<Member>(model);

        await unitOfWork.Members.AddAsync(member, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
    public async Task<Result<IReadOnlyList<MemberIndexViewModel>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var members = await unitOfWork.Members.GetAllAsync(cancellationToken);


        return Result<IReadOnlyList<MemberIndexViewModel>>.Success(
            mapper.Map<IReadOnlyList<MemberIndexViewModel>>(members));
    }

    public async Task<Result<MemberDetailsViewModel>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var member = await unitOfWork.Members.GetWithMemberShipAsyncById(
            today: clock.Today,
            id: id,
            cancellationToken: cancellationToken
            );

        if (member is null)
        {
            return Result<MemberDetailsViewModel>.Failure("Member not Found", nameof(id));
        }
        var activeMemberShip =
            member
            .MemberShips
            .FirstOrDefault();
        return Result<MemberDetailsViewModel>.Success(mapper.Map<MemberDetailsViewModel>(member));
    }
    // Health Record
    public async Task<Result<HealthRecordViewModel>> GetHealthRecordAsync(int id, CancellationToken cancellationToken = default)
    {
        var member = await unitOfWork.Members.GetHealthRecordsyncById(id, cancellationToken);
        if (member == null || member.HealthRecord == null)
        {
            return Result<HealthRecordViewModel>.Failure("Dosenot exist");
        }
        var healthRecord = member.HealthRecord;

        return Result<HealthRecordViewModel>.Success(mapper.Map<HealthRecordViewModel>(member.HealthRecord));
    }
    private static string ToDisplayBloodType(BloodType bloodType) => bloodType switch
    {
        BloodType.APositive => "A+",
        BloodType.ANegative => "A-",
        BloodType.BPositive => "B+",
        BloodType.BNegative => "B-",
        BloodType.ABPositive => "AB+",
        BloodType.ABNegative => "AB-",
        BloodType.OPositive => "O+",
        BloodType.ONegative => "O-",
        _ => bloodType.ToString()
    };

    // Get Data To Edit
    public async Task<Result<EditMemberViewModel>> GetForUpdateAsync(int id, CancellationToken cancellationToken)
    {
        var member = await unitOfWork.Members.GetByIdAsync(id, cancellationToken);

        if (member is null)
        {
            return Result<EditMemberViewModel>.Failure("Member is not Found", nameof(id));
        }

        return Result<EditMemberViewModel>.Success(mapper.Map<EditMemberViewModel>(member));
    }
    // Post Update
    public async Task<Result> UpdateAsync(int id, EditMemberViewModel model, CancellationToken cancellationToken)
    {
        var member = await unitOfWork.Members.GetByIdAsync(id, cancellationToken);
        if (member is null)
        {
            return Result.Failure("Member not found.");
        }
        if (member.Name != model.Name)
            return Result.Failure("Name cannot be changed", nameof(model.Name));

        var email = model.Email.Trim().ToLowerInvariant();
        var phone = model.Phone;
        if (await unitOfWork.Members.IsEmailTakenAsync(email, excludeId: id))
        {
            return Result.Failure("A member with this email already exists.", nameof(model.Email));
        }
        if (await unitOfWork.Members.IsPhoneTakenAsync(phone, excludeId: id))
        {
            return Result.Failure("A member with this phone number already exists.", nameof(model.Phone));
        }
        member.Email = email;
        member.Phone = phone;
        member.Address = new Address
        {
            BuildingNumber = model.BuildingNumber,
            City = model.City.Trim(),
            Street = model.Street.Trim()

        };

        await unitOfWork.CommitAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result<MemberDeleteViewModel>> GetForDeleteAsync(int id, CancellationToken cancellationToken)
    {
        Result<MemberDetailsViewModel>? detailsResult = await GetByIdAsync(id, cancellationToken);

        if (detailsResult.IsFailure)
        {
            return Result<MemberDeleteViewModel>.Failure(detailsResult.Error!, detailsResult.Errorkey);
        }
        var details = detailsResult.Value!;

        return Result<MemberDeleteViewModel>.Success(mapper.Map<MemberDeleteViewModel>(detailsResult.Value));
    }


    // Post Delete Member 
    public async Task<Result> DeleteAsync(int id,
        CancellationToken cancellationToken)
    {
        var member = await unitOfWork.Members.GetDetailsByIdAsync(id,
            includes: [m => m.Bookings, m => m.HealthRecord],
           cancellationToken: cancellationToken);


        if (member is null)
        { return Result.Failure("Member not found", nameof(id)); }

        if (await unitOfWork.Members.
        HasUpCommingBookingAsync(id, clock.UtcNow, cancellationToken: cancellationToken)
        )
        {
            return Result.Failure("Member Has a Session");
        }


        await unitOfWork.Members.SoftDeleteAsync(member, cancellationToken);
        if (member.HealthRecord is not null)
        {
            await unitOfWork.HealthRecords.SoftDeleteAsync(member.HealthRecord, cancellationToken);
        }

        await unitOfWork.CommitAsync(cancellationToken);
        return Result.Success();

    }
}
