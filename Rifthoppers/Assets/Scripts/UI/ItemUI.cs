using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemUI : MonoBehaviour {
  public Animator Animator;
  public TextMeshProUGUI Name;
  public TextMeshProUGUI Quote;

  //private void OnEnable() => UpgradeWeaponManager.Instance.OnUpgradeTaken += TriggerInfo;
  //private void OnDisable() => UpgradeWeaponManager.Instance.OnUpgradeTaken -= TriggerInfo;

  public void TriggerInfo(Upgrade upgrade) {
    Name.text = upgrade.Object.name;
    Quote.text = upgrade.Object.Quote;
    StartCoroutine(DoAnimation());
  }

  private IEnumerator DoAnimation() {
    Animator.SetBool("UpgradeTaken", true);
    yield return new WaitForSeconds(5);
    Animator.SetBool("UpgradeTaken", false);
  }
}
