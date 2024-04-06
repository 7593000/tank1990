using System.Collections;
using UnityEngine;

namespace Tank1990
{
    [RequireComponent(typeof(MoveComponent))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private SoundManager _soundManager;
        [SerializeField]
        private SideType _side;
        private DirectionType _direction;
        private SoundManager _sound;
        private MoveComponent _moveComp;

        [SerializeField]
        private int _damage = 1;
        [SerializeField]
        private float _lifeTime = 5.0f;

        public void SetParams(DirectionType direction, SideType side) => (_direction, _side) = (direction, side);
        public DirectionType GetDirection => _direction;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            var fire = collision.GetComponent<FireComponent>();

            if (fire != null)
            {
                if (fire.GetSide == _side) return;
                var condition = fire.GetComponent<ConditionComponent>();

                condition.SetDamage(_damage);

                gameObject.SetActive(false);
            }
            if (collision.gameObject.TryGetComponent<BotComponent>(out BotComponent bot))
            {

                bot.MoveAimPlayer(GetDirection.ConvertOppositeDirectionFromType());


            }

            if (collision.TryGetComponent<CellComponent>(out var cell))
            {
                if (cell.DestroyProjectile)//������� ���� ��� ��������� � ������������� ����
                {
                    //��������� ���� ��������� � ������ 
                    if (_side == SideType.Player) _sound.PlaySound(Sound.hittingZoneGame);

                    gameObject.SetActive(false);
                }

                if (cell.DestroyCell)
                {
                    Destroy(cell.gameObject); //������� ����������� ���� ��� ���������  
                                              //��������� ���� ��������� � ����

                    if (_side == SideType.Player) _sound.PlaySound(Sound.hittingBrick);

                    return;
                }

            }
        }
        IEnumerator AutoDestroy()
        {
            yield return new WaitForSeconds(_lifeTime);
            gameObject.SetActive(false);
        }






        private void Start()
        {
            _moveComp = GetComponent<MoveComponent>();
            StartCoroutine(AutoDestroy());
            _sound = FindFirstObjectByType<SoundManager>();
        }



        private void Update()
        {
            _moveComp.OnMove(_direction);
        }

    }

}
