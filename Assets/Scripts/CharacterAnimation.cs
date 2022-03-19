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
    public void Punch1()
    {
        anim.SetTrigger("Punch 1");
    }
    public void Punch2()
    {
        anim.SetTrigger("Punch 2");
    }
    public void Punch3()
    {
        anim.SetTrigger("Punch 3");
    }
    public void Kick1()
    {
        anim.SetTrigger("Kick 1");
    }
    public void Kick2()
    {
        anim.SetTrigger("Kick 2");
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
