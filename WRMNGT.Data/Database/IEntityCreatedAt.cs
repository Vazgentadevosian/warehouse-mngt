namespace WRMNGT.Data.Database;

public interface IEntityCreatedAt
{
    public DateTimeOffset CreatedAt { get; set; }
}