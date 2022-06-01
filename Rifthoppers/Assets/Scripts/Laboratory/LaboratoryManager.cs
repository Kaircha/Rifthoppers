using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaboratoryManager : Singleton<LaboratoryManager> {
  public bool PortalEntered;

  public void EnterPortal() => PortalEntered = true;
}