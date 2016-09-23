namespace View
{
    public interface IStudentView
    {
        bool WasStudentSaved { get; set; }
        bool ShouldSaveStudent { get; }
    }
}
