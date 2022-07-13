using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave {
  public Encounter Encounter;
  public Reward Reward;

  public Wave(Encounter encounter, Reward reward) {
    Encounter = encounter;
    Reward = reward;
  }
}