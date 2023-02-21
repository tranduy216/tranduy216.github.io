using Microsoft.AspNetCore.Components;

namespace demo_blazor_app.Business
{
    public static class StringExtension
    {
        public static string ZeroPrefix(this string input)
        {
            return input.PadLeft(3, '0');
        }
    }
}
