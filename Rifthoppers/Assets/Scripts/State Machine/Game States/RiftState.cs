using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;

public class RiftState : State {
  private ColorAdjustments ColorAdjustments;
  private Vignette Vignette;
  private AudioMixerGroup AudioMixerGroup;

  public override void Enter() {
    if (GameManager.Instance.PostProcessingVolume.profile.TryGet(out ColorAdjustments colorAdjustments)) ColorAdjustments = colorAdjustments;
    if (GameManager.Instance.PostProcessingVolume.profile.TryGet(out Vignette vignette)) Vignette = vignette;
    AudioMixerGroup = AudioManager.Instance.AudioMixerGroup;
  }

  public override IEnumerator Execute() {
    while (true) {
      Time.timeScale = RiftManager.Instance.EnergyMultiplier * RiftManager.Instance.SpeedMultiplier;

      ColorAdjustments.contrast.Override(25 * (1 - RiftManager.Instance.EnergyMultiplier));
      ColorAdjustments.saturation.Override(-125 * (1 - RiftManager.Instance.EnergyMultiplier));
      Vignette.intensity.Override(0.3f + 0.5f * (1 - RiftManager.Instance.EnergyMultiplier));
      AudioMixerGroup.audioMixer.SetFloat("Lowpass", RiftManager.Instance.EnergyMultiplier * 5000f);

      yield return null;
    }
  }

  public override void Exit() {
    Time.timeScale = 1;

    ColorAdjustments.contrast.Override(0);
    ColorAdjustments.saturation.Override(0);
    Vignette.intensity.Override(0.3f);
    AudioMixerGroup.audioMixer.SetFloat("Lowpass", 5000f);

    foreach (Player player in LobbyManager.Instance.Players) {
      player.Entity.RemoveUpgrades();
      player.Entity.RemoveEffects();
    }
  }
}