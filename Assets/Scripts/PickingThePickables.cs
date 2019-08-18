using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingThePickables : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickupStar"))
        {
            float scorePoint = other.GetComponent<Pickup>().ScoreValue;
            other.GetComponent<Pickup>().GetPickedUp();
            GameManager.Instance.AddToTotalScore(scorePoint);
            GameManager.Instance.AddCollectedStar();
        }
        else if(other.CompareTag("Trap1"))
        {
            other.GetComponentInParent<TrapSystem>().DamageToPlayer();
        }
        else if (other.CompareTag("Health"))
        {
            other.GetComponentInParent<PickableHealth>().AddHealth();
            other.GetComponent<PickableHealth>().GetPickedUp();
        }
        else if (other.CompareTag("Speed"))
        {
            other.GetComponentInParent<PickableSpeed>().AddSpeed();
            other.GetComponent<PickableSpeed>().GetPickedUp();
        }
        else if (other.CompareTag("Exit"))
        {
            other.GetComponent<ExitSystem>().EndTheLevel();
        }
    }
}
