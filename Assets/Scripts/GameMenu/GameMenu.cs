using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
 

namespace Tank1990
{
    public class GameMenu : MonoBehaviour
    {
      


        private SceneLoader _sceneLoader;
        [SerializeField]
        private GameObject _tankSelect;

        [SerializeField]
        private List<PointSelectMenu> _SelectMenuList;
        [SerializeField]
        private InputPlayer _inputPlayer;
        [Space]
        

        [Space]
        [SerializeField]
        private Canvas _canvasMenu;
        [SerializeField]
        private Canvas _canvasSetup;
        [SerializeField]
        private Slider _soundSlider;
        [SerializeField]
        private Slider _brightnessSlider;
        [SerializeField]
        private int _selectPosition;

        [Header("“екстовые пол€ в меню ")]
        [SerializeField]
        private TMP_Text _numberBotText;
        [SerializeField]
        private TMP_Text _liveLiteText;
        [SerializeField]
        private TMP_Text _liveMediumText;
        [SerializeField]
        private TMP_Text _liveHeavyText;
        [SerializeField]
        private TMP_Text _liveWheeledText;
        [SerializeField]
        private TMP_Text _livePlayerText;
        [SerializeField]
        private GameObject _screen;
        [SerializeField]
        private int _maxLives = 9;


        private const int LIVELITETANK = 0;
        private const int LIVEMEDIUMTANK = 1;
        private const int LIVEWHEELED = 2;
        private const int LIVEHEAVYTANK = 3;

        private const int NUMBERBOTS = 4;
        private const int LIVESPLAYER = 5;
        private const int BRIGHTNESS = 6;
        private const int START = 7;
        private const int SETUP = 8;
        private const int EXIT = 9;
        private const int VOLUME = 8;
        private const int BACK = 9;

  
        private void Awake()
        {
            _inputPlayer = new InputPlayer();
            _sceneLoader = GetComponent<SceneLoader>();
           
        }

        private void Start()
        {
            if (!PlayerPrefs.HasKey(TypeSave.StatusGame.ToString()))
            {
                SaveSettings(); //сохранить начальные установки игры
            }
               

            


            _soundSlider.value = SettingsData.LoadData(TypeSave.Sound);
            _brightnessSlider.value = SettingsData.LoadData(TypeSave.Bright);
            _livePlayerText.text = SettingsData.LoadData(TypeSave.LivePlayer).ToString();

            var tempInputString = SettingsData.ParsingData(TypeSave.LiveBot);


            _liveLiteText.text = tempInputString[TypeTank.LiteTank.ToString()].ToString();
            _liveMediumText.text = tempInputString[TypeTank.MediumTank.ToString()].ToString();
            _liveHeavyText.text = tempInputString[TypeTank.HeavyTank.ToString()].ToString();
            _liveWheeledText.text = tempInputString[TypeTank.WheeledTank.ToString()].ToString();

            SetBrightnessValue(_brightnessSlider.value);
        }
        private void OnEnable()
        {
            _inputPlayer.Enable();
            _inputPlayer.Menu.Enter.performed += OnEnter;
            _inputPlayer.Menu.SelectMenu.performed += OnSelectMenu;
         
        }

        private void OnDisable()
        {
            _inputPlayer.Disable();
          
            _inputPlayer.Menu.Enter.performed -= OnEnter;
            _inputPlayer.Menu.SelectMenu.performed -= OnSelectMenu;
        }


        private void SelectMenu()
        {
            var type = _SelectMenuList[_selectPosition].GetSelectMenu;

            switch (type)
            {

                case TypeSelectMenu.Start:
                    LoadScene();
                    break;
                case TypeSelectMenu.Setup:
                    OpenSetup();
                    break;
                case TypeSelectMenu.Back:
                    BackMenu();
                    break;
                case TypeSelectMenu.Exit:
                    ExitSelect();
                    break;
            }
        }

        private void SaveSettings()
        {
            var tempLivePlayer = int.Parse(_livePlayerText.text);
            var tempNumberBots = int.Parse(_numberBotText.text);
            string tanksLive =
                $"{TypeTank.LiteTank}:{_liveLiteText.text}," +
                $"{TypeTank.MediumTank}:{_liveMediumText.text}," +
                $"{TypeTank.HeavyTank}:{_liveHeavyText.text}," +
                $"{TypeTank.WheeledTank}:{_liveWheeledText.text}";

            SettingsData.SaveString(TypeSave.LiveBot, tanksLive);

            SettingsData.Save(TypeSave.numberBots, tempNumberBots);
            SettingsData.Save(TypeSave.Sound, _soundSlider.value);
            SettingsData.Save(TypeSave.Bright, _brightnessSlider.value);
            SettingsData.Save(TypeSave.LivePlayer, tempLivePlayer);
        }

