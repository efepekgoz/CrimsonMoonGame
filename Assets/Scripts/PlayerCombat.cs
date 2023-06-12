using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;
    private bool canDoAction = true;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int wep = 0;
    public int attackDamage = 40;
    public string aniText = "RawAttack";
    [SerializeField] public int playerHp = 300;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public bool damageTaken = false;

    public Text hpText;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        if (playerHp > 300)
        {
            playerHp = 300;
        }
        hpText.text = playerHp.ToString();
        if(playerHp <= 0 )
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
            playerHp = 300;
        }
        switch (wep)
        {
            case 0:
                aniText = "RawAttack";
                attackDamage = 20;
                break;
            case 1:
                aniText = "Attack"; 
                attackDamage = 25;
                break;
        }

        if (canDoAction == true && Input.GetKeyDown(KeyCode.X)==true)
        {
            Attack();
        }
        if (damageTaken == true)
        {
            StartCoroutine(ChangeSpriteTint());
            damageTaken = false;
        }
    }
    void OnTriggerEnter()
    {
        canDoAction = false;
    }

    void OnTriggerExit()
    {
        canDoAction = true;
    }
    private IEnumerator ChangeSpriteTint()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
    }

    void Attack()
    {
        animator.SetTrigger(aniText);
        
    }
    public void hitFrame()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            Witch witchComponent = enemy.GetComponent<Witch>();
            Lord lordComponent = enemy.GetComponent<Lord>();

            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(attackDamage);
            }

            if (witchComponent != null)
            {
                witchComponent.TakeDamage(attackDamage);
            }
            if (lordComponent != null)
            {
                lordComponent.TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
