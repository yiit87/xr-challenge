using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smooth = 1.5f;

    private Transform player;
    private Vector3 relCameraPos;
    private float relCameraPosMag;
    private Vector3 newPos;
    RaycastHit hit;
    Vector3 ExampleCoordinate;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        relCameraPos = transform.position - player.position;
        relCameraPosMag = relCameraPos.magnitude - 0.5f;
    }

    void LateUpdate()
    {
        if (!GameManager.Instance.Pause)
        {
            Vector3 standardPos = player.position + relCameraPos + ExampleCoordinate;
            Vector3 abovePos = player.position + Vector3.up * relCameraPosMag;
            

            Vector3[] checkPoints = new Vector3[5];
            checkPoints[0] = standardPos;
            checkPoints[1] = Vector3.Lerp(standardPos, abovePos, 10.25f);
            checkPoints[2] = Vector3.Lerp(standardPos, abovePos, 10.5f);
            checkPoints[3] = Vector3.Lerp(standardPos, abovePos, 10.75f);
            checkPoints[4] = abovePos;

            for (int i = 0; i < checkPoints.Length; i++)
            {
                if (ViewingPosCheck(checkPoints[i]))
                {
                    break;
                }
            }

            transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);
                
            SmoothLookAt();
        }
    }

    bool ViewingPosCheck(Vector3 checkPos)
    {
        if (Physics.Raycast(checkPos, player.position - checkPos, out hit, relCameraPosMag))
        {
            if (hit.transform != player && hit.transform.tag != "Enemy")
            //burasi playeri buldugu yer. Buraya bos kutulara tag koyup eklersek hareket etmez kamera
            {
                return false;
            }
        }
        newPos = checkPos;
        return true;
    }

    void SmoothLookAt()
    {
        Vector3 relPlayerPosition = player.position - transform.position;
        Quaternion lookAtRotation = Quaternion.LookRotation(relPlayerPosition + new Vector3(0, 0.5f, 2), Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, 5f * Time.deltaTime);
    }
}
