using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
using System;

[RequireComponent(typeof(Energy))]
public class RiftManager : Singleton<RiftManager> {
  [HideInInspector] public Energy Energy;
  public RiftGenerator RiftGenerator;
  public RiftSpawner RiftSpawner;
  public GameObject WormBoss; // Temporary until this has a better place to live

  [HideInInspector] public List<Wave> Rift;
  [HideInInspector] public int Index;
  public Encounter Encounter => Rift[Index].Encounter;
  public Coroutine EncounterRoutine;
  public Reward Reward => Rift[Index].Reward;
  public Coroutine RewardRoutine;

  public Material RiftWaveUIMaterial;
  public EdgeCollider2D RiftEdge;
  public SpriteMask RiftMask;
  [Range(0.5f, 3f)] public float SpeedMultiplier = 1;

  public float EnergyMultiplier => Mathf.Clamp(4 * Energy.Percentage, 0.1f, 1f);

  #region Events
  public void EnergyOrbSpawned() => OnEnergyOrbSpawned?.Invoke();
  public event Action OnEnergyOrbSpawned;
  public void EnergyCollected(float amount) => OnEnergyCollected?.Invoke(amount);
  public event Action<float> OnEnergyCollected;
  public void ExperienceCollected(float amount) => OnExperienceCollected?.Invoke(amount);
  public event Action<float> OnExperienceCollected;
  public void EncounterStarted() => OnEncounterStarted?.Invoke();
  public event Action OnEncounterStarted;
  public void EncounterEnded() => OnEncounterEnded?.Invoke();
  public event Action OnEncounterEnded;
  public void RewardStarted() => OnRewardStarted?.Invoke();
  public event Action OnRewardStarted;
  public void RewardEnded() => OnRewardEnded?.Invoke();
  public event Action OnRewardEnded;
  #endregion

  public override void Awake() {
    base.Awake();

    Energy = GetComponent<Energy>();
    Energy.Maximum = 100f;
    Energy.Heal();

    Rift = RiftGenerator.GenerateRift();
  }

  private void OnEnable() => Energy.OnDeath += Defeat;
  private void OnDisable() => Energy.OnDeath -= Defeat;

  public void StartEncounter() {
    Encounter.Area.Show();
    Encounter.EncounterStart();
    RiftSpawner.StartSpawning();
    EncounterStarted();
    EncounterRoutine = StartCoroutine(Encounter.EncounterRoutine());
  }
  public void EndEncounter() {
    if (EncounterRoutine != null) StopCoroutine(EncounterRoutine);
    Encounter.Area.Hide();
    Encounter.EncounterEnd();
    RiftSpawner.StopSpawning();
    EncounterEnded();
  }
  public void StartReward() {
    RewardRoutine = StartCoroutine(Reward.RewardRoutine());
    Reward.Area.Show();
    Reward.RewardStart();
    RewardStarted();
  }
  public void EndReward() {
    if (RewardRoutine != null) StopCoroutine(RewardRoutine);
    Reward.Area.Hide();
    Reward.RewardEnd();
    RewardEnded();
  }

  public void Restart() => GameManager.Instance.RiftRestart();
  public void Defeat(Entity entity) => GameManager.Instance.RiftDefeat();
}