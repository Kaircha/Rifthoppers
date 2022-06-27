using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave {
  public int Index;
  public Encounter Encounter;
  public Reward Reward;

  public Wave(int index, Encounter encounter, Reward reward) {
    Index = index;
    Encounter = encounter;
    Reward = reward;
  }
}