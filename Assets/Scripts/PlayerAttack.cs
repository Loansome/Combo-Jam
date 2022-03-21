using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComboState
{
    NONE,
    PUNCH,
    KICK,
    KISS
}

public class PlayerAttack : MonoBehaviour
{

    private CharacterAnimation playerAnimation;

    private bool activateTimerToReset;
    private float defaultComboTimer = 0.5f;
    private float currentComboTimer;
    private ComboState currentComboState;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    void Awake()
    {
        playerAnimation = GetComponentInChildren<CharacterAnimation>();
    }

    private void Start()
    {
        currentComboState = ComboState.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        AttackCombo();
        ResetComboState();
    }

    void AttackCombo()
    {
        if (Input.GetKeyDown(KeyCode.J)) // punch
        {
            if (currentComboState != ComboState.NONE) // don't punch if end of combo/kicking
                return;

            currentComboState++;
            activateTimerToReset = true;
            currentComboTimer = defaultComboTimer - .1f;

            if (currentComboState == ComboState.PUNCH)
                playerAnimation.Punch();

            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyMovement>().TakeDamage(1);
            }
        }

        if (Input.GetKeyDown(KeyCode.K)) // kick
        {
            if (currentComboState == ComboState.KICK || currentComboState == ComboState.KISS || currentComboTimer > 0.35f) // don't kick if ended combo
                return;

            if (currentComboState == ComboState.KICK)
                currentComboState++;
            else currentComboState = ComboState.KICK;

            activateTimerToReset = true;
            currentComboTimer = defaultComboTimer;

            if (currentComboState == ComboState.KICK)
                playerAnimation.Kick();

            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyMovement>().TakeDamage(1);
            }
        }

        if (Input.GetKeyDown(KeyCode.L)) // kiss
        {
            if (currentComboState == ComboState.KISS || currentComboTimer > 0.35f) // don't kiss if ended combo
                return;

            currentComboState = ComboState.KISS;

            activateTimerToReset = true;
            currentComboTimer = defaultComboTimer + .2f;

            playerAnimation.Kiss();

            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyMovement>().Kissed();
            }
        }
    }

    void ResetComboState() // counts down combo timer, resets combo when time is up
    {
        if (activateTimerToReset)
        {
            currentComboTimer -= Time.deltaTime;
            if (currentComboTimer <= 0f)
            {
                currentComboState = ComboState.NONE;
                activateTimerToReset = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
