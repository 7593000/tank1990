using TMPro;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
namespace Tank1990
{
    public class InputSystemComponent : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _cheatStatus;
        private InputPlayer _inputPlayer;
        private bool _isStatusCheat = false;
        [SerializeField]
        private PlayerConditionComponent _player;

        private void ExitGame(InputAction.CallbackContext context)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;

#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR
        Application.Quit();
#endif
        }
        private void OnCheat()
        {
            if (!CheckedScene()) { return; }


            _isStatusCheat = !_isStatusCheat;

            string textStatus = _isStatusCheat ? "Enable" : "Disable";

            _cheatStatus.text = $"GodeMode : {textStatus}";

            if (_player == null) { _player ??= FindFirstObjectByType<PlayerConditionComponent>(); }

            _player.OnCheatPlayer(_isStatusCheat);
        }
        ///todo=>TEMP
        private bool CheckedScene()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;
            if (sceneName == "GameMenu") return false;
            return true;
        }


        private void OnEnable()
        {
            _inputPlayer.Enable();
            _inputPlayer.Menu.Exit.performed += ExitGame;
            _inputPlayer.Menu.Cheat.performed += _ => OnCheat();


        }



        private void OnDestroy()
        {
            _inputPlayer.Disable();
            _inputPlayer.Menu.Exit.performed -= ExitGame;
            _inputPlayer.Menu.Cheat.performed -= _ => OnCheat();


        }
        private void Awake()
        {
            _inputPlayer = new InputPlayer();

        }

    }
}
