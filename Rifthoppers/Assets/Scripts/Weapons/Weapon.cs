using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject {
  [HideInInspector] public PlayerBrain Brain; // This shouldn't be here!
  [HideInInspector] public Barrel Barrel; // This shouldn't be here!
  public AudioClip ShootSFX;
  public AudioClip ShootingStartedSFX;
  public AudioClip ShootingUpdatedSFX;
  public AudioClip ShootingStoppedSFX;

  public abstract void ShootingStarted();
  public abstract void ShootingUpdated();
  public abstract void ShootingStopped();
}

[System.Flags]
public enum WeaponType {
  Default = 0,
  Charged = 1,
  Laser = 2,
}