namespace SurveyBasket.Api.Persistence.EntitisConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany(u => u.RefreshTokens).ToTable("RefreshTokens").WithOwner().HasForeignKey("UserId");

        builder.Property(u => u.FirstName).HasMaxLength(100); 
        builder.Property(p => p.LastName).HasMaxLength(100); 
    }
}
