using System;
using System.Collections.Generic;
using System.Threading;
using Code.Cell;
using Code.Grid;
using Code.Mole;
using Code.Pools;
using Code.Spawner;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Main
{
    [DisallowMultipleComponent]
    public class LevelEntryPoint : MonoBehaviour
    {
        //[SerializeField] private ScreenService _screenService;
        private readonly Dictionary<SystemType, EcsSystems> _systems = new();
        private readonly CancellationTokenSource _tokenSources = new();
        
        private EcsWorld _world;
        private EcsSystems _updateSystem;
        private Camera _camera;

        private void Awake()
        {
            //ScreenSwitcher.Initialize(_screenService.screens);
        }

        private void Start()
        {
            InitECS();
        }

        private void DistributeDataBetweenGameModes()
        {
            AddGameSystems();
            InjectGameObjects();
        }

        private void InitECS()
        {
            _world = new EcsWorld();
            var systemTypes = Enum.GetValues(typeof(SystemType));
            foreach (var item in systemTypes)
            {
                _systems.Add((SystemType)item, new EcsSystems(_world));
            }

#if UNITY_EDITOR
            AddDebugSystems();
#endif

            DistributeDataBetweenGameModes();

            foreach (var system in _systems)
            {
                system.Value.Init();
            }
        }

        private void InjectGameObjects()
        {
            _camera = Camera.main;
            var gridSettings = FindObjectOfType<GridSettings>();
            var molesSpawnerSettings = FindObjectOfType<MolesSpawnerSettings>();
            var poolCommonParent = FindObjectOfType<PoolCommonParent>();
            foreach (var system in _systems)
            {
                system.Value.Inject(gridSettings, molesSpawnerSettings, poolCommonParent, _camera);
            }
        }

        private void Update()
        {
            _systems[SystemType.Update].Run();
        }

        private void FixedUpdate()
        {
            _systems[SystemType.FixedUpdate].Run();
        }

        private void AddDebugSystems()
        {
#if UNITY_EDITOR
            _systems[SystemType.Update].Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif
        }

        private void AddGameSystems()
        {
            _systems[SystemType.Init]
                .Add(new GridInit())
                .Add(new MoleSpawnerInit());

            _systems[SystemType.Update]
                .Add(new MoleSpawner())
                .Add(new TimerForReturningToPool())
                .Add(new MoleHitter())
                .Add(new ResetMoleSettings())
                .Add(new MoleCleaner())
                .Add(new CellsCleaner());

            //_systems[SystemType.FixedUpdate]
        }

        private void OnDestroy()
        {
            _world?.Destroy();
            foreach (var system in _systems)
            {
                system.Value.Destroy();
            }

            _tokenSources.Cancel();
            _tokenSources.Dispose();
        }
    }
}