using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.models;

namespace SmartSchool.WebAPI.Data
{
    public class SmartContext : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<AlunoDisciplina> AlunosDisciplinas { get; set; }

        //relaciona as tabelas
        protected override void OnModelCreating(ModelBuilder builder)
        {
           builder.Entity<AlunoDisciplina>()
            .HasKey(AD => new {AD.AlunoId, AD.DisciplinaId}) 
        }

    }
}