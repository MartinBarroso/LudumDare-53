using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject attackHitbox;
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private GameObject map;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 direction;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip packageClip;
    [SerializeField] private AudioClip attackClip;
    public static bool hasPackage;
    private Rigidbody2D rb2D;
    private bool isFacingRight;
    [SerializeField] private float attackAnimationTime;
    private bool isAttacking;
    public bool isInteracting;


    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        isAttacking = false;
        hasPackage = false;
    }

    void FixedUpdate()
    {
        if (!isAttacking)
        {
            Move();
        }

    }

    void Update()
    {
        animator.SetBool("hasPackage", hasPackage);
        var aux = direction.x != 0 || direction.y != 0 ? true : false;
        animator.SetBool("isMoving", aux);
        FlipCharacter();

    }


    public void SetPackage()
    {
        SoundManager.Instance.PlaySound(packageClip);
        hasPackage = true;
        Bag.Instance.transform.SetParent(transform);
        Bag.Instance.transform.localPosition = Vector3.zero;
        Bag.isGrabbed = true;
    }
    private void FlipCharacter()
    {
        if (isFacingRight && direction.x < 0f)
        {
            Flip();
        }
        else if (!isFacingRight && direction.x > 0f)
        {
            Flip();
        }
    }

    private void Move()
    {
        rb2D.MovePosition(rb2D.position + speed * Time.deltaTime * direction);
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        Debug.Log("A" );
         if (!GameManager2.canPlayerUpdate || GameManager2.GameOver) return;
        direction = value.ReadValue<Vector2>();
        


    }

    public void OnMap()
    {
        Debug.Log("Entro1");
        if (!GameManager2.canPlayerUpdate || GameManager2.GameOver) return;
        Debug.Log("Entro2");
        map.SetActive(!map.activeSelf); 
    }

   public void OnAction()
    {
         if (!GameManager2.canPlayerUpdate || GameManager2.GameOver) return;
        if (!isAttacking && !hasPackage)
        {
            StartCoroutine(Attack());
        }
    }

    public void OnInteract()
    {

        if (hasPackage)
        {
            DropPackage();
        }
        else if (!isInteracting)
        {
            StartCoroutine(Interact());
        }

    }

    public void DropPackage()
    {
        SoundManager.Instance.PlaySound(packageClip);
        Bag.Instance.transform.parent = null;
        Bag.Instance.transform.position = transform.position + Vector3.down;
        hasPackage = false;
        Bag.isGrabbed = false;
        Bag.Instance.sr.enabled = true;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        var localScale = playerSprite.transform.localScale;
        var localScale2 = attackHitbox.transform.localScale;
        localScale.x *= -1f;
        localScale2.x *= -1f;
        playerSprite.transform.localScale = localScale;
        attackHitbox.transform.localScale = localScale2;
    }

    IEnumerator Attack()
    {
        SoundManager.Instance.PlaySound(attackClip);
        animator.SetTrigger("Attack");
        isAttacking = true;
        attackHitbox.SetActive(true);
        yield return new WaitForSeconds(attackAnimationTime);
        attackHitbox.SetActive(false);
        isAttacking = false;
        yield break;

    }
    IEnumerator Interact()
    {
        isInteracting = true;
        yield return new WaitForSeconds(attackAnimationTime);
        isInteracting = false;
        yield break;

    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Package") && !hasPackage && isInteracting)
        {
            SetPackage();
        }
    }

}
