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
