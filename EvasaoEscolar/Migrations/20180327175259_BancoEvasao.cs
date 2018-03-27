using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EvasaoEscolar.Migrations
{
    public partial class BancoEvasao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Aluno",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Matricula = table.Column<string>(maxLength: 15, nullable: false),
                    NomeAluno = table.Column<string>(maxLength: 100, nullable: false),
                    StatusAlunoEvadiu = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Aluno", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Cursos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NomeCurso = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Cursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Escola",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NomeEscola = table.Column<string>(maxLength: 100, nullable: false),
                    NumeroUnidade = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Escola", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Permissao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NomePermissao = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Permissao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Termo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NomeTermo = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Termo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Alerta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlertaAntigo = table.Column<bool>(nullable: false),
                    AlunoId = table.Column<int>(nullable: false),
                    DataAlerta = table.Column<DateTime>(nullable: false),
                    MensagemAlerta = table.Column<string>(maxLength: 255, nullable: false),
                    NivelPrioridade = table.Column<int>(nullable: false),
                    OrigemAlerta = table.Column<int>(maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Alerta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Alerta_tbl_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "tbl_Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Anotacoes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlunoId = table.Column<int>(nullable: false),
                    Mensagem = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Anotacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Anotacoes_tbl_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "tbl_Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Turma",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EscolaId = table.Column<int>(nullable: false),
                    NomeTurma = table.Column<string>(maxLength: 100, nullable: false),
                    Periodo = table.Column<int>(maxLength: 1, nullable: false),
                    StatusTurma = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Turma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Turma_tbl_Escola_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "tbl_Escola",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Celular = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    EscolaId = table.Column<int>(nullable: false),
                    NomeUsuario = table.Column<string>(maxLength: 50, nullable: false),
                    Senha = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Usuario_tbl_Escola_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "tbl_Escola",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Disciplina",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CursoId = table.Column<int>(nullable: false),
                    NomeDisciplina = table.Column<string>(maxLength: 100, nullable: false),
                    TermoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Disciplina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Disciplina_tbl_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "tbl_Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_Disciplina_tbl_Termo_TermoId",
                        column: x => x.TermoId,
                        principalTable: "tbl_Termo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Iot",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataDiaIot = table.Column<DateTime>(nullable: false),
                    TurmaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Iot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Iot_tbl_Turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "tbl_Turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UsuarioPermissao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PermissaoId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UsuarioPermissao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_UsuarioPermissao_tbl_Permissao_PermissaoId",
                        column: x => x.PermissaoId,
                        principalTable: "tbl_Permissao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_UsuarioPermissao_tbl_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "tbl_Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_DisciplinaTurma",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DisciplinaId = table.Column<int>(nullable: false),
                    TurmaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DisciplinaTurma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_DisciplinaTurma_tbl_Disciplina_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "tbl_Disciplina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_DisciplinaTurma_tbl_Turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "tbl_Turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_IotDados",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlunoId = table.Column<int>(nullable: false),
                    Anger = table.Column<decimal>(nullable: false),
                    AulaIot = table.Column<int>(nullable: false),
                    Contempt = table.Column<decimal>(nullable: false),
                    DataIotDados = table.Column<DateTime>(nullable: false),
                    Disgust = table.Column<decimal>(nullable: false),
                    EsposureLevel = table.Column<decimal>(nullable: false),
                    EsposureValue = table.Column<decimal>(nullable: false),
                    EyeOccluded = table.Column<bool>(nullable: false),
                    FaceId = table.Column<string>(maxLength: 100, nullable: true),
                    Fear = table.Column<decimal>(nullable: false),
                    ForeheadOccluded = table.Column<bool>(nullable: false),
                    Happiness = table.Column<decimal>(nullable: false),
                    HeadPose = table.Column<decimal>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    IotId = table.Column<int>(nullable: false),
                    Left = table.Column<int>(nullable: false),
                    MouthOccluded = table.Column<bool>(nullable: false),
                    Neutral = table.Column<decimal>(nullable: false),
                    Sadness = table.Column<decimal>(nullable: false),
                    Smile = table.Column<decimal>(nullable: false),
                    StatusProcessadoIotDados = table.Column<bool>(nullable: false),
                    Surprise = table.Column<decimal>(nullable: false),
                    Top = table.Column<int>(nullable: false),
                    Width = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_IotDados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_IotDados_tbl_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "tbl_Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_IotDados_tbl_Iot_IotId",
                        column: x => x.IotId,
                        principalTable: "tbl_Iot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_AlunoDisciplinaTurma",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlunoId = table.Column<int>(nullable: false),
                    DisciplinaTurmaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_AlunoDisciplinaTurma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_AlunoDisciplinaTurma_tbl_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "tbl_Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_AlunoDisciplinaTurma_tbl_DisciplinaTurma_DisciplinaTurmaId",
                        column: x => x.DisciplinaTurmaId,
                        principalTable: "tbl_DisciplinaTurma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UploadPlanilha",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataReferenciaPlanilha = table.Column<DateTime>(nullable: false),
                    DataUploadPlanilha = table.Column<DateTime>(nullable: false),
                    DisciplinaTurmaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UploadPlanilha", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_UploadPlanilha_tbl_DisciplinaTurma_DisciplinaTurmaId",
                        column: x => x.DisciplinaTurmaId,
                        principalTable: "tbl_DisciplinaTurma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Frequencia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlunoDisciplinaTurmaId = table.Column<int>(nullable: false),
                    Atraso = table.Column<int>(nullable: false),
                    Dias = table.Column<int>(nullable: false),
                    Falta = table.Column<int>(nullable: false),
                    FaltaComp = table.Column<int>(nullable: false),
                    NumeroDeAulas = table.Column<int>(nullable: false),
                    Presenca = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Frequencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Frequencia_tbl_AlunoDisciplinaTurma_AlunoDisciplinaTurmaId",
                        column: x => x.AlunoDisciplinaTurmaId,
                        principalTable: "tbl_AlunoDisciplinaTurma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Media",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlunoDisciplinaTurmaId = table.Column<int>(nullable: false),
                    CalculoMedia = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Media", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Media_tbl_AlunoDisciplinaTurma_AlunoDisciplinaTurmaId",
                        column: x => x.AlunoDisciplinaTurmaId,
                        principalTable: "tbl_AlunoDisciplinaTurma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Nota",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlunoDisciplinaTurmaId = table.Column<int>(nullable: false),
                    DescricaoNota = table.Column<string>(maxLength: 50, nullable: false),
                    ValorNota = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Nota", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Nota_tbl_AlunoDisciplinaTurma_AlunoDisciplinaTurmaId",
                        column: x => x.AlunoDisciplinaTurmaId,
                        principalTable: "tbl_AlunoDisciplinaTurma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PlanilhaDados",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlunoId = table.Column<int>(nullable: false),
                    Aula1Planilha = table.Column<string>(maxLength: 50, nullable: true),
                    Aula2Planilha = table.Column<string>(maxLength: 50, nullable: true),
                    Aula3Planilha = table.Column<string>(maxLength: 50, nullable: true),
                    Aula4Planilha = table.Column<string>(maxLength: 50, nullable: true),
                    Aula5Planilha = table.Column<string>(maxLength: 50, nullable: true),
                    DadosProcessados = table.Column<bool>(nullable: false),
                    JustFaltaPlanilha = table.Column<string>(maxLength: 50, nullable: true),
                    NomeAluno = table.Column<string>(maxLength: 50, nullable: true),
                    UploadPlanilhaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PlanilhaDados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_PlanilhaDados_tbl_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "tbl_Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_PlanilhaDados_tbl_UploadPlanilha_UploadPlanilhaId",
                        column: x => x.UploadPlanilhaId,
                        principalTable: "tbl_UploadPlanilha",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Alerta_AlunoId",
                table: "tbl_Alerta",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_AlunoDisciplinaTurma_AlunoId",
                table: "tbl_AlunoDisciplinaTurma",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_AlunoDisciplinaTurma_DisciplinaTurmaId",
                table: "tbl_AlunoDisciplinaTurma",
                column: "DisciplinaTurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Anotacoes_AlunoId",
                table: "tbl_Anotacoes",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Disciplina_CursoId",
                table: "tbl_Disciplina",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Disciplina_TermoId",
                table: "tbl_Disciplina",
                column: "TermoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DisciplinaTurma_DisciplinaId",
                table: "tbl_DisciplinaTurma",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DisciplinaTurma_TurmaId",
                table: "tbl_DisciplinaTurma",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Frequencia_AlunoDisciplinaTurmaId",
                table: "tbl_Frequencia",
                column: "AlunoDisciplinaTurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Iot_TurmaId",
                table: "tbl_Iot",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_IotDados_AlunoId",
                table: "tbl_IotDados",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_IotDados_IotId",
                table: "tbl_IotDados",
                column: "IotId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Media_AlunoDisciplinaTurmaId",
                table: "tbl_Media",
                column: "AlunoDisciplinaTurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Nota_AlunoDisciplinaTurmaId",
                table: "tbl_Nota",
                column: "AlunoDisciplinaTurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PlanilhaDados_AlunoId",
                table: "tbl_PlanilhaDados",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PlanilhaDados_UploadPlanilhaId",
                table: "tbl_PlanilhaDados",
                column: "UploadPlanilhaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Turma_EscolaId",
                table: "tbl_Turma",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UploadPlanilha_DisciplinaTurmaId",
                table: "tbl_UploadPlanilha",
                column: "DisciplinaTurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Usuario_EscolaId",
                table: "tbl_Usuario",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UsuarioPermissao_PermissaoId",
                table: "tbl_UsuarioPermissao",
                column: "PermissaoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UsuarioPermissao_UsuarioId",
                table: "tbl_UsuarioPermissao",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Alerta");

            migrationBuilder.DropTable(
                name: "tbl_Anotacoes");

            migrationBuilder.DropTable(
                name: "tbl_Frequencia");

            migrationBuilder.DropTable(
                name: "tbl_IotDados");

            migrationBuilder.DropTable(
                name: "tbl_Media");

            migrationBuilder.DropTable(
                name: "tbl_Nota");

            migrationBuilder.DropTable(
                name: "tbl_PlanilhaDados");

            migrationBuilder.DropTable(
                name: "tbl_UsuarioPermissao");

            migrationBuilder.DropTable(
                name: "tbl_Iot");

            migrationBuilder.DropTable(
                name: "tbl_AlunoDisciplinaTurma");

            migrationBuilder.DropTable(
                name: "tbl_UploadPlanilha");

            migrationBuilder.DropTable(
                name: "tbl_Permissao");

            migrationBuilder.DropTable(
                name: "tbl_Usuario");

            migrationBuilder.DropTable(
                name: "tbl_Aluno");

            migrationBuilder.DropTable(
                name: "tbl_DisciplinaTurma");

            migrationBuilder.DropTable(
                name: "tbl_Disciplina");

            migrationBuilder.DropTable(
                name: "tbl_Turma");

            migrationBuilder.DropTable(
                name: "tbl_Cursos");

            migrationBuilder.DropTable(
                name: "tbl_Termo");

            migrationBuilder.DropTable(
                name: "tbl_Escola");
        }
    }
}
