using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.system
{
    internal class IncomeSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _ecsWorld;

        private EcsFilter<PlayerComponent> _playerFilter;

        private EcsFilter<BusinessComponent, BusinessViewComponent> _businessFilter;

        private UIContainer _uiContainer;

        public void Init()
        {
            foreach (var businessIndex in _businessFilter)
            {
                ref var businessComponent = ref _businessFilter.Get1(businessIndex);
                ref var viewComponent = ref _businessFilter.Get2(businessIndex);
                viewComponent.view.IncomeText.text = CalculateIncome(ref businessComponent).ToString();
            }
        }

        public void Run()
        {
            foreach (var businessIndex in _businessFilter)
            {
                ref var businessComponent = ref _businessFilter.Get1(businessIndex);
                ref var viewComponent = ref _businessFilter.Get2(businessIndex);

                if (businessComponent.Level <= 0)
                {
                    return;
                }

                var currentIncome = CalculateIncome(ref businessComponent);
                
                businessComponent.IncomeGenerationTime += Time.deltaTime;
                if (businessComponent.IncomeGenerationTime >= businessComponent.IncomeGenerationInterval)
                {
                    foreach (var playerIndex in _playerFilter)
                    {
                        ref var player = ref _playerFilter.Get1(playerIndex);
                        player.CurrentMoney += currentIncome;
                        var request = _ecsWorld.NewEntity();
                        request.Get<MoneyRequest>();
                    }

                    businessComponent.IncomeGenerationTime = 0;
                }

                viewComponent.view.IncomeProgressFill.fillAmount = businessComponent.IncomeGenerationTime /
                                                                   businessComponent.IncomeGenerationInterval;
            }
        }

        private float CalculateIncome(ref BusinessComponent businessComponent)
        {
            var income = businessComponent.Level * businessComponent.BaseIncome;
            var boost = 1f;

            for (int index = 0; index < businessComponent.Upgrades.Length; index++)
            {
                var upgrade = businessComponent.Upgrades[index].UpgradeIsBuy;
                if (upgrade)
                {
                    boost += businessComponent.Upgrades[index].UpgradesIncomeBonus * 0.01f;
                }
            }

            businessComponent.CurrentIncome = income * boost;
            return businessComponent.CurrentIncome;
        }
    }
}