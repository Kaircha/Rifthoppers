using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Reward : MonoBehaviour {
  public Area Area;
  public bool IsFinished = true;

  public abstract void RewardStart();
  public abstract IEnumerator RewardRoutine();
  public abstract void RewardEnd();
}