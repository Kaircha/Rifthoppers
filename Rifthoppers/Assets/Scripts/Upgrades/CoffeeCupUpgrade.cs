using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Coffee Cup", menuName = "Upgrades/Coffee Cup")]
public class CoffeeCupUpgrade : Upgrade {
  public override void Add() => Brain.PlayerStats.Firerate += 3;
  public override IEnumerator UpgradeRoutine() { yield return null; }
  public override void Remove() => Brain.PlayerStats.Firerate -= 3;

}