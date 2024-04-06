using UnityEngine;

namespace Tank1990
{

    /// <summary>
    /// ����������� �����
    /// </summary>
    public class MoveComponent : MonoBehaviour
    {
        [SerializeField, Range(1.0f, 20.0f)]
        private float _speed = 1f;



        public void OnMove(DirectionType type)
        {

            //  transform.position = _speed * Time.deltaTime * ConvertTypeFromDirection(type);

            transform.position += _speed * Time.deltaTime * type.ConvertTypeFromDirection();// ����� ���������� (this) 
            transform.eulerAngles = type.ConvertTypeFromRotation();
        }
    }
}
