using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lord : MonoBehaviour
{
    public int maxHealth = 999;
    [SerializeField] int currentHealth;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public PlayerCombat playerCombat;

    public GameObject bloodRain;
    private Transform playerTransform;

    [SerializeField] public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayers;
    public bool fightActive = true;
    public int limit = 900;
    public int cdAuto = 1000;

    public GameObject objectToDestroy;
    public GameObject crimsonAmulet;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
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
        crimsonAmulet.SetActive(true);
    }
    public void ActivateRain()
    {
        Instantiate(bloodRain, new Vector3(0.99f, 0.83f, 0), Quaternion.identity);
        Instantiate(bloodRain, new Vector3(0.88f, 0.90f, 0), Quaternion.identity);
        Instantiate(bloodRain, new Vector3(0.77f, 1.01f, 0), Quaternion.identity);
        Instantiate(bloodRain, new Vector3(0.67f, 1.10f, 0), Quaternion.identity);
        Instantiate(bloodRain, new Vector3(0.57f, 1.20f, 0), Quaternion.identity);
        Instantiate(bloodRain, new Vector3(0.47f, 1.30f, 0), Quaternion.identity);
        Instantiate(bloodRain, new Vector3(0.37f, 1.40f, 0), Quaternion.identity);
        Instantiate(bloodRain, new Vector3(0.27f, 1.50f, 0), Quaternion.identity);
        Instantiate(bloodRain, new Vector3(0.17f, 1.60f, 0), Quaternion.identity);
        Instantiate(bloodRain, new Vector3(0.07f, 1.70f, 0), Quaternion.identity);
        Instantiate(bloodRain, new Vector3(-0.07f, 1.80f, 0), Quaternion.identity);
        Instantiate(bloodRain, new Vector3(-0.17f, 1.90f, 0), Quaternion.identity);
        Instantiate(bloodRain, new Vector3(-0.27f, 2.01f, 0), Quaternion.identity);
        Instantiate(bloodRain, new Vector3(-0.37f, 2.10f, 0), Quaternion.identity);
        Instantiate(bloodRain, new Vector3(-0.47f, 2.20f, 0), Quaternion.identity);
        Instantiate(bloodRain, new Vector3(-0.57f, 2.30f, 0), Quaternion.identity);
        Instantiate(bloodRain, new Vector3(-0.67f, 2.40f, 0), Quaternion.identity);
        Instantiate(bloodRain, new Vector3(-0.77f, 2.50f, 0), Quaternion.identity);
        Instantiate(bloodRain, new Vector3(-0.87f, 2.60f, 0), Quaternion.identity);
        Instantiate(bloodRain, new Vector3(-0.97f, 2.70f, 0), Quaternion.identity);
        Instantiate(bloodRain, new Vector3(-1.07f, 2.80f, 0), Quaternion.identity);
    }
    void Update()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerObject.transform;
        //auto attack anim

        if (currentHealth <= limit) {
            ActivateRain();
            limit -= 250;
        }

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        if (hitColliders.Length > 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")&&currentHealth<=cdAuto)
        {
            animator.SetTrigger("Attack");
            cdAuto -= 100;
        }

    }

    public void hitFrame()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

        foreach (Collider2D player in hitPlayers)
        {
            playerCombat.playerHp -= 100;
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
