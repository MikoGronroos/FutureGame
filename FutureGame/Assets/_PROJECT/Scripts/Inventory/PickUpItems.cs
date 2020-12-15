using UnityEngine;

public class PickUpItems : MonoBehaviour
{

    [SerializeField] private float handLength;

    private CharacterOwner _charOwner;
    private Camera _camera;

    private void Awake()
    {
        _charOwner = GetComponent<CharacterOwner>();
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_charOwner.Input.InteractInput())
        {
            Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out var hit))
            {
                if (Vector3.Distance(transform.position, hit.transform.position) > handLength) return;

                var hittedItem = hit.transform.GetComponent<ItemOnGround>();

                if (!hittedItem) return;

                _charOwner.Inventory.AddItem(hittedItem.GetID());
                Destroy(hittedItem.gameObject);
            }
        }
    }

}
