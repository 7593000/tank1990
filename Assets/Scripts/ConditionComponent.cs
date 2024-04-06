using UnityEngine;

namespace Tank1990
{
    public class ConditionComponent : MonoBehaviour
    {
        private SoundManager _sound;
        private PoolComponent _pool;

        private int _startHealth;
        [SerializeField]
        protected int _health = 3;


        [SerializeField]
        public virtual void SetDamage(int damage)
        {


            _health -= damage;

            if (_health <= 0)
            {
                var badaBoom = _pool.GetExplosion;

                badaBoom.StartSpriteAnimation(transform);
                _sound.PlaySound(Sound.explosion);

                DestroyTank();



            }
        }

        protected virtual void DestroyTank()
        {
            GetComponentInParent<BotComponent>().DestroyBot();
        }

        private void Start()
        {
            _health = GetComponentInParent<BotComponent>().HeightTank;
        }

        private void OnEnable()
        {
            if (GetComponentInParent<BotComponent>() == null) return;
            int tempHeight = GetComponentInParent<BotComponent>().HeightTank;

            _health = tempHeight;
        }



        private void Awake()
        {
            _sound = FindFirstObjectByType<SoundManager>();
            _pool = FindFirstObjectByType<PoolComponent>();
        }
    }
}