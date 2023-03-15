using Components;
using Leopotam.Ecs;
using StaticData;

namespace ECS.system
{
    public class BusinessLevelSystem : IEcsInitSystem,IEcsDestroySystem
    {
        private EcsWorld _ecsWorld;
        private EcsFilter<BusinessComponent,BusinessViewComponent> _businessFilter;
        private EcsFilter<PlayerComponent> _playerFilter;
        private UIContainer _uiContainer;
        
        public void Init()
        {
            foreach (var i in _businessFilter)
            {
                ref var business = ref _businessFilter.Get1(i);
                ref var view = ref _businessFilter.Get2(i);
                view.view.LevelText.text = $"{business.Level}";
                var levelCost = business.BaseCost * (business.Level + 1);
                view.view.LevelUpWidget.LevelPriceText.text = $"{levelCost}";
                view.view.LevelUpButtonClickedEvent += FindBusiness;
            }
        }

        public void Destroy()
        {
            foreach (var businessIndex in _businessFilter)
            {
                ref var view = ref _businessFilter.Get2(businessIndex);
                view.view.LevelUpButtonClickedEvent -= FindBusiness;
            }
        }

        private void FindBusiness(BusinessType businessType)
        {
            foreach (var businessIndex in _businessFilter)
            {
                ref var component = ref _businessFilter.Get1(businessIndex);
                if (component.BusinessType == businessType)
                {
                    LevelUpBusiness(ref component);
                }
            }
        }

        private void LevelUpBusiness(ref BusinessComponent component)
        {
            foreach (var playerIndex in _playerFilter)
            {
                ref var player = ref _playerFilter.Get1(playerIndex);
                var levelCost = component.BaseCost * (component.Level + 1);
                
                if (player.CurrentMoney >= levelCost)
                {
                    component.Level++;
                    component.LevelCost = component.BaseCost * (component.Level + 1);
                    player.CurrentMoney -= levelCost;

                    var request = _ecsWorld.NewEntity();
                    ref var leveUpComponent = ref request.Get<LevelUpRequest>();
                    leveUpComponent.BusinessType = component.BusinessType;
                    
                    var moneyRequest = _ecsWorld.NewEntity();
                    moneyRequest.Get<MoneyRequest>();
                }
            }
        }
    }
}

