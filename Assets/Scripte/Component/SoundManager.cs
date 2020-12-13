
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   public static float volume=0.5f;
   public static GameObject Holder;

   public static float MusicVolume = 0.5f;
   
   private static AudioSource ExplorationMisic;
   private static AudioSource TemporalExploratinMisic;
   private static AudioSource CombatMusic;
   
   public AudioClip StartMusic;
   

   private static float _tempoBase;
   private  static float _tempoPresent;

   private void Start()
   {
       Holder = gameObject;
       ExplorationMisic = Holder.AddComponent<AudioSource>();
       CombatMusic = Holder.AddComponent<AudioSource>();
       ExplorationMisic.clip = StartMusic;
       ExplorationMisic.volume = MusicVolume;
       ExplorationMisic.loop = true;
       ExplorationMisic.Play();
   }

   private void Update()
   {
       if (TemporalExploratinMisic != null)
       {
           _tempoPresent -= Time.deltaTime;

           ExplorationMisic.volume = MusicVolume * _tempoPresent / _tempoBase;
           TemporalExploratinMisic.volume =MusicVolume-(MusicVolume * _tempoPresent / _tempoBase);
           if (_tempoPresent <= 0)
           {
               Destroy(ExplorationMisic);
               ExplorationMisic = TemporalExploratinMisic;
               ExplorationMisic.volume = MusicVolume;
               TemporalExploratinMisic = null;
           }
       }
   }

   public static void PlaySound(AudioClip audioClip, float volume2)
   {
       AudioSource  audioSource = Holder.AddComponent<AudioSource>();
       audioSource.clip = audioClip;
       audioSource.volume = volume2 * volume;
       audioSource.Play();
       Destroy(audioSource,audioClip.length+1);
   }

   public static void PlayMusic(AudioClip audioClip, float timer)
   {
       if (TemporalExploratinMisic == null)
       {
           TemporalExploratinMisic = Holder.AddComponent<AudioSource>();
           TemporalExploratinMisic.clip = audioClip;
           TemporalExploratinMisic.loop = true;
           TemporalExploratinMisic.Play();
           _tempoBase = timer;
           _tempoPresent = timer;
       }
   }

   public static void StartCombatMusic(AudioClip audioClip)
   {
       ExplorationMisic.volume = 0;
       CombatMusic.clip = audioClip;
       CombatMusic.loop = true;
       CombatMusic.volume = MusicVolume;
       CombatMusic.Play();
   }

   public static void EndCombatMusic()
   {
       CombatMusic.volume = 0;
       ExplorationMisic.volume = MusicVolume;
   }
}
