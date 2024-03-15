using UnityEngine;
using TMPro;

public class RandomCodeGenerator : MonoBehaviour
{
        public TextMeshProUGUI  codeText;
        
        [Range(1, 6)]
        public int codeLength;

        private void Start()
        {
            GenerateRandomCode(); 
        }

        private void GenerateRandomCode()
        {
            string code = "";
            for (int i = 0; i < codeLength; i++)
            {
                code += Random.Range(1, 5).ToString(); 
            }
            Debug.Log("Código generado: " + code);
    
            if (codeText != null)
            {
                codeText.text = "" + code; 
            }
            else
            {
                Debug.LogWarning("No se ha asignado un objeto TextMeshPro para mostrar el código.");
            }
        }
}
