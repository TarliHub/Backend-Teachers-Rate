using Microsoft.IdentityModel.Tokens;

namespace TeacherRateProject.Helpers;

public static class StringHelper
{
    public static bool IsEmptyString(this string value)
    {
        return value.Trim().IsNullOrEmpty();
    }
}
