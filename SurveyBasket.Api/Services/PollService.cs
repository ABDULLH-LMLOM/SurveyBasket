namespace SurveyBasket.Api.Services;

public class PollService : IPollService
{
    private static readonly List<Poll> _polls = new List<Poll>()
    {
        new Poll { Id = 1, Name = "Poll 1", description = "Description for Poll 1" },
        new Poll { Id = 2, Name = "Poll 2", description = "Description for Poll 2" },
        new Poll { Id = 3, Name = "Poll 3", description = "Description for Poll 3" },

    };

    public List<Poll> GetAll()
    {
        return _polls;
    }

    public Poll? GetBy(int id)
    {
        return _polls.FirstOrDefault(pool => pool.Id == id);
    }
    public Poll Add(Poll poll)
    {
        poll.Id = _polls.Count + 1;
        _polls.Add(poll);
        return poll;
    }

    public bool Update(Poll poll)
    {
        var updatePoll = GetBy(poll.Id);

        if (updatePoll is null)
            return false;

        updatePoll.Name = poll.Name;
        updatePoll.description = poll.description;
        return true;
    }

    public bool Delete(int id)
    {
        var poll = GetBy(id);

        if (poll is null)
            return false;

        _polls.Remove(poll);
        return true;

    }
}
