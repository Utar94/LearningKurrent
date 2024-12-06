namespace LearningKurrent.Application.Models;

public class ActorModel
{
  public static ActorModel System => new()
  {
    Id = "SYSTEM",
    DisplayName = ActorType.System.ToString()
  };

  public string Id { get; set; } = string.Empty;
  public ActorType Type { get; set; }
  public bool IsDeleted { get; set; }

  public string DisplayName { get; set; } = string.Empty;
  public string? EmailAddress { get; set; }
  public string? PictureUrl { get; set; }

  public override bool Equals(object? obj) => obj is ActorModel actor && actor.Id == Id;
  public override int GetHashCode() => Id.GetHashCode();
  public override string ToString()
  {
    StringBuilder s = new(DisplayName);
    if (EmailAddress != null)
    {
      s.Append(" <").Append(EmailAddress).Append('>');
    }
    s.Append(" (").Append(Type).Append(".Id=").Append(Id).Append(')');
    return s.ToString();
  }
}
