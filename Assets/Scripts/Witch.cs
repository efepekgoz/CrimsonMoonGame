using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField] int currentHealth;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public PlayerCombat playerCombat;

    public GameObject woodenLogPrefab;
    private Transform playerTransform;

    public GameObject objectToDestroy;
    public GameObject fireSoul;
    public GameObject gateWay;

    [SerializeField] public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayers;
    public bool fightActive = true;

    private IEnumerator timerCoroutine;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        timerCoroutine = Timer();
        StartCoroutine(timerCoroutine);
    }

    private IEnumerator Timer()
    {
        while (fightActive)
        {
            yield return new WaitForSeconds(4f);
            Instantiate(woodenLogPrefab, new Vector3(playerTransform.position.x, 1.3f, 0), Quaternion.identity);
        }
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
        fireSoul.SetActive(true);
        StopCoroutine(timerCoroutine);
        gateWay.SetActive(true);

        // Die Anim
        //disable corpse 
    }


    void Update()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerObject.transform;
    //    if (Input.GetKeyDown(KeyCode.Z))
      //  {
      //      Instantiate(woodenLogPrefab, new Vector3(playerTransform.position.x, 1.3f, 0), Quaternion.identity);
      //  }

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        if (hitColliders.Length > 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.SetTrigger("Attack");
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

