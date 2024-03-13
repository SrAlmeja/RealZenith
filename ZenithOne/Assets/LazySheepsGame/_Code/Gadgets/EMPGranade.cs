using com.LazyGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPGranade : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private LayerMask _interactWithLayers;

    [SerializeField] private TypeOfGadget _typeOfGadget;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == _interactWithLayers)
        {
            ExplodeShpereCast();
        }
    }

    private void ExplodeShpereCast()
    {
        //Physics.SphereCast
    }
}
