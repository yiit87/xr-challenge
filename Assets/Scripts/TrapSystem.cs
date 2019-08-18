using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSystem : MonoBehaviour
{
    public int DamageValue;

    public void DamageToPlayer()
    {
        GameManager.Instance.PlayerTakeDamage(DamageValue);
    }

}
