namespace SurveyBasket.Api.Contracts.Responses;

public class PollResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
}
