using SurveyBasket.Api.Mapping;
using SurveyBasket.Api.Contracts.Requests;
namespace SurveyBasket.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PollsController : ControllerBase
{
    private readonly IPollService _pollService;

    public PollsController(IPollService pollService)
    {
        _pollService = pollService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var polls = _pollService.GetAll();
        return Ok(polls.ToResponse());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var poll =_pollService.GetBy(id);

        return poll is null ? NotFound() : Ok(poll.ToResponse());
    }

    [HttpPost("")]
    public IActionResult Add(PollRequest poll)
    {
         var NewPoll = _pollService.Add(poll.ToModel());
        return CreatedAtAction(nameof(GetById) , new { id = NewPoll.Id } , NewPoll.ToResponse());
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id , PollRequest poll)
    {
        var IsUpdated = _pollService.Update(poll.ToModel());
        return IsUpdated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var IsDeleted = _pollService.Delete(id);
        return IsDeleted ? NoContent() : NotFound();
    }

}
