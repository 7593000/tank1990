using UnityEngine;
namespace Tank1990
{
    public class PointSelectMenu : MonoBehaviour
    {
        [SerializeField]
        private TypeSelectMenu _type;

        public TypeSelectMenu GetSelectMenu { get => _type; set => _type = value; }

    }
}
