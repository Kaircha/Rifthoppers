using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Energy Amplifier", menuName = "Upgrades/Energy Amplifier")]
public class EnergyAmplifierUpgrade : Upgrade {
  public override void Add() => Brain.AmmoStats.AmmoBaseDamage += 4;
  public override IEnumerator UpgradeRoutine() { yield return null; }
  public override void Remove() => Brain.AmmoStats.AmmoBaseDamage -= 4;
}