using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormLaserState : State {
  public WormBrain Brain;
  public float Direction = 1f;
  public float Duration = 1f;

  public WormLaserState(WormBrain brain, float direction, float duration) {
    Brain = brain;
    Direction = direction;
    Duration = Mathf.Max(0.01f, duration); // Prevent dividing by <= 0
  }

  public override void Enter() {
    // Get nearest point on the circle that the head is at
  }

  public override IEnumerator Execute() {
    while (true) {
      Brain.Angle = (Brain.Angle + Time.deltaTime / Duration * Direction * 2 * Mathf.PI) % (2 * Mathf.PI);
      Brain.Target.position = 19f * new Vector2(Mathf.Sin(Brain.Angle), Mathf.Cos(Brain.Angle));
      yield return null;
    }
  }

  public override void Exit() { }
}