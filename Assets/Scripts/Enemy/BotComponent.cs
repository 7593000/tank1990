
using System;
using System.Collections;
using UnityEngine;

namespace Tank1990
{
    [RequireComponent(typeof(MoveComponent))]
    public class BotComponent : MonoBehaviour
    {
        public static event Action<BotComponent> OnDeleteBot;
        [SerializeField]
        private TypeTank _type;
        [SerializeField]
        private PoolComponent _pool;

        private MoveComponent _moveComp;
        [SerializeField]
        private FireComponent _fireComp;
        [SerializeField]
        private ColliderComponent _collider;

        private SoundManager _sound;
        /// <summary>
        /// ����������� ���������
        /// </summary>
        [SerializeField]
        private bool _onMove;

        public int HeightTank { get; set; }

        private DirectionType _direction;
        /// <summary>
        /// �������� ��� ����� TypeTank
        /// </summary>
        public TypeTank GetTypeTank { get => _type; }

        [SerializeField, Tooltip("����� �������� � ���� ������� ����")]
        [Range(1f, 10f)]
        private float _timerForMove = 5f;

        [SerializeField]
        private const float offset = 2f;
        [SerializeField]
        private float _minDistance = 0.1f;
        [SerializeField]
        private float _expectNewAttempt = 5f;
        private bool isInInteractionZone = false;

        private Collider2D currentCollider;
        private Rigidbody2D _rb;
        [SerializeField]
        private Coroutine _moveCoroutine;

#if UNITY_EDITOR

        private Color _colorObstacle;
        private Color _selectColor;
#endif

        private void Awake()
        {
            _pool = FindFirstObjectByType<PoolComponent>();
            _rb ??= GetComponent<Rigidbody2D>();
            _collider ??= GetComponentInChildren<ColliderComponent>();
            _fireComp ??= GetComponentInChildren<FireComponent>();
            _moveComp = GetComponent<MoveComponent>();
            _sound = FindFirstObjectByType<SoundManager>();

#if UNITY_EDITOR

            _colorObstacle = Color.red;
#endif 

        }
        /// <summary>
        /// ��������� ���� � ������� ������ 
        /// </summary>

        public void MoveAimPlayer(DirectionType type)
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
                _moveCoroutine = null;
            }

