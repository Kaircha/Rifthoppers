using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormIdleState : State {
  public WormBrain Brain;
  public float Direction = 1f;
  public float Duration = 1f;
  public const float Radius = 25f;

  public WormIdleState(WormBrain brain, float direction, float duration) {
    Brain = brain;
    Direction = direction;
    Duration = Mathf.Max(0.01f, duration); // Prevent dividing by <= 0
  }

  public override void Enter() {
    // Get nearest point on the circle that the head is at
  }

  public override IEnumerator Execute() {
    if (Brain.Radius != Radius) yield return Machine.StartCoroutine(EnterRoutine());

    while (true) {
      Brain.Angle = (Brain.Angle + Time.deltaTime / Duration * Direction * 2 * Mathf.PI) % (2 * Mathf.PI);
      Brain.Target.position = 25f * new Vector2(Mathf.Sin(Brain.Angle), Mathf.Cos(Brain.Angle));
      yield return null;
    }
  }

  public override void Exit() {
    Brain.Radius = Radius;
  }

  public IEnumerator EnterRoutine() {
    float timer = 0f;
    while (timer < 1) {
      Brain.Angle = (Brain.Angle + Time.deltaTime / Duration * Direction * 2 * Mathf.PI) % (2 * Mathf.PI);
      Brain.Target.position = Mathf.Lerp(Brain.Radius, Radius, timer) * new Vector2(Mathf.Sin(Brain.Angle), Mathf.Cos(Brain.Angle));
      timer += Time.deltaTime;
      yield return null;
    }
  }
}