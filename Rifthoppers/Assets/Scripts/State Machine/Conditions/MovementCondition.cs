using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCondition : Condition {
  public Entity Entity;
  public float Duration;
  public bool MoveOrIdle; // True => Move; False => Idle
  public bool AboveOrBelow; // True => Above Duration; False => Below Duration
  private float Timer;

  public MovementCondition(Entity entity, float duration, bool moveOrIdle, bool aboveOrBelow) {
    Entity = entity;
    Duration = Mathf.Max(0, duration);
    MoveOrIdle = moveOrIdle;
    AboveOrBelow = aboveOrBelow;
  }

  public override bool Satisfied => AboveOrBelow ? Timer > Duration : Timer < Duration;
  public override void Initialize() => Entity.StartCoroutine(MovementRoutine());
  public override void Reset() => Timer = 0;
  public override void Terminate() => Entity.StopCoroutine(MovementRoutine());

  private IEnumerator MovementRoutine() {
    while (true) {
      if ((MoveOrIdle && Entity.IsMoving) || (!MoveOrIdle && !Entity.IsMoving)) Timer += Time.deltaTime;
      yield return null;
    }
  }
}