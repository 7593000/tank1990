using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Tank1990
{
    public class PoolComponent : MonoBehaviour
    {
        private GameManager _gm;

        [Header("Префаб Взрыва")]
        [SerializeField]
        private SpriteAnimation _prefExplosion;
        [Header("Префаб спавна")]
        [SerializeField]
        private SpriteAnimation _prefSpawn;
        [Space]
        [Header("Префаб пули")]
        [SerializeField]
        private Projectile _prefProjectile;
        [Space]
        [Header("Префабы типов танков")]
        [SerializeField, Tooltip("Легкий танк")]
        private BotComponent _liteTank;
        [SerializeField, Tooltip("Колесный танк")]
        private BotComponent _wheeledTank;
        [SerializeField, Tooltip("стредний танк")]
        private BotComponent _mediumTank;
        [SerializeField, Tooltip("Тяжелый танк")]
        private BotComponent _heavyTank;
        [Space]
        [Header("Родители для пуля")]
        [SerializeField]
        private Transform _parentSpawn;
        [SerializeField]
        private Transform _parentExplosion;
        [SerializeField]
        private Transform _parentProjectile;
        [SerializeField]
        private Transform _parentBot;
        [Space]
        [Header("Количество необходимых элементов")]
        [SerializeField, Range(0f, 50f)]
        private int _projectileCount = 20;
        [SerializeField, Range(0f, 10f)]
        private int _spawnCount = 7;
        [SerializeField, Range(0f, 10f)]
        private int _explosionCount = 5;
        [SerializeField, Range(0f, 10f)]

        private List<SpriteAnimation> _poolExplosion = new();
        private List<SpriteAnimation> _poolSpawn = new();
        private List<Projectile> _poolProjectile = new();

        /// <summary>
        /// Получить пулю из пула
        /// </summary>
        public Projectile GetBullet => GetElement(_poolProjectile);
        /// <summary>
        /// Получить анимацию спавна из пула
        /// </summary>
        public SpriteAnimation GetSpawn => GetElement(_poolSpawn);
        /// <summary>
        /// Получить анимацию взрыва из пула 
        /// </summary>
        public SpriteAnimation GetExplosion => GetElement(_poolExplosion);

        public Dictionary<TypeTank, List<BotComponent>> _tanksCollections = new Dictionary<TypeTank, List<BotComponent>>();
        public Dictionary<TypeTank, List<BotComponent>> GetTanks { get => _tanksCollections; }

        private void CreatePoolTanks(TypeTank type, List<BotComponent> list, int count)
        {

            switch (type)
            {
                case TypeTank.LiteTank:
                    CreatePool(_liteTank, _parentBot, list, count);
                    break;
                case TypeTank.WheeledTank:
                    CreatePool(_wheeledTank, _parentBot, list, count);
                    break;
                case TypeTank.MediumTank:
                    CreatePool(_mediumTank, _parentBot, list, count);
                    break;
                case TypeTank.HeavyTank:
                    CreatePool(_heavyTank, _parentBot, list, count);
                    break;
            }



        }

        /// <summary>
        /// Создание пула для элементов
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="prefab">Префаб элемента</param>
        /// <param name="parent">Родитель для элементов</param>
        /// <param name="poolList">Лист для элементов</param>
        /// <param name="count">Колчисетво создаваемых элементов</param>
        private void CreatePool<T>(T prefab, Transform parent, List<T> poolList, int count = 1) where T : MonoBehaviour
        {


            for (int i = 0; i < count; i++)
            {


                T poolElement = Instantiate(prefab, parent);
                poolElement.gameObject.SetActive(false);
                poolList.Add(poolElement);
            }

        }


        private T GetElement<T>(List<T> list) where T : MonoBehaviour
        {
            if (list.Count == 0)
            {
                Debug.LogWarning("Список пуст!");
                return default;
            }
            T inactiveElement = list.FirstOrDefault(element => !element.gameObject.activeSelf);


            if (inactiveElement == null)
            {
                //todo => метод создания пуль, если все пули заняты
                Debug.LogWarning("Не найдено неактивных элементов!");

                return null;
            }

            if (inactiveElement == null || inactiveElement.Equals(null))
            {
                Debug.LogWarning("Элемент удален, но вы все еще пытаетесь получить к нему доступ!");
                return null;
            }

            return inactiveElement;
        }

        private void CreateCountTank()
        {
            List<GamingWaves> tempTank = _gm.GetGameingWaves;

            foreach (var tank in tempTank)
            {


                foreach (var type in tank.GetCountTanks)
                {
                    TypeTank tankType = type.Key;
                    int tankCount = type.Value;

                    if (tankCount == 0) continue;

                    if (_tanksCollections.ContainsKey(tankType))
                    {
                        if (_tanksCollections[tankType].Count == 0)
                        {
                            CreatePoolTanks(tankType, _tanksCollections[tankType], tankCount);
                        }
                        else if (_tanksCollections[tankType].Count < tankCount)
                        {
                            int newCount = tankCount - _tanksCollections[tankType].Count;

                            CreatePoolTanks(tankType, _tanksCollections[tankType], newCount);
                        }
                    }
                    else
                    {

                        List<BotComponent> newTankList = new List<BotComponent>();
                        CreatePoolTanks(tankType, newTankList, tankCount);

                        _tanksCollections[tankType] = newTankList;
                    }
                }
            }
        }

        ////Создание пула танков
        //foreach (var tank in _tanksCollection)
        //{
        //  //  CreatePoolTanks(tank.Key, tank.Value);
        //}


        private void Awake()
        {
            _gm = FindFirstObjectByType<GameManager>();

        }
        private void Start()
        {

            CreatePool(_prefExplosion, _parentExplosion, _poolExplosion, _explosionCount);
            CreatePool(_prefSpawn, _parentSpawn, _poolSpawn, _spawnCount);
            CreatePool(_prefProjectile, _parentProjectile, _poolProjectile, _projectileCount);
            CreateCountTank();

        }
    }
}

