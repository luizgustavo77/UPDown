namespace AulaAPI.Data.Entities
{
    public class Conteudo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long IdProfessor { get; set; }
        public string NameProfessor { get; set; }
        public long IdMateria { get; set; }
        public string NameMateria { get; set; }
        public string Descricao { get; set; }
        public string Video { get; set; }
        public string Documento { get; set; }
        public bool Disponivel { get; set; }
    }
}
