using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave {
  public int Index;
  public Encounter Encounter;
  public Reward Reward;
  public Wave Parent;
  public Wave Child;

  public void SetChild(Wave wave) {
    Child = wave;
    wave.Parent = this;
  }

  public Wave(int index, Encounter encounter, Reward reward, Wave parent = null, Wave child = null) {
    Index = index;
    Encounter = encounter;
    Reward = reward;
    Parent = parent;
    Child = child;
  }
}