using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{

    private Animator anim;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Walk(bool move)
    {
        anim.SetBool("Movement", move);
    }
    public void Punch()
    {
        anim.SetTrigger("Punch");
    }
    public void Kick()
    {
        anim.SetTrigger("Kick");
    }
    public void Kiss()
    {
        anim.SetTrigger("Kiss");
    }

    public void EnemyAttack(int attack)
    {
        if (attack == 0)
            anim.SetTrigger("Attack1");
        if (attack == 1)
            anim.SetTrigger("Attack2");
        if (attack == 2)
            anim.SetTrigger("Attack3");
    }
    public void playIdle()
    {
        anim.Play("Idle");
    }
    public void Knockdown()
    {
        anim.SetTrigger("Knockdown");
    }
    public void StandUp()
    {
        anim.SetTrigger("StandUp");
    }
    public void Hit()
    {
        anim.SetTrigger("Hit");
    }
    public void Death()
    {
        anim.SetTrigger("Death");
    }

}
