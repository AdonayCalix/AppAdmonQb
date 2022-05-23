namespace AppAdmonQb.Models
{
    public class Response
    {
        public int MovementId { get; set; }
        public int MovementDetailId { get; set; }
        public int ProjectId { get; set; }
        public int Code { get; set; }
        public string? Kind { get; set; }
        public string? Amount { get; set; }
        public string? NumberRef { get; set; }
        public string? Date { get; set; }
        public string? ListId { get; set; }
    }
}
