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
using API_Hospitalar.DTOs.Atendimentos;

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

            if (pacienteModel.Status_De_Atendimento == "Atendido")
            {
                pacienteModel.TotalAtendimentos++;
            }
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
        public bool PacienteGet_ItensObrigatorios(PacienteDTO paciente)
        {
            if (paciente.Nome == null || paciente.Nome == "string" || paciente.Contato_de_Emergencia == null || paciente.Contato_de_Emergencia == "string"
                || paciente.Telefone == null || paciente.Telefone == "string")
            {
                return false;
            }
            else
            {
                bool statusAtendimento = ValidacaoStatusAtendimento(paciente.Status_De_Atendimento);
                if (statusAtendimento)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool PacientePut_ItensObrigatorios(PacientePutDTO paciente)
        {
            if (paciente.Nome == null || paciente.Nome == "string" || paciente.Contato_de_Emergencia == null || paciente.Contato_de_Emergencia == "string"
                || paciente.Telefone == null || paciente.Telefone == "string")
            {
                return false;
            }
            else
            {
                return true;
            }
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
            return (medicoModel);
        }
        public string ValidacaoItensObrigatoriosMedicos(MedicoDTO novoMedico)
        {
            string validacao;
            if (novoMedico.Nome == null || novoMedico.Nome == "string" || novoMedico.CPF == null || novoMedico.CPF == "string"
                || novoMedico.Data_de_Nascimento == null || novoMedico.CRM_UF == null || novoMedico.CRM_UF == "string"
                || novoMedico.Telefone == null || novoMedico.Telefone == "string" || novoMedico.InstituicaoDeFormacao == null
                || novoMedico.InstituicaoDeFormacao == "string")
            {
                validacao = "dadosNulos";
                return validacao;
            }
            else
            {
                if (novoMedico.EspecializacaoClinica == "Clínico Geral" || novoMedico.EspecializacaoClinica == "Clinico Geral" ||
                   novoMedico.EspecializacaoClinica == "Anestesista" || novoMedico.EspecializacaoClinica == "Dermatologia" ||
                   novoMedico.EspecializacaoClinica == "Ginecologia" || novoMedico.EspecializacaoClinica == "Neurologia" ||
                   novoMedico.EspecializacaoClinica == "Pediatria" || novoMedico.EspecializacaoClinica == "Psiquiatria" ||
                   novoMedico.EspecializacaoClinica == "Ortopedia")
                {
                    bool verificaCPF = BuscaCPF(novoMedico.CPF);
                    if (verificaCPF != true)
                    {
                        validacao = "CpfExistente";
                        return (validacao);
                    }
                    else
                    {
                        validacao = "ok";
                        return validacao;
                    }

                }
                else
                {
                    validacao = "problemaEspecializacao";
                    return validacao;
                }
            }
        }
        public string MedicoPutItensObrigatorios(MedicoPutDTO medicoEditado)
        {
            string validacao;
            if (medicoEditado.Nome == null || medicoEditado.Nome == "string" || medicoEditado.CRM_UF == null || medicoEditado.CRM_UF == "string"
                || medicoEditado.Telefone == null || medicoEditado.Telefone == "string" || medicoEditado.InstituicaoDeFormacao == null
                || medicoEditado.InstituicaoDeFormacao == "string")
            {
                validacao = "dadosNulos";
                return validacao;
            }
            else
            {
                if (medicoEditado.EspecializacaoClinica == "Clínico Geral" || medicoEditado.EspecializacaoClinica == "Clinico Geral" ||
                   medicoEditado.EspecializacaoClinica == "Anestesista" || medicoEditado.EspecializacaoClinica == "Dermatologia" ||
                   medicoEditado.EspecializacaoClinica == "Ginecologia" || medicoEditado.EspecializacaoClinica == "Neurologia" ||
                   medicoEditado.EspecializacaoClinica == "Pediatria" || medicoEditado.EspecializacaoClinica == "Psiquiatria" ||
                   medicoEditado.EspecializacaoClinica == "Ortopedia")
                {
                    validacao = "ok";
                    return validacao;

                }
                else
                {
                    validacao = "problemaEspecializacao";
                    return validacao;
                }
            }
        }


        public string AtendimentoItensObrigatorios (AtendimentosDTO atendimento)
        {
            string resultado;
            if (atendimento.Descricao == null || atendimento.Descricao == "string" || atendimento.Identificador_medico == 0 || atendimento.Identificador_paciente == 0)
            {
                resultado = "faltamInformacoes";
                return resultado;
            }
            else
            {
                resultado = "ok";
                return resultado;
            }
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
            return enfermeiroModel;
        }
        public EnfermeiroModel EnfermeiroPut_para_Model (EnfermeiroPutDTO enfermeiroEditado, EnfermeiroModel enfermeiroModel)
        {
            enfermeiroModel.Nome = enfermeiroEditado.Nome;
            enfermeiroModel.Telefone = enfermeiroEditado.Telefone;
            enfermeiroModel.CadastroCOFEN_UF = enfermeiroEditado.CadastroCOFEN_UF;
            enfermeiroModel.InstituicaoDeFormacao = enfermeiroEditado.InstituicaoDeFormacao;
            enfermeiroModel.Genero = Genero(enfermeiroEditado.Genero);
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
        public string ValidacaoItensObrigatoriosEnfermeiros(EnfermeiroDTO novoEnfermeiro)
        {
            string resultado;

            if (novoEnfermeiro.Nome == null || novoEnfermeiro.Nome == "string" || novoEnfermeiro.CPF == null || novoEnfermeiro.CPF == "string"
                || novoEnfermeiro.Data_de_Nascimento == null || novoEnfermeiro.CadastroCOFEN_UF == null || novoEnfermeiro.CadastroCOFEN_UF == "string"
                || novoEnfermeiro.Telefone == null || novoEnfermeiro.Telefone == "string" || novoEnfermeiro.InstituicaoDeFormacao == null
                || novoEnfermeiro.InstituicaoDeFormacao == "string")
            {
                resultado = "faltaDadosObrigatorios";
                return resultado;
            }
            else
            {
                bool verificaCPF = BuscaCPF(novoEnfermeiro.CPF);
                if (verificaCPF)
                {
                    resultado = "CpfExistente";
                    return resultado;
                }
                resultado = "Ok";
                return resultado; ;
            }
        }
        public bool ValidacaoEnfermeiroPutDTO(EnfermeiroPutDTO enfermeiro)
        {
            if (enfermeiro.Nome == null || enfermeiro.Nome == "string" || enfermeiro.CadastroCOFEN_UF == null || enfermeiro.CadastroCOFEN_UF == "string"
                || enfermeiro.Telefone == null || enfermeiro.Telefone == "string" || enfermeiro.InstituicaoDeFormacao == null
                || enfermeiro.InstituicaoDeFormacao == "string")
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public bool ValidacaoStatusAtendimento(string status)
        {
            List<string> possibilidades = new List<string>() {"AGUARDANDO_ATENDIMENTO", "ATENDIDO", "NAO_ATENDIDO", "EM_ATENDIMENTO"};
            foreach(var item in possibilidades)
            {
                if(item == status)
                {
                    return true;
                }
            }
            return false;
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
        private bool BuscaCPF (string cpf)
        {
            
            PacienteModel buscacpfPaciente = _dbContext.DbPacientes.Where(i => i.CPF == cpf).FirstOrDefault();
            if (buscacpfPaciente == null)
            {
                MedicoModel buscacpfMedico = _dbContext.DbMedicos.Where(i => i.CPF == cpf).FirstOrDefault();
                if (buscacpfMedico == null)
                {
                    EnfermeiroModel buscacpfEnfermeiro = _dbContext.DbEnfermeiros.Where(i => i.CPF == cpf).FirstOrDefault();
                    if (buscacpfEnfermeiro == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
    }
}