using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect {
  public Entity Entity;
  public abstract IEnumerator EffectRoutine();
  public abstract void Add(Effect effect);
}