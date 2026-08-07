
using SurveyBasket.Api.Services;

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
        return Ok(_pollService.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var poll =_pollService.GetBy(id);

        return poll is null ? NotFound() : Ok(poll);
    }

    [HttpPost("")]
    public IActionResult Add(Poll poll)
    {
         var NewPoll = _pollService.Add(poll);
        return CreatedAtAction(nameof(GetById) , new { id = NewPoll.Id } , NewPoll);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id , Poll poll)
    {
        var IsUpdated = _pollService.Update(poll);
        return IsUpdated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var IsDeleted = _pollService.Delete(id);
        return IsDeleted ? NoContent() : NotFound();
    }

}
