using Components;
using ECS.system;
using Leopotam.Ecs;
using UnityEngine;

#if UNITY_EDITOR
using Leopotam.Ecs.UnityIntegration;
#endif

public class Startup : MonoBehaviour
{
    [SerializeField] private BusinessConfiguration _businessConfiguration;
    [SerializeField] private NameConfiguration _nameConfiguration;
    [SerializeField] private UIContainer _uiContainer;
    private EcsWorld _ecsWorld;
    private EcsSystems _systems;

    private void Start()
    {
        _ecsWorld = new EcsWorld();
        
#if UNITY_EDITOR
        EcsWorldObserver.Create (_ecsWorld);
#endif

        _systems = new EcsSystems(_ecsWorld)
            .Add(new InitializeFieldSystem())
            .Add(new ProgressSystem())
            .OneFrame<MoneyRequest>()
            .Add(new UIInitSystem())
            .Add(new IncomeSystem())
            .Add(new BusinessLevelSystem())
            .Add(new BusinessUpgradeSystem())
            .Add(new UIRunSystem())
            .OneFrame<MoneyRequest>()
            .OneFrame<LevelUpRequest>()
            .Inject(_nameConfiguration)
            .Inject(_businessConfiguration)
            .Inject(_uiContainer);
        
        _systems?.Init();
        
#if UNITY_EDITOR
        EcsSystemsObserver.Create (_systems);
#endif
    }

    private void Update()
    {
        _systems?.Run();
    }

    private void OnDestroy()
    {
        _systems?.Destroy();
        _systems = null;
        _ecsWorld?.Destroy();
        _ecsWorld = null;
    }


    private void OnApplicationQuit()
    {
        var saveEntity = _ecsWorld.NewEntity();
        saveEntity.Get<SaveRequest>();
    }
    

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            return;
        }
        var saveEntity = _ecsWorld.NewEntity();
        saveEntity.Get<SaveRequest>();
    }
}