using System.ComponentModel.DataAnnotations;

namespace SimpleWebApi.Contracts.Requests
{
    public class RegisterRequestModel
    {
        [MinLength(5)]
        public string UserName { get; set; }
        [MinLength(6)]
        public string Password { get; set; }
    }
}
