namespace SurveyBasket.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PollsController (IPollService pollService) : ControllerBase
{
    private readonly IPollService _pollService = pollService;



    [HttpGet]
    public IActionResult GetAll()
    {
        var polls = _pollService.GetAll();
        return Ok(polls.Adapt<List<PollResponse>>());
    }

    [HttpGet("{id}")]
    public IActionResult GetById( int id)
    {
        var poll =_pollService.GetBy(id);


        return poll is null ? NotFound() : Ok(poll.Adapt<PollResponse>());
    }

    [HttpPost("")]
    public IActionResult Add(PollRequest poll)
    {
         var NewPoll = _pollService.Add(poll.Adapt<Poll>());

        return CreatedAtAction(nameof(GetById) , new { id = NewPoll.Id } , NewPoll.Adapt<PollResponse>());
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, PollRequest poll)
    {
        var IsUpdated = _pollService.Update(id , poll.Adapt<Poll>());
        return IsUpdated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var IsDeleted = _pollService.Delete(id);
        return IsDeleted ? NoContent() : NotFound();
    }
    [HttpGet("Test")]
    public IActionResult Test()
    {
        var student = new Student
        {
            Id = 1,
            FirstName = "John",
            MiddleName = "M.",
            LastName = "Doe",
            BirthOfDay = new DateTime(1999, 1, 1),
            Department = null
            //Department = new Department { Id = 2  , Name  = "Test" }
        };

        return Ok(student.Adapt<StudentResponse>());

    }

}
