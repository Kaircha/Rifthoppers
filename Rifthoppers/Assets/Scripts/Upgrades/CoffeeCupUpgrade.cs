using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Coffee Cup", menuName = "Upgrades/Coffee Cup")]
public class CoffeeCupUpgrade : UpgradeObject {
  public override Upgrade Upgrade() => new CoffeeCup(this);

  public class CoffeeCup : Upgrade {
    public CoffeeCup(UpgradeObject obj) {
      Object = obj;
    }

    public override void Add() => Brain.PlayerStats.Firerate += 1;
    public override IEnumerator UpgradeRoutine() { yield return null; }
    public override void Remove() => Brain.PlayerStats.Firerate -= 1;
  }
} 