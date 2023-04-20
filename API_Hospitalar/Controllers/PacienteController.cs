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
            bool verificacao = _IService.ValidacaoItensObrigatoriosPacientes(novoPaciente);
            if (verificacao == false)
            {
                return BadRequest("Impossível cadastrar paciente. Cpf, nome, data de nascimento" +
                    ", telefone e contato de emergência são de preenchimento obrigatório, verifique e tente novamente.");
            }
            else
            {
                var buscaCPF = _dbContext.DbPacientes.Where(paciente => paciente.CPF == novoPaciente.CPF).FirstOrDefault();
                if (buscaCPF != null)
                {
                    return Conflict("CPF já cadastrado em nosso sistema. Código do paciente com este cpf:" + buscaCPF.Id + ".");
                }
                else
                {
                    PacienteModel pacienteModel = _IService.PacienteDTO_para_Model(novoPaciente);
                    bool validacao = _IService.ValidacaoStatusAtendimento(novoPaciente.Status_De_Atendimento);
                    if (validacao)
                    {
                        pacienteModel.Status_De_Atendimento = novoPaciente.Status_De_Atendimento;
                        if (novoPaciente.Status_De_Atendimento == "ATENDIDO")
                        {
                            return BadRequest("Impossível fornecer status de atendimento no ato de cadastro, possibilidades aceitas: NAO_ATENDIDO," +
                                " AGUARDANDO_ATENDIMENTO ou EM_ATENDIMENTO. Tente novamente.");
                        }
                    }
                    else
                    {
                        return BadRequest("ATENÇÃO: apenas os seguintes status são aceitos: ATENDIDO, EM_ATENDIMENTO, NAO_ATENDIDO ou AGUARDANDO_ATENDIMENTO.Verifique e " +
                            "insira um status de atendimento válido.");
                    }
                    _dbContext.DbPacientes.Add(pacienteModel);
                    _dbContext.SaveChanges();

                    //DAQUI EM DIANTE É O RESPONSE

                    PacienteGetDTO pacienteGetDTO = _IService.PacienteModel_para_GetDTO(pacienteModel);
                    return Created(Request.Path, pacienteGetDTO);
                }
            }

        }

        [HttpPut("{ídentificador}")]
        public ActionResult<PacienteDTO> EditarPorId([FromBody] PacienteDTO pacienteEditado, [FromRoute] int ídentificador)
        {
            PacienteModel buscaPaciente = _dbContext.DbPacientes.Where(i => i.Id == ídentificador).FirstOrDefault();
            if (buscaPaciente != null)
            {
                bool verificacao = _IService.ValidacaoItensObrigatoriosPacientes(pacienteEditado);
                if (verificacao)
                {
                    buscaPaciente.Nome = pacienteEditado.Nome;
                    buscaPaciente.CPF = pacienteEditado.CPF;
                    buscaPaciente.Contato_de_Emergencia = pacienteEditado.Contato_de_Emergencia;
                    buscaPaciente.Convenio = pacienteEditado.Convenio;
                    buscaPaciente.Genero = pacienteEditado.Genero;
                    buscaPaciente.Telefone = pacienteEditado.Telefone;
                    buscaPaciente.Status_De_Atendimento = pacienteEditado.Status_De_Atendimento;
                    _dbContext.DbPacientes.Attach(buscaPaciente);
                    _dbContext.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest("Impossível atualizar paciente. Cpf, nome, data de nascimento" +
                    ", telefone, status de atendimento e contato de emergência são de preenchimento obrigatório, verifique e tente novamente.");
                }
            }
            else
            {
                return NotFound("Não foi possivel localizar este identificador.");
            }


        }

        [HttpPut("{identificador}/status")]
        public ActionResult<PacienteDTO> AlterarStatus([FromBody] StatusPacienteDTO paciente, [FromRoute] int identificador)
        {
            PacienteModel buscaPaciente = _dbContext.DbPacientes.Where(i => i.Id == identificador).FirstOrDefault();
            if (buscaPaciente != null)
            {
                bool validacaoStatus = _IService.ValidacaoStatusAtendimento(paciente.Status_de_Atendimento);
                if (validacaoStatus)
                {
                    buscaPaciente.Status_De_Atendimento = paciente.Status_de_Atendimento;
                    if (buscaPaciente.Status_De_Atendimento == "Atendido" || buscaPaciente.Status_De_Atendimento == "atendido")
                    {
                        buscaPaciente.TotalAtendimentos++;
                    }
                    _dbContext.DbPacientes.Attach(buscaPaciente);
                    _dbContext.SaveChanges();
                    return Ok(buscaPaciente);
                }
                else
                {
                    return BadRequest("Não foi possível alterar status. Deve-se inserir um dos seguintes status de atendimento: ATENDIDO, NAO_ATENDIDO, EM_ATENDIMENTO ou AGUARDANDO_ATENDIMENTO.");
                }
            }
            else
            {
                return NotFound("Não foi possivel localizar o código identificador em nosso sistema.");
            }
        }

        [HttpGet]
        public ActionResult<List<PacienteGetDTO>> ObterTodos([FromQuery] string status)
        {
            if (status != null)
            {
                bool validacao = _IService.ValidacaoStatusAtendimento(status);
                if (validacao == true)
                {
                    List<PacienteGetDTO> listaPacientes = new List<PacienteGetDTO>();
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
                    return NotFound("ATENÇÃO: apenas os seguintes status são aceitos: ATENDIDO, EM_ATENDIMENTO, NAO_ATENDIDO ou AGUARDANDO_ATENDIMENTO. Verifique e insira um status de atendimento válido.");
                }
            }
            else
            {
                List<PacienteGetDTO> listaPacientes = new List<PacienteGetDTO>();
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
            if (identificador != null)
            {
                PacienteModel buscaPaciente = _dbContext.DbPacientes.Where(i => i.Id == identificador).FirstOrDefault();
                if (buscaPaciente != null)
                {
                    PacienteGetDTO pacienteGetDTO = _IService.PacienteModel_para_GetDTO(buscaPaciente);
                    return Ok(pacienteGetDTO);
                }
                else
                {
                    return NotFound("Identificador não encontrado em nosso sistema.");
                }
            }
            else
            {
                return BadRequest("Por gentileza, informe um código identificador.");
            }
        }

        [HttpDelete("{identificador}")]
        public ActionResult DeletarPorId([FromRoute] int identificador)
        {
            if (identificador != null)
            {
                PacienteModel buscaPaciente = _dbContext.DbPacientes.Where(i => i.Id == identificador).FirstOrDefault();
                if (buscaPaciente != null)
                {
                    Alergias buscaAlergia = _dbContext.DbAlergias.Where(i => i.PacienteID == buscaPaciente.Id).FirstOrDefault();
                    if (buscaAlergia != null)
                    {
                        _dbContext.DbAlergias.Remove(buscaAlergia);
                    }
                    
                    Cuidados buscaCuidado = _dbContext.DbCuidados.Where(i => i.PacienteID == buscaPaciente.Id).FirstOrDefault();
                    if (buscaCuidado != null)
                    {
                        _dbContext.DbCuidados.Remove(buscaCuidado);
                    }
                    
                    Atendimentos buscaAtendimentos = _dbContext.DbAtendimentos.Where(i => i.PacienteID == buscaPaciente.Id).FirstOrDefault();
                    if (buscaAtendimentos != null)
                    {
                        _dbContext.DbAtendimentos.Remove(buscaAtendimentos);
                    }

                    _dbContext.DbPacientes.Remove(buscaPaciente);
                    _dbContext.SaveChanges();
                    return NoContent();
                }
                else
                {
                    return NotFound("Código identificador não encontrado em nosso sistema.");
                }
            }
            else
            {
                return BadRequest("Insira um código identificador.");
            }

        }
    }
}