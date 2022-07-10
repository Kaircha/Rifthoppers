using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeInteractable : MonoBehaviour{

  private UpgradeObject UpgradeObject;
  public SpriteRenderer rend;

  public void Initialize(UpgradeObject upgradeObject){
    UpgradeObject = upgradeObject;

    rend.sprite = upgradeObject.Sprite;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.attachedRigidbody.TryGetComponent<PlayerBrain>(out PlayerBrain brain)){

      Upgrade upgrade = UpgradeObject.Upgrade();
      upgrade.Brain = brain;
      upgrade.Add();

      // might be more complicated for multiplayer
      RewardManager.Instance.upgrading = false;
    }
  }

}
