using Ftech.Lib.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ftech.Lib.Common
{
    public class SoundManager<T> : SingletonBind<T> where T : MonoBehaviour
    {
        [Header("==== Audio Source ====")]
        [SerializeField] protected AudioSource backgroundMusic;
        [SerializeField] protected AudioSource soundEffect;

        protected List<AudioSource> loopAudios;

        protected bool backgroundPausing;
        protected int previousPriovity = int.MaxValue;

        public enum PlaySoundType { None, Override, Duplicate }

        public bool BackgroundMusicEnable
        {
            get { return PlayerPrefExtension.GetBool("BackgroundMusicEnable", true) && BackgroundVolume > 0; }
            set
            {
                bool current = PlayerPrefExtension.GetBool("BackgroundMusicEnable", true);
                if (current == value)
                    return;
                PlayerPrefExtension.SetBool("BackgroundMusicEnable", value);

                if (value)
                    PlayBackgroundMusic();
                else
                    PauseBackgroundMusic();
            }
        }

        public bool SoundEffectEnable
        {
            get { return PlayerPrefExtension.GetBool("SoundEffectEnable", true) && SoundEffectVolume > 0; }
            set { PlayerPrefExtension.SetBool("SoundEffectEnable", value); }
        }

        public float BackgroundVolume
        {
            get { return PlayerPrefs.GetFloat("BackgroundMusicVolume", 1); }
            set
            {
                float v = Mathf.Clamp01(value);
                PlayerPrefs.SetFloat("BackgroundMusicVolume", v);
                if (backgroundMusic)
                    backgroundMusic.volume = v;
            }
        }

        public float SoundEffectVolume
        {
            get { return PlayerPrefs.GetFloat("SoundEffectVolume", 1); }
            set
            {
                float v = Mathf.Clamp01(value);
                PlayerPrefs.SetFloat("SoundEffectVolume", v);
                if (soundEffect)
                    soundEffect.volume = v;
            }
        }

        #region Background Music
        public void PlayBackgroundMusic(bool resume = false, bool fadein = false, float fadeDuration = 1, bool loop = true, float volume = 1f)
        {
            if (backgroundMusic == null || !BackgroundMusicEnable || backgroundMusic.isPlaying)
                return;

            backgroundPausing = false;
            backgroundMusic.loop = loop;
            if (resume)
                backgroundMusic.UnPause();
            else
                backgroundMusic.Play();

            if (!fadein)
                backgroundMusic.volume = BackgroundVolume * volume;
            else
                FadeInBackgroundMusic(fadeDuration);
        }

        public void StopBackgroundMusic(bool fadeout = false, float fadeDuration = 1, Action onComplete = null)
        {
            if (backgroundMusic == null || !backgroundMusic.isPlaying)
            {
                onComplete?.Invoke();
                return;
            }

            if (!fadeout)
            {
                backgroundMusic.Stop();
                onComplete?.Invoke();
            }
            else
                FadeOutBackgroundMusic(fadeDuration, () =>
                {
                    backgroundMusic.Stop();
                    onComplete?.Invoke();
                });
        }

        public void PauseBackgroundMusic(bool fadeout = false, float fadeDuration = 1)
        {
            if (backgroundMusic == null || !backgroundMusic.isPlaying)
                return;

            backgroundPausing = true;
            if (!fadeout)
            {
                backgroundMusic.Pause();
            }
            else
                FadeOutBackgroundMusic(fadeDuration, () =>
                {
                    backgroundMusic.Pause();
                });
        }
        #endregion

        #region Other Loop audio
        public AudioSource Loop(AudioClip clip)
        {
            var source = (Instantiate(backgroundMusic.gameObject) as GameObject).GetComponent<AudioSource>();
            source.volume = BackgroundVolume;
            source.transform.SetParent(transform);
            source.clip = clip;
            source.Play();

            if (loopAudios != null)
                loopAudios.Add(source);
            else
            {
                loopAudios = new List<AudioSource>();
                loopAudios.Add(source);
            }

            return source;
        }

        public void StopLoop(AudioSource source)
        {
            if (source == null)
                return;
            source.Stop();
            loopAudios.Remove(source);
            Destroy(source.gameObject);
        }
        #endregion

        #region Volume Fade Effect
        private void FadeInBackgroundMusic(float duration = 1, System.Action callback = null)
        {
            FadeInSound(backgroundMusic, BackgroundVolume, duration, callback);
        }

        private void FadeOutBackgroundMusic(float duration = 1, System.Action callback = null)
        {
            FadeOutSound(backgroundMusic, duration, callback);
        }

        private void FadeInSound(AudioSource audio, float toVolume, float duration = 1, System.Action callback = null)
        {
            StartCoroutine(IEFadeSound(audio, 0, toVolume, duration, callback));
        }

        private void FadeOutSound(AudioSource audio, float duration = 1, System.Action callback = null)
        {
            StartCoroutine(IEFadeSound(audio, audio.volume, 0, duration, callback));
        }

        IEnumerator IEFadeSound(AudioSource audio, float froVolume, float toVolume, float duration = 1, System.Action callback = null)
        {
            if (audio == null)
            {
                Debug.LogWarning(string.Format("[SoundManager] Fade Sound Fall! Null audio {0}", audio.name));
                yield break;
            }
            float t = 0;
            while (t < duration)
            {
                t += Time.deltaTime;
                audio.volume = Mathf.Lerp(froVolume, toVolume, t / duration);
                yield return null;
            }
            audio.volume = toVolume;
            if (callback != null)
                callback.Invoke();
        }
        #endregion


        #region Sound

        public void PlaySoundEffect(AudioClip audio, PlaySoundType playSoundType = PlaySoundType.Override, float scaleVolume = 1f, int priovity = 0)
        {
            if (soundEffect == null || !SoundEffectEnable)
                return;
            if (soundEffect.isPlaying)
            {
                if (playSoundType == PlaySoundType.Override && priovity <= previousPriovity)
                {
                    previousPriovity = priovity;
                    soundEffect.Stop();
                }
                else if (playSoundType == PlaySoundType.Duplicate)
                {
                    var dup = soundEffect.Spawn();
                    if (dup)
                    {
                        AutoDestroy auto = dup.GetComponent<AutoDestroy>();
                        if (auto == null)
                        {
                            auto = dup.gameObject.AddComponent<AutoDestroy>();
                        }
                        auto.StartAutoDestroy(audio.length + 1f, AutoDestroy.DestroyType.Pool);
                        dup.PlayOneShot(audio, SoundEffectVolume * scaleVolume);
                    }
                    return;
                }
                else
                {
                    return;
                }
            }
            soundEffect.PlayOneShot(audio, SoundEffectVolume * scaleVolume);
        }

        #endregion
    }
}
