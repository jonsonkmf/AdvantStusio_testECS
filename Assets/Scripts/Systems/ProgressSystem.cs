using System;
using System.IO;
using Components;
using Leopotam.Ecs;
using Tools;
using UnityEngine;

namespace ECS.system
{
    public class ProgressSystem : IEcsInitSystem, IEcsDestroySystem, IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private EcsFilter<BusinessComponent> _businessFilter;
        private EcsFilter<PlayerComponent> _playerFilter;
        private EcsFilter<SaveRequest> _saveFilter;

        private BusinessConfiguration _businessConfiguration;

        private string _savepath;
        private string _savepathPlayer;
        private string _saveFileName = "data.json";
        private string _saveFileNamePlayer = "Playerdata.json";


        public void Init()
        {
            CheckPlatform();
            LoadProgress();
        }

        public void Run()
        {
            if (_saveFilter.GetEntitiesCount()>0)
            {
                SaveProgress();
            }
        }

        public void Destroy()
        {
            //SaveProgress();
        }

        private void CheckPlatform()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            _savepath = Path.Combine(Application.persistentDataPath, _saveFileName);
            _savepathPlayer = Path.Combine(Application.persistentDataPath, _saveFileNamePlayer);
#else
            _savepath = Path.Combine(Application.dataPath, _saveFileName);
            _savepathPlayer = Path.Combine(Application.dataPath, _saveFileNamePlayer);
#endif
        }

        private void LoadProgress()
        {
            if (LoadBusinessProgress()) return;

            LoadPlayerMoney();
        }

        private void LoadPlayerMoney()
        {
            if (!File.Exists(_savepathPlayer))
            {
                return;
            }

            var jsonPlayer = File.ReadAllText(_savepathPlayer);
            var player = JsonUtility.FromJson<PlayerComponent>(jsonPlayer);

            foreach (var index in _playerFilter)
            {
                ref var component = ref _playerFilter.Get1(index);
                component = player;
            }
        }

        private bool LoadBusinessProgress()
        {
            if (!File.Exists(_savepath))
            {
                return true;
            }

            var json = File.ReadAllText(_savepath);
            BusinessComponent[] savedata = JsonHelper.FromJson<BusinessComponent>(json);
            foreach (var index in _businessFilter)
            {
                ref var component = ref _businessFilter.Get1(index);
                component = savedata[index];
            }

            return false;
        }

        private void SaveProgress()
        {
            SaveBusinessProgress();

            SavePlayerMoney();
        }

        private void SavePlayerMoney()
        {
            foreach (var index in _playerFilter)
            {
                var player = _playerFilter.Get1(index);
                var jsonPlayer = JsonUtility.ToJson(player, true);
                File.WriteAllText(_savepathPlayer, jsonPlayer);
            }
        }

        private void SaveBusinessProgress()
        {
            BusinessComponent[] savedata = new BusinessComponent[_businessConfiguration._business.Count];

            foreach (var index in _businessFilter)
            {
                var component = _businessFilter.Get1(index);
                savedata[index] = component;
            }

            var json = JsonHelper.ToJson(savedata, true);
            File.WriteAllText(_savepath, json);
        }
    }
}