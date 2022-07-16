using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatsMenu : MonoBehaviour
{
  public void GodMode(bool ok) => RiftManager.Instance.Energy.CanTakeDamage = !ok;
  public void SkipEncounter() => RiftManager.Instance.ExperienceCollected(99999999999);
  public void FreezeProgress(bool ok ) => RiftManager.Instance.xpMulti = ok ? 0 : 1;
}
