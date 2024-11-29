namespace Quize2;

public class Result
{
    public string? Message { get; set; }
    public bool Status { get; set; }

    public Result(string message, bool status)
    {
        Message = message;
        Status = status;
    }
}
