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
        public PacienteModel PacienteDTO_para_Model(PacienteDTO novoPaciente)
        {
           PacienteModel pacienteModel = new PacienteModel()
            {
                Nome = novoPaciente.Nome,
                CPF = novoPaciente.CPF,
                Telefone = novoPaciente.Telefone,
                Data_de_Nascimento = novoPaciente.Data_de_Nascimento.ToString("d/MMM/yyyy"),
                Contato_de_Emergencia = novoPaciente.Contato_de_Emergencia,
                Genero = Genero (novoPaciente.Genero)
            };
            if (novoPaciente.Convenio != "string") 
            { 
                pacienteModel.Convenio = novoPaciente.Convenio; 
            } 
            else 
            { 
                pacienteModel.Convenio = null; novoPaciente.Convenio = "Null"; 
            }
            if (novoPaciente.Descricao_alergia != null && novoPaciente.Descricao_alergia != "string")
            {
                Alergias alergia = new Alergias();
                alergia.DescricaoAlergia = novoPaciente.Descricao_alergia;

                var buscaIdIdentado = _dbContext.DbPacientes.Where(paciente => paciente.CPF == pacienteModel.CPF).FirstOrDefault();

                alergia.PacienteID = buscaIdIdentado.Id;
                _dbContext.DbAlergias.Add(alergia);
                _dbContext.SaveChanges();
            }

            if (novoPaciente.Descricao_cuidados != null && novoPaciente.Descricao_cuidados != "string")
            {
                Cuidados cuidadosEspecificos = new Cuidados();
                cuidadosEspecificos.DescricaoCuidado = novoPaciente.Descricao_cuidados;

                var buscaIdIdentado = _dbContext.DbPacientes.Where(paciente => paciente.CPF == pacienteModel.CPF).FirstOrDefault();

                cuidadosEspecificos.PacienteID = buscaIdIdentado.Id;
                _dbContext.DbCuidados.Add(cuidadosEspecificos);
                _dbContext.SaveChanges();
            }
            return (pacienteModel);
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
        public bool ValidacaoItensObrigatoriosPacientes(PacienteDTO paciente)
        {
            if (paciente.Nome == null || paciente.Nome == "string" || paciente.CPF == null || paciente.CPF == "string"
                || paciente.Data_de_Nascimento == null || paciente.Contato_de_Emergencia == null || paciente.Contato_de_Emergencia == "string"
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
        public string ValidacaoItensObrigatoriosEnfermeiros(EnfermeiroDTO novoEnfermeiro)
        {
            string resultado;

            if (novoEnfermeiro.Nome == null || novoEnfermeiro.Nome == "string" || novoEnfermeiro.CPF == null || novoEnfermeiro.CPF == "string"
                || novoEnfermeiro.Data_de_Nascimento == null || novoEnfermeiro.CadastroCOFEN_UF == null || novoEnfermeiro.CadastroCOFEN_UF == "string"
                || novoEnfermeiro.Telefone == null || novoEnfermeiro.Telefone == "string" || novoEnfermeiro.InstituicaoDeFormacao==null
                || novoEnfermeiro.InstituicaoDeFormacao=="string")
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
                if(novoMedico.EspecializacaoClinica == "Clínico Geral" || novoMedico.EspecializacaoClinica == "Clinico Geral" ||
                   novoMedico.EspecializacaoClinica == "Anestesista" ||novoMedico.EspecializacaoClinica == "Dermatologia" ||
                   novoMedico.EspecializacaoClinica == "Ginecologia" ||novoMedico.EspecializacaoClinica == "Neurologia" ||
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