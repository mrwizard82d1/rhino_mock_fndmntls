using System;
using DataAccess;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Services.Tests
{
    [TestClass]
    public class RegisterStudentShould
    {
        [TestMethod]
        public void SaveTheStudent()
        {
            var mockStudentRepository = MockRepository.GenerateMock<IStudentRepository>();
            var sut = new StudentRegistrationService(mockStudentRepository);
            var toRegister = new Student();

            sut.RegisterNewStudent(toRegister);

            mockStudentRepository.AssertWasCalled(msr => msr.Save(toRegister));
        }
    }
}
