namespace IServices.ISysServices
{
    public interface IUserInfo
    {
        string UserId { get; }
        string DepartmentId { get; }
        string UserName { get; }
        bool IsAuthenticated { get; }
    }
}