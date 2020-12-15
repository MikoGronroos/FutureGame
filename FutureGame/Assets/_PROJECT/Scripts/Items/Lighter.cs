using UnityEngine;

public class Lighter : MonoBehaviour
{

    private CharacterOwner _charOwner;

    private bool _isEnabled;

    private void Awake()
    {
        _charOwner = CharacterOwner.Instance;
    }

    private void Update()
    {
        if (_charOwner.Input.DefendInput())
        {
            if (_isEnabled)
            {
                _isEnabled = false;
            }
            else if(!_isEnabled)
            {
                _isEnabled = true;
            }
        }
    }
}
