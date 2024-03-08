using UnityEngine;
using NaughtyAttributes;
using Autohand;
using Obvious.Soap;
using Lean.Pool;

public class OmnitrixRecycler : MonoBehaviour
{
    [Header("Dependencies")]
    [Required][SerializeField] private PlacePoint _placePoint;
    [Required][SerializeField] private GameObject _grabbableItem;
    [Required][SerializeField] private IntVariable _itemCount;

    private GameObject _grabbableObject;
    private Grabbable _objectGrabbable;

    private void OnEnable()
    {
        _placePoint.OnRemoveEvent += RecycleItem;
        _itemCount.OnValueChanged += UpdateItemAvailability;
        UpdateItemAvailability(_itemCount.Value);
    }

    private void OnDisable()
    {
        _placePoint.OnRemoveEvent -= RecycleItem;
        _itemCount.OnValueChanged -= UpdateItemAvailability;
    }

    private void Update()
    {
        ForcePlacement();
    }

    private void RecycleItem(PlacePoint placePoint, Grabbable grabbable)
    {
        //TODO Activate ghrabbed item to be useabble
        SpawnNewItem();
        PlaceNewItem(_objectGrabbable);
        _itemCount.Value--;
    }

    private void SpawnNewItem()
    {
        _grabbableObject = LeanPool.Spawn(_grabbableItem);
        _grabbableObject.HasGrabbable(out _objectGrabbable);
    }

    private void PlaceNewItem(Grabbable grabbable)
    {
        _placePoint.Place(grabbable);
    }

    
    private void UpdateItemAvailability(int newValue)
    {
        if (_placePoint.GetGrabbable() == null) return;
        if(newValue == 0)
        {
            _placePoint.GetGrabbable().isGrabbable = false;
        } else
        {
            _placePoint.GetGrabbable().isGrabbable = true;
        }
    }

    private void ForcePlacement()
    {
        if (_placePoint.GetGrabbable() != null) return;
        if (_grabbableObject == null) SpawnNewItem();
        PlaceNewItem(_objectGrabbable);
        UpdateItemAvailability(_itemCount.Value);
    }

    [Button]
    private void TestRemove()
    {
        Grabbable testGrabbable = _placePoint.placedObject;
        _placePoint.Remove(testGrabbable);
    }
}
