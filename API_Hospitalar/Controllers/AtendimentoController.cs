using API_Hospitalar.DTOs.Atendimentos;
using API_Hospitalar.DTOs.AtendimentosDTO;
using API_Hospitalar.HospitalContextDb;
using API_Hospitalar.IHospital;
using API_Hospitalar.Models;
using Microsoft.AspNetCore.Mvc;


namespace API_Hospitalar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtendimentoController : ControllerBase
    {
        private readonly HospitalContext _dbContext;
        private readonly IHospitalService _IService;

        public AtendimentoController(HospitalContext dbContext, IHospitalService IService)
        {
            _dbContext = dbContext;
            _IService = IService;
        }


//continua daqui
        [HttpPost]
        public ActionResult<AtendimentosGetDTO> InserirAtendimeto([FromBody] AtendimentosDTO atendimentosDTO)
        {
            string validacao =_IService.AtendimentoItensObrigatorios(atendimentosDTO);
            if (validacao == "faltamInformacoes")
            {
                return BadRequest("Atendimento não registrado. Todos os itens são de preenchimento obrigatório, identificador_médico e " +
                    "identificador_paciente devem ser diferentaes de zero. ");
            }
            else
            {
                PacienteModel buscaPaciente = _dbContext.DbPacientes.Where(i => i.Id == atendimentosDTO.Identificador_paciente).FirstOrDefault();
                if (buscaPaciente != null)
                {
                    MedicoModel buscaMedico = _dbContext.DbMedicos.Where(i => i.Id == atendimentosDTO.Identificador_medico).FirstOrDefault();
                    if (buscaMedico != null)
                    {
                        Atendimentos atendimento = new Atendimentos()
                        {
                            Descricao = atendimentosDTO.Descricao,
                            MedicoId = atendimentosDTO.Identificador_medico,
                            PacienteID = atendimentosDTO.Identificador_paciente
                        };
                        _dbContext.DbAtendimentos.Add(atendimento);
                        _dbContext.SaveChanges();
                    }
                }
                else
                {
                    return NotFound("Identificador de paciente não encontrado.");
                }
            }
            return Ok();
        }
    }
}
