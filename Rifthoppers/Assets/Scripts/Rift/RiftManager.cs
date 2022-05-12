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
[RequireComponent(typeof(Energy))]
public class RiftManager : Singleton<RiftManager> {

  [HideInInspector] public RiftLoader RiftLoader;
  [HideInInspector] public RiftScaler RiftScaler;
  [HideInInspector] public RiftSpawner RiftSpawner;
  [HideInInspector] public Energy Energy;

  public Experience Experience;
  public GameObject Checkpoint;

  [Header("Dynamic Effects")]
  public Material RiftWaveUIMaterial;
  public AudioMixerGroup AudioMixerGroup;
  public Volume PostProcessingVolume => GameManager.Instance.PostProcessingVolume;
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
  public void WaveStarted() => OnWaveStarted?.Invoke();
  public event Action OnWaveEnded;
  public void WaveEnded() => OnWaveEnded?.Invoke();


  public override void Awake() {
    base.Awake();

    RiftLoader = GetComponent<RiftLoader>();
    RiftScaler = GetComponent<RiftScaler>();
    RiftSpawner = GetComponent<RiftSpawner>();
    Energy = GetComponent<Energy>();
    Energy.Maximum = 100f;
    Energy.Heal();
  }

  private void Start() {
    if (PostProcessingVolume.profile.TryGet(out ColorAdjustments colorAdjustments)) ColorAdjustments = colorAdjustments;
    if (PostProcessingVolume.profile.TryGet(out Vignette vignette)) Vignette = vignette;
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
  public void NextWave() => GameManager.Instance.UpgradeToWave();
  [ContextMenu("Victory")]
  public void Victory() => GameManager.Instance.WaveToUpgrade();
  [ContextMenu("Defeat")]
  public void Defeat(Entity entity) => StartCoroutine(GameManager.Instance.WaveToLab());
}