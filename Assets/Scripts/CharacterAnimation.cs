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

    public void EnemyAttack()
    {
        //if (attack == 0)
            anim.SetTrigger("Attack");
    }
    public void playIdle()
    {
        anim.Play("Idle");
    }
    public void Hit()
    {
        anim.SetTrigger("Hit");
    }
    public void Death()
    {
        anim.SetTrigger("Death");
    }
    public void Kissed()
    {
        anim.SetTrigger("Kissed");
    }

}
