﻿using API_Hospitalar.DTOs.Medicos;
using API_Hospitalar.HospitalContextDb;
using API_Hospitalar.HospitalServices;
using API_Hospitalar.IHospital;
using API_Hospitalar.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace API_Hospitalar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly HospitalContext _dbContext;
        private IHospitalService _IService;

        public MedicoController(HospitalContext dbContext, IHospitalService IService)
        {
            _IService = IService;
            _dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult<MedicoGetDTO> CadastroDeMedico ([FromBody] MedicoDTO medicoDTO)
        {
            var verificaCpf = _dbContext.DbMedicos.Where(i => i.CPF == medicoDTO.CPF).FirstOrDefault();
                if(verificaCpf != null)
                {
                    return Conflict("Este CPF ja consta em nosso sistema.");
                }
                else
                {
                    MedicoModel medicoModel = new MedicoModel();
                    _IService.MedicoDTO_para_Model(medicoDTO, medicoModel);
                    MedicoGetDTO medicoGetDTO = _IService.MedicoModel_para_GetDTO(medicoModel);
                        
                    return Created(Request.Path, medicoGetDTO);
                }
            
        }

        [HttpGet]
        public ActionResult<List<MedicoGetDTO>> ObterTodosMedicos([FromQuery] string estado_no_sistema)
        {
            List<MedicoGetDTO> listaMedicoGetDTO = new List<MedicoGetDTO>();
            if (estado_no_sistema != null)
            {
                if (estado_no_sistema == "true" || estado_no_sistema == "Ativo" || estado_no_sistema == "ativo")
                {
                    foreach (var medico in _dbContext.DbMedicos)
                    {
                        if (medico.EstadoNoSistema == "Ativo")
                        {
                            var medicoGetDTO = _IService.MedicoModel_para_GetDTO(medico);
                            listaMedicoGetDTO.Add(medicoGetDTO);
                        }
                    }
                    return Ok(listaMedicoGetDTO);
                }
                else if (estado_no_sistema == "false" || estado_no_sistema == "Inativo" || estado_no_sistema == "inativo")
                {
                    foreach (var medico in _dbContext.DbMedicos)
                    {
                        if (medico.EstadoNoSistema == "Inativo")
                        {
                            var medicoGetDTO = _IService.MedicoModel_para_GetDTO(medico);
                            listaMedicoGetDTO.Add(medicoGetDTO);
                        }
                    }
                    return Ok(listaMedicoGetDTO);
                }
                else
                {
                    return BadRequest("Por favor, insira um item válido em 'estado_no_sistema'. Valores aceitos: true, Ativo, false ou Inativo");
                }
            }
            else
            {
                foreach (var medico in _dbContext.DbMedicos)
                {
                    var medicoGetDTO = _IService.MedicoModel_para_GetDTO(medico);
                    listaMedicoGetDTO.Add(medicoGetDTO);
                }
                return Ok(listaMedicoGetDTO);
            }
        }

        [HttpGet("{identificador}")]
        public ActionResult<MedicoGetDTO> ObterPorId([FromRoute] int identificador)
        {
            if (identificador != 0)
            {
                MedicoModel medicoModel = _dbContext.DbMedicos.Where(i => i.Id == identificador).FirstOrDefault();
                if (medicoModel != null)
                {
                    MedicoGetDTO medicoGetDTO = _IService.MedicoModel_para_GetDTO(medicoModel);
                    return Ok(medicoGetDTO);
                }
                else
                {
                    return NotFound("Identificador não encontrado.");
                }
            }
            else
            {
                return BadRequest("Por favor insira o número de identificação do médico. Deve ser diferente de zero.");
            }
        }

        [HttpPut("{identificador}")]
        public ActionResult <MedicoGetDTO> EditarMedico ([FromBody] MedicoPutDTO medicoEditado,[FromRoute] int identificador)
        {
            MedicoModel buscaMedico = _dbContext.DbMedicos.Where(i => i.Id == identificador).FirstOrDefault();
            if (buscaMedico != null)
            {
                buscaMedico = _IService.MedicoPutDTO_para_Model(medicoEditado, buscaMedico);
                MedicoGetDTO medicoGetDTO = _IService.MedicoModel_para_GetDTO(buscaMedico);
                return Ok (medicoGetDTO);
            }
            else
            {
                return NotFound("Não foi possivel localizar o identiicador em nosso sistema.");
            }
        }

        [HttpPut("status/{identificador}")]
        public ActionResult <MedicoGetDTO> AtualizarStatusMedico ([FromRoute] int identificador, [FromBody] EstadoSistemaDTO medicoEditado)
        {
            MedicoModel buscaMedico = _dbContext.DbMedicos.Where(i => i.Id == identificador).FirstOrDefault();
            if (buscaMedico != null)
            {
                if (medicoEditado.Estado_No_Sistema == true)
                {
                    buscaMedico.EstadoNoSistema = "Ativo";
                }
                else
                {
                    buscaMedico.EstadoNoSistema = "Inativo";
                }
                _dbContext.DbMedicos.Attach(buscaMedico);
                _dbContext.SaveChanges();
                var medicoGetDto = _IService.MedicoModel_para_GetDTO(buscaMedico);
                return Ok(medicoGetDto);
            }
            else
            {
                return NotFound("Identificador não encontrado");
            }
        }

        [HttpDelete("{identificador}")]
        public ActionResult DeletarPorId([FromRoute] int identificador)
        {
            if (identificador != 0)
            {
                MedicoModel medicoModel = _dbContext.DbMedicos.Where(i => i.Id == identificador).FirstOrDefault();
                if (medicoModel != null)
                {
                    foreach (var atendimento in _dbContext.DbAtendimentos)
                    {
                        if (atendimento.MedicoId == medicoModel.Id)
                        {
                            _dbContext.DbAtendimentos.Remove(atendimento);
                            _dbContext.SaveChanges();
                        }
                    }
                    _dbContext.DbMedicos.Remove(medicoModel);
                    _dbContext.SaveChanges();
                    return NoContent();
                }
                else
                {
                    return NotFound("Identificador não encontrado.");
                }
            }
            else
            {
                return BadRequest("Por favor insira o número de identificação do médico. Deve ser diferente de zero.");
            }
        }
    }
}
