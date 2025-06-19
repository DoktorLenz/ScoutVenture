namespace ScoutVenture
{
    public abstract class ProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        private ProblemDetails()
        {
        }

        public static Microsoft.AspNetCore.Mvc.ProblemDetails New(string title, int status)
        {
            return new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Title = title,
                Status = status
            };
        }

        public static Microsoft.AspNetCore.Mvc.ProblemDetails New(string title, int status, string detail)
        {
            return new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Title = title,
                Status = status,
                Detail = detail
            };
        }
    }
}