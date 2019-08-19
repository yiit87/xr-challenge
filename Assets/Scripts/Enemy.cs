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

    public enum State
    {
        PATROL,
        ATTACK
    }

    // Use this for initialization
    void Start()
    {
        gameObject.tag = "Enemy";
        transform.position = patrolPoints[0].position;

        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = agent.updateRotation = true;

        targetPlayer = GameObject.FindGameObjectWithTag("Player");
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
                //case State.SUSPICIOUS:
                //    Suspicious();
                //    break;
                //case State.CHASE:
                //    Chase();
                //    break;
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

            agent.angularSpeed = 250f;

            //if enemy position is the players last known position or remaining distance to the patrol location is smaller than stop distance 
            if (agent.remainingDistance < agent.stoppingDistance)
            {
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
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log(angle);

                if (angle < 25)
                {
                    //Shoot Now we saw the player
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
        ShootingLaser.SetPosition(0, LaserGun.transform.position);//new Vector3(LaserGun.transform.position,x,1.5f, transform.position.z - 0.2f));
        ShootingLaser.SetPosition(1, targetPlayer.transform.position);

        GameManager.Instance.PlayerTakeDamage(100);
        targetPlayer.GetComponent<PlayerMovement>().DieAnimation();


        StartCoroutine(DieAnimationDelay());
    }

    IEnumerator DieAnimationDelay()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.PlayerDiedPanelActivation();
        GameManager.Instance.PauseTheGame(true);
    }
}
