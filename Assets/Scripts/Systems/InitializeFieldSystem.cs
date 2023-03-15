using System.Linq;
using Components;
using Leopotam.Ecs;

namespace ECS.system
{
    internal class InitializeFieldSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;

        private BusinessConfiguration _businessConfiguration;
        private NameConfiguration _nameConfiguration;

        public void Init()
        {
            for (int businessIndex = 0; businessIndex < _businessConfiguration._business.Count; businessIndex++)
            {
                var businessConfig = _businessConfiguration._business[businessIndex];
                EcsEntity businessEntity = _ecsWorld.NewEntity();
                ref BusinessComponent business = ref businessEntity.Get<BusinessComponent>();
                business.BusinessType = businessConfig.BusinessType;
                business.IncomeGenerationInterval = businessConfig.IncomeGenerationInterval;
                business.Level = businessConfig.Level;
                business.BaseIncome = businessConfig.BaseIncome;
                business.BaseCost = businessConfig.BaseCost;
                business.Upgrades = businessConfig.Upgrades;
                business.CurrentIncome = businessConfig.CurrentIncome;
                business.LevelCost = businessConfig.LevelCost;
                
                business.BusinessName = _nameConfiguration.BusinessNames
                    .FirstOrDefault(x => x.BusinessType == businessConfig.BusinessType).Name;
            
                for (int upgradeIndex = 0; upgradeIndex < businessConfig.Upgrades.Length; upgradeIndex++)
                {
                    business.Upgrades[upgradeIndex] = businessConfig.Upgrades[upgradeIndex];
                    business.Upgrades[upgradeIndex].UpgradeNames = _nameConfiguration.UpgradeNames
                        .FirstOrDefault(x => x.BusinessType == businessConfig.BusinessType).Names[upgradeIndex];
                }
            }

            EcsEntity playerEntity = _ecsWorld.NewEntity();
            ref PlayerComponent player = ref playerEntity.Get<PlayerComponent>();
        }
    }
}