namespace Application.Common.Interfaces
{
    public interface ITokenService
    {
        string CreateJwtSecurityToken(int id);
    }
}
