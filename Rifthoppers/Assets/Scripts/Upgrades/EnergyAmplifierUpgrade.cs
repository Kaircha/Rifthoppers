using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Energy Amplifier", menuName = "Upgrades/Energy Amplifier")]
public class EnergyAmplifierUpgrade : UpgradeObject {
  public override Upgrade Upgrade() => new EnergyAmplifier(this);

  public class EnergyAmplifier : Upgrade {
    public EnergyAmplifier(UpgradeObject obj) {
      Object = obj;
    }

    public override void Add() => Brain.PlayerStats.AmmoStats.AmmoBaseDamage += 4;
    public override IEnumerator UpgradeRoutine() { yield return null; }
    public override void Remove() => Brain.PlayerStats.AmmoStats.AmmoBaseDamage -= 4;
  }
}
