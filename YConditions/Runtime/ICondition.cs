namespace YellowJelloGames.YConditions
{
    public interface ICondition<in TArgs>
    {
        bool IsSatisfied(TArgs args);
    }
}