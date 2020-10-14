using UnityEngine;

public class AnimationModule
{

    public string STATE;
    public Animator Animator;

    AnimationModule(Animator animator, string state)
    {
        Animator = animator;
        STATE = state;
    }

}
