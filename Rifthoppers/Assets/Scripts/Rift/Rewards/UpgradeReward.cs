using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeReward : Reward {
  public override void RewardStart() {
    IsFinished = false;
    foreach (UpgradeInteraction interaction in GetComponentsInChildren<UpgradeInteraction>()) {
      interaction.Reward = this;
    }
    foreach (Player player in LobbyManager.Instance.Players) {
      player.Brain.EnterInteractState();
      player.Brain.Entity.transform.position = Vector3.zero;
    }
  }

  public override IEnumerator RewardRoutine() {
    yield return null;
  }

  public override void RewardEnd() {
    IsFinished = true;
  }
}