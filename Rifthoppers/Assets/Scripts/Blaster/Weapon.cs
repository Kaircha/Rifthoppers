using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject {
  public AudioClip ShootSFX;
  public AudioClip ShootingStartedSFX;
  public AudioClip ShootingUpdatedSFX;
  public AudioClip ShootingStoppedSFX;

  public abstract void ShootingStarted(Entity entity, AmmoStats ammo, Barrel barrel);
  public abstract void ShootingUpdated(Entity entity, AmmoStats ammo, Barrel barrel);
  public abstract void ShootingStopped(Entity entity, AmmoStats ammo, Barrel barrel);
}

[System.Flags]
public enum WeaponType {
  Default = 0,
  Charged = 1,
  Laser = 2,
}