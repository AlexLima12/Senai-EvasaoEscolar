using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using EvasaoEscolar.CONTRACTS;
using EvasaoEscolar.MODELS;
using Excel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;


namespace EvasaoEscolar.UTIL
{
    public class AlertasUtil
    {
        private IBaseRepository<AlertasDomain>  _alertasRepository;
        private IBaseRepository<FrequenciaDomain> _frequenciaRepository;

        private IBaseRepository<AlunoDisciplinaTurmaDomain> _alunodisciplinaturmaRepository;
        public AlertasUtil()
        {
            
        }
        public string InserirAlerta(IBaseRepository<AlertasDomain> alertasRepository, IBaseRepository<FrequenciaDomain> frequenciaRepository){
          
            _frequenciaRepository= frequenciaRepository;
            _alertasRepository=alertasRepository;

         //   var frequencias = _alunodisciplinaturmaRepository.Listar(new string[]{"AlunoDisciplinaTurma"}).Where(c => c.AlunoId ==  );




            string retorno = "";
            return retorno;
        }
    }
}