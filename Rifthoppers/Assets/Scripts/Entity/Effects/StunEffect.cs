using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEffect : Effect {
  public float Duration;

  public StunEffect(float duration) {
    Duration = duration;
  }

  // Cannot stun an Entity who is already stunned?
  public override void Add(Effect effect) { }

  public override IEnumerator EffectRoutine() {
    while (Duration > 0) {
      Duration -= Time.deltaTime;
      yield return null;
    }

    Entity.RemoveEffect(this);
  }
}