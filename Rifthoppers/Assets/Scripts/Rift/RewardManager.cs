using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : Singleton<RewardManager> {
  public List<UpgradeObject> Upgrades = new();

  public IEnumerator RewardRoutine() {
    yield return null;
    // Time slows down.
    // Upgrades appear on - screen.
    // Energy begins draining, and the Rift closes(old animation).
    // Background is now black, with only the character at the center, behind the upgrades.
    // Energy fills back up, maybe in a different color?
    // Energy is full, and the selected / random upgrade is picked.
    // Unchosen upgrades disappear; picked upgrade shrinks.
    // Little upgrade flies toward the character, and hits it with a little happy dinging noise.
    // Music intro of the next area.
    // New area opens very quickly, with some cool transition effect, like screen-tearing, static, whatever.
  }
}