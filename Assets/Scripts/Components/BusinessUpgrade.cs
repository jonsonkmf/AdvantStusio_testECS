using System;

namespace Components
{
    [Serializable]
    public struct BusinessUpgrade
    {
        public string UpgradeNames;
        public bool UpgradeIsBuy;
        public float UpgradesCost;
        public float UpgradesIncomeBonus;
    }
}