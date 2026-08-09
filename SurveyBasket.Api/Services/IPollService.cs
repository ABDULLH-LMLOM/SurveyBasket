namespace SurveyBasket.Api.Services;

public interface IPollService
{
    List<Poll> GetAll();
    Poll? GetBy(int id);
    Poll Add(Poll poll);
    bool Update(int id , Poll poll);
    bool Delete(int id);
}
