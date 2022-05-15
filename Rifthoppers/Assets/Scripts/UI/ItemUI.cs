using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemUI : MonoBehaviour {
  public Animator anim;
  public TextMeshProUGUI Name;
  public TextMeshProUGUI Quote;

  private void OnEnable() => UpgradeWeaponManager.Instance.OnUpgradeTaken += TriggerInfo;
  private void OnDisable() => UpgradeWeaponManager.Instance.OnUpgradeTaken -= TriggerInfo;

  public void TriggerInfo(Upgrade upgrade) {
    Name.text = upgrade.name;
    Quote.text = upgrade.Quote;
    StartCoroutine(DoAnimation());
  }

  private IEnumerator DoAnimation() {
    anim.SetBool("UpgradeTaken", true);
    yield return new WaitForSeconds(5);
    anim.SetBool("UpgradeTaken", false);
  }
}
