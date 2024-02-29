namespace ReadyTechTeaPotRESTAPI.Models
{
    public class SuccessResponse : ICoffeeBrewerResponse
    {
        public string Message { get; set; } = "Your piping hot coffee is ready";
        public string Prepared { get; set; } = "";
        public SuccessResponse() { }
        public SuccessResponse(string message, string timestamp)
        {   
            Message = message;
            Prepared = timestamp;
        }
    }
}
