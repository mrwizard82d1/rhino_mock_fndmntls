using System.Text;
using Domain;
using Rhino.Mocks.Constraints;

namespace Services.Tests
{
    public class StudentConstraint : AbstractConstraint
    {
        private readonly Student expectedStudent;
        private Student actualStudent;

        public StudentConstraint(Student expectedStudent)
        {
            this.expectedStudent = expectedStudent;
        }

        public override bool Eval(object obj)
        {
            actualStudent = obj as Student;
            if (actualStudent == null)
            {
                return expectedStudent == null;
            }

            return (expectedStudent.StudentId == actualStudent.StudentId) &&
                   (expectedStudent.FirstName == actualStudent.FirstName) &&
                   (expectedStudent.LastName == actualStudent.LastName);
        }

        public override string Message
        {
            get
            {
                var builder = new StringBuilder();
                if (expectedStudent.StudentId != actualStudent.StudentId)
                {
                    builder.Append(
                        $"Expected student id {expectedStudent.StudentId} but was actual student id {actualStudent.StudentId}");
                }

                if (expectedStudent.FirstName != actualStudent.FirstName)
                {
                    builder.Append(
                        $"Expected first name {expectedStudent.FirstName} but was actual first name {actualStudent.FirstName}");
                }

                if (expectedStudent.LastName != actualStudent.LastName)
                {
                    builder.Append(
                        $"Expected last name {expectedStudent.LastName} but was actual last name {actualStudent.LastName}");
                }

                return builder.ToString();
            }
        }
    }
}