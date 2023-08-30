namespace Presentation.Models
{
    public class ResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
}
