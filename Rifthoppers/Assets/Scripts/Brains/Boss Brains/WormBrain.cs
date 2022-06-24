using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBrain : Brain {
  public override Entity Entity => Segments[0];
  public List<Entity> Segments = new();
  public Transform Target;
  public float Angle;

  public IEnumerator Start() {
    while (true) {
      Machine.ChangeState(new WormCycleState(this, 1, 5f));
      yield return new WaitForSeconds(5f);
      Machine.ChangeState(new WormLaserState(this, 1, 8f));
      yield return new WaitForSeconds(8f);
    }
  }
}