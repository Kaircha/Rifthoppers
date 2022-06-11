using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(IHealth))]
public class Entity : MonoBehaviour {
  [HideInInspector] public Rigidbody2D Rigidbody;
  [HideInInspector] public Animator Animator;
  [HideInInspector] public IHealth Health;
  [HideInInspector] public Stats Stats;
  [HideInInspector] public ApplyUpgradeVFX UpgradeVFX;
  [HideInInspector] public InputDevice Device;
  [HideInInspector] public InputData Input;
  [HideInInspector] public Vector2 Direction;
  [HideInInspector] public bool IsMoving;
  public SpriteRenderer Sprite;
  public AudioSource HurtAudio;
  public Transform Target;
  public float Speed;
  public float AISpeed;
  public bool IsFlying;
  public List<Upgrade> Upgrades = new();
  public List<Effect> Effects = new();
  public List<Orbital> Orbitals = new();
  public Blaster Blaster;

  // Dealer, Receiver, Amount, isDoT
  public event Action<Entity, Entity, float, bool> OnDamageDealt;
  public void HasDealtDamage(Entity receiver, float amount, bool isDoT) => OnDamageDealt?.Invoke(this, receiver, amount, isDoT);
  public event Action OnAttacked;
  public void HasAttacked() => OnAttacked?.Invoke();
  public event Action<Entity, Surface> OnSurfaceWalked;
  public void SurfaceWalked(Entity entity, Surface surface) => OnSurfaceWalked?.Invoke(this, surface);

  public virtual void Awake() {
    Rigidbody = GetComponent<Rigidbody2D>();
    Animator = GetComponent<Animator>();
    Health = GetComponent<IHealth>();
    Stats = GetComponent<Stats>();
    UpgradeVFX = GetComponent<ApplyUpgradeVFX>();
    Blaster = GetComponentInChildren<Blaster>(true);
  }

  private void OnEnable() {
    Health.OnDeath += OnDeath;
    Health.OnDamageTaken += OnDamageTaken;
  }

  private void OnDisable() {
    Health.OnDeath -= OnDeath;
  }

  private void Update() {
    Looking();
    Orbiting();
  }

  private void FixedUpdate() => Moving();

  public virtual void Moving() {
    Rigidbody.AddForce(Speed * Direction, ForceMode2D.Impulse);
    IsMoving = Direction.magnitude > 0;
  }

  public void Orbiting() {
    foreach (Orbital orbital in Orbitals) {
      orbital.transform.RotateAround(transform.position, new Vector3(0, 0, 1), 60f * Time.deltaTime);
      orbital.transform.rotation = Quaternion.identity;
    }
  }

  public virtual void Looking() {
    if (Target != null) {
      Sprite.flipX = transform.position.x > Target.position.x;
      Animator.SetFloat("Direction", (Target.position - transform.position).y > 0.6f ? 1 : -1);
    }
    Animator.SetBool("IsMoving", IsMoving);
  }


  private Coroutine HurtRoutine;
  public virtual void OnDamageTaken(Entity dealer, Entity target, float amount, bool isDoT) {
    if (isDoT) return;
    if (HurtRoutine != null) StopCoroutine(HurtRoutine);
    HurtRoutine = StartCoroutine(OnDamageTakenRoutine());
  }
  public IEnumerator OnDamageTakenRoutine() {
    if (HurtAudio != null && HurtAudio.clip != null) HurtAudio.Play();
    Sprite.material.SetInt("_IsHurt", 1);
    yield return new WaitForSeconds(0.2f);
    Sprite.material.SetInt("_IsHurt", 0);
  }

  public virtual void OnDeath(Entity entity) { }

  #region Upgrades
  public void AddUpgrade(Upgrade upgrade) {
    //DataManager.Instance.Set($"{ID}TimesObtained", DataManager.Instance.Get<int>($"{ID}TimesObtained") + 1);
    upgrade.Entity = this;
    upgrade.Add();
    upgrade.Coroutine = StartCoroutine(upgrade.UpgradeRoutine());
    Upgrades.Add(upgrade);
  }

  public void RemoveUpgrades() {
    foreach (Upgrade upgrade in Upgrades) {
      upgrade.Remove();
      StopCoroutine(upgrade.Coroutine);
    }
    Upgrades = new();
  }
  #endregion

  #region Effects
  public void AddEffect(Effect effect) {
    foreach (Effect e in Effects) {
      if (e.GetType() == effect.GetType()) {
        e.Add(effect);
        return;
      }
    }

    effect.Entity = this;
    effect.Coroutine = StartCoroutine(effect.EffectRoutine());
    Effects.Add(effect);
  }

  public void RemoveEffect(Effect effect) {
    if (Effects.Contains(effect)) {
      Effects.Remove(effect);
      StopCoroutine(effect.Coroutine);
    }
  }

  public void RemoveEffects() {
    foreach (Effect effect in Effects) {
      StopCoroutine(effect.Coroutine);
    }
    Effects = new();
  }
  #endregion

  #region Orbitals
  public void AddOrbital(Orbital orbital) {
    Orbitals.Add(orbital);
    StartCoroutine(OrderOrbitalsRoutine());
  }

  public void RemoveOrbital(Orbital orbital) {
    if (Orbitals.Contains(orbital)) {
      Orbitals.Remove(orbital);
      (orbital as IPoolable).Release(orbital.gameObject);
      StartCoroutine(OrderOrbitalsRoutine());
    }
  }

  public void RemoveOrbitals() {
    foreach (Orbital orbital in Orbitals) {
      (orbital as IPoolable).Release(orbital.gameObject);
    }
    Orbitals = new();
  }

  public IEnumerator OrderOrbitalsRoutine() {
    float timer = 0f;
    List<Vector3> origins = Orbitals.Select(x => x.transform.localPosition).ToList();
    List<Vector3> targets = new();
    for (int i = 0; i < Orbitals.Count; i++) {
      float angle = i * 360f / Orbitals.Count;
      targets.Add(4f * new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)));
    }

    while (timer < 1) {
      for (int i = 0; i < Orbitals.Count; i++) Orbitals[i].transform.localPosition = Vector3.Lerp(origins[i], targets[i], timer);
      timer += Time.deltaTime;
      yield return null;
    }
  }
  #endregion
}