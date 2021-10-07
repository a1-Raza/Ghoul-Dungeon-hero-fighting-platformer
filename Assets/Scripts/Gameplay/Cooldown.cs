using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown
{
    bool cooldownVar;
    float cooldownTime;

    public Cooldown(float time)
    {
        cooldownTime = time;
    }

    public IEnumerator StartCoolDown()
    {
        cooldownVar = true;
        yield return new WaitForSeconds(cooldownTime);
        cooldownVar = false;
    }

    public bool IsInCooldown()
    {
        return cooldownVar;
    }
}
