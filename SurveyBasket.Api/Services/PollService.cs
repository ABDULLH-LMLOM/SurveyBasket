namespace SurveyBasket.Api.Services;

public class PollService(AppDbContext Context) : IPollService
{
    private readonly AppDbContext _Context = Context;
    public async Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _Context.Polls.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Poll?> GetByAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _Context.Polls.FirstOrDefaultAsync(pool => pool.Id == id, cancellationToken);
    }
    public async Task<Poll> AddAsync(Poll poll, CancellationToken cancellationToken = default)
    {
        await _Context.Polls.AddAsync(poll, cancellationToken);
        await _Context.SaveChangesAsync(cancellationToken);
        return poll;
    }

    public async Task<bool> UpdateAsync(int id, Poll poll, CancellationToken cancellationToken = default)
    {
        var updatePoll = await GetByAsync(id, cancellationToken);

        if (updatePoll is null)
            return false;

        updatePoll.Title = poll.Title;
        updatePoll.Summary = poll.Summary;
        updatePoll.StartsAt = poll.StartsAt;
        updatePoll.EndsAt = poll.EndsAt;
        await _Context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var poll = await GetByAsync(id, cancellationToken);

        if (poll is null)
            return false;

         _Context.Polls.Remove(poll);
        await _Context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
