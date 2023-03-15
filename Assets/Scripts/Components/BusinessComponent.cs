using System;
using StaticData;

namespace Components
{
    [Serializable]
    public struct BusinessComponent
    {
        public BusinessType BusinessType;
        public string BusinessName;
        public float IncomeGenerationTime;
        public float BaseCost;
        public float BaseIncome;
        public float CurrentIncome;
        public float IncomeGenerationInterval;
        public int Level;
        public float LevelCost;
        public BusinessUpgrade[] Upgrades;
    }
}