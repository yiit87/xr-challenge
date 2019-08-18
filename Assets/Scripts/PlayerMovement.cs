using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController cc;
    public float PlayerSpeed = 4;

    private Vector3 moveDirectionPlayer = Vector3.zero;

    private Vector3 lookDirection;

    bool FasterActive = false;

    private float TotalSpeedActiveTime = 5f;
    private float DefaultSpeed;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();

        DefaultSpeed = PlayerSpeed;
    }

    public void AdjustSpeed(int value)
    {
        FasterActive = true;
        PlayerSpeed = value;
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.Pause)
        {
            if (FasterActive)
            {
                TotalSpeedActiveTime -= 1 * Time.deltaTime;

                if (TotalSpeedActiveTime <= 0)
                {
                    PlayerSpeed = DefaultSpeed;
                    TotalSpeedActiveTime = 5;
                    FasterActive = false;
                }
            }

            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            lookDirection = new Vector3(h, v, 0).normalized;

            MovementManagement(lookDirection.x, lookDirection.y);
        }
    }
    private void MovementManagement(float horizontal, float vertical)
    {
        if ((horizontal != 0f || vertical != 0f))
        {
            PlayerMove(horizontal, vertical);
        }
        
    }
    private void PlayerMove(float horizontal, float vertical)
    {
        moveDirectionPlayer = new Vector3(horizontal, 0, vertical);
        moveDirectionPlayer *= PlayerSpeed;
        cc.Move(moveDirectionPlayer * Time.deltaTime);

        Quaternion targetRotation = Quaternion.LookRotation(moveDirectionPlayer);
        Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 15 * Time.deltaTime);

        transform.rotation = newRotation;
    }
}
