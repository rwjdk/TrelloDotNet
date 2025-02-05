namespace TrelloDotNet.Model
{
    public enum CardsCondition
    {
        Equal = 1,
        NotEqual = 2,
        GreaterThan = 3,
        LessThan = 4,
        GreaterThanOrEqual = 5,
        LessThanOrEqual = 6,
        HasAnyValue = 7,
        DoNotHaveAnyValue = 8,
        Contains = 9,
        DoNotContains = 10,
        AnyOfThese = 11,
        AllOfThese = 12,
        NoneOfThese = 13,
        RegEx = 14,
        StartsWith = 15,
        EndsWith = 16,
        DoNotStartWith = 17,
        DoNotEndWith = 18,
        Between = 19,
        NotBetween = 20
    }

    public enum CardsConditionNumber
    {
        Equal = 1,
        NotEqual = 2,
        GreaterThan = 3,
        LessThan = 4,
        GreaterThanOrEqual = 5,
        LessThanOrEqual = 6,
        HasAnyValue = 7,
        DoNotHaveAnyValue = 8,
        AnyOfThese = 11,
        NoneOfThese = 13,
        Between = 19,
        NotBetween = 20
    }

    public enum CardsConditionCount
    {
        Equal = 1,
        NotEqual = 2,
        GreaterThan = 3,
        LessThan = 4,
        GreaterThanOrEqual = 5,
        LessThanOrEqual = 6,
    }

    public enum CardsConditionDate
    {
        Equal = 1,
        NotEqual = 2,
        GreaterThan = 3,
        LessThan = 4,
        GreaterThanOrEqual = 5,
        LessThanOrEqual = 6,
        HasAnyValue = 7,
        DoNotHaveAnyValue = 8,
        AnyOfThese = 11,
        NoneOfThese = 13,
        Between = 19,
        NotBetween = 20
    }

    public enum CardsConditionString
    {
        Equal = 1,
        NotEqual = 2,
        Contains = 9,
        DoNotContains = 10,
        AnyOfThese = 11,
        NoneOfThese = 13,
        RegEx = 14,
        StartsWith = 15,
        EndsWith = 16,
        DoNotStartWith = 17,
        DoNotEndWith = 18,
    }

    public enum CardsConditionId
    {
        Equal = 1,
        NotEqual = 2,
        AnyOfThese = 11,
        AllOfThese = 12,
        NoneOfThese = 13,
    }

    public enum CardsConditionBoolean
    {
        Equal = 1,
        NotEqual = 2,
    }
}