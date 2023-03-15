using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.system
{
    public class UIInitSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private EcsFilter<BusinessComponent> _businessFilter;
        private EcsFilter<BusinessComponent, BusinessViewComponent> _businessViewFilter;
        private EcsFilter<PlayerComponent> _playerFilter;

        private UIContainer _uiContainer;

        public void Init()
        {
            foreach (var playerIndex in _playerFilter)
            {
                var player = _playerFilter.Get1(playerIndex);
                _uiContainer.HUDView.MoneyText.text = $"Баланс: {player.CurrentMoney} $";
            }
            
            foreach (var businessIndex in _businessFilter)
            {
                var view = Object.Instantiate(_uiContainer.MainView.ViewPrefab, _uiContainer.MainView.Container);

                //инициализация UI
                var component = _businessFilter.Get1(businessIndex);

                view.BusinessNameText.text = component.BusinessName;
                //init upgrades view
                for (int index = 0; index < component.Upgrades.Length; index++)
                {
                    var upgrade = component.Upgrades[index];
                    view.UpgradeWidget.Create(upgrade.UpgradeNames, upgrade.UpgradesIncomeBonus.ToString(),
                        upgrade.UpgradesCost.ToString(), component.BusinessType, index, upgrade.UpgradeIsBuy);
                }

                ref var businessViewComponent =
                    ref _businessFilter.GetEntity(businessIndex).Get<BusinessViewComponent>();
                businessViewComponent.view = view;
                businessViewComponent.view.BusinessType = component.BusinessType;
            }
        }

        public void Run()
        {
            foreach (var playerIndex in _playerFilter)
            {
                var player = _playerFilter.Get1(playerIndex);
                _uiContainer.HUDView.MoneyText.text = $"Баланс: {player.CurrentMoney} $";
            }

            foreach (var businessIndex in _businessViewFilter)
            {
                var business = _businessViewFilter.Get1(businessIndex);
                var view = _businessViewFilter.Get2(businessIndex);

                view.view.IncomeText.text = business.CurrentIncome.ToString();
                view.view.LevelText.text = business.Level.ToString();
                view.view.LevelUpWidget.LevelPriceText.text = business.LevelCost.ToString();
            }
        }
    }
}