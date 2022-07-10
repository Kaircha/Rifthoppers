using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSurface : Surface{

  private UpgradeObject UpgradeObject;
  public SpriteRenderer rend;

  public void Initialize(UpgradeObject upgradeObject){
    UpgradeObject = upgradeObject;

    rend.sprite = upgradeObject.Sprite;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.attachedRigidbody.TryGetComponent<PlayerBrain>(out PlayerBrain brain)) {
      Upgrade upgrade = UpgradeObject.Upgrade();
      upgrade.Brain = brain;
      RewardManager.Instance.CurrentUpgrade = upgrade;
    }
  }

  public override void SurfaceEnter(Entity entity)
  {
    throw new System.NotImplementedException();
  }

  public override void SurfaceStay(Entity entity)
  {
    throw new System.NotImplementedException();
  }

  public override void SurfaceExit(Entity entity)
  {
    throw new System.NotImplementedException();
  }
}
