namespace API.Utilities.Handler;

public class ResponseServiceHandler<TAny>
{
    public int? Code { get; set; }
    public string? Message { get; set; }
    public TAny? Data { get; set; }
}