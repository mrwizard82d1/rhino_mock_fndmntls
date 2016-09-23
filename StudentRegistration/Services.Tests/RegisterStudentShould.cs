using System;
using DataAccess;
using Domain;
using NUnit.Framework;
using Rhino.Mocks;

namespace Services.Tests
{
    [TestFixture]
    public class RegisterStudentShould
    {
        [TestCase]
        public void SaveTheStudentIfStudentValid()
        {
            var mockStudentRepository = MockRepository.GenerateMock<IStudentRepository>();
            var mockStudentValidator = MockRepository.GenerateMock<IStudentValidator>();
            var toRegister = new Student();
            mockStudentValidator.Stub(sv => sv.ValidateStudent(toRegister)).Return(true);
            var sut = new StudentRegistrationService(mockStudentRepository, mockStudentValidator);

            sut.RegisterNewStudent(toRegister);

            mockStudentRepository.AssertWasCalled(msr => msr.Save(toRegister));
        }

        [TestCase]
        public void SaveTheCreatedStudentIfStudentValid()
        {
            var mockStudentRepository = MockRepository.GenerateMock<IStudentRepository>();
            var mockStudentValidator = MockRepository.GenerateMock<IStudentValidator>();
            mockStudentValidator.Stub(sv => sv.ValidateStudent(Arg<Student>.Is.Anything)).Return(true);
            var sut = new StudentRegistrationService(mockStudentRepository, mockStudentValidator);

            const int studentId = 314159;
            const string firstName = "First";
            const string lastName = "Last";
            sut.RegisterNewStudent(studentId, firstName, lastName);

            mockStudentRepository.AssertWasCalled(
                msr => msr.Save(Arg<Student>.Matches(s => HasCorrectDetails(s, studentId, firstName, lastName))));
        }

        private static bool HasCorrectDetails(Student s, int studentId, string firstName, string lastName)
        {
            return (s.StudentId == studentId) && (s.FirstName == firstName) && (s.LastName == lastName);
        }

        [TestCase]
        public void SaveTheCreatedStudentIfStudentValidUsingStudentConstraint()
        {
            var mockStudentRepository = MockRepository.GenerateMock<IStudentRepository>();
            var mockStudentValidator = MockRepository.GenerateMock<IStudentValidator>();
            mockStudentValidator.Stub(sv => sv.ValidateStudent(Arg<Student>.Is.Anything)).Return(true);
            var sut = new StudentRegistrationService(mockStudentRepository, mockStudentValidator);

            const int studentId = 314159;
            const string firstName = "First";
            const string lastName = "Last";
            sut.RegisterNewStudent(studentId, firstName, lastName);

            var expectedStudent = new Student
            {
                StudentId = studentId,
                FirstName = firstName,
                LastName = lastName
            };
            mockStudentRepository.AssertWasCalled(
                msr => msr.Save(Arg<Student>.Matches(new StudentConstraint(expectedStudent))));
        }

        [TestCase]
        public void ThrowExceptionIfStudentInvalid()
        {
            var mockStudentRepository = MockRepository.GenerateMock<IStudentRepository>();
            var mockStudentValidator = MockRepository.GenerateMock<IStudentValidator>();
            var toRegister = new Student();
            mockStudentValidator.Stub(sv => sv.ValidateStudent(toRegister)).Return(false);
            var sut = new StudentRegistrationService(mockStudentRepository, mockStudentValidator);

            Assert.Throws<Exception>(() => sut.RegisterNewStudent(toRegister));
        }
    }
}
