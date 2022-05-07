using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Power : MonoBehaviour {
  public Entity Entity;
  public float PressCost;
  public float HoldCost;
  public float ReleaseCost;
  public float CostMulti => Entity.Stats.PowerCostMulti;
  public float Range => Entity.Stats.PowerRange;
  public float Strength => Entity.Stats.PowerStrength;

  public event Action OnPowerUsed;

  public abstract void Press();
  public abstract void Hold();
  public abstract void Release();

  public IEnumerator PowerRoutine() {
    while (true) {
      yield return new WaitUntil(() => Entity.Input.Power);

      Press();
      RiftManager.Instance.Energy.Hurt(Entity, null, PressCost * CostMulti, false);

      while (Entity.Input.Power) {
        Hold();
        RiftManager.Instance.Energy.Hurt(Entity, null, HoldCost * CostMulti * Time.deltaTime, true);
        yield return null;
      }

      Release();
      RiftManager.Instance.Energy.Hurt(Entity, null, ReleaseCost * CostMulti, false);

      OnPowerUsed?.Invoke();
    }
  }
}