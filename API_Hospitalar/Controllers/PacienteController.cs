using API_Hospitalar.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_Hospitalar.HospitalContextDb;
using API_Hospitalar.DTO;
using API_Hospitalar.IHospital;
using API_Hospitalar.HospitalServices;
using API_Hospitalar.DTO.Paciente;
using API_Hospitalar.DTOs.Alergias;
using API_Hospitalar.DTOs.Cuidados;
using API_Hospitalar.DTOs.AtendimentosDTO;
using Microsoft.EntityFrameworkCore;
using API_Hospitalar.DTOs.Pacientes;

namespace API_Hospitalar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {

        private readonly HospitalContext _dbContext;
        private readonly IHospitalService _IService;

        public PacienteController(HospitalContext dbContext, IHospitalService IService)
        {
            _dbContext = dbContext;
            _IService = IService;
        }

        [HttpPost]
        public ActionResult<PacienteGetDTO> CadastrarPaciente([FromBody] PacienteDTO novoPaciente)
        {
            var buscaCPF = _dbContext.DbPacientes.Where(paciente => paciente.CPF == novoPaciente.CPF).FirstOrDefault();
            if (buscaCPF != null)
            {
                return Conflict("CPF já cadastrado em nosso sistema. Identificador do paciente com este cpf:" + buscaCPF.Id + ".");
            }
            else
            {
                PacienteModel pacienteModel = new PacienteModel();
                pacienteModel = _IService.PacienteDTO_para_Model(novoPaciente, pacienteModel);
                    


                PacienteGetDTO pacienteGetDTO = _IService.PacienteModel_para_GetDTO(pacienteModel);
                return Created(Request.Path, pacienteGetDTO);
            }

        }

        [HttpGet]
        public ActionResult<List<PacienteGetDTO>> ObterTodos([FromQuery] string status)
        {
            List<PacienteGetDTO> listaPacientes = new List<PacienteGetDTO>();
            if (status != null)
            {
                List<string> statusValoresAceitos = new List<string>() { "AGUARDANDO_ATENDIMENTO", "ATENDIDO", "NAO_ATENDIDO", "EM_ATENDIMENTO" };
                if (statusValoresAceitos.Contains(status))
                {
                    foreach (var paciente in _dbContext.DbPacientes)
                    {
                        if (paciente.Status_De_Atendimento == status)
                        {
                            PacienteGetDTO pacienteGetDTO = _IService.PacienteModel_para_GetDTO(paciente);
                            listaPacientes.Add(pacienteGetDTO);
                        }
                    }
                    return Ok(listaPacientes);
                }
                else
                {
                    return BadRequest("Status de atendimento inválido, status aceitos : ATENDIDO, AGUARDANDO_ATENDIMENTO, NAO_ATENDIDO e EM_ATENDIMENTO.");
                }
            }
            else
            {
                foreach (var paciente in _dbContext.DbPacientes)
                {
                    PacienteGetDTO pacienteGetDTO = _IService.PacienteModel_para_GetDTO(paciente);
                    listaPacientes.Add(pacienteGetDTO);
                }
                return Ok(listaPacientes);
            }
        }

        [HttpGet("{identificador}")]
        public ActionResult<PacienteGetDTO> ObterPorId([FromRoute] int identificador)
        {
            if (identificador != 0)
            {
                PacienteModel buscaPaciente = _dbContext.DbPacientes.Where(i => i.Id == identificador).FirstOrDefault();
                if (buscaPaciente != null)
                {
                    PacienteGetDTO pacienteGetDTO = _IService.PacienteModel_para_GetDTO(buscaPaciente);
                    return Ok(pacienteGetDTO);
                }
                else
                {
                    return NotFound("Código identificador não encontrado em nosso sistema.");
                }
            }
            else
            {
                return BadRequest("Por gentileza, informe um código identificador diferente de zero.");
            }
        }

        [HttpPut("{ídentificador}")]
        public ActionResult<PacienteGetDTO> EditarPorId([FromBody] PacientePutDTO pacienteEditado, [FromRoute] int ídentificador)
        {
            PacienteModel buscaPaciente = _dbContext.DbPacientes.Where(i => i.Id == ídentificador).FirstOrDefault();
            if (buscaPaciente != null)
            {
                _IService.PacientePut_para_Model(pacienteEditado, buscaPaciente);
                _dbContext.DbPacientes.Attach(buscaPaciente);
                _dbContext.SaveChanges();
                PacienteGetDTO pacienteGet = _IService.PacienteModel_para_GetDTO(buscaPaciente);
                return Ok(pacienteGet);
            }
            else
            {
                return NotFound ("Código identificador não encontrado em nosso sistema.");
            }


        }

        [HttpPut("{identificador}/status")]
        public ActionResult<PacienteDTO> AlterarStatus([FromBody] StatusPacienteDTO paciente, [FromRoute] int identificador)
        {
            PacienteModel buscaPaciente = _dbContext.DbPacientes.Where(i => i.Id == identificador).FirstOrDefault();
            if (buscaPaciente != null)
            {
                buscaPaciente.Status_De_Atendimento = paciente.Status_de_Atendimento;
                if (buscaPaciente.Status_De_Atendimento == "ATENDIDO")
                {
                    buscaPaciente.TotalAtendimentos++;
                }
                _dbContext.DbPacientes.Attach(buscaPaciente);
                _dbContext.SaveChanges();
                return Ok(buscaPaciente);
            }
            else
            {
                return NotFound ("Código identificador não encontrado em nosso sistema.");
            }
        }


        [HttpDelete("{identificador}")]
        public ActionResult DeletarPorId([FromRoute] int identificador)
        {
            if (identificador != 0)
            {
                PacienteModel buscaPaciente = _dbContext.DbPacientes.Where(i => i.Id == identificador).FirstOrDefault();
                if (buscaPaciente != null)
                {
                    Alergias buscaAlergia = _dbContext.DbAlergias.Where(i => i.PacienteID == buscaPaciente.Id).FirstOrDefault();
                    if (buscaAlergia != null)
                    {
                        _dbContext.DbAlergias.Remove(buscaAlergia);
                        _dbContext.SaveChanges();
                    }
                    
                    Cuidados buscaCuidado = _dbContext.DbCuidados.Where(i => i.PacienteID == buscaPaciente.Id).FirstOrDefault();
                    if (buscaCuidado != null)
                    {
                        _dbContext.DbCuidados.Remove(buscaCuidado);
                        _dbContext.SaveChanges();
                    }

                    foreach (var atendimento in _dbContext.DbAtendimentos)
                    {
                        if (atendimento.PacienteID == buscaPaciente.Id)
                        {
                            _dbContext.DbAtendimentos.Remove(atendimento);
                            _dbContext.SaveChanges();
                        }
                    }
                    _dbContext.DbPacientes.Remove(buscaPaciente);
                    _dbContext.SaveChanges();
                    return NoContent();
                }
                else
                {
                    return NotFound ("Código identificador não encontrado em nosso sistema.");
                }
            }
            else
            {
                return BadRequest("Insira um código identificador diferente de zero.");
            }

        }
    }
}