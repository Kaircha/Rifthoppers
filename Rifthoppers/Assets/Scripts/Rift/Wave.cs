using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave {
  public int Index;
  public Wave Parent;
  public Area Area;
  public Objective Objective;
  public Reward Reward;
  // public List<Wave> Children;
  public Wave Child;
}