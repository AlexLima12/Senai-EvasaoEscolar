using Microsoft.EntityFrameworkCore;
using EvasaoEscolar.MODELS;
using System.Linq;

namespace EvasaoEscolar.CONTEXTO
{
    public class EvasaEscolarContext : DbContext
    {
        public EvasaEscolarContext(DbContextOptions<EvasaEscolarContext> options)
       : base(options)

        {

        }

        public DbSet<CursoDomain> DbCursos { get; set; }
        public DbSet<AlunoDomain> DbAlunos { get; set; }
        public DbSet<AlertasDomain> DbAlertas { get; set; }
        public DbSet<AnotacoesDomain> DbAnotacoes { get; set; }
        public DbSet<AlunoDisciplinaTurmaDomain> DbAlunoDisciplinaTurmas { get; set; }
        public DbSet<DisciplinasDomain> DbDisciplinas { get; set; }
        public DbSet<DisciplinaTurmaDomain> DbDisciplinaTurmas { get; set; }
        public DbSet<FrequenciaDomain> DbFrequencias { get; set; }
        public DbSet<MediaDomain> DbMedias { get; set; }
        public DbSet<NotaDomain> DbNotas { get; set; }
        public DbSet<PermissaoDomain> DbPermissoes { get; set; }
        public DbSet<PlanilhaDadosDomain> DbPlanilhaDados { get; set; }
        public DbSet<TermoDomain> DbTermos { get; set; }
        public DbSet<TurmaDomain> DbTurmas { get; set; }
        public DbSet<UploadPlanilhaDomain> DbUploadPlanilhas { get; set; }
        public DbSet<UsuarioDomain> DbUsuarios { get; set; }
        public DbSet<UsuarioPermissaoDomain> DbUsuariosPermissoes { get; set; }
        public DbSet<EscolaDomain> DbEscolas { get; set; }
        public DbSet<IotDomain> DbIots { get; set; }
        public DbSet<IotDadosDomain> DbIotDados { get; set; }
      //  public DbSet<AlertaItemDomain> DbAlertaItens { get; set; }
 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CursoDomain>().ToTable("tbl_Cursos");
            modelBuilder.Entity<AnotacoesDomain>().ToTable("tbl_Anotacoes");
            modelBuilder.Entity<AlunoDomain>().ToTable("tbl_Aluno");
            modelBuilder.Entity<AlertasDomain>().ToTable("tbl_Alerta");
            modelBuilder.Entity<AlunoDisciplinaTurmaDomain>().ToTable("tbl_AlunoDisciplinaTurma");
            modelBuilder.Entity<DisciplinasDomain>().ToTable("tbl_Disciplina");
            modelBuilder.Entity<DisciplinaTurmaDomain>().ToTable("tbl_DisciplinaTurma");
            modelBuilder.Entity<FrequenciaDomain>().ToTable("tbl_Frequencia");
            modelBuilder.Entity<MediaDomain>().ToTable("tbl_Media");
            modelBuilder.Entity<NotaDomain>().ToTable("tbl_Nota");
            modelBuilder.Entity<PermissaoDomain>().ToTable("tbl_Permissao");
            modelBuilder.Entity<PlanilhaDadosDomain>().ToTable("tbl_PlanilhaDados");
            modelBuilder.Entity<TermoDomain>().ToTable("tbl_Termo");
            modelBuilder.Entity<TurmaDomain>().ToTable("tbl_Turma");
            modelBuilder.Entity<UploadPlanilhaDomain>().ToTable("tbl_UploadPlanilha");            
            modelBuilder.Entity<UsuarioDomain>().ToTable("tbl_Usuario");
            modelBuilder.Entity<UsuarioPermissaoDomain>().ToTable("tbl_UsuarioPermissao");
            modelBuilder.Entity<EscolaDomain>().ToTable("tbl_Escola");
            modelBuilder.Entity<IotDomain>().ToTable("tbl_Iot");
            modelBuilder.Entity<IotDadosDomain>().ToTable("tbl_IotDados");
        


            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }


            base.OnModelCreating(modelBuilder);


            
        }



    }
}