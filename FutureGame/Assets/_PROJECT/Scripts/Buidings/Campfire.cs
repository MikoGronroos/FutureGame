using UnityEngine;

public class Campfire : MonoBehaviour, IInteractable
{

    [SerializeField] private ParticleSystem campfireParticles;

    private bool _isEnabled;

    private void Awake()
    {
        campfireParticles.Stop();
    }

    public void Interact()
    {
        if (_isEnabled)
        {
            campfireParticles.Stop();
            _isEnabled = false;
        }
        else if (!_isEnabled)
        {
            campfireParticles.Play();
            _isEnabled = true;
        }
    }
}
