using NaughtyAttributes;
using Obvious.Soap;
using UnityEngine;

public class OmnitrixFresnelColor : MonoBehaviour
{
    [Header("SO Dependencies")]
    [Required][SerializeField] private IntVariable _itemCount;

    [Header("Dependencies")]

    [Required][SerializeField] private MeshRenderer _renderer;
    [Required][SerializeField] private Material _depletedMaterial;
    [Required][SerializeField] private Material _lowMaterial;
    [Required][SerializeField] private Material _mediumMaterial;
    [Required][SerializeField] private Material _highMaterial;

    [Header("Settings")]
    [SerializeField] private int _depletedValue = 0;
    [SerializeField] private int _lowValue = 1;
    [SerializeField] private int _mediumValue = 2;
    [SerializeField] private int _highValue = 3;

    private void OnEnable()
    {
        _itemCount.OnValueChanged += UpdateMaterialOnValue;
        UpdateMaterialOnValue(_itemCount.Value);
    }

    private void OnDisable()
    {
        _itemCount.OnValueChanged -= UpdateMaterialOnValue;
    }

    private void UpdateMaterialOnValue(int newValue)
    {
        if (newValue <= _depletedValue && newValue < _lowValue)
        {
            _renderer.material = _depletedMaterial;
        }

        if (newValue >= _lowValue && newValue < _mediumValue)
        {
            _renderer.material = _lowMaterial;
        }

        if (newValue >= _mediumValue && newValue < _highValue)
        {
            _renderer.material = _mediumMaterial;
        }

        if (newValue >= _highValue)
        {
            _renderer.material = _highMaterial;
        }
    }
}
