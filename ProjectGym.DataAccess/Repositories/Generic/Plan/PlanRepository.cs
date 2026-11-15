using ProjectGym.DataAccess.Data.Contexts;
using ProjectGym.DataAccess.Entities;
using ProjectGym.DataAccess.Repositories;


public class PlanRepository(GymDbContext dbContext) :
    Repository<Plan>(dbContext), IPlanRepository
{

}

//public class OrderService
//{
//    IPayment _payment;

//    public OrderService(IPayment payment)
//    {
//        _payment = payment;
//    }
//}
//public interface IPayment
//{
//    public void Pay();
//}
//public class InstaPay : IPayment
//{
//    public void Pay()
//    {
//        Console.WriteLine("Pay With Instapay");
//    }
//}