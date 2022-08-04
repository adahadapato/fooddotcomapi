namespace fooddotcomapi.Services
{
    public interface IUserService
    {
        string GetMyName();
        object GetById(int value);
    }
}
