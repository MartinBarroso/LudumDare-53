using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Zombie : MonoBehaviour
{
    [Header("Zombie Variables")]

    [SerializeField] private GameObject target;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject cemetery;

    [SerializeField] private float initialSpeed;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float addSpeed;
    [SerializeField] private float maxEnemyDistance;
    [SerializeField] private float maxDistanceToPick;
    [SerializeField] private AudioClip hit;

    const int INITIAL_SPEED = 10;
    const float DEATH_ANIMATION_TIME = 1.05f;

    [SerializeField]private bool hasABag = false;
    private float distance;
    public int health;
    public bool hasBeenAttacked;
    private float timer;
    public float maxEnemyInvencibilty;
    
    private bool isActive = true;
    private Animator animator = null;

    public Action<Zombie> OnZombieDeath;

    static int zombieCount = 0;
    static bool zombieGrabbedPackage = false;

    private void Awake()
    {
        zombieCount++;
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        distance = int.MaxValue;
        rb = GetComponent<Rigidbody2D>();
        timer = 0.0f;
    }

    public static int GetZombieCount() => zombieCount;

    // Update is called once per frame
    void Update()
    {
        if (!IsAlive()) return;

        GetTargetDistance();

        if (hasBeenAttacked)
        {
            timer += Time.deltaTime;
            if (timer > maxEnemyInvencibilty)
            {
                timer = 0.0f;
                hasBeenAttacked = false;
            }
        }

        if(hasABag)
        {
            target = cemetery;
            animator.SetBool("hasPackage", true);
            animator.SetBool("isMoving", true);

            Bag.Attach(transform);
            GetTargetDistance();
            if (zombieGrabbedPackage)
            {
                if (IsInRangeToPick())
                {
                    hasABag = false;
                    zombieGrabbedPackage = false;
                    Bag.ResetBag(Destination.CEMENTERY);
                    Bag.isGrabbed = false;
                    LoseHealth(health);
                    //Crear nuevo pedido y no dar puntos
                }
            }

            var aux = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime * currentSpeed);
            rb.MovePosition(aux);
            currentSpeed = INITIAL_SPEED;


        }
        else
        {
            target = Bag.Instance.gameObject;
            animator.SetBool("hasPackage", false);

            if (!zombieGrabbedPackage)
            {
                if (IsInRangeToPick())
                {
                    hasABag = true;
                    Bag.Instance.transform.parent = null;
                    zombieGrabbedPackage = true;
                    Bag.isGrabbed = true;
                    Player.hasPackage = false;
                }
            }

            if (IsInRangeToChase())
            {
                var aux = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime * currentSpeed);
                rb.MovePosition(aux);
                currentSpeed += addSpeed;
                animator.SetBool("isMoving", true);
            }
            else
            {
                currentSpeed = initialSpeed;
                animator.SetBool("isMoving", false);
            }
        }

    }

    public void SetCementery(GameObject cemetery)
    {
        this.cemetery = cemetery;
    }

    public void SetPosition(Vector3 newPos)
    {
        transform.position = newPos;
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    public void SetActiveState(bool state = true)
    {
        isActive = state;
        gameObject.SetActive(state);
    }

    private void GetTargetDistance()
    {
        if (target != null)
            distance = Vector2.Distance(transform.position, target.transform.position);
        else
            distance = int.MaxValue;
    }

    private bool IsInRangeToChase()
    {
        if (target != null)
        {
            return distance < maxEnemyDistance;
        }
        else
        {
            return false;
        }
    }

    private bool IsInRangeToPick()
    {
        if (target != null)
        {
            return distance < maxDistanceToPick;
        }
        else
        {
            return false;
        }
    }

    public void LoseHealth(int damage)
    {
        health -= damage;
        SoundManager.Instance.PlaySound(hit);
        //Debug.Log(health);
        if (!IsAlive())
        {
            animator.SetBool("wasKilled", true);
            StartCoroutine(WaitForDeathAnimation());
        }
    }

    IEnumerator WaitForDeathAnimation()
    {
        float time = 0.0f;
        while (time<DEATH_ANIMATION_TIME)
        {
            time += Time.deltaTime;
            yield return null;
        }

        OnZombieDeath(this);
        gameObject.SetActive(IsAlive());

    }

    public void ResetAnimatorVariables()
    {
        animator.SetBool("wasKilled", false);
        animator.SetBool("hasPackage", false);
        animator.SetBool("isMoving", false);
    }
    private bool IsAlive()
    {
        return health > 0;
    }

    public void IsActive() => IsAlive();
    void OnDrawGizmos()
    {
       //Gizmos.color = Color.black;
       //Gizmos.DrawWireSphere(transform.position,maxEnemyDistance);
       //Gizmos.color = Color.red;
       //Gizmos.DrawWireSphere(transform.position,maxDistanceToPick);
       //Gizmos.DrawLine(transform.position,target.transform.position);
    }

}
