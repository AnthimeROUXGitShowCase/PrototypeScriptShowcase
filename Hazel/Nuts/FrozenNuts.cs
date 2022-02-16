using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenNuts : Nuts
{
    private bool grabed;
  public override void Grabbed()
  {
     PlayerController.instance.OnSlow();
     grabed = true;
  }

  private void Update()
  {
      if ((grabed) && (transform.parent.name == "Grabber"))
      {
          StartCoroutine(PlayerController.instance.DeSlow());
      }
  }
}
