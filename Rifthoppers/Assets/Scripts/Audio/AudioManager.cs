using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEditor;

/// <summary>
/// The AudioManager requires both Singleton and DataManager scripts to function. <br></br> <br></br>
/// 
/// Additionally, the project must be set up with an AudioMixer, containing a Master, Music and SFX Controller. <br></br>
/// Each of which with their volume parameter exposed as MasterVolume, MusicVolume and SFXVolume respectively. <br></br> <br></br>
/// 
/// The volume parameter is in Decibel values, calculated as 20 * Log10(Percent), with Percent being a range of 0.0001f to 1f. <br></br>
/// This range doesn't start at 0 as to not break the Log10 calculation. Optionally, adjust the Sliders' minimum values to 0.0001f.
/// </summary>
public class AudioManager : Singleton<AudioManager> {
  public AudioMixerGroup AudioMixerGroup;
  public AudioSource Active;
  public AudioSource Inactive;
  [Range(0.1f, 2f)] public float CrossfadeDuration = 0.5f;
  public List<SceneMusic> SceneMusic;

  // Contains all audio channels in the AudioMixerGroup
  public static readonly string[] Channels = { "MasterVolume", "MusicVolume", "SFXVolume" };

  private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
  private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;
  private void OnSceneLoaded(Scene scene, LoadSceneMode mode) => PlaySceneMusic(scene.name);
  private void PlaySceneMusic(string sceneName) {
    SceneMusic music = SceneMusic.FirstOrDefault(x => x.scene == sceneName);
    if (music != null) PlayMusic(music);
  }

  private void Start() {
    foreach (string channel in Channels) {
      //SetVolume(channel, DataManager.Instance.Has(channel) ? DataManager.Instance.Get<float>(channel) : 0.5f);
      SetVolume(channel, 0.5f);
    }
  }

  public void MasterVolume(float value) => SetVolume("MasterVolume", value);
  public void MusicVolume(float value) => SetVolume("MusicVolume", value);
  public void SFXVolume(float value) => SetVolume("SFXVolume", value);
  public void SetVolume(string channel = "MasterVolume", float value = 1f) {
    value = Mathf.Clamp(value, 0.0001f, 1f);
    //DataManager.Instance.Set(channel, value);
    AudioMixerGroup.audioMixer.SetFloat(channel, Mathf.Log10(value) * 20);
  }

  public void PlayMusic(SceneMusic sceneMusic) => PlayMusic(sceneMusic.start, sceneMusic.loop);
  public void PlayMusic(AudioClip start, AudioClip loop) {
    if (loop == null) return;
    Inactive.clip = loop;
    if (start == null) Inactive.Play();
    else {
      Inactive.PlayOneShot(start);
      Inactive.PlayScheduled(AudioSettings.dspTime + start.length);
    }
    Crossfade();
  }

  private void Crossfade() {
    Fadeout(Active);
    Fadein(Inactive);
    (Active, Inactive) = (Inactive, Active);
  }

  private void Fadein(AudioSource audio) => StartCoroutine(FadeinRoutine(audio));
  private void Fadeout(AudioSource audio) => StartCoroutine(FadeoutRoutine(audio));

  private IEnumerator FadeinRoutine(AudioSource audio) {
    if (CrossfadeDuration <= 0f) CrossfadeDuration = 0.1f;
    while (audio.volume < 1f) {
      audio.volume += Time.deltaTime / CrossfadeDuration;
      yield return null;
    }
  }

  private IEnumerator FadeoutRoutine(AudioSource audio) {
    if (CrossfadeDuration <= 0f) CrossfadeDuration = 0.1f;
    while (audio.volume > 0f) {
      audio.volume -= Time.deltaTime / CrossfadeDuration;
      yield return null;
    }
    audio.Stop();
    audio.clip = null;
  }
}

[System.Serializable]
public class SceneMusic {
  public AudioClip start;
  public AudioClip loop;
  public string scene;
}