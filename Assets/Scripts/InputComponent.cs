using UnityEngine;
using UnityEngine.InputSystem;

namespace Tank1990
{
    [RequireComponent(typeof(MoveComponent), typeof(FireComponent))]
    public class InputComponent : MonoBehaviour
    {
        private DirectionType _lastType;
        private MoveComponent _moveComp;
        private FireComponent _fireComp;
        [SerializeField]
        private InputAction _move;
        [SerializeField]
        private InputAction _fire;
        private SoundManager _soundManager;
        private bool _isMoving;

        private void Move()
        {
            var fire = _fire.ReadValue<float>();
            if (fire == 1)
            {
                if (_fireComp.GetCanFire) _soundManager.PlaySound(Sound.shoot);

                _fireComp.OnFire();

            }
            var direction = _move.ReadValue<Vector2>();

            DirectionType type;

            if (direction.x != 0f && direction.y != 0f)
            {
                type = _lastType;
            }
            else if (direction.x == 0f && direction.y == 0f)

            {
                _isMoving = false;

                return;
            }
            else type = _lastType = direction.ConvertDirectionFromType();


            _isMoving = true;
            _moveComp.OnMove(type);
        }

        private void SoundOn()
        {
            if (_isMoving)
            {
                _soundManager.PlaySound(Sound.move, true);

            }
            else
            {
                _soundManager.PlaySound(Sound.stand, true);

            }
        }

        private void Start()
        {
            _soundManager = FindFirstObjectByType<SoundManager>();
            _moveComp = GetComponent<MoveComponent>();
            _fireComp = GetComponent<FireComponent>();

            _move.Enable();
            _fire.Enable();
        }

        private void Update()
        {
            SoundOn();
            Move();

        }



        private void OnDestroy()
        {
            _move.Disable();
            _fire.Disable();
        }
    }
}
