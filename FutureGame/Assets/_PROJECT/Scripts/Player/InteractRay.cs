using UnityEngine;

public class InteractRay : MonoBehaviour
{

    private Camera _camera;
    private RaycastHit _previousHit;
    private CharacterOwner _charOwner;

    private Transform hitsTransform;

    private void Awake()
    {
        _charOwner = GetComponent<CharacterOwner>();
        _camera = Camera.main;
    }

    private void Update()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out var hit))
        {

            hitsTransform = hit.transform;

            if (hitsTransform.GetComponent<IInteractable>() == null) return;

            if (_previousHit.transform != null)
            {
                if (_previousHit.transform.GetInstanceID() == hit.transform.GetInstanceID())
                {
                    if (_charOwner.Input.InteractInput())
                    {
                        hitsTransform.GetComponent<IInteractable>().Interact();
                    }
                    return;
                }
                else
                {
                    _previousHit = hit;
                }
            }
            else
            {
                _previousHit = hit;
            }
        }
    }
}
