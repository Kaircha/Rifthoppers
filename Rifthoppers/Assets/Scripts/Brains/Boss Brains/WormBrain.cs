using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBrain : Brain {
  public override Entity Entity => Segments[0];
  public List<WormSegment> Segments = new();
  public Transform Target;
  [HideInInspector] public float Angle;
  [HideInInspector] public float Radius;
  [HideInInspector] public AmmoStats Ammo;
  [HideInInspector] public Health Health;
  [HideInInspector] public CinemachineImpulseSource Impulse;
  public LineRenderer LaserRenderer;
  public EdgeCollider2D LaserCollider;

  public override void Awake() {
    base.Awake();
    Ammo = GetComponent<AmmoStats>();
    Health = GetComponent<Health>();
    Impulse = Segments[0].GetComponent<CinemachineImpulseSource>();
  }

  public IEnumerator Start() {
    Radius = 25f;
    while (!Health.IsDead) {
      yield return StateRoutine(new WormIdleState(this, 1, 8f), 5f);
      yield return StateRoutine(new WormNovaChargeState(this, 8));
    }
    StartCoroutine(StateRoutine(new WormDeathState(this)));
  }
  public IEnumerator StateRoutine(State state, float duration = 0) {
    if (duration <= state.ExecutionTime) duration = state.ExecutionTime;
    Machine.ChangeState(state);
    yield return new WaitForSeconds(duration);
  }

  public void UpdateLaser(Vector3 A, Vector3 B) {
    A -= transform.position;
    B -= transform.position;
    LaserRenderer.SetPosition(0, A);
    LaserRenderer.SetPosition(1, B);
    LaserCollider.SetPoints(new() { A, B });
  }

}