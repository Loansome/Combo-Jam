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
}
