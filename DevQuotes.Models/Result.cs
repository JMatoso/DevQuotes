using Microsoft.AspNetCore.Http;

namespace DevQuotes.Models;

public class Result
{
    public bool Successful { get; set; }
    public string Message { get; set; } = default!;
    public int Code { get; set; }

    public Result(bool successful = true, string message = "", int code = StatusCodes.Status200OK)
    {
        Successful = successful;
        Message = message;
        Code = code;
    }

    public Result(bool successful, string message)
    {
        Successful = successful;
        Message = message;
    }

    public Result(bool successful)
    {
        Successful = successful;
    }
}
