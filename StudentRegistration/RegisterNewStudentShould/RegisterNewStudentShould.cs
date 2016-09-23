using DataAccess;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using View;

namespace RegisterNewStudentShould
{
    [TestClass]
    public class RegisterNewStudentShould
    {
        [TestMethod]
        public void SaveTheNewStudentIfValidStudent()
        {
            var mockStudentView = MockRepository.GenerateMock<IStudentView>();
            var mockStudentRepsitory = MockRepository.GenerateMock<IStudentRepository>();
            var sut = new StudentRegistrationPresenter(mockStudentView, mockStudentRepsitory);

            var newStudent = new Student();
            sut.RegisterNewStudent(newStudent);

            mockStudentRepsitory.AssertWasCalled(sr => sr.Save(newStudent));
        }

        [TestMethod]
        public void SetWasStudentSavedToTrueIfValidStudent()
        {
            var mockStudentView = MockRepository.GenerateMock<IStudentView>();
            var mockStudentRepsitory = MockRepository.GenerateMock<IStudentRepository>();
            var sut = new StudentRegistrationPresenter(mockStudentView, mockStudentRepsitory);

            var newStudent = new Student();
            sut.RegisterNewStudent(newStudent);

            mockStudentView.AssertWasCalled(sv => sv.WasStudentSaved = true);
        }

        [TestMethod]
        public void NotSaveTheNewStudentIfInvalidStudent()
        {
            var mockStudentView = MockRepository.GenerateMock<IStudentView>();
            var mockStudentRepsitory = MockRepository.GenerateMock<IStudentRepository>();
            var sut = new StudentRegistrationPresenter(mockStudentView, mockStudentRepsitory);

            sut.RegisterNewStudent(null);

            mockStudentRepsitory.AssertWasNotCalled(sr => sr.Save(null));
        }

        [TestMethod]
        public void SetWasStudentSavedToFalseIfInvalidStudent()
        {
            var mockStudentView = MockRepository.GenerateMock<IStudentView>();
            var mockStudentRepsitory = MockRepository.GenerateMock<IStudentRepository>();
            var sut = new StudentRegistrationPresenter(mockStudentView, mockStudentRepsitory);

            sut.RegisterNewStudent(null);

            mockStudentView.AssertWasCalled(sv => sv.WasStudentSaved = false);
        }
    }
}
