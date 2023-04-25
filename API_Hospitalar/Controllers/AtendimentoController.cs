using API_Hospitalar.DTOs.AtendimentosDTO;
using API_Hospitalar.HospitalContextDb;
using API_Hospitalar.IHospital;
using API_Hospitalar.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using API_Hospitalar.DTOs.Pacientes;
using API_Hospitalar.DTOs.Medicos;

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

        [HttpPost]
        public ActionResult<AtendimentosGetDTO> InserirAtendimeto([FromBody] AtendimentosDTO atendimentosDTO)
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

                    buscaPaciente.Status_De_Atendimento = "ATENDIDO";
                    _dbContext.DbPacientes.Attach(buscaPaciente);
                    _dbContext.DbAtendimentos.Add(atendimento);
                    _dbContext.SaveChanges();
                    AtendimentosGetDTO getDTO = new AtendimentosGetDTO()
                    {
                        Id_Atendimento = atendimento.Id_Atendimento,
                        Descricao = atendimento.Descricao
                    };
                    getDTO.Paciente = new PacienteSimplesGetDTO()
                    {
                        Identificador = buscaPaciente.Id,
                        Nome = buscaPaciente.Nome,
                        Convenio = buscaPaciente.Convenio
                    };
                    getDTO.Medico = new MedicoSimplesGetDTO()
                    {
                        Identificador = buscaMedico.Id,
                        Nome = buscaMedico.Nome,
                        CRM_UF = buscaMedico.CRM_UF,
                        EspecializacaoClinica = buscaMedico.EspecializacaoClinica
                    };
                    buscaPaciente.TotalAtendimentos++;
                    buscaMedico.TotalAtendimentos++;
                    buscaPaciente.Status_De_Atendimento = "ATENDIDO";
                    return Created(Request.Path,getDTO);
                }
                else
                {
                    return NotFound("Identificador de médico não encontrado.");
                }
            }
            else
            {
                return NotFound("Identificador de paciente não encontrado.");
            }
        }

        [HttpGet]
        public ActionResult<List<AtendimentosGetDTO>> ObterTodos()
        {
            List<AtendimentosGetDTO> listaAtendimentos = new List<AtendimentosGetDTO>();
            foreach (var atendimento in _dbContext.DbAtendimentos)
            {
                AtendimentosGetDTO atendimentoGet = new AtendimentosGetDTO()
                {
                    Id_Atendimento = atendimento.Id_Atendimento,
                    Descricao = atendimento.Descricao
                };
                PacienteModel pacienteModel = _dbContext.DbPacientes.Where(i => i.Id == atendimento.PacienteID).FirstOrDefault();
                atendimentoGet.Paciente = new PacienteSimplesGetDTO()
                {
                    Identificador = atendimento.PacienteID,
                    Nome = pacienteModel.Nome,
                    Convenio = pacienteModel.Convenio
                };
                MedicoModel medicoModel = _dbContext.DbMedicos.Where(i => i.Id == atendimento.MedicoId).FirstOrDefault();
                atendimentoGet.Medico = new MedicoSimplesGetDTO()
                {
                    Identificador = atendimento.MedicoId,
                    Nome = medicoModel.Nome,
                    EspecializacaoClinica = medicoModel.EspecializacaoClinica,
                    CRM_UF = medicoModel.CRM_UF
                };
                listaAtendimentos.Add(atendimentoGet);
            }
            return Ok(listaAtendimentos);
        }

        [HttpGet("{identificador}")]
        public ActionResult<AtendimentosGetDTO> ObterPorId([FromRoute] int identificador)
        {
            if (identificador != 0)
            {
                Atendimentos atendimento = _dbContext.DbAtendimentos.Where(i => i.Id_Atendimento == identificador).FirstOrDefault();
                if (atendimento != null)
                {
                    AtendimentosGetDTO atendimentoGet = new AtendimentosGetDTO()
                    {
                        Id_Atendimento = atendimento.Id_Atendimento,
                        Descricao = atendimento.Descricao
                    };
                    PacienteModel pacienteModel = _dbContext.DbPacientes.Where(i => i.Id == atendimento.PacienteID).FirstOrDefault();
                    atendimentoGet.Paciente = new PacienteSimplesGetDTO()
                    {
                        Identificador = atendimento.PacienteID,
                        Nome = pacienteModel.Nome,
                        Convenio = pacienteModel.Convenio
                    };
                    MedicoModel medicoModel = _dbContext.DbMedicos.Where(i => i.Id == atendimento.MedicoId).FirstOrDefault();
                    atendimentoGet.Medico = new MedicoSimplesGetDTO()
                    {
                        Identificador = atendimento.MedicoId,
                        Nome = medicoModel.Nome,
                        EspecializacaoClinica = medicoModel.EspecializacaoClinica
                    };
                    return Ok(atendimentoGet);
                }
                else
                {
                    return NotFound("Identificadornão encontrado.");
                }
            }
            else
            {
                return BadRequest("Identificador deve ser diferente de zero.");
            }
        }

        [HttpPut("{identificador}")]
        public ActionResult <AtendimentosGetDTO> EditarAtendimento([FromRoute]int identificador, [FromBody] AtendimentoPutDTO atendimentoEditado)
        {
            if(atendimentoEditado !=null || identificador != 0)
            {
                Atendimentos atendimento = _dbContext.DbAtendimentos.Where(i => i.Id_Atendimento == identificador).FirstOrDefault();
                if(atendimento != null)
                {
                    atendimento.Descricao = atendimentoEditado.Descricao;
                    _dbContext.DbAtendimentos.Attach(atendimento);
                    _dbContext.SaveChanges();
                    AtendimentosGetDTO atendimentoGet = new AtendimentosGetDTO()
                    {
                        Id_Atendimento = atendimento.Id_Atendimento,
                        Descricao = atendimento.Descricao
                    };
                    PacienteModel pacienteModel = _dbContext.DbPacientes.Where(i => i.Id == atendimento.PacienteID).FirstOrDefault();
                    atendimentoGet.Paciente = new PacienteSimplesGetDTO()
                    {
                        Identificador = atendimento.PacienteID,
                        Nome = pacienteModel.Nome,
                        Convenio = pacienteModel.Convenio
                    };
                    MedicoModel medicoModel = _dbContext.DbMedicos.Where(i => i.Id == atendimento.MedicoId).FirstOrDefault();
                    atendimentoGet.Medico = new MedicoSimplesGetDTO()
                    {
                        Identificador = atendimento.MedicoId,
                        Nome = medicoModel.Nome,
                        EspecializacaoClinica = medicoModel.EspecializacaoClinica
                    };
                    return Ok(atendimentoGet);
                }
                else
                {
                    return NotFound("Identificador não encontrado.");
                }
            }
            else
            {
                return BadRequest("Descrição de atendimento deve ser preenchido, e Identificador dierente de zero.");
            }
        }

        [HttpDelete("{identificador}")]
        public ActionResult DeletarAtendimento([FromRoute] int identificador)
        {
            if (identificador != 0)
            {
                Atendimentos atendimento = _dbContext.DbAtendimentos.Where(i => i.Id_Atendimento == identificador).FirstOrDefault();
                if (atendimento != null)
                {
                    _dbContext.DbAtendimentos.Remove(atendimento);
                    _dbContext.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound("Identificador não encontrado.");
                }
            }
            else
            {
                return BadRequest("Insira um identificador dieferente de zero.");
            }
        }

    }
}
