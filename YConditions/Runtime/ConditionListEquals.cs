using UnityEngine;

namespace YellowJelloGames.YConditions
{
    [System.Serializable]
    public class ConditionListEquals<T>
    {
        [SerializeField]
        private ConditionOperators checkMode;

        [SerializeField]
        private T[] conditions = System.Array.Empty<T>();

        public bool AllTrueRequired => checkMode == ConditionOperators.All;

        public int Count => conditions.Length;

        public T[] Conditions => conditions;

        public bool IsSatisfied(T target)
        {
            int total = conditions.Length;

            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (total == 0) return true;
            if (total == 1) return OnCheckCondition(conditions[0], target);

            int succeedChecks = 0;
            for (int i = 0; i < total; ++i)
            {
                if (OnCheckCondition(conditions[i], target))
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

        protected virtual bool OnCheckCondition(T condition, T target)
        {
            return condition.Equals(target);
        }
    }
}