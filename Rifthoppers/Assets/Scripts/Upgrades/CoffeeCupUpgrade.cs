using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Coffee Cup", menuName = "Upgrades/Coffee Cup")]
public class CoffeeCupUpgrade : Upgrade {
  public override void Add() => Entity.Stats.PlayerFirerate += 3;
  public override IEnumerator UpgradeRoutine() { yield return null; }
  public override void Remove() => Entity.Stats.PlayerFirerate -= 3;

}