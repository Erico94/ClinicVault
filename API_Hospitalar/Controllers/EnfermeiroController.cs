using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_Hospitalar.HospitalContextDb;
using API_Hospitalar.Models;
using API_Hospitalar.DTOs.Enfermeiros;
using API_Hospitalar.IHospital;
using API_Hospitalar.HospitalServices;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API_Hospitalar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnfermeiroController : ControllerBase
    {
        private readonly HospitalContext _dbContext;
        private IHospitalService _IService;

        public EnfermeiroController(HospitalContext dbContext, IHospitalService IService)
        {
            _IService = IService;
            _dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult<EnfermeiroGetDTO> CadastrarEnfermeiro([FromBody] EnfermeiroDTO novoEnfermeiro)
        {
            string verificacao = _IService.ValidacaoItensObrigatoriosEnfermeiros(novoEnfermeiro);
            if (verificacao == "faltaDadosObrigatorios")
            {
                return BadRequest("Impossível cadastrar enfermeiro. Cpf, nome, data de nascimento" +
                    ", telefone, instituição de formação e cadastro COFEN  são de preenchimento obrigatório, verifique e tente novamente.");
            }
            else if(verificacao == "CpfExistente")
            {
                return Conflict("CPF ja cadastrado em nosso sistema.");
            }
            EnfermeiroModel enfermeiroModel = new EnfermeiroModel();
            enfermeiroModel = _IService.EnfermeiroDTO_para_EnfermeiroModel( enfermeiroModel, novoEnfermeiro);
            _dbContext.DbEnfermeiros.Add(enfermeiroModel);
            _dbContext.SaveChanges();

            //RESPONSE
            EnfermeiroGetDTO enfermeiroGetDTO = _IService.EnfermeiroModel_para_EnfermeiroGetDTO(enfermeiroModel);
            return Created(Request.Path, enfermeiroGetDTO);
            

        }

        [HttpPut("{identificador}")]
        public ActionResult<EnfermeiroGetDTO> EditarEnfermeiro([FromRoute] int identificador, [FromBody] EnfermeiroPutDTO enfermeiroEditado)
        {
            EnfermeiroModel buscaEnfermeiroPorId = _dbContext.DbEnfermeiros.Where(i => i.Id == identificador).FirstOrDefault();
            if (buscaEnfermeiroPorId == null)
            {
                return NotFound("Id não encontrado em nosso sistema.");
            }
            else
            {
                bool verificacao = _IService.ValidacaoEnfermeiroPutDTO(enfermeiroEditado);
                if (verificacao==false)
                {
                    return BadRequest("Impossível editar enfermeiro. Nome, telefone, instituição de formação e cadastro COFEN  são de preenchimento obrigatório," +
                        " verifique e tente novamente.");
                }
                _IService.EnfermeiroPut_para_Model ( enfermeiroEditado, buscaEnfermeiroPorId);
                _dbContext.DbEnfermeiros.Attach(buscaEnfermeiroPorId);
                _dbContext.SaveChanges();
                EnfermeiroGetDTO enfermeiroGet = _IService.EnfermeiroModel_para_EnfermeiroGetDTO(buscaEnfermeiroPorId);
                return Ok(enfermeiroGet);
            }
        }

        [HttpGet]
        public ActionResult<List<EnfermeiroGetDTO>> ObterTodosEnfermeiros()
        {
            List<EnfermeiroGetDTO> ListaEnfermeiros = new List<EnfermeiroGetDTO>();
            foreach (var item in _dbContext.DbEnfermeiros)
            {
                EnfermeiroGetDTO enfermeiroGetDTO = _IService.EnfermeiroModel_para_EnfermeiroGetDTO(item);
                ListaEnfermeiros.Add(enfermeiroGetDTO);
            }
            return Ok(ListaEnfermeiros);
        }

        [HttpGet("{identificador}")]
        public ActionResult<EnfermeiroGetDTO> ObterPorIdentificador([FromRoute] int identificador)
        {
            EnfermeiroModel enermeiroModel = _dbContext.DbEnfermeiros.Where(i=>i.Id== identificador).FirstOrDefault();
            if(enermeiroModel == null)
            {
                return NotFound("Não foi possivel localizar este código identificador em nosso sistema.");
            }
            else
            {
                EnfermeiroGetDTO enfermeiroGetDTO = _IService.EnfermeiroModel_para_EnfermeiroGetDTO(enermeiroModel);
                return Ok(enfermeiroGetDTO);
            }
        }

        [HttpDelete("{identificador}")]
        public ActionResult DeletarPorCodigo([FromRoute] int identificador)
        {
            EnfermeiroModel enfermeiroModel = _dbContext.DbEnfermeiros.Where(i => i.Id == identificador).FirstOrDefault();
            if (enfermeiroModel == null)
            {
                return NotFound("Código identificador não encontrado em nosso sistema.");
            }
            _dbContext.DbEnfermeiros.Remove(enfermeiroModel);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
