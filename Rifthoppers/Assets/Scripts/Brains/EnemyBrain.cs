using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBrain : Brain, IPoolable {
  [HideInInspector] public EnemyStats Stats;
  [HideInInspector] public Transform Target;
  public Pool Pool { get; set; }
  public void Spawn() => OnSpawn?.Invoke();
  public event Action OnSpawn;

  public abstract State EntryState { get; }
  public List<Transition> Transitions = new();

  public void Initialize() {
    InitializeEntity();
    InitializeTransitions();
    Spawn();
  }
  public virtual void InitializeEntity() {
    (Entity.Health as Health).Maximum = Stats.Health;
    Entity.Health.Revive();
    Entity.RemoveEffects();
    GetComponentInChildren<Damager>(true).Initialize(Stats.Damage, false, Entity);
  }
  public abstract void InitializeTransitions();

  public override void Awake() {
    base.Awake();
    Stats = GetComponent<EnemyStats>();
  }

  public void Update() {
    if (Target == null) return;
    Entity.Sprite.flipX = transform.position.x > Target.position.x;
    Entity.Animator.SetFloat("Direction", (Target.position - transform.position).y > 0.6f ? 1 : -1);
  }

  private void OnEnable() {
    Initialize();
    // When Disabled and Re-Enabled, the Brain will re-enter into its EntryState
    Machine.ChangeState(EntryState);
    foreach (Transition transition in Transitions) {
      transition.Condition.Initialize();
    }
    StartCoroutine(TransitionRoutine());
    Entity.Health.OnDeath += OnDeath;
  }

  private IEnumerator TransitionRoutine() {
    yield return null; // yield point
    while (true) {
      foreach (Transition transition in Transitions) {
        if (transition.To == null || transition.To == Machine.State || !transition.To.CanEnter) continue;
        if (!transition.Condition.Satisfied) continue;
        if (transition.From == null || (transition.From == Machine.State && transition.From.CanExit)) {
          Machine.ChangeState(transition.To);
          transition.Condition.Reset();
          break;
        }
      }
      //yield return new WaitForSeconds(1f);
      yield return null;
    }
  }

  private void OnDisable() {
    foreach (Transition transition in Transitions) {
      transition.Condition.Terminate();
    }
    StopAllCoroutines();
    Entity.Health.OnDeath -= OnDeath;
  }

  private void OnDeath(Entity entity) {
    int nr = UnityEngine.Random.Range(Stats.MinOrbs, Stats.MaxOrbs + 1);
    while (nr-- > 0) {
      GameObject orblet = PoolManager.Instance.EnergyOrblets.Objects.Get();
      orblet.transform.position = transform.position;
      orblet.GetComponent<Rigidbody2D>().AddForce(5f * UnityEngine.Random.insideUnitCircle, ForceMode2D.Impulse);
    }
  }
}