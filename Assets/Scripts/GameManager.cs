
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Tank1990
{
    public class GameManager : MonoBehaviour
    {




        [SerializeField]
        private List<PointEnemy> _point;

        [Space]
        [SerializeField]
        private int _healthPlayer;


        [Space]
        [SerializeField]
        private List<GamingWaves> _waves;

           private int _countTankActive = 4;



        [Space]
        [SerializeField]
        private float _spawnTimer = 3f;
        private bool _nextWave = true;
        /// <summary>
        /// Получить количество разных типов танков в волне.
        /// </summary>
        public List<GamingWaves> GetGameingWaves { get => _waves; }

        /// <summary>
        /// Номер волны 
        /// </summary>
        private int GetCountWaves { get; set; } = 0;
        /// <summary>
        /// Общее количество танков в 1 этапе волны. 
        /// </summary>
        private int GetCountTanksStart { get; set; }

        private PoolComponent _pool;
        [SerializeField]
        List<BotComponent> _tanks = new List<BotComponent>();

        private static Dictionary<TypeTank, int> _liveTanks = new Dictionary<TypeTank, int>();


        /// <summary>
        /// Рандом
        /// </summary>
        /// <returns></returns>
        public static int RandomRange(int min, int max) => Random.Range(min, max);

        /// <summary>
        /// Получить количество жизней для игроков
        /// </summary>
        public static int GetHealthPlayer { get; private set; }


        private void GenericWave()
        {
            if (_nextWave)
            {
               

                if (_waves[GetCountWaves] == null) return;

                foreach (var tanks in _waves[GetCountWaves].GetCountTanks)
                {
                    TypeTank tempKay = tanks.Key;

                    int tempV = tanks.Value;

                    for (int y = 0; y < tempV; y++)
                    {
                        _tanks.Add(_pool.GetTanks[tempKay][y]);
                    }

                }
                GetCountTanksStart = _tanks.Count;// устанавливаем общее количество танков в игре на 1 волну

            }
            SpawnEnemy(_countTankActive);// спавн врагов. _countTankActive - стартовое количество спавна танков одновременно 
            GetCountWaves++;
        }

        /// <summary>
        /// Проверка на активный элемент 
        /// </summary>
        /// <returns></returns>
        private T CheckingActiveElement<T>(List<T> elements, bool active = true) where T : Component
        {
            int randomIndex;
            do
            {
                randomIndex = RandomRange(0, elements.Count);
            }
            while (elements[randomIndex].gameObject.activeSelf != active);

            elements[randomIndex].gameObject.SetActive(!active);
            return elements[randomIndex];
        }
        /// <summary>
        /// Активировать все точки , которые были деактивированными. 
        /// </summary>
        private void ActivateAllInactivePoints()
        {
            // Выбираем все неактивные точки
            var inactivePoints = _point.Where(point => !point.gameObject.activeSelf).ToList();

            // Активируем все неактивные точки
            inactivePoints.ForEach(point => point.gameObject.SetActive(true));
        }

        /// <summary>
        /// Спавн танков в случайных точках респавна
        /// </summary>
        /// <param name="count">количество танков для активации</param>
        private void SpawnEnemy(int count = 1)
        {
            ActivateAllInactivePoints();

            if (_point.Count == 0) return;  //todo => возможно, не актуально 
            if (_tanks.Count == 0)
            {
                GenericWave();
                return;
            }
            if(_point.Count < count ) count = _point.Count;


            if (_tanks.Count < count) count = _tanks.Count;// проверка на возможность запустить нужное количество танков при старте
            if (GetCountTanksStart <= 0) return; //Проверка на возможность взять танк из пула объектов. 



            for (int i = 0; i < count; i++)
            {
                GetCountTanksStart--;
                var point = CheckingActiveElement(_point, true);
                var bot = CheckingActiveElement(_tanks, false);
                int life = GetLiveTank(bot.GetTypeTank);
                bot.HeightTank = life;
                bot.transform.position = point.transform.position;
                bot.SpawnBot();

            }
        }
        /// <summary>
        /// Удалить танк из списка
        /// </summary>
        /// <param name="tank"></param>
        private void RemoveTank(BotComponent tank)
        {
            if (_tanks.Contains(tank))
            {

                int index = _tanks.IndexOf(tank);

                // Удаляем танк из списка
                _tanks.RemoveAt(index);

                // Отключаем объект танка
                tank.gameObject.SetActive(false);

                StartCoroutine(SpawmTimer());

            }

            if (!_tanks.Contains(tank)) Debug.Log("Танк удален");
        }
        /// <summary>
        /// Запуск метода спавна противника с таймером
        /// </summary>
        /// <returns>новый танк противника</returns>
        private IEnumerator SpawmTimer()
        {
            yield return new WaitForSeconds(_spawnTimer);

            SpawnEnemy();
        }
        /// <summary>
        /// Загрузка сохраненных данных 
        /// </summary>
        private void LoadSettingsTanks()
        {
            Dictionary<string, int> inputLife = SettingsData.ParsingData(TypeSave.LiveBot);

            foreach (TypeTank tankType in System.Enum.GetValues(typeof(TypeTank)))
            {

                string key = tankType.ToString();
                if (inputLife.TryGetValue(key, out int value))
                {
                    _liveTanks[tankType] = value;
                }
            }
        }
        /// <summary>
        /// Получить количество жизней для танка
        /// </summary>
        /// <param name="tank">Тип танка</param>
        /// <returns>int жизни</returns>
        public static int GetLiveTank(TypeTank tank)
        {
            int defaultLife = 1;

            if (_liveTanks.TryGetValue(tank, out int life))
            {
                return life;
            }
            return defaultLife;
        }



        private void OnEnable()
        {

            BotComponent.OnDeleteBot += RemoveTank;
        }



        private void OnDestroy()
        {

            BotComponent.OnDeleteBot -= RemoveTank;
        }
        private void Awake()
        {
            _pool = FindFirstObjectByType<PoolComponent>();
            if (_point == null) Debug.LogError("не добавлены точки для респавна!");
            if (_pool == null) Debug.LogError("Не добавлен компонент пула!");
            GetHealthPlayer = (int)SettingsData.LoadData(TypeSave.LivePlayer);//загрузка жизней игрока
             _countTankActive = (int)SettingsData.LoadData(TypeSave.numberBots );//загрузить количество ботов при старте
           
            LoadSettingsTanks();
        }

        private void Start()=>GenericWave();
       


    }
}

