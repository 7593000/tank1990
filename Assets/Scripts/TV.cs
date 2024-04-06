using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
///��������� : ��������� 
/// </summary>
namespace Tank1990
{
    public class TV : MonoBehaviour
    {
        [Header("��������� ���������� ������")]
        [SerializeField]
        private Canvas _loaderCanvas;
        [SerializeField]
        private TMP_Text _StageText;

        public int StageNumber { get; set; } = 1;
        private SoundManager _sound;
        private void Awake()
        {
            // ��������� ������� ��� ��������������� ��� �������� ����� �����
            DontDestroyOnLoad(gameObject);
            _sound =FindFirstObjectByType<SoundManager>();
        }

        //todo => TEMP
        private void OnEnable()
        {
            SceneLoader.OnLoadScene += b =>  LoaderStage(b);
        }
        public void LoaderStage(  bool status)
        {
            if (status)
            {
                _StageText.text = $"STAGE {StageNumber}";
                _loaderCanvas.GetComponent<Canvas>().enabled = true;
            }
            else
            {
                OffLoaderStage();
            }
           
        }



        private void OffLoaderStage()
        {
            SceneLoader.OnLoadScene -= b => LoaderStage(b);
            StartCoroutine(CloseLoaderPanel());
        }

        private IEnumerator CloseLoaderPanel()
        {
            _sound.PlaySound(Sound.startGame);
            yield return new WaitForSeconds(2f);
            _loaderCanvas.GetComponent<Canvas>().enabled = false;
        }




    }
}
