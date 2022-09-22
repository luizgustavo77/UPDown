using FTTBlazor.Common.Core;

namespace UPDown.Common.AulaAPI
{
    public class CompetenciaDTO : FTTEntity
    {
        public string IdProfessor { get; set; }
        public string NameProfessor { get; set; }
        public string IdMateria { get; set; }
        public string NameMateria { get; set; }
    }
}
