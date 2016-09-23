using Domain;

namespace DataAccess
{
    public interface IStudentRepository
    {
        void Save(Student toSave);
    }
}
