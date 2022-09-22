using FTTBlazor.Common.Core;

namespace UPDown.Common.PessoaAPI
{
    public class ProfessorDTO : FTTEntity
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string CPF { get; set; }
        public string CEP { get; set; }
    }
}
