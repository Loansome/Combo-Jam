using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComboState
{
    NONE,
    PUNCH1,
    PUNCH2,
    PUNCH3,
    KICK1,
    KICK2
}

public class PlayerAttack : MonoBehaviour
{

    private CharacterAnimation playerAnimation;

    private bool activateTimerToReset;
    private float defaultComboTimer = 0.4f;
    private float currentComboTimer;
    private ComboState currentComboState;

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
            if (currentComboState == ComboState.PUNCH3 || currentComboState == ComboState.KICK1 || currentComboState == ComboState.KICK2 /* || currentComboTimer > 0.25f (startup frames)*/) // don't punch if end of combo/kicking
                return;

            currentComboState++;
            activateTimerToReset = true;
            currentComboTimer = defaultComboTimer;

            if (currentComboState == ComboState.PUNCH1)
                playerAnimation.Punch1();
            if (currentComboState == ComboState.PUNCH2)
                playerAnimation.Punch2();
            if (currentComboState == ComboState.PUNCH3)
                playerAnimation.Punch3();

        }

        if (Input.GetKeyDown(KeyCode.K)) // kick
        {
            if (currentComboState == ComboState.KICK2 || currentComboState == ComboState.PUNCH3) // don't kick if ended combo
                return;

            if (currentComboState == ComboState.KICK1)
                currentComboState++;
            else currentComboState = ComboState.KICK1;

            activateTimerToReset = true;
            currentComboTimer = defaultComboTimer;

            if (currentComboState == ComboState.KICK1)
                playerAnimation.Kick1();
            if (currentComboState == ComboState.KICK2)
                playerAnimation.Kick2();
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
}
