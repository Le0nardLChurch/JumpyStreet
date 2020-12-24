using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
#pragma warning disable 0649 
    [SerializeField] Animator animator;
#pragma warning restore 0649 

    public static bool IsHopping { get; private set; }
    public bool OnLog { set { animator.SetBool("OnLog", value); FinishHop(); } }


    public void FinishHop()
    {
        IsHopping = false;
    }

    public void PlayHop()
    {
        animator.SetTrigger("Hop");
        IsHopping = true;
    }

    private void Awake()
    {
        animator = animator == null ? gameObject.GetComponent<Animator>() : animator;
    }
}





