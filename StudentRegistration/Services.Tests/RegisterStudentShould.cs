using System;
using DataAccess;
using Domain;
using NUnit.Framework;
using Rhino.Mocks;
using StructureMap.AutoMocking;

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
        public void SaveTheCreatedStudentIfStudentValidUsingAutoMocker()
        {
            var autoMocker = new RhinoAutoMocker<StudentRegistrationService>();

            // **Caution**! One must **not** construct mock instances manually; instead, all references to mocked
            // instances **must** be retrieved from the `autoMocker` (container) instance.
            //
            // Additionally, one can ask `autoMocker` for a mock of **any** interface; however, the `autoMocker` 
            // instance ensures that any instance involved in constructing an object is the reference used to 
            // construct the object.
            //
            // Although AutoMocker provides some benefit in setting up tests, the most broad reaching benefit is that
            // one is now free to change the constructor of the class under test. AutoMocker will correctly construct
            // instances with the new argument(s).
            var mockStudentValidator = autoMocker.Get<IStudentValidator>();
            mockStudentValidator.Stub(sv => sv.ValidateStudent(Arg<Student>.Is.Anything)).Return(true);

            const int studentId = 314159;
            const string firstName = "First";
            const string lastName = "Last";
            autoMocker.ClassUnderTest.RegisterNewStudent(studentId, firstName, lastName);

            var expectedStudent = new Student
            {
                StudentId = studentId,
                FirstName = firstName,
                LastName = lastName
            };
            autoMocker.Get<IStudentRepository>().AssertWasCalled(
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
