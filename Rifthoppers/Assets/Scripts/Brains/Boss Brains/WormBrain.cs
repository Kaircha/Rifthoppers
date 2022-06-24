using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBrain : Brain {
  public override Entity Entity => Segments[0];
  public List<WormSegment> Segments = new();
  public Transform Target;
  [HideInInspector] public float Angle;

  public IEnumerator Start() {
    while (true) {
      Machine.ChangeState(new WormIdleState(this, 1, 5f));
      yield return new WaitForSeconds(7f);
      Machine.ChangeState(new WormChargeState(this));
      yield return new WaitForSeconds(1f);
      Machine.ChangeState(new WormIdleState(this, 1, 5f));
      yield return new WaitForSeconds(2f);
      Machine.ChangeState(new WormLaserState(this, 1, 8f));
      yield return new WaitForSeconds(8f);
      Machine.ChangeState(new WormIdleState(this, 1, 5f));
      yield return new WaitForSeconds(2f);
      Machine.ChangeState(new WormChargeState(this));
      yield return new WaitForSeconds(1f);
    }
  }
}