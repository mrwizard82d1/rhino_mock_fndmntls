using DataAccess;
using Domain;
using NUnit.Framework;
using Rhino.Mocks;
using View;

namespace RegisterNewStudentShould
{
    [TestFixture]
    public class RegisterNewStudentShould
    {
        [TestCase]
        public void SaveTheNewStudentIfValidStudentAndShouldSaveTrueOnView()
        {
            var mockStudentView = MockRepository.GenerateMock<IStudentView>();
            // Remember that the property `ShouldSaveStudent` on the `mockStudentView` returns the default value for
            // the return type if I do not explicitly set the return value.
            mockStudentView.Stub(sv => sv.ShouldSaveStudent).Return(true);
            var mockStudentRepsitory = MockRepository.GenerateMock<IStudentRepository>();
            var sut = new StudentRegistrationPresenter(mockStudentView, mockStudentRepsitory);

            var newStudent = new Student();
            sut.RegisterNewStudent(newStudent);

            mockStudentRepsitory.AssertWasCalled(sr => sr.Save(newStudent));
        }

        [TestCase]
        public void SetWasStudentSavedToTrueIfValidStudentAndShouldSaveTrueOnView()
        {
            var mockStudentView = MockRepository.GenerateMock<IStudentView>();
            // Remember that the property `ShouldSaveStudent` on the `mockStudentView` returns the default value for
            // the return type if I do not explicitly set the return value.
            mockStudentView.Stub(sv => sv.ShouldSaveStudent).Return(true);
            var mockStudentRepsitory = MockRepository.GenerateMock<IStudentRepository>();
            var sut = new StudentRegistrationPresenter(mockStudentView, mockStudentRepsitory);

            var newStudent = new Student();
            sut.RegisterNewStudent(newStudent);

            mockStudentView.AssertWasCalled(sv => sv.WasStudentSaved = true);
        }

        [TestCase]
        public void NotSaveTheNewStudentIfInvalidStudent()
        {
            var mockStudentView = MockRepository.GenerateMock<IStudentView>();
            var mockStudentRepsitory = MockRepository.GenerateMock<IStudentRepository>();
            var sut = new StudentRegistrationPresenter(mockStudentView, mockStudentRepsitory);

            sut.RegisterNewStudent(null);

            mockStudentRepsitory.AssertWasNotCalled(sr => sr.Save(null));
        }

        [TestCase]
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
