﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Joakim Linna

public class AbilityTrigger : DebugTool
{
    public WeaponBase[] weapons;
    private WeaponBase currentWeapon;

	public WeaponBase GetEquippedWeapon() { return currentWeapon; }

    protected override void OnAwake()
    {
        if (weapons.Length > 0 && currentWeapon == null)
        {
            currentWeapon = weapons[0];
        }
        else
        {
            //Debug.LogError("NO WEAPON");
        }
    }

    protected override void OnUpdate()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString())) 
            {
                currentWeapon = weapons[i];

                //Debug.Log(currentWeapon.gameObject.name);
            }
        }
        
        if (currentWeapon.HasAmmo())
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse0))
            {
                currentWeapon.Trigger();
            }
        }
        else
        {
            currentWeapon.DoReload();
        }
    }
}
