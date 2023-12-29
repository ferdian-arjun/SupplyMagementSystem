namespace API.Interface;

public interface ISoftDeletable
{
    DateTime? DeletedAt { get; set; }
}