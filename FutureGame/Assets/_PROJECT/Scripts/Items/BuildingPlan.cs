using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlan : MonoBehaviour
{

    [SerializeField] private bool canBuild;
    [SerializeField] private bool hasBuildingSelected;

    [SerializeField] private LayerMask buildableMask;
    [SerializeField] private LayerMask unbuildableMask;
    [SerializeField] private float overlapSphereRadious;
    
    [SerializeField] private GameObject currentlyInspecting;

    private GameObject _realObject;
    private Camera _camera;
    private CharacterOwner _charOwner;
    private BuildingUI _buildingUI;
    private Vector2[] _neededItems;

    private int fingerID = -1;

    public bool CanBuild { get { return canBuild; } set { canBuild = value; } }

    private void OnEnable()
    {
        _buildingUI = FindObjectOfType<BuildingUI>();
        _buildingUI.SetEnabledValue(true);
        _camera = Camera.main;
        _charOwner = CharacterOwner.Instance;
        canBuild = true;
    }

    private void OnDisable()
    {
        SetHasObject(false);
        _buildingUI.SetEnabledValue(false);
        Destroy(currentlyInspecting);
    }

    private void Update()
    {

        if (EventSystem.current.IsPointerOverGameObject(fingerID))
        {
            return;
        }

        if (!hasBuildingSelected) return;

        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out var hit, 10f, buildableMask))
        {

            currentlyInspecting.transform.position = hit.point + new Vector3(0, currentlyInspecting.transform.lossyScale.y / 2, 0);

            if (!canBuild) return;

            Collider[] hitColliders = Physics.OverlapSphere(currentlyInspecting.transform.position, overlapSphereRadious, unbuildableMask);

            if (hitColliders.Length > 0) return;

            if (_charOwner.Input.HitInput())
            {
                for (int i = 0; i < _neededItems.Length; i++)
                {
                    if (!Inventory.Instance.CheckIfInventoryHasAmountOfItems((int)_neededItems[i].x, (int)_neededItems[i].y))
                    {
                        return;
                    }
                }

                for (int i = 0; i < _neededItems.Length; i++)
                {
                    Inventory.Instance.RemoveMultipleItemsFromInventoryWithId((int)_neededItems[i].x, (int)_neededItems[i].y);
                }

                PlaceObject(_realObject, currentlyInspecting.transform.position);
            }
        }
    }

    public void ChangeInspectObject(GameObject obj)
    {
        if (currentlyInspecting != null)
        {
            Destroy(currentlyInspecting.gameObject);
        }

        GameObject inspectObj = Instantiate(obj);
        currentlyInspecting = inspectObj;
    }

    private void PlaceObject(GameObject obj, Vector3 pos)
    {
        GameObject placedObject = Instantiate(obj, pos, Quaternion.identity);
    }

    public void SetHasObject(bool value)
    {
        hasBuildingSelected = value;
    }

    public void SetRealObject(GameObject obj)
    {
        _realObject = obj;
    }

    public void SetNeededItems(Vector2[] array)
    {
        _neededItems = array;
    }

    private void OnDrawGizmos()
    {
        if (currentlyInspecting == null) return;

        Gizmos.DrawSphere(currentlyInspecting.transform.position, overlapSphereRadious);
    }

}
