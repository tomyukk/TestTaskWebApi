namespace SimpleWebApi.Interfaces
{
    public interface IHashingService
    {
        string GenerateSalt();
        string Hash(string valueToHash, string salt);
    }
}
