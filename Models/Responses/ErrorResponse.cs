namespace Backend_UserManagementApi.Models.Responses
{
  public class ErrorResponse
  {
    public int StatusCode { get; set; }
    public required string Message { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
    public string? Detail { get; set; }
  }
}