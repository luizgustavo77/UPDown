using FTTBlazor.Common.Core;

namespace UPDown.Common.AulaAPI
{
    public class CompetenciaDTO : FTTEntity
    {
        public long IdProfessor { get; set; }
        public string NameProfessor { get; set; }
        public long IdMateria { get; set; }
        public string NameMateria { get; set; }
    }
}
