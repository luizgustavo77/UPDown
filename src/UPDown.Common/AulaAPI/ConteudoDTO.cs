using FTTBlazor.Common.Core;

namespace UPDown.Common.AulaAPI
{
    public class ConteudoDTO : FTTEntity
    {
        public string IdProfessor { get; set; }
        public string NameProfessor { get; set; }
        public string IdMateria { get; set; }
        public string NameMateria { get; set; }
        public string Descricao { get; set; }
        public string Video { get; set; }
        public string Documento { get; set; }
        public bool Disponivel { get; set; }
    }
}
