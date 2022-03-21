using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private CharacterAnimation enemyAnim;
    private Rigidbody enemyBody;
    private Transform playerTarget;

    public float speed = 1.8f;
    public float attackDistance = 1.5f;
    private float chaseAfterAttack = 1f;
    private float currentAttackTime;
    private float defaultAttackTime = 2f;
    private bool following, attacking;
    private bool kissed;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;

    public int maxHealth = 7;
    int currentHealth;

    private void Awake()
    {
        enemyAnim = GetComponentInChildren<CharacterAnimation>();
        enemyBody = GetComponent<Rigidbody>();
        playerTarget = GameObject.Find("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        following = true;
        currentAttackTime = defaultAttackTime;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        currentAttackTime += Time.deltaTime;
        if (currentAttackTime > defaultAttackTime && kissed)
        {
            kissed = false;
            currentAttackTime = defaultAttackTime;
            enemyAnim.playIdle();
        }
    }

    private void FixedUpdate()
    {
        FollowTarget();
        if (Vector3.Dot((playerTarget.position - transform.position).normalized, transform.forward) < 0f)
            transform.rotation = Quaternion.LookRotation(-transform.forward, Vector3.up);
    }

    void FollowTarget()
    {
        if (!following || kissed) return;
        if (Vector3.Distance(transform.position, playerTarget.position) > attackDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, speed * Time.deltaTime);

            enemyAnim.Walk(true);
        }
        else if (Vector3.Distance(transform.position, playerTarget.position) <= attackDistance)
        {
            //enemyBody.velocity = Vector3.zero;
            enemyAnim.Walk(false);
            following = false;
            attacking = true;
        }
    }

    void Attack()
    {
        if (!attacking) return;
        if (currentAttackTime > defaultAttackTime)
        {
            enemyAnim.EnemyAttack();
            currentAttackTime = 0f;

            Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);
            foreach (Collider player in hitPlayer)
            {
                player.GetComponent<PlayerMovement>().TakeDamage(177);
            }
        }
        if (Vector3.Distance(transform.position, playerTarget.position) > attackDistance + chaseAfterAttack)
        {
            attacking = false;
            following = true;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        enemyAnim.Hit();
        if (kissed)
        {
            kissed = false;
            currentAttackTime = 0f;
        }

        if (currentHealth <= 0)
        {
            EnemyDeath();
        }
        else
            FindObjectOfType<AudioManager>().Play("Hit");
    }

    void EnemyDeath()
    {
        Debug.Log("Enemy downed");
        enemyAnim.Death();
        this.enabled = false;
        FindObjectOfType<AudioManager>().Play("Enemy Death");
        GetComponent<Collider>().enabled = false;
    }

    public void Kissed()
    {
        if (!kissed)
        {
            enemyAnim.Kissed();
            currentAttackTime -= 5f;
            kissed = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
