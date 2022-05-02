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
  public List<Upgrade> Upgrades = new();

  [Header("Dynamic Effects")]
  public Material RiftWaveUIMaterial;
  public AudioMixerGroup AudioMixerGroup;
  public Volume PostProcessingVolume;
  private ColorAdjustments ColorAdjustments;
  private Vignette Vignette;

  public float DifficultyMultiplier => (10f + Experience.Level) / 10f;
  public float EnergySlowness => Mathf.Clamp01(0.1f + (4 * Energy.Percentage));

  public event Action<PlayerEntity> OnEnergyCollected;
  public void EnergyCollected(PlayerEntity entity) => OnEnergyCollected?.Invoke(entity);
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
    Energy.Static = StatManager.Instance.Get(StatType.EnergyTotal);
    Energy.Dynamic = StatManager.Instance.Get(StatType.EnergyTotal);
    Energy.Maximum = StatManager.Instance.Get(StatType.EnergyTotal);

    if (PostProcessingVolume.profile.TryGet(out ColorAdjustments colorAdjustments)) ColorAdjustments = colorAdjustments;
    if (PostProcessingVolume.profile.TryGet(out Vignette vignette)) Vignette = vignette;

    NextWave();
  }

  private void Update() {
    ColorAdjustments.contrast.Override(25 * (1 - EnergySlowness));
    ColorAdjustments.saturation.Override(-125 * (1 - EnergySlowness));
    Vignette.intensity.Override(0.3f + 0.5f * (1 - EnergySlowness));
    AudioMixerGroup.audioMixer.SetFloat("Lowpass", EnergySlowness * 5000f);
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