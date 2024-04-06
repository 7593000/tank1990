using System.Collections;
using UnityEngine;

namespace Tank1990
{
    public class FireComponent : MonoBehaviour
    {
        private PoolComponent _pool;
        [SerializeField, Range(0.5f, 5.0f)]
        private float _delatFire = 1f;
        [SerializeField]
        private bool _canFire = true;

        [SerializeField]
        private Projectile _prefab;

        [SerializeField]
        private SideType _side;

        public SideType GetSide => _side;
        public bool GetCanFire => _canFire;
        public void OnFire()
        {
            if (!_canFire) { return; }
            var bullet = _pool.GetBullet;

            bullet.transform.position = transform.position;
            bullet.SetParams(transform.eulerAngles.ConvertRotationFromType(), _side);
            bullet.gameObject.SetActive(true);
            StartCoroutine(OnDelay());
        }

        private IEnumerator OnDelay()
        {
            _canFire = false;
            yield return new WaitForSeconds(_delatFire);
            _canFire = true;
        }
        private void OnEnable()
        {
            if (_canFire == false) _canFire = true;
        }
        private void Awake()
        {
            _pool = FindFirstObjectByType<PoolComponent>();
        }
    }


}

