using UnityEngine;
namespace Tank1990
{
    public class ColliderComponent : MonoBehaviour
    {
        private bool _isCollision = false;

        public bool GetCollision { get => _isCollision; }

        private void OnCollisionEnter2D(Collision2D collision)
        {

            if (collision.gameObject.layer == LayerMask.NameToLayer(Extensions.LayerBullet)) return;

            _isCollision = true;
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            _isCollision = false;
        }
    }
}
