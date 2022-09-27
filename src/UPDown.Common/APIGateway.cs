using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPDown.Common
{
    public static class APIGateway
    {
        public static string Pessoa { get; set; } = "https://localhost:44306/";
        public static string Aula { get; set; } = "https://localhost:44307/";
#if DEBUG
        public static string Conteudos { get; set; } = Aula + "Conteudo";
        public static string Competencias { get; set; } = Aula + "Competencia";
        public static string Materias { get; set; } = Aula + "Materia";
        public static string Alunos { get; set; } = Pessoa + "Aluno";
        public static string Professores { get; set; } = Pessoa + "Professor";
#else
        public static string Conteudos { get; set; } = Aula + "Conteudo";
        public static string Competencias { get; set; } = Aula + "Competencia";
        public static string Materias { get; set; } = Aula + "Materia";
        public static string Alunos { get; set; } = Pessoa + "Aluno";
        public static string Professores { get; set; } = Pessoa + "Professor";
#endif
    }
}
