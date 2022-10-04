using UnityEngine;

namespace YellowJelloGames.YConditions
{
    [System.Serializable]
    public abstract class ConditionList<TCondition, TArgs> where TCondition: ICondition<TArgs>
    {
        [SerializeField]
        private ConditionOperators checkMode;

        [SerializeField]
        private TCondition[] conditions = System.Array.Empty<TCondition>();

        public bool AllTrueRequired => checkMode == ConditionOperators.All;

        public int Count => conditions.Length;

        public TCondition[] Conditions => conditions;

        public bool IsSatisfied(TArgs args = default)
        {
            int total = conditions.Length;

            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (total == 0) return true;
            if (total == 1) return OnCheckCondition(conditions[0], args);

            int succeedChecks = 0;
            for (int i = 0; i < total; ++i)
            {
                if (OnCheckCondition(conditions[i], args))
                {
                    if (checkMode == ConditionOperators.Any) return true;

                    ++succeedChecks;
                }
                else
                {
                    if (checkMode == ConditionOperators.All) return false;
                }
            }

            return succeedChecks == conditions.Length;
        }

        protected virtual bool OnCheckCondition(TCondition condition, TArgs args = default)
        {
            return condition.IsSatisfied(args);
        }
    }
}