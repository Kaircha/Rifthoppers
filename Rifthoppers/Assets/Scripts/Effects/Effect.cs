using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect {
  public Entity Entity;
  public Coroutine Coroutine;
  public abstract void Add(Effect effect);
  public abstract IEnumerator EffectRoutine();
}