using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Power : MonoBehaviour {
  public PlayerBrain Brain;
  public float PressCost;
  public float HoldCost;
  public float ReleaseCost;
  public float CostMulti => Brain.PlayerStats.PowerCostMulti;
  public float Range => Brain.PlayerStats.PowerRange;
  public float Strength => Brain.PlayerStats.PowerStrength;

  public event Action OnPowerUsed;

  public abstract void Press();
  public abstract void Hold();
  public abstract void Release();

  public IEnumerator PowerRoutine() {
    while (true) {
      yield return new WaitUntil(() => Brain.Input.Power);

      Press();
      RiftManager.Instance.Energy.Hurt(Brain.Entity, null, PressCost * CostMulti, false);

      while (Brain.Input.Power) {
        Hold();
        RiftManager.Instance.Energy.Hurt(Brain.Entity, null, HoldCost * CostMulti * Time.deltaTime, true);
        yield return null;
      }

      Release();
      RiftManager.Instance.Energy.Hurt(Brain.Entity, null, ReleaseCost * CostMulti, false);

      OnPowerUsed?.Invoke();
    }
  }
}