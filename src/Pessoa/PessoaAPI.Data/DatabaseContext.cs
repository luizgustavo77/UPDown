using PessoaAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PessoaAPI.Data
{
    public class DatabaseContext : Microsoft.EntityFrameworkCore.DbContext
    {
        #region Contructor
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
                : base(options)
        {
            Database.EnsureCreated();
        }
        #endregion

        #region Public properties
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        #endregion

        #region Overidden methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>().HasData(GetAlunos());
            modelBuilder.Entity<Professor>().HasData(GetProfessors());
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
