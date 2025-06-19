using System.Net;

namespace ScoutVenture
{
    public class ErrorResponse
    {
        public required string ErrorMessage { get; init; }
        public required string Path { get; init; }
        public required HttpStatusCode StatusCode { get; init; }
        public DateTimeOffset Timestamp { get; } = DateTimeOffset.Now;
        public required List<string> Details { get; init; }
    }
}