        private void BackMenu()
        {
            SaveSettings();


            _SelectMenuList[LIVELITETANK].GetSelectMenu = TypeSelectMenu.None;
            _SelectMenuList[LIVEMEDIUMTANK].GetSelectMenu = TypeSelectMenu.None;
            _SelectMenuList[LIVEHEAVYTANK].GetSelectMenu = TypeSelectMenu.None;
            _SelectMenuList[LIVEWHEELED].GetSelectMenu = TypeSelectMenu.None;
            _SelectMenuList[NUMBERBOTS].GetSelectMenu = TypeSelectMenu.None;
            _SelectMenuList[LIVESPLAYER].GetSelectMenu = TypeSelectMenu.None;
            _SelectMenuList[BRIGHTNESS].GetSelectMenu = TypeSelectMenu.None;
            _SelectMenuList[START].GetSelectMenu = TypeSelectMenu.Start;
            _SelectMenuList[SETUP].GetSelectMenu = TypeSelectMenu.Setup;
            _SelectMenuList[EXIT].GetSelectMenu = TypeSelectMenu.Exit;
            _canvasMenu.enabled = true;
            _canvasSetup.enabled = false;

            _selectPosition = SETUP;
            _tankSelect.transform.position = _SelectMenuList[SETUP].transform.position;
        }

        private void OpenSetup()
        {
            _SelectMenuList[LIVELITETANK].GetSelectMenu = TypeSelectMenu.LiteTank;
            _SelectMenuList[LIVEMEDIUMTANK].GetSelectMenu = TypeSelectMenu.MediumTank;
            _SelectMenuList[LIVEHEAVYTANK].GetSelectMenu = TypeSelectMenu.HeavyTank;
            _SelectMenuList[LIVEWHEELED].GetSelectMenu = TypeSelectMenu.WheeledTank;
            _SelectMenuList[NUMBERBOTS].GetSelectMenu = TypeSelectMenu.NumberBots;
            _SelectMenuList[LIVESPLAYER].GetSelectMenu = TypeSelectMenu.LivesPlayer;
            _SelectMenuList[BRIGHTNESS].GetSelectMenu = TypeSelectMenu.Brightness;
            _SelectMenuList[START].GetSelectMenu = TypeSelectMenu.None;
            _SelectMenuList[VOLUME].GetSelectMenu = TypeSelectMenu.Volume;
            _SelectMenuList[BACK].GetSelectMenu = TypeSelectMenu.Back;
            _canvasMenu.enabled = false;
            _canvasSetup.enabled = true;
            _selectPosition = LIVELITETANK;
            _tankSelect.transform.position = _SelectMenuList[LIVELITETANK].transform.position;

        }


        private void ExitSelect()
        {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;

#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR
        Application.Quit();
#endif


        }


        private void OnSelectMenu(InputAction.CallbackContext context)
        {
            var inputVector = context.ReadValue<Vector2>();
            int vertical = (int)inputVector.y;
            float horizontal = inputVector.x;
            if (vertical != 0) { SelectMenuList(vertical); }
            if (horizontal != 0) { SelectSlidersValue(horizontal); }




        }
        private void SelectMenuList(int value)
        {
            
            var tempCount = _selectPosition;

            do
            {
                tempCount += value;

                if (tempCount < 0 || tempCount >= _SelectMenuList.Count) return;

            } while (_SelectMenuList[tempCount].GetSelectMenu == TypeSelectMenu.None);

            _tankSelect.transform.position = _SelectMenuList[tempCount].transform.position;

            _selectPosition = tempCount;



        }

        private void SelectSlidersValue(float value)
        {




            switch (_SelectMenuList[_selectPosition].GetSelectMenu)
            {
                case TypeSelectMenu.Volume:

                    SelecSlider(_soundSlider, value);
                    break;
                case TypeSelectMenu.Brightness:
                    SelecSlider(_brightnessSlider, value);

                    break;
                case TypeSelectMenu.LiteTank:
                    SetValueLives(_liveLiteText, value);
                    break;
                case TypeSelectMenu.MediumTank:
                    SetValueLives(_liveMediumText, value);
                    break;
                case TypeSelectMenu.HeavyTank:
                    SetValueLives(_liveHeavyText, value);
                    break;
                case TypeSelectMenu.WheeledTank:
                    SetValueLives(_liveWheeledText, value);
                    break;

                case TypeSelectMenu.LivesPlayer:
                    SetValueLives(_livePlayerText, value);
                    break;
                case TypeSelectMenu.NumberBots:
                    SetValueLives(_numberBotText, value);
                    break;
            }


        }
        /// <summary>
        /// ”становка значений жизней игрока и бота в настройках игры
        /// </summary>
        private void SetValueLives(TMP_Text text, float value)
        {
            if (int.TryParse(text.text, out int tempText))
            {
                tempText += (int)value;
                tempText = Mathf.Clamp(tempText, 1, _maxLives);

            }
            text.text = tempText.ToString();

        }




        private void SelecSlider(Slider slider, float value)
        {


            var tempValue = slider.value;

            tempValue += value * 0.1f;
            tempValue = Mathf.Clamp01(tempValue);
            slider.value = tempValue;
            if (slider == _brightnessSlider)
            {
                SetBrightnessValue(tempValue);

            }
        }

        private void SetBrightnessValue(float tempValue)
        {
            SpriteRenderer spriteRenderer = _screen.GetComponent<SpriteRenderer>();

            Color color = spriteRenderer.color;

            tempValue = Mathf.Clamp(tempValue, 0f, 1f);
            color.a = tempValue;


            spriteRenderer.color = color;
        }

        private void OnEnter(InputAction.CallbackContext context) => SelectMenu();

  
       
        private void LoadScene()
        {
          

            if (_sceneLoader != null)
            {
                _sceneLoader.LoadSceneAsync("Game");
            }
        }
       
        
    }
}
