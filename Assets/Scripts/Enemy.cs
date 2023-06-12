using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
   [SerializeField] int currentHealth;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    [SerializeField] public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayers;
    public PlayerCombat playerCombat;
    public Transform Visuals;
    public Transform enemyBody;
    private Vector3 localScale;
    bool facingLeft=false;
    bool facingRight = true;
    float speed=0.15f;

    public GameObject objectToDestroy;



    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        localScale = transform.localScale;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //hurt anim
        StartCoroutine(ChangeSpriteTint());


        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private IEnumerator ChangeSpriteTint()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
    }

    void Die()
    {
        Debug.Log("Enemy died! ");
        Destroy(objectToDestroy);
        // Die Anim
        //disable corpse 
    }
    void Update()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        if (hitColliders.Length > 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) 
        {
            animator.SetTrigger("EnemyAttack");
            speed = 0f;
        }
        else { speed = 0.15f;
        }

        if (Mathf.Abs(Visuals.position.x-enemyBody.position.x)<0.7f&& Visuals.position.x - enemyBody.position.x>0) {

      //      Debug.Log("close");
            enemyBody.Translate(Vector3.right * speed * Time.deltaTime);
            if (facingRight == false)
            {
                localScale.x *= -1;
                transform.localScale = localScale;
                facingRight = true;
                facingLeft = false;
            }
            }
        else if (Mathf.Abs(Visuals.position.x - enemyBody.position.x) < 0.7f && Visuals.position.x - enemyBody.position.x < 0)
        {
            enemyBody.Translate(Vector3.left * speed * Time.deltaTime);
            if(facingLeft ==false) { 
            localScale.x *= -1;
            transform.localScale = localScale;
                facingLeft = true;
                facingRight = false;
            }
        }


    }



    public void hitFrame()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

        foreach (Collider2D player in hitPlayers)
        {
            playerCombat.playerHp -= 20;
            Debug.Log(playerCombat.playerHp);
            playerCombat.damageTaken = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
