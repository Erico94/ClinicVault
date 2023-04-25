using API_Hospitalar.DTOs.Alergias;
using API_Hospitalar.DTOs.Cuidados;
using API_Hospitalar.HospitalContextDb;
using API_Hospitalar.IHospital;
using API_Hospitalar.Models;
using ClinicVault.DTOs.Alergias;
using ClinicVault.DTOs.Cuidados;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuidadosController : ControllerBase
    {
        private readonly HospitalContext _dbContext;
        public CuidadosController(HospitalContext hospitalContext)
        {
            _dbContext = hospitalContext;
        }

        [HttpPost]
        public ActionResult<CuidadosGetDTO> AdicionarCuidado([FromBody] CuidadoPostDTO novoCuidado)
        {
            PacienteModel verificaSeExiste = _dbContext.DbPacientes.Find(novoCuidado.Identificador_paciente);
            if (verificaSeExiste == null)
            {
                return NotFound("Identificador de paciente não existe.");
            }
            Cuidados cuidadoModel = new Cuidados()
            {
                DescricaoCuidado = novoCuidado.Descricao_cuidado,
                PacienteID = novoCuidado.Identificador_paciente
            };
            _dbContext.DbCuidados.Add(cuidadoModel);
            _dbContext.SaveChanges();
            CuidadosGetDTO cuidadoGet = new CuidadosGetDTO()
            {
                Identificador = cuidadoModel.Id,
                DescricaoCuidado = cuidadoModel.DescricaoCuidado,
                Identificador_paciente = cuidadoModel.PacienteID
            };
            return Created(Request.Path, (cuidadoGet));
        }

        [HttpGet]
        public ActionResult<List<CuidadosGetDTO>> ObterTodos()
        {
            List<CuidadosGetDTO> listaCuidadoGet = new List<CuidadosGetDTO>();
            foreach (var cuidado in _dbContext.DbCuidados)
            {
                CuidadosGetDTO cuidadoGet = new CuidadosGetDTO()
                {
                    Identificador = cuidado.Id,
                    DescricaoCuidado = cuidado.DescricaoCuidado,
                    Identificador_paciente = cuidado.PacienteID
                };
                listaCuidadoGet.Add(cuidadoGet);
            }
            return Ok(listaCuidadoGet);
        }
        [HttpGet("{identificador}")]
        public ActionResult<CuidadosGetDTO> ObterPorId([FromRoute] int identificador)
        {
            if (identificador == 0)
            {
                return BadRequest("Por favor, insira um identificador diferente de zero.");
            }
            Cuidados cuidadoModel = _dbContext.DbCuidados.Find(identificador);
            if (cuidadoModel == null)
            {
                return NotFound("Identificador de cuidado não encontrado.");
            }
            CuidadosGetDTO cuidadoGet = new CuidadosGetDTO()
            {
                Identificador = cuidadoModel.Id,
                DescricaoCuidado = cuidadoModel.DescricaoCuidado,
                Identificador_paciente = cuidadoModel.PacienteID
            };
            return Ok(cuidadoGet);
        }

        [HttpPut("{identificador}")]
        public ActionResult<CuidadosGetDTO> AtualizarCuidado([FromRoute] int identificador, [FromBody] CuidadoPutDTO cuidadoPut)
        {
            if (identificador == 0)
            {
                return BadRequest("Por favor, insira um identificador diferente de zero.");
            }
            Cuidados cuidadoModel = _dbContext.DbCuidados.Find(identificador);
            if (cuidadoModel == null)
            {
                return NotFound("Identificador de cuidado não encontrado.");
            }
            cuidadoModel.DescricaoCuidado = cuidadoPut.Descricao_cuidado;
            _dbContext.DbCuidados.Attach(cuidadoModel);
            _dbContext.SaveChanges();
            CuidadosGetDTO cuidadoGet = new CuidadosGetDTO()
            {
                Identificador = cuidadoModel.Id,
                DescricaoCuidado = cuidadoModel.DescricaoCuidado,
                Identificador_paciente = cuidadoModel.PacienteID
            };
            return Ok(cuidadoGet);
        }

        [HttpDelete("{identificador}")]
        public ActionResult DeletarCuidado([FromRoute] int identificador)
        {
            if (identificador == 0)
            {
                return BadRequest("Por favor, insira um identificador diferente de zero.");
            }
            Cuidados cuidadoModel = _dbContext.DbCuidados.Find(identificador);
            if (cuidadoModel == null)
            {
                return NotFound("Identificador de cuidado não encontrado.");
            }
            _dbContext.DbCuidados.Remove(cuidadoModel);
            _dbContext.SaveChanges();
            return Ok();
        }

    }
}
