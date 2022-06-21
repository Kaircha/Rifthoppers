using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject {
  public AudioClip ShootSFX;
  public AudioClip ShootingStartedSFX;
  public AudioClip ShootingUpdatedSFX;
  public AudioClip ShootingStoppedSFX;

  public abstract void ShootingStarted(PlayerBrain brain, Barrel barrel);
  public abstract void ShootingUpdated(PlayerBrain brain, Barrel barrel);
  public abstract void ShootingStopped(PlayerBrain brain, Barrel barrel);
}

[System.Flags]
public enum WeaponType {
  Default = 0,
  Charged = 1,
  Laser = 2,
}