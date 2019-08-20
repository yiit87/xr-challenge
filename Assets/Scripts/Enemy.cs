using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform[] patrolPoints;

    private int currentPoint = 0;

    public float moveSpeed = 0.5f;

    private GameObject targetPlayer;

    private float currentTime;
    Animator anim, anim2;

    public NavMeshAgent agent;
    public bool playerDies = false;
    public bool playerDeadOnTheFloor = false;

    private SphereCollider col;
    
    private float patrolTimer;
    private float patrolWaitTime = 2f;
    private int wayPointIndex;

    public State state = State.PATROL;

    private bool alive = true;

    LineRenderer ShootingLaser;
    public GameObject LaserGun;

    private const string TAG_ENEMY = "Enemy";
    private const string TAG_PLAYER = "Player";
    private const float ANGULAR_SPEED = 250f;
    private const float ANGLE_TO_SEE = 25f;
    private const int DAMAGE_VALUE = 100;

    public enum State
    {
        PATROL,
        ATTACK
    }

    // Use this for initialization
    void Start()
    {
        gameObject.tag = TAG_ENEMY;
        transform.position = patrolPoints[0].position; //Enemy position is the first patrol location position

        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = agent.updateRotation = true;

        targetPlayer = GameObject.FindGameObjectWithTag(TAG_PLAYER);

        col = GetComponent<SphereCollider>();
        anim = GetComponentInChildren<Animator>();
        ShootingLaser = GetComponentInChildren<LineRenderer>();
        ShootingLaser.enabled = false;

        //Start FSM
        StartCoroutine("FSM");
    }

    IEnumerator FSM()
    {
        while (alive)
        {
            switch (state)
            {
                case State.PATROL:
                    Patrol();
                    break;
   
                case State.ATTACK:
                    Attack();
                    break;
            }
            yield return null;
        }
    }

    void Patrol()
    {
        if (!GameManager.Instance.Pause)
        {

            //Enemy speed
            agent.speed = moveSpeed;
            agent.angularSpeed = ANGULAR_SPEED;

            if (agent.remainingDistance < agent.stoppingDistance)
            {
                //Go to a patrol location wait few seconds and go to next one
                patrolTimer += Time.deltaTime;

                if (patrolTimer >= patrolWaitTime)
                {
                    if (wayPointIndex == patrolPoints.Length - 1)
                    {
                        wayPointIndex = 0;
                    }
                    else
                    {
                        wayPointIndex++;
                    }

                    patrolTimer = 0f;
                }
            }
            else
            {
                patrolTimer = 0f;
            }

            //set next patrol location
            agent.destination = (patrolPoints[wayPointIndex].position);
        }
    }

    private void Update()
    {
        Vector3 direction = targetPlayer.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction.normalized, out hit, col.radius))
        {
            if (hit.collider.CompareTag(TAG_PLAYER))
            {
                Debug.Log(angle);

                if (angle < ANGLE_TO_SEE)
                {
                    //Now we saw the player
                    Debug.Log("Saw you human!");
                    state = State.ATTACK;
                }
            }
        }

        //Debug.DrawRay(transform.position, transform.forward, Color.magenta, Mathf.Infinity);
    }

    void Attack()
    {
        Debug.Log("Attaaaaack!");
        agent.isStopped = true;
        ShootingLaser.enabled = true;
        ShootingLaser.SetPosition(0, LaserGun.transform.position);
        ShootingLaser.SetPosition(1, targetPlayer.transform.position);

        GameManager.Instance.PlayerTakeDamage(DAMAGE_VALUE);
        targetPlayer.GetComponent<PlayerMovement>().DieAnimation();

        StartCoroutine(DieAnimationDelay());
    }

    IEnumerator DieAnimationDelay()
    {
        GameManager.Instance.PlayerDeadCondition(true);
        yield return new WaitForSeconds(2);
        GameManager.Instance.PlayerDiedPanelActivation();
        GameManager.Instance.PauseTheGame(true);
    }
}
