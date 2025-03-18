using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public void InvokeEventFunction()
    {
        FindObjectOfType<SwitchSystem>().EventFunction();
    }
    public void InvokeZombieEventFunction()
    {
        ZombieAttack zombieAttack = transform.GetComponentInParent<ZombieAttack>();
        if (zombieAttack != null)
        {
            zombieAttack.ZombieEventFunction();
        }
    }
}
