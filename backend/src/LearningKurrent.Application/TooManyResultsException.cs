﻿using Logitar;

namespace LearningKurrent.Application;

public class TooManyResultsException : Exception
{
  private const string ErrorMessage = "The query did find too many results.";

  public string Type
  {
    get => (string)Data[nameof(Type)]!;
    private set => Data[nameof(Type)] = value;
  }
  public int ExpectedCount
  {
    get => (int)Data[nameof(ExpectedCount)]!;
    private set => Data[nameof(ExpectedCount)] = value;
  }
  public int ActualCount
  {
    get => (int)Data[nameof(ActualCount)]!;
    private set => Data[nameof(ActualCount)] = value;
  }

  public TooManyResultsException(Type type, int expectedCount, int actualCount)
    : base(BuildMessage(type, expectedCount, actualCount))
  {
    Type = type.GetNamespaceQualifiedName();
    ExpectedCount = expectedCount;
    ActualCount = actualCount;
  }

  private static string BuildMessage(Type type, int expectedCount, int actualCount) => new ErrorMessageBuilder(ErrorMessage)
    .AddData(nameof(Type), type.GetNamespaceQualifiedName())
    .AddData(nameof(ExpectedCount), expectedCount)
    .AddData(nameof(ActualCount), actualCount)
    .Build();
}

public class TooManyResultsException<T> : TooManyResultsException
{
  public TooManyResultsException(int expectedCount, int actualCount) : base(typeof(T), expectedCount, actualCount)
  {
  }

  public static TooManyResultsException<T> ExpectedSingle(int actualCount) => new(expectedCount: 1, actualCount);
}
