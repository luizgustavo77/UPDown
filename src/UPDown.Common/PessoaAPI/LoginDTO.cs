using FTTBlazor.Common.Core;

namespace UPDown.Common.PessoaAPI
{
    public enum LoginType
    {
        Admin = 1,
        Aluno,
        Professor
    }

    public class LoginDTO : FTTEntity
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public LoginType Type { get; set; }
    }
}
