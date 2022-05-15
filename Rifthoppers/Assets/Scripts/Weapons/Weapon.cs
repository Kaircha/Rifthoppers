using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject {
  [HideInInspector] public Entity Entity;
  [HideInInspector] public Barrel Barrel;
  public AudioClip ShootSFX;
  public AudioClip ShootingStartedSFX;
  public AudioClip ShootingUpdatedSFX;
  public AudioClip ShootingStoppedSFX;

  public abstract void Shoot();
  public abstract void ShootingStarted();
  public abstract void ShootingUpdated();
  public abstract void ShootingStopped();
}

[System.Flags]
public enum WeaponType {
  Default = 0,
  Charged = 1,
  Remote = 2,
}