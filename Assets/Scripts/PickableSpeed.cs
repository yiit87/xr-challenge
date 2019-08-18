using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableSpeed : Pickup
{
    public void AddSpeed()
    {
        GameManager.Instance.PlayerSpeedIncrease(ScoreValue);
    }
}
