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

            // The Rhino Mocks `Is` constraint provides some level of safety; however, note that `Anything` will
            // succceed if one saves a `null` value. One could also use the constraint `Is.NotNull`. This constraint
            // would fail if one saves a `null` value. 
            mockStudentRepository.AssertWasCalled(
                msr =>
                    msr.Save(
                        Arg<Student>.Matches(
                            s => (s.StudentId == studentId) && (s.FirstName == firstName) && (s.LastName == lastName))));
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
