using UnityEngine;

public class CharacterAnimator : MonoBehaviour, IAnimator
{

    [SerializeField] private AnimationModule RunAnimationModule;
    [SerializeField] private AnimationModule IdleAnimationModule;
    [SerializeField] private AnimationModule WalkAnimationModule;
    [SerializeField] private AnimationModule RollAnimationModule;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            PlayRunAnimation();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            PlayIdleAnimation();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            PlayWalkAnimation();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayRollAnimation();
        }
    }

    public void PlayRunAnimation()
    {
        UniversalAnimator.Instance.SwitchAnimState(RunAnimationModule);
    }

    public void PlayIdleAnimation()
    {
        UniversalAnimator.Instance.SwitchAnimState(IdleAnimationModule);
    }

    public void PlayWalkAnimation()
    {
        UniversalAnimator.Instance.SwitchAnimState(WalkAnimationModule);
    }

    public void PlayRollAnimation()
    {
        UniversalAnimator.Instance.SwitchAnimState(RollAnimationModule);
    }

}
