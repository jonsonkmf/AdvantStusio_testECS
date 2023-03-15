using Components;
using Leopotam.Ecs;
using StaticData;

namespace ECS.system
{
    public class BusinessUpgradeSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private EcsWorld _ecsWorld;
        private EcsFilter<BusinessComponent, BusinessViewComponent> _businessFilter;
        private EcsFilter<PlayerComponent> _playerFilter;

        public void Init()
        {
            foreach (var businessIndex in _businessFilter)
            {
                ref var view = ref _businessFilter.Get2(businessIndex);
                foreach (var widgetElement in view.view.UpgradeWidget.Elements)
                {
                    widgetElement.BuyUpgradeButtonClick += BuyUpgrade;
                }
            }
        }

        public void Destroy()
        {
            foreach (var businessIndex in _businessFilter)
            {
                ref var view = ref _businessFilter.Get2(businessIndex);
                foreach (var widgetElement in view.view.UpgradeWidget.Elements)
                {
                    widgetElement.BuyUpgradeButtonClick -= BuyUpgrade;
                }
            }
        }

        private void BuyUpgrade(UpgradeWidgetElement widgetElement, BusinessType businessType, int upgradeNumber)
        {
            foreach (var businessIndex in _businessFilter)
            {
                ref var component = ref _businessFilter.Get1(businessIndex);
                if (CheckBusinessType(widgetElement, businessType, upgradeNumber, ref component)) return;
            }
        }

        private bool CheckBusinessType(UpgradeWidgetElement widgetElement, BusinessType businessType,
            int upgradeNumber, ref BusinessComponent component)
        {
            if (component.BusinessType == businessType)
            {
                if (CheckBuyUpgrade(component, upgradeNumber))
                {
                    return true;
                }

                LevelUpBusiness(widgetElement, upgradeNumber, ref component);
            }

            return false;
        }

        private void LevelUpBusiness(UpgradeWidgetElement widgetElement, int upgradeNumber,
            ref BusinessComponent component)
        {
            foreach (var playerIndex in _playerFilter)
            {
                ref var player = ref _playerFilter.Get1(playerIndex);
                if (player.CurrentMoney >= component.Upgrades[upgradeNumber].UpgradesCost && component.Level > 0)
                {
                    component.Upgrades[upgradeNumber].UpgradeIsBuy = true;
                    player.CurrentMoney -= component.Upgrades[upgradeNumber].UpgradesCost;
                    widgetElement.PriceText.text = "Куплено";
                    
                    var request = _ecsWorld.NewEntity();
                    ref var leveUpComponent = ref request.Get<UpgradeRequest>();
                    leveUpComponent.BusinessType = component.BusinessType;
                    
                    var moneyRequest = _ecsWorld.NewEntity();
                    moneyRequest.Get<MoneyRequest>();
                }
            }
        }

        private bool CheckBuyUpgrade(BusinessComponent component, int upgradeNumber)
        {
            return component.Upgrades[upgradeNumber].UpgradeIsBuy;
        }
    }
}