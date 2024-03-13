using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDefaultState : ActionBaseState
{
    public override void EnterState(ActionStateManager actionManager)
    {
    }

    public override void UpdateState(ActionStateManager actionManager)
    {
        //Eller tekrar Rig kontrolüne veriliyor.
        actionManager.rHandAim.weight = Mathf.Lerp(actionManager.rHandAim.weight, 1, 10 * Time.deltaTime);
        actionManager.lHandIK.weight = Mathf.Lerp(actionManager.lHandIK.weight, 1, 10 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.R) && CanReload(actionManager))
        {
            actionManager.SwitchState(actionManager.Reload);
        }
    }

    bool CanReload(ActionStateManager actionManager)
    {
        //Þarjör full
        if (actionManager.ammo.currentAmmo == actionManager.ammo.clipSize) return false;
        //Yedek mermi kalmamýþ
        else if(actionManager.ammo.extraAmmo == 0) return false;
        else return true;
    }
}
