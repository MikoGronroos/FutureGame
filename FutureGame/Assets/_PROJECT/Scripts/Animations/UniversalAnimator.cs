using UnityEngine;

public class UniversalAnimator : MonoBehaviour
{

    private string _currentState;

    public void SwitchAnimState(AnimationModule module)
    {
        if (_currentState == module.STATE) return;

        module.Animator.Play(module.STATE);

        _currentState = module.STATE;
    }

}
