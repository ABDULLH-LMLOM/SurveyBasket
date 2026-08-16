using Microsoft.AspNetCore.Identity;

namespace SurveyBasket.Api.Entities;

public class User :IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
