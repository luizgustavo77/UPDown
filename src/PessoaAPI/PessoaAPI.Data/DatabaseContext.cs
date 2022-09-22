using Microsoft.EntityFrameworkCore;
using PessoaAPI.Data.Entities;
using System.Collections.Generic;

namespace PessoaAPI.Data
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
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        #endregion

        #region Overidden methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<Aluno>().HasData(GetAlunos());
            _ = modelBuilder.Entity<Professor>().HasData(GetProfessors());
            base.OnModelCreating(modelBuilder);
        }
        #endregion

        #region Private methods
        private List<Aluno> GetAlunos()
        {
            return new List<Aluno>
            {
                new Aluno
                {
                    Id = 1,
                    Name = "Luiz",
                    Email="luiz_gustavo_77@hotmail.com",
                    Senha="senha"
                }
            };
        }
        private List<Professor> GetProfessors()
        {
            return new List<Professor>
            {
                new Professor
                {
                    Id = 1,
                    Name = "Luiz",
                    Email="luiz_gustavo_77@hotmail.com",
                    Senha="senha",
                    CEP="09070-060",
                    CPF="666.666.666-66",
                }
            };
        }
        #endregion
    }
}
