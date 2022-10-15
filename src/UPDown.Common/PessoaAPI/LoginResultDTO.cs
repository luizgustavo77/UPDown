using System;

namespace UPDown.Common.PessoaAPI
{
    public class LoginResultDTO
    {
        public string Token { get; set; }
        public UserType User { get; set; }
        public DateTime Expiry { get; set; }
    }
}
