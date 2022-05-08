using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
using System;

[RequireComponent(typeof(RiftLoader))]
[RequireComponent(typeof(RiftScaler))]
[RequireComponent(typeof(RiftSpawner))]
[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(Energy))]
public class RiftManager : Singleton<RiftManager> {
  public override bool Persistent => false;

  [HideInInspector] public RiftLoader RiftLoader;
  [HideInInspector] public RiftScaler RiftScaler;
  [HideInInspector] public RiftSpawner RiftSpawner;
  [HideInInspector] public StateMachine Machine;
  [HideInInspector] public Energy Energy;

  public RiftWaveState RiftWaveState = new RiftWaveState();
  public RiftUpgradeState RiftUpgradeState = new RiftUpgradeState();
  public RiftDeadState RiftDeadState = new RiftDeadState();

  public Experience Experience;
  public GameObject Checkpoint;

  [Header("Dynamic Effects")]
  public Material RiftWaveUIMaterial;
  public AudioMixerGroup AudioMixerGroup;
  public Volume PostProcessingVolume;
  private ColorAdjustments ColorAdjustments;
  private Vignette Vignette;

  [Header("Experimenting")]
  [Range(0.5f, 3f)] public float SpeedMultiplier = 1;
  public bool DisableEnergyDamage = false;

  public float DifficultyMultiplier => (10f + Experience.Level) / 10f;
  public float EnergyMultiplier => Mathf.Clamp(4 * Energy.Percentage, 0.1f, 1f);

  public event Action OnEnergyCollected;
  public void EnergyCollected() => OnEnergyCollected?.Invoke();
  public event Action<PlayerEntity> OnKilled;
  public void HasKilled(PlayerEntity entity) => OnKilled?.Invoke(entity);
  public event Action OnWaveStarted;
  public event Action OnWaveEnded;


  public override void Awake() {
    base.Awake();

    RiftLoader = GetComponent<RiftLoader>();
    RiftScaler = GetComponent<RiftScaler>();
    RiftSpawner = GetComponent<RiftSpawner>();
    Machine = GetComponent<StateMachine>();
    Energy = GetComponent<Energy>();
  }

  private void Start() {
    // Up for change
    Energy.Static = 100f;
    Energy.Dynamic = 100f;
    Energy.Maximum = 100f;

    if (PostProcessingVolume.profile.TryGet(out ColorAdjustments colorAdjustments)) ColorAdjustments = colorAdjustments;
    if (PostProcessingVolume.profile.TryGet(out Vignette vignette)) Vignette = vignette;

    NextWave();
  }

  private void Update() {
    ColorAdjustments.contrast.Override(25 * (1 - EnergyMultiplier));
    ColorAdjustments.saturation.Override(-125 * (1 - EnergyMultiplier));
    Vignette.intensity.Override(0.3f + 0.5f * (1 - EnergyMultiplier));
    AudioMixerGroup.audioMixer.SetFloat("Lowpass", EnergyMultiplier * 5000f);

    if (DisableEnergyDamage) Energy.Heal(); // Ugly code just for lazy debugging
  }

  private void OnEnable() {
    Experience.OnLevelUp += Victory;
    Energy.OnDeath += Defeat;
  }
  private void OnDisable() {
    Experience.OnLevelUp -= Victory;
    Energy.OnDeath -= Defeat;
  }

  [ContextMenu("Next")]
  public void NextWave() {
    Machine.State = RiftWaveState;
    OnWaveStarted?.Invoke();
  }
  [ContextMenu("Victory")]
  public void Victory() {
    Machine.State = RiftUpgradeState;
    OnWaveEnded?.Invoke();
  }
  [ContextMenu("Defeat")]
  public void Defeat(Entity entity) {
    Machine.State = RiftDeadState;
    OnWaveEnded?.Invoke();
  }
}