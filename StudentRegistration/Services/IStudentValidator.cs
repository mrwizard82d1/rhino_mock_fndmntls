using Domain;

namespace Services
{
    public interface IStudentValidator
    {
        bool ValidateStudent(Student newStudent);
    }
}