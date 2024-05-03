namespace TeacherRate.Domain.Exceptions;

public class DetailedException : Exception
{
    public DetailedException(string message, string name)
        : base($"'{name}': '{message}'")
    { 
    }
}
