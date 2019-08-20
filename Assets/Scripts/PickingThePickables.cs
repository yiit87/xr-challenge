using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingThePickables : MonoBehaviour
{
    private const string TAG_PICKSTAR = "PickupStar";
    private const string TAG_TRAP = "Trap1";
    private const string TAG_HEALTH = "Health";
    private const string TAG_SPEED = "Speed";
    private const string TAG_EXIT = "Exit";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TAG_PICKSTAR))
        {
            float scorePoint = other.GetComponent<Pickup>().ScoreValue;
            other.GetComponent<Pickup>().GetPickedUp();
            GameManager.Instance.AddToTotalScore(scorePoint);
            GameManager.Instance.AddCollectedStar();
            GetComponent<MoveStarToNewLocation>().NewStarPosition();
            other.GetComponent<AudioSource>().Play();
        }
        else if(other.CompareTag(TAG_TRAP))
        {
            other.GetComponentInParent<TrapSystem>().DamageToPlayer();
        }
        else if (other.CompareTag(TAG_HEALTH))
        {
            other.GetComponentInParent<PickableHealth>().AddHealth();
            other.GetComponent<PickableHealth>().GetPickedUp();
        }
        else if (other.CompareTag(TAG_SPEED))
        {
            other.GetComponentInParent<PickableSpeed>().AddSpeed();
            other.GetComponent<PickableSpeed>().GetPickedUp();
        }
        else if (other.CompareTag(TAG_EXIT))
        {
            other.GetComponent<ExitSystem>().EndTheLevel();
        }
    }
}
