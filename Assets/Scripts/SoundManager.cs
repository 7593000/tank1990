using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Tank1990
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] _sound;
        private float _value;
        private AudioSource[] _sources;
        private Dictionary<AudioSource, Sound> _isPlayerSound = new();
        // »спользуем словарь дл€ хранени€ списка индексов дл€ каждого типа звука и каждого источника звука
        private Dictionary<Sound, int> _soundSources = new();
       
        private void Awake()
        {
            
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _sources = GetComponents<AudioSource>();

            _value = SettingsData.LoadData(TypeSave.Sound);
            //√руппировка звуков по источникам проигрывани€ AudioSource
            _soundSources[Sound.move] = 0;
            _soundSources[Sound.stand] = 0;
            _soundSources[Sound.shoot] = 1;
            _soundSources[Sound.explosion] = 2;
            _soundSources[Sound.startGame] = 2;
            _soundSources[Sound.hittingBrick] = 2;
            _soundSources[Sound.hittingZoneGame] = 2;


            _isPlayerSound = _sources.ToDictionary(source => source, source => Sound.none);

        }

        public void PlaySound(Sound clip, bool loop = false)
        {

            if (_isPlayerSound[_sources[_soundSources[clip]]] != clip)
            {
                StopPlaySound(clip);

            }
            if (!_sources[_soundSources[clip]].isPlaying)

            {
                _isPlayerSound[_sources[_soundSources[clip]]] = clip;
                _sources[_soundSources[clip]].volume = _value;
                _sources[_soundSources[clip]].loop = loop;
                _sources[_soundSources[clip]].clip = _sound[(int)clip];
                _sources[_soundSources[clip]].Play();
            }




        }

        public void StopPlaySound(Sound clip)
        {
            _isPlayerSound[_sources[_soundSources[clip]]] = Sound.none;
            _sources[_soundSources[clip]].Stop();


        }

    }
}
