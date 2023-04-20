using API_Hospitalar.DTO.Paciente;
using API_Hospitalar.DTOs.Enfermeiros;
using API_Hospitalar.DTOs.Medicos;
using API_Hospitalar.Models;

namespace API_Hospitalar.IHospital
{
    public interface IHospitalService
    {
        public bool ValidacaoItensObrigatoriosPacientes(PacienteDTO paciente);
        public EnfermeiroModel EnfermeiroDTO_para_EnfermeiroModel(EnfermeiroModel enfermeiroModel, EnfermeiroDTO enfermeiroDTO);
        public EnfermeiroGetDTO EnfermeiroModel_para_EnfermeiroGetDTO(EnfermeiroModel enfermeiroModel);
        public PacienteGetDTO PacienteModel_para_GetDTO(PacienteModel pacienteModel);
        public PacienteModel PacienteDTO_para_Model(PacienteDTO novoPaciente);
        public MedicoModel MedicoDTO_para_Model(MedicoDTO medicoDTO, MedicoModel medicoModel);
        public MedicoGetDTO MedicoModel_para_GetDTO(MedicoModel medicoModel);
        public string ValidacaoItensObrigatoriosEnfermeiros(EnfermeiroDTO novoEnfermeiro);
        public string ValidacaoItensObrigatoriosMedicos(MedicoDTO novoMedico);
        public bool ValidacaoStatusAtendimento(string paciente);
    }
}
