using Components;
using Leopotam.Ecs;

namespace ECS.system
{
    public class UIRunSystem : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private EcsFilter<BusinessComponent> _businessFilter;
        private EcsFilter<BusinessComponent, BusinessViewComponent> _businessViewFilter;
        private EcsFilter<PlayerComponent> _playerFilter;
        private EcsFilter<MoneyRequest> _moneyRequestFilter;
        private EcsFilter<LevelUpRequest> _levelUpRequestFilter;
        private EcsFilter<UpgradeRequest> _upgradeRequestFilter;

        private UIContainer _uiContainer;


        public void Run()
        {
            if (_moneyRequestFilter.GetEntitiesCount() > 0)
            {
                foreach (var playerIndex in _playerFilter)
                {
                    var player = _playerFilter.Get1(playerIndex);
                    _uiContainer.HUDView.MoneyText.text = $"Баланс: {player.CurrentMoney} $";
                }
            }

            foreach (var index in _levelUpRequestFilter)
            {
                var levelUpRequest = _levelUpRequestFilter.Get1(index);
                foreach (var businessIndex in _businessViewFilter)
                {

                    var business = _businessViewFilter.Get1(businessIndex);
                    var view = _businessViewFilter.Get2(businessIndex);
                    if (business.BusinessType == levelUpRequest.BusinessType)
                    {
                        view.view.IncomeText.text = business.CurrentIncome.ToString();
                        view.view.LevelText.text = business.Level.ToString();
                        view.view.LevelUpWidget.LevelPriceText.text = business.LevelCost.ToString();
                    }
                }
            }

            foreach (var index in _upgradeRequestFilter)
            {
                var levelUpRequest = _upgradeRequestFilter.Get1(index);
                foreach (var businessIndex in _businessViewFilter)
                {

                    var business = _businessViewFilter.Get1(businessIndex);
                    var view = _businessViewFilter.Get2(businessIndex);
                    if (business.BusinessType == levelUpRequest.BusinessType)
                    {
                        view.view.IncomeText.text = business.CurrentIncome.ToString();
                    }
                }
            }
        }
    }
}