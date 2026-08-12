namespace SurveyBasket.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PollsController (IPollService pollService) : ControllerBase
{
    private readonly IPollService _pollService = pollService;



    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var polls =  await _pollService.GetAllAsync( cancellationToken);
        return Ok(polls.Adapt<IEnumerable<PollResponse>>());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById( int id , CancellationToken cancellationToken)
    {
        var poll = await _pollService.GetByAsync(id ,  cancellationToken);


        return poll is null ? NotFound() : Ok(poll.Adapt<PollResponse>());
    }

    [HttpPost("")]
    public async Task<IActionResult> Add(PollRequest poll , CancellationToken cancellationToken)
    {
         var NewPoll =  await _pollService.AddAsync(poll.Adapt<Poll>() ,  cancellationToken);

        return CreatedAtAction(nameof(GetById) , new { id = NewPoll.Id } , NewPoll.Adapt<PollResponse>());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PollRequest poll , CancellationToken cancellationToken)
    {
        var IsUpdated = await _pollService.UpdateAsync(id, poll.Adapt<Poll>() , cancellationToken);
        return IsUpdated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id , CancellationToken cancellationToken)
    {
        var IsDeleted = await _pollService.DeleteAsync(id , cancellationToken);
        return IsDeleted ? NoContent() : NotFound();
    }


}
