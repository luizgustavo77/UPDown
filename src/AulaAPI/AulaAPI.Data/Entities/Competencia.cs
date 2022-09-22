namespace AulaAPI.Data.Entities
{
    public class Competencia
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long IdProfessor { get; set; }
        public string NameProfessor { get; set; }
        public long IdMateria { get; set; }
        public string NameMateria { get; set; }
    }
}
