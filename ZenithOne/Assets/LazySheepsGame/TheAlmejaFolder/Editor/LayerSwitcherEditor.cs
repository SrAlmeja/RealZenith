using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LayerSwitcher))]
public class LayerSwitcherEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LayerSwitcher layerSwitcher = (LayerSwitcher)target;

        // Dibuja un botón en el inspector
        if (GUILayout.Button("Switch Layer"))
        {
            layerSwitcher.TheSwitcher();
        }
    }
}