using UnityEngine;

public class Ladder : MonoBehaviour
{

    private bool _canClimb;
    private bool _climbing;

    private CharacterOwner _player;

    private void Awake()
    {
        _player = FindObjectOfType<CharacterOwner>();
    }

    private void Update()
    {
        if (!_canClimb) return;

        if (_player.Input.InteractInput()) _climbing = true;

        if (_climbing)
        {

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _canClimb = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _canClimb = false;
        }
    }
}
