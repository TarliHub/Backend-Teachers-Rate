namespace TeacherRateProject.Helpers;

public static class StringHelper
{
    public static bool IsEmptyString(this string value)
    {
        return string.IsNullOrEmpty(value.Trim());
    }
}
