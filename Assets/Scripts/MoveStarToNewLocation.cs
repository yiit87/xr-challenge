using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStarToNewLocation : MonoBehaviour
{
    public List<Transform> StarPointLocation = new List<Transform>();
    public GameObject Star;
    private int Counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        Star.transform.position = StarPointLocation[0].position;
    }


    public void NewStarPosition()
    {
        if (Counter < StarPointLocation.Count - 1)
        {
            Counter++;
            Star.transform.position = StarPointLocation[Counter].position;
            Star.GetComponent<Pickup>().Init();
        }
    }
}
