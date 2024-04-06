using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Tank1990

{


    public static class Extensions
    {
        private static Dictionary<DirectionType, Vector3> _directions;
        private static Dictionary<DirectionType, Vector3> _rotation;
        public static string LayerBullet = "Bullet";
        public static string LayerCrossroad = "Crossroad";

        static Extensions()
        {
            _directions = new Dictionary<DirectionType, Vector3>()
            {
                {DirectionType.Up,    new Vector3( 0f,  1f, 0f) },
                {DirectionType.Right, new Vector3( 1f,  0f, 0f) },
                {DirectionType.Left,  new Vector3(-1f,  0f, 0f) },
                {DirectionType.Down,  new Vector3( 0f, -1f, 0f) }
            };

            _rotation = new Dictionary<DirectionType, Vector3>()
            {

                {DirectionType.Up, new Vector3(0f, 0f, 0f) },
                {DirectionType.Right, new Vector3(0f, 0f, 270f) },
                {DirectionType.Down, new Vector3(0f, 0f, 180f) },
                {DirectionType.Left, new Vector3(0f, 0f, 90f) }

            };
        }

        /// <summary>
        /// Вернуть Vertor3 для движения
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Vector3 ConvertTypeFromDirection(this DirectionType type)
        {

            return _directions[type];
        }
        public static DirectionType ConvertDirectionFromType(this Vector3 direction) => _directions.First(t => t.Value == direction).Key;
        public static DirectionType ConvertDirectionFromType(this Vector2 direction)
        {
            Vector3 dir = (Vector3)direction;

            return _directions.First(t => t.Value == dir).Key;
        }
        /// <summary>
        /// Взять противоположную сторону движения
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns> 
        public static DirectionType ConvertOppositeDirectionFromType(this DirectionType type)
        {
            if (_directions.TryGetValue(type, out Vector3 temp))
            {
                Vector3 oppositeVector = temp * -1f;
                var keyValuePair = _directions.FirstOrDefault(x => x.Value == oppositeVector);
                if (!keyValuePair.Equals(default(KeyValuePair<DirectionType, Vector3>)))
                {

                    DirectionType key = keyValuePair.Key;
                    return key;
                }
                else
                {
                    // Вектор не найден
                    Console.WriteLine("Вектор не найден в словаре.");

                }

            }
            return DirectionType.Left;
        }
        /// <summary>
        /// Получить поворот 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Vector3 ConvertTypeFromRotation(this DirectionType type)
        {

            return _rotation[type];
        }

        public static DirectionType ConvertRotationFromType(this Vector3 rotation) => _rotation.First(t => t.Value == rotation).Key;






    }
}
