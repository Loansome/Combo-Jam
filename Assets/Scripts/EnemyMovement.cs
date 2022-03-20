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
    private float defaultAttackTime;
    private bool following, attacking;

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
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void FixedUpdate()
    {
        FollowTarget();
        if (Vector3.Dot((playerTarget.position - transform.position).normalized, transform.forward) < 0f)
            transform.rotation = Quaternion.LookRotation(-transform.forward, Vector3.up);
    }

    void FollowTarget()
    {
        if (!following) return;
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

        currentAttackTime += Time.deltaTime;
        if (currentAttackTime > defaultAttackTime)
        {
            enemyAnim.EnemyAttack(Random.Range(0, 3));
            currentAttackTime = 0f;
        }
        if (Vector3.Distance(transform.position, playerTarget.position) > attackDistance + chaseAfterAttack)
        {
            attacking = false;
            following = true;
        }
    }
}
