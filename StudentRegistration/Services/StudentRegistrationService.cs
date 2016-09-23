using DataAccess;
using Domain;

namespace Services
{
    public class StudentRegistrationService
    {
        private IStudentRepository studentRepository;

        public StudentRegistrationService(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public void RegisterNewStudent(Student toRegister)
        {
            studentRepository.Save(toRegister);
        }
    }
}
