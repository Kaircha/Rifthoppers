using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Blaster Buddy", menuName = "Upgrades/Blaster Buddy")]
public class BlasterBuddyUpgrade : UpgradeObject {
  public GameObject Prefab;

  public override Upgrade Upgrade() => new BlasterBuddy(this, Prefab);

  public class BlasterBuddy : Upgrade {
    public Orbital Orbital;
    public SimpleBlaster SimpleBlaster;
    private readonly GameObject Prefab;

    public BlasterBuddy(UpgradeObject obj, GameObject prefab) {
      Object = obj;
      Prefab = prefab;
    }

    public override void Add() {
      GameObject blasterBuddy = Instantiate(Prefab, Brain.Orbitals.transform);
      SimpleBlaster = blasterBuddy.GetComponent<SimpleBlaster>();
      SimpleBlaster.Entity = Brain.Entity;
      SimpleBlaster.ShootingStarted(Brain.Entity, Brain.PlayerStats.AmmoStats, SimpleBlaster.Barrels[0]);
      Orbital = blasterBuddy.GetComponent<Orbital>();
      Brain.Orbitals.Add(Orbital);
    }

    public override IEnumerator UpgradeRoutine() {
      while (true) {
        yield return new WaitUntil(() => Orbital.DoBehavior);
        SimpleBlaster.ShootingUpdated(Brain.Entity, Brain.PlayerStats.AmmoStats, SimpleBlaster.Barrels[0]);
        yield return new WaitForSeconds(2f / Brain.PlayerStats.Firerate);
      }
    }

    public override void Remove() {
      SimpleBlaster.ShootingStopped(Brain.Entity, Brain.PlayerStats.AmmoStats, SimpleBlaster.Barrels[0]);
      Brain.Orbitals.Remove(Orbital);
    }
  }
}