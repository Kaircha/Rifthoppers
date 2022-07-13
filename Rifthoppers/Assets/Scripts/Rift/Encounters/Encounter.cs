using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;

public abstract class Encounter : State {
  public Area Area;
  public bool IsStarted;
  public abstract bool IsFinished { get; }
  public abstract float Progress { get; }

  public ColorAdjustments ColorAdjustments;
  public Vignette Vignette;
  public AudioMixerGroup AudioMixerGroup;

  public override void Enter() {
    LobbyManager.Instance.Players.ForEach(player => player.Brain.EnterCombatState());
    RiftManager.Instance.Energy.Heal();
    RiftManager.Instance.Energy.CanTakeDamage = true;
    RiftManager.Instance.EncounterStarted();
    Area.Show();


    if (GameManager.Instance.PostProcessingVolume.profile.TryGet(out ColorAdjustments colorAdjustments)) ColorAdjustments = colorAdjustments;
    if (GameManager.Instance.PostProcessingVolume.profile.TryGet(out Vignette vignette)) Vignette = vignette;
    AudioMixerGroup = AudioManager.Instance.AudioMixerGroup;
    // RiftManager.Instance.RiftWaveUIMaterial.color = Color.white; // Animate this?
  }
  public override IEnumerator Execute() { yield return null; }
  public override void Exit() {
    RiftManager.Instance.Energy.Heal();
    RiftManager.Instance.Energy.CanTakeDamage = false;
    RiftManager.Instance.EncounterEnded();
    Area.Hide();
    // RiftManager.Instance.RiftWaveUIMaterial.color = Color.clear; // Animate this?
  }
}