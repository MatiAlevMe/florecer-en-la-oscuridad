using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance; // Singleton para fácil acceso

        public AudioSource musicSource; // Asigna la AudioSource para la música en el Inspector
        public AudioSource sfxSource;   // Asigna la AudioSource para los efectos de sonido en el Inspector

        public AudioClip backgroundMusic; // Asigna la música de fondo en el Inspector
        
        void Awake()
        {
            // Singleton pattern
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Mantener el AudioManager al cambiar de escena
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            // Reproducir la música de fondo al inicio
            PlayBackgroundMusic();
        }

        public void PlayBackgroundMusic()
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }

        public void PlaySFX(AudioClip clip)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
