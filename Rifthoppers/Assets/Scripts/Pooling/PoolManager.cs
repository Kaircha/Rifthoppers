using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : Singleton<PoolManager> {
  public Pool SoundEffects;
  public Pool Orbitals;
  public Pool Bullets;
  public Pool BulletImpacts;
  public Pool Squids;
  public Pool Squidmothers;
  public Pool Squidlings;
  public Pool Moths;
  public Pool ExplosiveSquids;
  public Pool EnergyOrbs;
  public Pool EnergyOrblets;
}