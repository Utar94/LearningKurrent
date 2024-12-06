namespace LearningKurrent.Application.Models;

public record TextSearch
{
  public List<SearchTerm> Terms { get; set; } = [];
  public SearchOperator Operator { get; set; }

  public TextSearch()
  {
  }

  public TextSearch(IEnumerable<SearchTerm> terms, SearchOperator @operator = SearchOperator.And)
  {
    Terms.AddRange(terms);
    Operator = @operator;
  }
}
