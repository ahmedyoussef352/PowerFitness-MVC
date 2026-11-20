namespace ProjectGym.BusinessLogic.Comman
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
        DateOnly Today { get; }
    }
}
