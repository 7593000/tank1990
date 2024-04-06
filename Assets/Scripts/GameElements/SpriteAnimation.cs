using System.Collections;
using UnityEngine;
namespace Tank1990
{
    public class SpriteAnimation : MonoBehaviour
    {
        private SpriteRenderer _render;
        [SerializeField]
        private Sprite[] _sprite;
        [SerializeField]
        private float _frameDelay = 0.5f;
        private void Awake()
        {
            _render = GetComponent<SpriteRenderer>();
        }

        public void StartSpriteAnimation(Transform transform)
        {

            gameObject.transform.position = transform.position;
            gameObject.SetActive(true);
            StartCoroutine(OnExplosion());
        }

        private IEnumerator OnExplosion()
        {
            int i = 0;

            while (++i < _sprite.Length)
            {
                _render.sprite = _sprite[i];
                yield return new WaitForSeconds(_frameDelay);
            }

            _render.sprite = null;

            gameObject.SetActive(false);
        }

    }
}
