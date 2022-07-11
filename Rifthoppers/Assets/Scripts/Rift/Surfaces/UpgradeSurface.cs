using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSurface : Surface {
  private UpgradeObject UpgradeObject;
  public SpriteRenderer Renderer;

  public void Initialize(UpgradeObject upgradeObject) {
    UpgradeObject = upgradeObject;
    Renderer.sprite = upgradeObject.Sprite;
  }

  public override void SurfaceEnter(Entity entity) {
    if (entity.TryGetComponent(out PlayerBrain brain)) {
      RewardManager.Instance.UpgradePairs.Add(new UpgradePair(brain.Upgrades, UpgradeObject.Upgrade()));
    }
  }

  public override void SurfaceStay(Entity entity) { }

  public override void SurfaceExit(Entity entity) {
    if (entity.TryGetComponent(out PlayerBrain brain)) {
      RewardManager.Instance.UpgradePairs.Remove(new UpgradePair(brain.Upgrades, UpgradeObject.Upgrade()));
    }
  }
}
