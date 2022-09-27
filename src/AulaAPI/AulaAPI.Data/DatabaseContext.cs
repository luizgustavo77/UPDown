using AulaAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AulaAPI.Data
{
    public class DatabaseContext : Microsoft.EntityFrameworkCore.DbContext
    {
        #region Contructor
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
                : base(options)
        {
            _ = Database.EnsureCreated();
        }
        #endregion

        #region Public properties
        public DbSet<Competencia> Competencias { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Conteudo> Conteudos { get; set; }
        #endregion

        #region Overidden methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<Competencia>().HasData(GetAlunos());
            _ = modelBuilder.Entity<Materia>().HasData(GetMaterias());
            base.OnModelCreating(modelBuilder);
        }
        #endregion

        #region Private methods
        private List<Competencia> GetAlunos()
        {
            return new List<Competencia>
            {
                new Competencia
                {
                    Id = 1,
                    Name = "Luiz",
                    IdMateria = 1,
                    IdProfessor =1,
                    NameMateria = "Informatica",
                    NameProfessor= "Luiz"
                }
            };
        }
        private List<Materia> GetMaterias()
        {
            return new List<Materia>
            {
                new Materia
                {
                    Id = 1,
                    Name = "Informatica"
                }
            };
        }
        #endregion
    }
}
