using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : State {
  // Little bit lazy, would ideally have more logic here and less in RewardManager
  public override float ExecutionTime => RewardManager.Instance.TimeLimit;
  public override void Enter() => RewardManager.Instance.StartReward();
  public override IEnumerator Execute() => RewardManager.Instance.RewardRoutine();
  public override void Exit() => RewardManager.Instance.EndReward();
}