namespace SurveyBasket.Api.Persistence.EntitisConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(100); 
        builder.Property(p => p.LastName).HasMaxLength(100); 
    }
}
