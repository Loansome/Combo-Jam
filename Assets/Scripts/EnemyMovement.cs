using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private CharacterAnimation enemyAnim;
    private Rigidbody enemyBody;
    private Transform playerTarget;

    public float speed = 3f;
    public float attackDistance = 1f;
    private float chaseAfterAttack = 1f;
    private float currentAttackTime;
    private float defaultAttackTime;
    private bool following, attacking;

    private void Awake()
    {
        enemyAnim = GetComponentInChildren<CharacterAnimation>();
        enemyBody = GetComponent<Rigidbody>();
        playerTarget = GameObject.FindWithTag("Player").transform;
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
        FollowTarget();
    }

    void FollowTarget()
    {
        if (!following) return;
        if (Vector3.Distance(transform.position, playerTarget.position) > attackDistance)
        {
            transform.LookAt(playerTarget);
            enemyBody.velocity = transform.forward * speed;
        }
    }

}
