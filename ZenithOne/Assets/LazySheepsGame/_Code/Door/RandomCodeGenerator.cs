using System;
using System.Collections.Generic;
using Meta.WitAi.Utilities;
using UnityEngine;
using UnityEngine.UI;
using Obvious.Soap;
using Random = UnityEngine.Random;

public class RandomCodeGenerator : MonoBehaviour
{
        [Range(1, 6)]
        [SerializeField] private int _codeLength;
        [SerializeField] private List<Image> _arrowImages;
        [SerializeField] private GenericDataChannelSO _doorCodeEvent;
        [SerializeField] private GameObject _door;
        private int stringConfirm = 0;
        [SerializeField] private List<GameObject> buttons;
        
        private string _doorCode;

        private void Awake()
        {
            for (int j = 0; j < 6; j++) 
            {
                _arrowImages[j].gameObject.transform.GetChild(0).gameObject.SetActive(false);
                _arrowImages[j].gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            GenerateRandomCode();
            _doorCodeEvent.StringEventGO += CheckCode;
        }

        private void Update()
        {
            if (stringConfirm == _codeLength)
            {
                _door.SetActive(false);
                buttons.ForEach(button => button.SetActive(false));
                enabled = false;
            }
        }

        private void GenerateRandomCode()
        {
            _doorCode = "";
            for (int i = 0; i < _codeLength; i++)
            {
                _doorCode += Random.Range(1, 5).ToString(); 
                _arrowImages[i].gameObject.SetActive(true);
                string arrow = _doorCode.Substring(i, 1);
                switch (int.Parse(arrow))
                {
                    case 1:
                        _arrowImages[i].transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    case 2:
                        _arrowImages[i].transform.rotation = Quaternion.Euler(0, 0, 90);
                        break;
                    case 3:
                        _arrowImages[i].transform.rotation = Quaternion.Euler(0, 0, 180);
                        break;
                    case 4:
                        _arrowImages[i].transform.rotation = Quaternion.Euler(0, 0, 270);
                        break;
                }
            }
            
        }

        private void CheckCode(string code, GameObject checkObject)
        {
            Debug.Log("Button Pressed");
            if(this.gameObject == checkObject)
            {
                string arrow = _doorCode.Substring(stringConfirm, 1);
                if(arrow == code)
                {
                    _arrowImages[stringConfirm ].gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    stringConfirm++;
                }
                else
                {
                    for (int j = 0; j < 6; j++) 
                    {
                        _arrowImages[j].gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    stringConfirm = 0;
                }
            }
        }
}
