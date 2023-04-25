using API_Hospitalar.DTOs.Alergias;
using API_Hospitalar.HospitalContextDb;
using API_Hospitalar.IHospital;
using API_Hospitalar.Models;
using ClinicVault.DTOs.Alergias;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Globalization;

namespace ClinicVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlergiaController : ControllerBase
    {
        private readonly HospitalContext _dbContext;
        
        public AlergiaController(HospitalContext hospitalContext)
        {
            _dbContext = hospitalContext;
        }

        [HttpPost]
        public ActionResult<AlergiaGetDTO> Adicionar([FromBody] AlergiaPostDTO novaAlergia)
        {
            PacienteModel verificaSeExistePaciente = _dbContext.DbPacientes.Find(novaAlergia.Identificador_do_paciente);
            if (verificaSeExistePaciente == null)
            {
                return NotFound("Identificador de paciente não existe.");
            }
            Alergias alergiaModel = new Alergias()
            {
                DescricaoAlergia = novaAlergia.Descricao,
                PacienteID = novaAlergia.Identificador_do_paciente
            };
            _dbContext.DbAlergias.Add(alergiaModel);
            _dbContext.SaveChanges();
            AlergiaGetDTO alergiaGet = new AlergiaGetDTO()
            {
                Identificador_alergia = alergiaModel.Id,
                Descricao_Alergia = alergiaModel.DescricaoAlergia,
                Identificador_paciente = alergiaModel.PacienteID
            };
            return Created(Request.Path,(alergiaGet));
        }

        [HttpGet]
        public ActionResult<List<AlergiaGetDTO>> ObterTodos()
        {
            List<AlergiaGetDTO> listaAlergiaGet = new List<AlergiaGetDTO>();
            foreach (var alergia in _dbContext.DbAlergias)
            {
                AlergiaGetDTO alergiaGet = new AlergiaGetDTO()
                {
                    Identificador_alergia = alergia.Id,
                    Descricao_Alergia = alergia.DescricaoAlergia,
                    Identificador_paciente = alergia.PacienteID
                };
                listaAlergiaGet.Add(alergiaGet);
            }
            return Ok(listaAlergiaGet);
        }

        [HttpGet("{identificador}")]
        public ActionResult<AlergiaGetDTO> ObterPorId([FromRoute] int identificador)
        {
            if (identificador == 0)
            {
                return BadRequest("Por favor, insira um identificador diferente de zero.");
            }
            Alergias alergiaModel = _dbContext.DbAlergias.Find(identificador);
            if (alergiaModel == null)
            {
                return NotFound("Identificador de alergia não encontrado.");
            }
            AlergiaGetDTO alergiaGet = new AlergiaGetDTO()
            {
                Identificador_alergia = alergiaModel.Id,
                Descricao_Alergia = alergiaModel.DescricaoAlergia,
                Identificador_paciente = alergiaModel.PacienteID
            };
            return Ok(alergiaGet); 
        }

        [HttpPut("{identificador}")]
        public ActionResult<AlergiaGetDTO> AtualizarAlergia([FromRoute] int identificador, [FromBody] AlergiaPutDTO alergiaPut)
        {
            if (identificador == 0)
            {
                return BadRequest("Por favor, insira um identificador diferente de zero.");
            }
            Alergias alergiaModel = _dbContext.DbAlergias.Find(identificador);
            if (alergiaModel == null)
            {
                return NotFound("Identificador de alergia não encontrado.");
            }
            alergiaModel.DescricaoAlergia = alergiaPut.Descricao_alergia;
            _dbContext.DbAlergias.Attach(alergiaModel);
            _dbContext.SaveChanges();
            AlergiaGetDTO alergiaGet = new AlergiaGetDTO()
            {
                Identificador_alergia = alergiaModel.Id,
                Descricao_Alergia = alergiaModel.DescricaoAlergia,
                Identificador_paciente = alergiaModel.PacienteID
            };
            return Ok(alergiaGet);
        }

        [HttpDelete("{identificador}")]
        public ActionResult DeletarAlergia([FromRoute] int identificador)
        {
            if (identificador == 0)
            {
                return BadRequest("Por favor, insira um identificador diferente de zero.");
            }
            Alergias alergiaModel = _dbContext.DbAlergias.Find(identificador);
            if (alergiaModel == null)
            {
                return NotFound("Identificador de alergia não encontrado.");
            }
            _dbContext.DbAlergias.Remove(alergiaModel);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
