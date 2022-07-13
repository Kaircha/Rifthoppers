using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;

public class CollectionEncounter : Encounter {
  public override bool IsFinished { get; set; }
  public override float Progress => Mathf.Clamp01(Amount / Required);
  public float Amount = 0f;
  public float Required = 100f;
  public Coroutine OrbSpawnRoutine;

  public override void Enter() {
    base.Enter();
    RiftManager.Instance.RiftSpawner.StartSpawning();
    RiftManager.Instance.OnExperienceCollected += OnExperienceCollected;
    OrbSpawnRoutine = GameManager.Instance.StartCoroutine(EnergyOrbSpawnRoutine());
    IsStarted = true;
    IsFinished = false;
  }

  public override IEnumerator Execute() {
    float timerA = 0f;
    float speedA = Time.timeScale;
    while (timerA < 1) {
      Time.timeScale = Mathf.Lerp(speedA, 1f, timerA);
      timerA += Time.deltaTime;
      yield return null;
    }

    while (Progress < 1f) {
      RiftManager.Instance.Energy.Hurt(null, null, 5f * Time.deltaTime, true);

      Time.timeScale = RiftManager.Instance.EnergyMultiplier * RiftManager.Instance.SpeedMultiplier;
      ColorAdjustments.contrast.Override(25 * (1 - RiftManager.Instance.EnergyMultiplier));
      ColorAdjustments.saturation.Override(-125 * (1 - RiftManager.Instance.EnergyMultiplier));
      Vignette.intensity.Override(0.3f + 0.5f * (1 - RiftManager.Instance.EnergyMultiplier));
      AudioMixerGroup.audioMixer.SetFloat("Lowpass", RiftManager.Instance.EnergyMultiplier * 5000f);
      yield return null;
    }

    // Transition speed to 0.5f
    float timerB = 0f;
    float speedB = Time.timeScale;
    while (timerB < 1) {
      Time.timeScale = Mathf.Lerp(speedB, 0.5f, timerB);
      timerB += Time.deltaTime;
      yield return null;
    }

    // Confirm the end of the encounter
    IsFinished = true;
  }

  public override void Exit() {
    base.Exit();
    RiftManager.Instance.RiftSpawner.StopSpawning();
    RiftManager.Instance.OnExperienceCollected -= OnExperienceCollected;
    GameManager.Instance.StopCoroutine(OrbSpawnRoutine);

    Time.timeScale = 1;
    ColorAdjustments.contrast.Override(0);
    ColorAdjustments.saturation.Override(0);
    Vignette.intensity.Override(0.3f);
    AudioMixerGroup.audioMixer.SetFloat("Lowpass", 5000f);
  }

  private void OnExperienceCollected(float amount) => Amount += amount;

  private IEnumerator EnergyOrbSpawnRoutine() {
    while (true) {
      yield return new WaitUntil(() => PoolManager.Instance.EnergyOrbs.Objects.CountActive < 1);
      yield return new WaitForSeconds(2f);
      GameObject energyOrb = PoolManager.Instance.EnergyOrbs.Objects.Get();
      energyOrb.transform.position = 18f * Random.insideUnitCircle;
    }
  }
}