namespace LearningKurrent.Application.Models;

public record SearchPayload
{
  public List<string> IdIn { get; set; } = [];
  public TextSearch Search { get; set; } = new();

  public List<SortOption> Sort { get; set; } = [];

  public int Skip { get; set; }
  public int Limit { get; set; }
}
