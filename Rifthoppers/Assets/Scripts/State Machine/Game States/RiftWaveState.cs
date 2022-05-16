using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;

public class RiftWaveState : State {
  public Transform CurrentArea;

  private ColorAdjustments ColorAdjustments;
  private Vignette Vignette;
  private AudioMixerGroup AudioMixerGroup;

  public override void Enter() {
    foreach (Player player in LobbyManager.Instance.Players) {
      player.Entity.EnterRiftState();
    }
    RiftManager.Instance.Energy.CanTakeDamage = true;
    RiftManager.Instance.Energy.Heal();
    RiftManager.Instance.Checkpoint.SetActive(true);
    RiftManager.Instance.RiftSpawner.StartSpawning();

    RiftManager.Instance.AreaLoader.Resize(20f);
    RiftManager.Instance.AreaLoader.LoadWave();
    // Animate this?
    RiftManager.Instance.RiftWaveUIMaterial.color = Color.white;
    RiftManager.Instance.WaveStarted();

    if (GameManager.Instance.PostProcessingVolume.profile.TryGet(out ColorAdjustments colorAdjustments)) ColorAdjustments = colorAdjustments;
    if (GameManager.Instance.PostProcessingVolume.profile.TryGet(out Vignette vignette)) Vignette = vignette;
    AudioMixerGroup = AudioManager.Instance.AudioMixerGroup;
  }

  public override IEnumerator Execute() {
    while (true) {
      RiftManager.Instance.Energy.Hurt(null, null, 5f * Time.deltaTime, true);

      Time.timeScale = RiftManager.Instance.EnergyMultiplier * RiftManager.Instance.SpeedMultiplier;

      ColorAdjustments.contrast.Override(25 * (1 - RiftManager.Instance.EnergyMultiplier));
      ColorAdjustments.saturation.Override(-125 * (1 - RiftManager.Instance.EnergyMultiplier));
      Vignette.intensity.Override(0.3f + 0.5f * (1 - RiftManager.Instance.EnergyMultiplier));
      AudioMixerGroup.audioMixer.SetFloat("Lowpass", RiftManager.Instance.EnergyMultiplier * 5000f);

      yield return null;
    }
  }

  public override void Exit() {
    // Sometimes never gets called!

    RiftManager.Instance.Energy.CanTakeDamage = false;
    RiftManager.Instance.Checkpoint.SetActive(false);
    RiftManager.Instance.RiftSpawner.StopSpawning();
    // Animate this?
    RiftManager.Instance.RiftWaveUIMaterial.color = Color.clear;
    RiftManager.Instance.WaveEnded();

    Time.timeScale = 1;

    ColorAdjustments.contrast.Override(0);
    ColorAdjustments.saturation.Override(0);
    Vignette.intensity.Override(0.3f);
    AudioMixerGroup.audioMixer.SetFloat("Lowpass", 5000f);
  }
}