using UnityEngine;

public class UniversalAnimator : MonoBehaviour
{

    private string _currentState;

    private static UniversalAnimator _instance;

    public static UniversalAnimator Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void SwitchAnimState(AnimationModule module)
    {

        var animatorInfo = module.Animator.GetCurrentAnimatorClipInfo(0);
        _currentState = animatorInfo[0].clip.name;

        if (_currentState == module.STATE) return;

        module.Animator.Play(module.STATE);
    }

}
