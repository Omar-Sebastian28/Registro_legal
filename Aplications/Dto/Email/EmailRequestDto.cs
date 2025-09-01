namespace RegistroLegal.Core.Aplications.Dto.Email
{
    public class EmailRequestDto
    {
        public string? To { get; set; }

        public required string Subject { get; set; }

        public required string HtmlBdy { get; set; }

        public List<string> ToRange { get; set; } = new List<string>();
    }
}
