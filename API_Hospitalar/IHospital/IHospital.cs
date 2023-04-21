using API_Hospitalar.DTO.Paciente;
using API_Hospitalar.DTOs.AtendimentosDTO;
using API_Hospitalar.DTOs.Enfermeiros;
using API_Hospitalar.DTOs.Medicos;
using API_Hospitalar.DTOs.Pacientes;
using API_Hospitalar.Models;

namespace API_Hospitalar.IHospital
{
    public interface IHospitalService
    {
        
        public EnfermeiroModel EnfermeiroDTO_para_EnfermeiroModel(EnfermeiroModel enfermeiroModel, EnfermeiroDTO enfermeiroDTO);
        public EnfermeiroModel EnfermeiroPut_para_Model(EnfermeiroPutDTO enfermeiroEditado, EnfermeiroModel enfermeiroModel);
        public EnfermeiroGetDTO EnfermeiroModel_para_EnfermeiroGetDTO(EnfermeiroModel enfermeiroModel);

        public PacienteGetDTO PacienteModel_para_GetDTO(PacienteModel pacienteModel);
        public PacienteModel PacienteDTO_para_Model(PacienteDTO novoPaciente, PacienteModel pacienteModel);
        public PacienteModel PacientePut_para_Model(PacientePutDTO pacienteEditado, PacienteModel pacienteModel);

        public MedicoModel MedicoDTO_para_Model(MedicoDTO medicoDTO, MedicoModel medicoModel);
        public MedicoModel MedicoPutDTO_para_Model(MedicoPutDTO medicoPutDTO, MedicoModel medicoModel);
        public MedicoGetDTO MedicoModel_para_GetDTO(MedicoModel medicoModel);
    }
}
