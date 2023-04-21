using API_Hospitalar.Models;
using API_Hospitalar.IHospital;
using API_Hospitalar.DTO.Paciente;
using API_Hospitalar.DTOs.Enfermeiros;
using API_Hospitalar.DTOs.Medicos;
using API_Hospitalar.DTOs.Alergias;
using API_Hospitalar.DTOs.AtendimentosDTO;
using API_Hospitalar.DTOs.Cuidados;
using Microsoft.EntityFrameworkCore;
using API_Hospitalar.HospitalContextDb;
using API_Hospitalar.DTOs.Pacientes;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API_Hospitalar.HospitalServices
{
    public class HospitalService : IHospitalService
    {
        private readonly HospitalContext _dbContext;
        public HospitalService (HospitalContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PacienteGetDTO PacienteModel_para_GetDTO( PacienteModel pacienteModel)
        {
            PacienteGetDTO pacienteGetDTO = new PacienteGetDTO()
            {
                Identificador = pacienteModel.Id,
                Nome = pacienteModel.Nome,
                CPF = pacienteModel.CPF,
                Telefone = pacienteModel.Telefone,
                Convenio = pacienteModel.Convenio,
                Data_de_Nascimento = pacienteModel.Data_de_Nascimento,
                Genero = pacienteModel.Genero,
                Contato_de_Emergencia = pacienteModel.Contato_de_Emergencia,
                Status_De_Atendimento = pacienteModel.Status_De_Atendimento,
                TotalAtendimentos = pacienteModel.TotalAtendimentos
            };
            pacienteGetDTO.Alergias = new List<AlergiaGetDTO>();
            foreach (var alergia in _dbContext.DbAlergias)
            {
                if (alergia.PacienteID == pacienteGetDTO.Identificador)
                {
                    AlergiaGetDTO alergiaDTO = new AlergiaGetDTO()
                    {
                        Id_alergia = alergia.Id,
                        Identificador_paciente = alergia.PacienteID,
                        DescricaoAlergia = alergia.DescricaoAlergia
                    };
                    pacienteGetDTO.Alergias.Add(alergiaDTO);
                }
            }

            pacienteGetDTO.Cuidados_Especificos = new List<CuidadosGetDTO>();
            foreach (var cuidado in _dbContext.DbCuidados)
            {
                if (cuidado.PacienteID == pacienteGetDTO.Identificador)
                {
                    CuidadosGetDTO cuidadoDTO = new CuidadosGetDTO()
                    {
                        Id = cuidado.Id,
                        DescricaoCuidado = cuidado.DescricaoCuidado,
                        Identificador_paciente = cuidado.PacienteID
                    };
                    pacienteGetDTO.Cuidados_Especificos.Add(cuidadoDTO);
                }
            }

            pacienteGetDTO.Atendimentos = new List<AtendimentosGetDTO>();
            foreach (var atendimento in _dbContext.DbAtendimentos)
            {
                if (atendimento.PacienteID == pacienteGetDTO.Identificador)
                {
                    AtendimentosGetDTO atendimentoDTO = new AtendimentosGetDTO()
                    {
                        Id_Atendimento = atendimento.Id_Atendimento,
                        Descricao = atendimento.Descricao,
                        Identificador_medico = atendimento.MedicoId,
                        Identificador_paciente = atendimento.PacienteID
                    };
                    pacienteGetDTO.Atendimentos.Add(atendimentoDTO);
                }
            }
            return pacienteGetDTO;
        }
        public PacienteModel PacienteDTO_para_Model(PacienteDTO novoPaciente, PacienteModel pacienteModel)
        {
            pacienteModel.Nome = novoPaciente.Nome;
            pacienteModel.CPF = novoPaciente.CPF;
            pacienteModel.Telefone = novoPaciente.Telefone;
            pacienteModel.Data_de_Nascimento = novoPaciente.Data_de_Nascimento.ToString("d/MMM/yyyy");
            pacienteModel.Contato_de_Emergencia = novoPaciente.Contato_de_Emergencia;
            pacienteModel.Genero = Genero(novoPaciente.Genero);
            pacienteModel.Status_De_Atendimento = novoPaciente.Status_De_Atendimento;
            
            if (novoPaciente.Convenio != "string") 
            { 
                pacienteModel.Convenio = novoPaciente.Convenio; 
            } 
            else 
            { 
                pacienteModel.Convenio = null; novoPaciente.Convenio = "Null"; 
            }

            if (novoPaciente.Alergias != null && novoPaciente.Alergias != "string")
            {
                Alergias alergia = new Alergias();
                alergia.DescricaoAlergia = novoPaciente.Alergias;

                var buscaIdIdentado = _dbContext.DbPacientes.Where(paciente => paciente.CPF == pacienteModel.CPF).FirstOrDefault();

                alergia.PacienteID = buscaIdIdentado.Id;
                _dbContext.DbAlergias.Add(alergia);
                _dbContext.SaveChanges();
            }

            if (novoPaciente.Cuidados_especificos != null && novoPaciente.Cuidados_especificos != "string")
            {
                Cuidados cuidadosEspecificos = new Cuidados();
                cuidadosEspecificos.DescricaoCuidado = novoPaciente.Cuidados_especificos;

                var buscaIdIdentado = _dbContext.DbPacientes.Where(paciente => paciente.CPF == pacienteModel.CPF).FirstOrDefault();

                cuidadosEspecificos.PacienteID = buscaIdIdentado.Id;
                _dbContext.DbCuidados.Add(cuidadosEspecificos);
                _dbContext.SaveChanges();
            }

            if (pacienteModel.Status_De_Atendimento == "ATENDIDO")
            {
                pacienteModel.TotalAtendimentos++;
            }
            _dbContext.DbPacientes.Add(pacienteModel);
            _dbContext.SaveChanges();
            return (pacienteModel);
        }
        public PacienteModel PacientePut_para_Model(PacientePutDTO pacienteEditado, PacienteModel pacienteModel)
        {
            pacienteModel.Nome = pacienteEditado.Nome;
            pacienteModel.Telefone = pacienteEditado.Telefone;
            pacienteModel.Contato_de_Emergencia = pacienteEditado.Contato_de_Emergencia;
            pacienteModel.Genero = Genero(pacienteEditado.Genero);

            if (pacienteEditado.Convenio != "string")
            {
                pacienteModel.Convenio = pacienteEditado.Convenio;
            }
            else
            {
                pacienteModel.Convenio = null; pacienteEditado.Convenio = "Null";
            }
            return pacienteModel;
        }


        public MedicoGetDTO MedicoModel_para_GetDTO(MedicoModel medicoModel)
        {
            MedicoGetDTO medicoGet = new MedicoGetDTO()
            {
                Identificador = medicoModel.Id,
                Nome = medicoModel.Nome,
                Genero = medicoModel.Genero,
                Data_de_Nascimento = medicoModel.Data_de_Nascimento,
                CPF = medicoModel.CPF,
                Telefone = medicoModel.Telefone,
                InstituicaoDeFormacao = medicoModel.InstituicaoDeFormacao,
                CRM_UF = medicoModel.CRM_UF,
                EspecializacaoClinica = medicoModel.EspecializacaoClinica,
                EstadoNoSistema = medicoModel.EstadoNoSistema,
                Atendimentos = medicoModel.TotalAtendimentos,
            };
            medicoGet.Lista_de_Atendimentos = new List<AtendimentosGetDTO>();
            foreach (var atendimento in _dbContext.DbAtendimentos)
            {
                if (atendimento.MedicoId == medicoGet.Identificador)
                {
                    AtendimentosGetDTO atendimentoDTO = new AtendimentosGetDTO()
                    {
                        Id_Atendimento = atendimento.Id_Atendimento,
                        Descricao = atendimento.Descricao,
                        Identificador_medico = atendimento.MedicoId,
                        Identificador_paciente = atendimento.PacienteID
                    };
                    medicoGet.Lista_de_Atendimentos.Add(atendimentoDTO);
                }
            }
            return medicoGet;
        }
        public MedicoModel MedicoDTO_para_Model(MedicoDTO medicoDTO, MedicoModel medicoModel)
        {
            medicoModel.Nome = medicoDTO.Nome;
            medicoModel.Data_de_Nascimento = medicoDTO.Data_de_Nascimento.ToString("d/MMM/yyyy");
            medicoModel.CPF = medicoDTO.CPF;
            medicoModel.Telefone = medicoDTO.Telefone;
            medicoModel.InstituicaoDeFormacao = medicoDTO.InstituicaoDeFormacao;
            medicoModel.CRM_UF = medicoDTO.CRM_UF;
            medicoModel.EspecializacaoClinica = medicoDTO.EspecializacaoClinica;
            medicoModel.Genero = Genero(medicoDTO.Genero);
            if (medicoDTO.EstadoNoSistema == true)
            {
                medicoModel.EstadoNoSistema = "Ativo";
            }
            else
            {
                medicoModel.EstadoNoSistema = "Inativo";
            }
            _dbContext.DbMedicos.Add(medicoModel);
            _dbContext.SaveChanges();
            return (medicoModel);
        }
        public MedicoModel MedicoPutDTO_para_Model(MedicoPutDTO medicoPutDTO, MedicoModel medicoModel)
        {
            medicoModel.Nome = medicoPutDTO.Nome;
            medicoModel.Telefone = medicoPutDTO.Telefone;
            medicoModel.InstituicaoDeFormacao = medicoPutDTO.InstituicaoDeFormacao;
            medicoModel.CRM_UF = medicoPutDTO.CRM_UF;
            medicoModel.EspecializacaoClinica = medicoPutDTO.EspecializacaoClinica;
            medicoModel.Genero = Genero(medicoPutDTO.Genero);
            _dbContext.DbMedicos.Attach(medicoModel);
            _dbContext.SaveChanges();
            return (medicoModel);
        }


        public EnfermeiroModel EnfermeiroDTO_para_EnfermeiroModel (EnfermeiroModel enfermeiroModel, EnfermeiroDTO enfermeiroDTO)
        {
            enfermeiroModel.Nome = enfermeiroDTO.Nome;
            enfermeiroModel.CPF = enfermeiroDTO.CPF;
            enfermeiroModel.Telefone = enfermeiroDTO.Telefone;
            enfermeiroModel.Data_de_Nascimento = enfermeiroDTO.Data_de_Nascimento.ToString("dd,MM,yyyy");
            enfermeiroModel.CadastroCOFEN_UF = enfermeiroDTO.CadastroCOFEN_UF;
            enfermeiroModel.InstituicaoDeFormacao = enfermeiroDTO.InstituicaoDeFormacao;
            enfermeiroModel.Genero = Genero(enfermeiroDTO.Genero);
            _dbContext.DbEnfermeiros.Add(enfermeiroModel);
            _dbContext.SaveChanges();
            return enfermeiroModel;
        }
        public EnfermeiroModel EnfermeiroPut_para_Model (EnfermeiroPutDTO enfermeiroEditado, EnfermeiroModel enfermeiroModel)
        {
            enfermeiroModel.Nome = enfermeiroEditado.Nome;
            enfermeiroModel.Telefone = enfermeiroEditado.Telefone;
            enfermeiroModel.CadastroCOFEN_UF = enfermeiroEditado.CadastroCOFEN_UF;
            enfermeiroModel.InstituicaoDeFormacao = enfermeiroEditado.InstituicaoDeFormacao;
            enfermeiroModel.Genero = Genero(enfermeiroEditado.Genero);
            _dbContext.DbEnfermeiros.Attach(enfermeiroModel);
            _dbContext.SaveChanges();
            return enfermeiroModel;
        }
        public EnfermeiroGetDTO EnfermeiroModel_para_EnfermeiroGetDTO (EnfermeiroModel enfermeiroModel)
        {
            EnfermeiroGetDTO enfermeiroGetDTO = new EnfermeiroGetDTO()
            {
                Identificador = enfermeiroModel.Id,
                Nome = enfermeiroModel.Nome,
                Genero = enfermeiroModel.Genero,
                Data_de_Nascimento = enfermeiroModel.Data_de_Nascimento,
                CPF = enfermeiroModel.CPF,
                Telefone = enfermeiroModel.Telefone,
                CadastroCOFEN_UF = enfermeiroModel.CadastroCOFEN_UF,
                InstituicaoDeFormacao = enfermeiroModel.InstituicaoDeFormacao
            };
            return enfermeiroGetDTO;
        }
        
        private string Genero (string genero)
        {
            List<string> valores = new List<string>() { "Masculino", "Feminino", "masculino", "feminino", "M", "m", "F", "f", "Outro", "outro",
                "Bissexual", "bissexual", "Homossexual", "homossexual", "Transexual", "transexual" };
            foreach ( var item in valores )
            {
                if (item == genero)
                {
                    return genero;
                }
            }
            genero = "Não informado";
            return genero;
        }

    }
}