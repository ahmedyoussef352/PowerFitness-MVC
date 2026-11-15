using ProjectGym.DataAccess.Enums;

namespace ProjectGym.DataAccess.Entities
{
    public class Trainer : User
    {
        public Speciality Speciality { get; set; }
        public DateOnly HireDate { get; set; }
        //ICollection <Session> 1-m
        public ICollection<Session> Sessions { get; set; } = [];
    }
}
