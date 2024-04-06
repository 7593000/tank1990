using System.Collections;
using UnityEngine;

namespace Tank1990
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerConditionComponent : ConditionComponent
    {

        private Vector3 _startPoint;
        private SpriteRenderer _spriteRenderer;
        protected bool _immortal; //проверка на бессмертие
        [SerializeField]
        private bool _isCheat = false;

        [SerializeField]
        private float _immortalTime = 3.0f;
        [SerializeField]
        private float _immortalSwitchVisual = 0.2f;

        public override void SetDamage(int damage)
        {
            if (_immortal) return;

            base.SetDamage(damage);

            StartCoroutine(OnImmortal());
            transform.position = _startPoint;


        }

        protected override void DestroyTank()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// ¬ключение бессмерти€
        /// </summary>
        /// <returns></returns>
        private IEnumerator OnImmortal()
        {

            _immortal = true;
            var time = _immortalTime * 0.03;
            while (time > 0)
            {
                _spriteRenderer.enabled = !_spriteRenderer.enabled;

                if (!_isCheat)
                {
                    time -= Time.deltaTime;

                }


                yield return new WaitForSeconds(_immortalSwitchVisual);
            }

            _immortal = false;
            _spriteRenderer.enabled = true;
        }

        public void OnCheatPlayer(bool status)
        {

            _isCheat = status;
            if (_isCheat) StartCoroutine(OnImmortal());
        }

        private void Start()
        {
            _health = GameManager.GetHealthPlayer;
            _startPoint = transform.position;
            _spriteRenderer = GetComponent<SpriteRenderer>();


        }
    }
}
