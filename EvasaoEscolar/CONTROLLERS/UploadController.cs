using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EvasaoEscolar.CONTRACTS;
using EvasaoEscolar.MODELS;
using EvasaoEscolar.UTIL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.Cors;

namespace EvasaoEscolar.CONTROLLERS
{
    [Route("api/upload")]
    [EnableCors("AllowAnyOrigin")]
    public class UploadController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        public IBaseRepository<AlunoDomain> _alunoRepository;
        private IBaseRepository<PlanilhaDadosDomain> _planilhaDadosRepository;
        private IBaseRepository<UploadPlanilhaDomain> _uploadplanilhaRepository;
        private IBaseRepository<DisciplinaTurmaDomain> _disciplinaturmaRepository;
        private IBaseRepository<AlunoDisciplinaTurmaDomain> _alunoDisciplinaTurmaRepository;
        private IBaseRepository<AlertasDomain> _alertasRepository;

        private IBaseRepository<FrequenciaDomain> _frequenciaRepository;

        //Valores recortados do contrutor abaixo:
        // , IBaseRepository<AlunoDomain> alunoRepository, IBaseRepository<PlanilhaDadosDomain> planilhaDadosRepository,
        //  IBaseRepository<DisciplinaTurmaDomain> disciplinaturmaRepository, IBaseRepository<UploadPlanilhaDomain> uploadplanilhaRepository
        public UploadController(IHostingEnvironment hostingEnvironment, IBaseRepository<AlunoDomain> alunoRepository, IBaseRepository<PlanilhaDadosDomain> planilhaDadosRepository,
        IBaseRepository<DisciplinaTurmaDomain> disciplinaturmaRepository, IBaseRepository<UploadPlanilhaDomain> uploadplanilhaRepository,
        IBaseRepository<AlunoDisciplinaTurmaDomain> alunoDisciplinaTurmaRepository, IBaseRepository<FrequenciaDomain> frequenciaRepository, IBaseRepository<AlertasDomain> alertasRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _alunoRepository = alunoRepository;
            _planilhaDadosRepository = planilhaDadosRepository;
            _disciplinaturmaRepository = disciplinaturmaRepository;
            _uploadplanilhaRepository = uploadplanilhaRepository;
            _alunoDisciplinaTurmaRepository = alunoDisciplinaTurmaRepository;
            _frequenciaRepository = frequenciaRepository;
            _alertasRepository = alertasRepository;
        }

        [HttpPost("UploadFiles")]

        public ActionResult Post(List<IFormFile> files, int turma, int disciplina, DateTime dataCorrespondente)
        {
            ProcessarPlanilha pp = new ProcessarPlanilha();

            string retorno = pp.ProcessandoPlanilha(files[0], turma, disciplina, dataCorrespondente, _alunoRepository, _planilhaDadosRepository,
            _disciplinaturmaRepository, _uploadplanilhaRepository, _alunoDisciplinaTurmaRepository, _frequenciaRepository, _alertasRepository);

            return Json(new { success = true, responseText = retorno.ToString() });

        }
    }
}



// string folderName = "Upload";
// string webRootPath = _hostingEnvironment.WebRootPath;
// string newPath = Path.Combine(webRootPath, folderName);
// StringBuilder sb = new StringBuilder();
// if (!Directory.Exists(newPath))
// {
//     Directory.CreateDirectory(newPath);
// }
// if (files[0].Length > 0)
// {
//     string sFileExtension = Path.GetExtension(files[0].FileName).ToLower();
//     ISheet sheet;
//     string fullPath = Path.Combine(newPath, files[0].FileName);
//     using (var stream = new FileStream(fullPath, FileMode.Create))
//     {
//         files[0].CopyTo(stream);
//         stream.Position = 0;
//         if (sFileExtension == ".xls")
//         {
//             HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
//             sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
//         }
//         else
//         {
//             XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
//             sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
//         }
//         IRow headerRow = sheet.GetRow(0); //Get Header Row
//         int cellCount = headerRow.LastCellNum;
//         sb.Append("<table class='table'><tr>");
//         for (int j = 0; j < cellCount; j++)
//         {
//             NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
//             if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
//             sb.Append("<th>" + cell.ToString() + "</th>");
//         }
//         sb.Append("</tr>");
//         sb.AppendLine("<tr>");
//         for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
//         {
//             IRow row = sheet.GetRow(i);
//             if (row == null) continue;
//             if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
//             for (int j = row.FirstCellNum; j < cellCount; j++)
//             {
//                 if (row.GetCell(j) != null)
//                     sb.Append("<td>" + row.GetCell(j).ToString() + "</td>");
//             }
//             sb.AppendLine("</tr>");
//         }
//         sb.Append("</table>");
//     }
// }

//  Response.StatusCode = (int)HttpStatusCode.OK; 
//  return Json(new { success = true, responseText= sb.ToString()});