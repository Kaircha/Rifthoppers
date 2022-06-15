using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Coffee Cup", menuName = "Upgrades/Coffee Cup")]
public class CoffeeCupUpgrade : Upgrade {
  public override void Add() => Brain.Stats.Firerate += 3;
  public override IEnumerator UpgradeRoutine() { yield return null; }
  public override void Remove() => Brain.Stats.Firerate -= 3;

}