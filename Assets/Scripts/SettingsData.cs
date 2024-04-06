using System.Collections.Generic;
using UnityEngine;
namespace Tank1990
{
    public class SettingsData : MonoBehaviour
    {
        static readonly float _deafaultValue = 0.5f;
        static readonly Dictionary<string, int> _deafaultDictionary = new() {

            {TypeTank.LiteTank.ToString(), 1 },
            {TypeTank.MediumTank.ToString(), 2 },
            {TypeTank.HeavyTank.ToString(), 4 },
            {TypeTank.WheeledTank.ToString(), 3 },

    };


        /// <summary>
        /// —охранение значений настроек игры
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static void Save(TypeSave type, float dataSave)
        {
            PlayerPrefs.SetFloat(type.ToString(), dataSave);
            PlayerPrefs.Save();
            Debug.Log($"Game {type} : {dataSave} saved!");
        }

        /// <summary>
        ///  —охранение значений в строку
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dataSave"></param>
        public static void SaveString(TypeSave type, string dataSave)
        {

            PlayerPrefs.SetString(type.ToString(), dataSave);
            PlayerPrefs.Save();

        }
        public static string TestParsing(TypeSave type)
        {

            if (PlayerPrefs.HasKey(type.ToString()))
            {
                string loadedData = PlayerPrefs.GetString(type.ToString());
                Debug.Log("Loaded data: " + loadedData);
                return PlayerPrefs.GetString(type.ToString());
            }
            return "NULL";
        }
        /// <summary>
        /// ѕарсинг строковых значений  с переводом в массив
        /// </summary>
        /// <param name="type"> </param>
        /// <returns> Dictionary<string, int> </returns>
        public static Dictionary<string, int> ParsingData(TypeSave type)
        {
            Dictionary<string, int> tempResult = new();

            if (!PlayerPrefs.HasKey(type.ToString())) return _deafaultDictionary;

            string input = PlayerPrefs.GetString(type.ToString());

            string[] tankData = input.Split(',');//раздел€ем на массивы разделенные зап€той элементы
            string[] tankNames = new string[tankData.Length];
            int[] tankLive = new int[tankData.Length];


            for (int i = 0; i < tankData.Length; i++)
            {

                string[] parts = tankData[i].Split(':');//раздел€ем на массивы разделенные : элементы
                tankNames[i] = parts[0].Trim();  //удал€ем пробелы
                tankLive[i] = int.Parse(parts[1].Trim());

                tempResult[tankNames[i]] = tankLive[i];

            }

            return tempResult;
        }

        

        /// <summary>
        /// «агрузка значений настроек игры
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static float LoadData(TypeSave type)
        {



            if (PlayerPrefs.HasKey(type.ToString()))
            {



                Debug.Log($"Game {type} load!");
                return PlayerPrefs.GetFloat(type.ToString());

            }
            else
            {
                Debug.LogError($"There is no save {type} data!");

                return _deafaultValue;
            }
        }

    }
}