            MoveBotLogic(type);
        }



        /// <summary>
        /// �������� �� ����������� ����� � ������������ ��� 
        /// </summary>
        private void CheckedCollision()
        {
            if (_collider.GetCollision)
            {


                if (_moveCoroutine != null)
                {
                    StopCoroutine(_moveCoroutine);
                    _moveCoroutine = null;
                }

                FindingFreePath((int)_direction);

            }
        }




        /// <summary>
        /// �������� ����������� �������� � ��������� �������
        /// </summary>
        public bool CheckedPath(DirectionType patch)
        {
            Vector3 temp = patch.ConvertTypeFromDirection();

            RaycastHit2D hit = Physics2D.Raycast(transform.position, temp, offset, 3);

            return (hit.collider == null);
        }

        /// <summary>
        /// ����� ���������� ����
        /// </summary>
        private void FindingFreePath(int skipPath = 5)//skipPath - ��������� ������� , ��� ����������� ( 5 - �� ������������ ������� ) 
        {
            _onMove = false;
            const int maxAttempts = 20;

            DirectionType randomDirection;

            for (int attempts = 0; attempts < maxAttempts; attempts++)
            {
                randomDirection = (DirectionType)GameManager.RandomRange(0, System.Enum.GetValues(typeof(DirectionType)).Length);

                if (randomDirection == (DirectionType)skipPath)
                    continue;

                if (CheckedPath(randomDirection))
                {
                    if (_moveCoroutine != null)
                    {
                        StopCoroutine(_moveCoroutine);
                        _moveCoroutine = null;
                    }

                    _direction = randomDirection;
                    // ������� ��������� �����������
                    _onMove = true;
                    MoveBotLogic(randomDirection);
                    return;
                }
            }

            StartCoroutine(WaitFreePath(_expectNewAttempt));

            // ���� �� ���� ��������� ����������� �� ������� ����� maxAttempts �������
            // ����� �������� ��������������� ��������� �����
        }

        private IEnumerator WaitFreePath(float timer)
        {

            yield return new WaitForSeconds(timer);
            FindingFreePath();
        }

        /// <summary>
        /// ���������� ����
        /// </summary>
        public void DestroyBot()
        {
            OnDeleteBot?.Invoke(this);
            _onMove = false;
            //_sound.PlaySound(Sound.explosion);

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isInInteractionZone == true) return;

            if (collision.gameObject.layer == LayerMask.NameToLayer(Extensions.LayerCrossroad))
            {

                isInInteractionZone = true;
                // ������������� ����, ��� �� � ���� ��������������
                currentCollider = collision; // ��������� ������ �� ������� ���������
                StartCoroutine(CheckDistance());



            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            // ���� �� ����� �� ���� �������������� � �����������, ���������� ����
            if (collision == currentCollider)
            {
                isInInteractionZone = false;
                StopCoroutine(CheckDistance());
            }
        }

        /// <summary>
        /// �������� ��������� �� ����� ���������� 
        /// </summary>
        /// <returns></returns>
        private IEnumerator CheckDistance()
        {
            while (isInInteractionZone && currentCollider != null)
            {
                // �������� ������� ������ ����������
                Vector2 colliderCenter = currentCollider.transform.position;

                Vector2 myPosition = transform.position;

                // ��������� ������� ���������� ����� ����� �������� � ������� ����������
                float sqrDistance = (colliderCenter - myPosition).sqrMagnitude;

                if (sqrDistance < _minDistance * _minDistance)
                {

                    isInInteractionZone = false;

                    transform.position = colliderCenter;

                    FindingFreePath();
                }

                yield return null;
            }
        }

        /// <summary>
        /// ��������� �������� �����
        /// </summary>
        /// <param name="type"></param>
        private void MoveBotLogic(DirectionType type)
        {

            if (gameObject.activeSelf)
            {
                _moveCoroutine = StartCoroutine(MoveBot(type));

            }
        }

        private IEnumerator MoveBot(DirectionType type)
        {
            var timerNewFind = _timerForMove;
            _rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;//������ ��������

            while (_onMove)

            {
                //todo => ��������, �����
                //������������ ������ �� ���, ��� ���������� ��� ��������. ����� ���� �� ������� �� ��������� 
                switch (type)//todo =>��������, ����� �� ������
                {
                    case DirectionType.Up:
                    case DirectionType.Down:
                        _rb.constraints = ~RigidbodyConstraints2D.FreezePositionY;
                        break;
                    case DirectionType.Right:
                    case DirectionType.Left:
                        _rb.constraints = ~RigidbodyConstraints2D.FreezePositionX;
                        break;
                }


                _moveComp.OnMove(type);

                if (timerNewFind < 0) { FindingFreePath(); yield break; }

                //�������� ���������� ����������� �� ����
                if (!CheckedPath(type))
                {

                    FindingFreePath();

                    yield break;
                }

                timerNewFind -= Time.deltaTime;
                yield return null;
            }

        }

        /// <summary>
        /// ����� ����
        /// </summary>
        public void SpawnBot()
        {
            //todo => ��������� �������� ��������. 
            _pool.GetSpawn.StartSpriteAnimation(transform);
            //   gameObject.SetActive(true);
            _onMove = true;
            FindingFreePath();
        }

        private void Update() => _fireComp.OnFire();

        private void FixedUpdate() => CheckedCollision();



        //****************************** OnDrawGizmos ******************************
#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            _selectColor = _colorObstacle;

            Vector2 endPositionGizmos = transform.position + transform.up * offset;
            Vector2 endPositionGizmosRight = transform.position + transform.right * offset;
            Vector2 endPositionGizmosLeft = transform.position - transform.right * offset;
            Vector2 endPositionGizmosDown = transform.position - transform.up * offset;


            Debug.DrawLine(transform.position, endPositionGizmos, _selectColor);
            Debug.DrawLine(transform.position, endPositionGizmosRight, _selectColor);
            Debug.DrawLine(transform.position, endPositionGizmosLeft, _selectColor);
            Debug.DrawLine(transform.position, endPositionGizmosDown, _selectColor);
        }
#endif

    }
}
