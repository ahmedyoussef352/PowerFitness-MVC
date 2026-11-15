

using ProjectGym.BusinessLogic.Comman;
using ProjectGym.BusinessLogic.Servies;
using ProjectGym.BusinessLogic.ViewModel.HeathRecord;
using ProjectGym.BusinessLogic.ViewModel.Members;
using ProjectGym.DataAccess.Entities;
using ProjectGym.DataAccess.Entities.ValueObjects;

public class MemberService(IMemberRepository memberRepository) : IMemberService
{
    public async Task<Result> CreateAsync(CreateMemberViewModel model, CancellationToken cancellationToken = default)
    {
        var email = model.Email.Trim().ToLower();
        var name = model.Name.Trim().ToLower();
        var phone = model.Phone.Trim();

        if (await memberRepository.IsEmailTakenAsync(email: email, cancellationToken: cancellationToken))
        {
            return Result.Failure("A member with this email already exists.");
        }

        if (await memberRepository.IsPhoneTakenAsync(phone: phone, cancellationToken: cancellationToken))
        {
            return Result.Failure("A member with this phone number already exists.");
        }

        if (model.Gender is null)
        {
            return Result.Failure("Invalid gender selection.");
        }

        if (model.HealthRecord.BloodType is null)
        {
            return Result.Failure("Invalid blood type selection.");
        }

        var member = new Member
        {
            Name = name,
            Email = email,
            Phone = phone,
            Gender = model.Gender.Value,
            DateOfBirth = model.DateOfBirth,
            JoinDate = DateOnly.FromDateTime(DateTime.Now),
            Address = new Address
            {
                BuildingNumber = model.BuildingNumber,
                City = model.City,
                Street = model.Street
            },
            HealthRecord = new HealthRecord
            {
                Height = model.HealthRecord.Height,
                Weight = model.HealthRecord.Weight,
                BloodType = model.HealthRecord.BloodType.Value,
                Notes = model.HealthRecord.Notes
            }
        };

        await memberRepository.AddAsync(member, cancellationToken);
        await memberRepository.SaveChangesAsync(cancellationToken);

        return Result.Success("Member added successfully!");
    }
    public async Task<IEnumerable<MemberIndexViewModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        var members = await memberRepository.GetAllAsync(cancellationToken);

        return members.Select(m => new MemberIndexViewModel
        {
            Id = m.Id,
            Name = m.Name,
            Email = m.Email,
            Phone = m.Phone,
            Gender = m.Gender.ToString(),
            JoinDate = m.JoinDate
        });
    }

    public async Task<MemberDetailsViewModel?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var member = await memberRepository.GetWithMemberShipAsyncById(
            id: id,
            cancellationToken: cancellationToken
            );

        if (member is null)
        {
            return null;
        }
        var today = DateOnly.FromDateTime(DateTime.Now);
        var activeMemberShip =
            member
            .MemberShips
            .FirstOrDefault(ms => ms.EndDate >= today);
        return new MemberDetailsViewModel
        {
            Id = member.Id,
            Name = member.Name,
            Email = member.Email,
            Phone = member.Phone,
            Gender = member.Gender.ToString(),
            DateOfBirth = member.DateOfBirth.ToShortDateString(),
            Address = $"{member.Address.BuildingNumber} - {member.Address.Street} - {member.Address.City}",
            PlanName = activeMemberShip?.Plan.Name ?? "No Active Plan",
            MembershipStartDate = activeMemberShip?.StartDate.ToShortDateString() ?? "-",
            MembershipEndDate = activeMemberShip?.EndDate.ToShortDateString() ?? "-",
            PhotoUrl = member.ImageUrl
        };
    }
    // Health Record
    public async Task<HealthRecordViewModel?> GetHealthRecordAsync(int id, CancellationToken cancellationToken = default)
    {
        var member = await memberRepository.GetHealthRecordsyncById(id, cancellationToken);
        if (member == null || member.HealthRecord == null)
        {
            return null;
        }
        var healthRecord = member.HealthRecord;

        return new HealthRecordViewModel
        {
            Height = healthRecord.Height,
            Weight = healthRecord.Weight,
            BloodType = healthRecord.BloodType,
            Notes = healthRecord.Notes

        };
    }

    // Get Data To Edit
    public async Task<EditMemberViewModel?> GetForUpdateAsync(int id, CancellationToken cancellationToken)
    {
        var member = await memberRepository.GetByIdAsync(id, cancellationToken);

        if (member is null)
        {
            return null;
        }

        return new EditMemberViewModel
        {
            Name = member.Name,
            Email = member.Email,
            Phone = member.Phone,
            BuildingNumber = member.Address.BuildingNumber,
            Street = member.Address.Street,
            City = member.Address.City,
            Photo = member.ImageUrl,


        };
    }
    // Post Update
    public async Task<Result> UpdateAsync(int id, EditMemberViewModel model, CancellationToken cancellationToken)
    {
        var member = await memberRepository.GetByIdAsync(id, cancellationToken);
        if (member is null)
        {
            return Result.Failure("Member not found.");
        }
        if (member.Name != model.Name)
            return Result.Failure("Name cannot be changed");

        var email = model.Email.Trim().ToLowerInvariant();
        var phone = model.Phone;
        if (await memberRepository.IsEmailTakenAsync(email, excludeId: id))
        {
            return Result.Failure("A member with this email already exists.");
        }
        if (await memberRepository.IsPhoneTakenAsync(phone, excludeId: id))
        {
            return Result.Failure("A member with this phone number already exists.");
        }
        member.Email = email;
        member.Phone = phone;
        member.Address = new Address
        {
            BuildingNumber = model.BuildingNumber,
            City = model.City.Trim(),
            Street = model.Street.Trim()

        };

        await memberRepository.SaveChangesAsync(cancellationToken);
        return Result.Success("Member updated Successfully!");
    }
    // Delete Member 
    public async Task<Result> DeleteAsync(int id,
        CancellationToken cancellationToken)
    {
        var member = await memberRepository.GetDetailsByIdAsync(id,
            includes: m => m.Bookings,
           cancellationToken: cancellationToken);


        if (member is null)
            return Result.Failure("Member not found");

        if (await memberRepository.
        HasUpCommingBookingAsync(id, cancellationToken: cancellationToken)
        )
            return Result.Failure("Member Not Found");

        await memberRepository.SoftDeleteAsync(member, cancellationToken);
        await memberRepository.SaveChangesAsync(cancellationToken);
        return Result.Success("Member Deleted Successfully!");

    }

}
