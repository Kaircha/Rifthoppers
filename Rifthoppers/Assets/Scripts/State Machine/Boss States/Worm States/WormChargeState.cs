using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormChargeState : State {
  public WormBrain Brain;

  public WormChargeState(WormBrain brain) {
    Brain = brain;
  }

  public override void Enter() {
    // Activate Head Damage?
  }

  public override IEnumerator Execute() {
    Vector3 A = Brain.Target.position;
    float angleB = (Brain.Angle + Mathf.PI - 0.5f) % (2f * Mathf.PI);
    float angleC = (Brain.Angle + Mathf.PI + 0.5f) % (2f * Mathf.PI);
    Vector3 B = 25f * new Vector2(Mathf.Sin(angleB), Mathf.Cos(angleB));
    Vector3 C = 25f * new Vector2(Mathf.Sin(angleC), Mathf.Cos(angleC));
    Brain.Angle = angleC;

    float timer = 0f;
    while (timer < 1f) {
      Brain.Target.position = Vector3.Lerp(A, Vector3.Slerp(B, C, timer), timer);
      timer += Time.deltaTime;
      yield return null;
    }
  }

  public override void Exit() {
    // Deactivate Head Damage?
  }
}