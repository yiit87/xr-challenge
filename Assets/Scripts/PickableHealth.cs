using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableHealth : Pickup
{
    public void AddHealth()
    {
        GameManager.Instance.PlayerAddHealth(ScoreValue);
    }
}
