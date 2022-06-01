using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffect : MonoBehaviour, IPoolable {
  public AudioSource AudioSource;

  public Pool Pool { get; set; }

  public void Play(AudioClip clip, float volume = 1f, float pitch = 1f) {
    AudioSource.pitch = pitch;
    AudioSource.volume = volume;
    AudioSource.PlayOneShot(clip);
  }
}