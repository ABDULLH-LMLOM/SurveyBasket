using SurveyBasket.Api.Contracts.Requests;
using SurveyBasket.Api.Contracts.Responses;

namespace SurveyBasket.Api.Mapping;

public static class PollMapping
{
    public static Poll ToModel(this PollRequest pollRequest)
    {
        return new Poll
        {
            Name = pollRequest.Name,
            description = pollRequest.description
        };
    }

    public static PollResponse ToResponse(this Poll poll)
    {
        return new PollResponse
        {
            Id = poll.Id,
            Name = poll.Name,
            description = poll.description
        };
    }

    public static List<PollResponse> ToResponse(this IEnumerable<Poll> polls)
    {
        return polls.Select(ToResponse).ToList();
    }
}
