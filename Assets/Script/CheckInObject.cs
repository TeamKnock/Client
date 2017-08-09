using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckInObject : MonoBehaviour
{

    public GameObject realObject;

    public List<LayerMask> checkLayers;

    void OnTriggerEnter(Collider col)
    {


        if (!CheckLayers(1 << col.gameObject.layer))
            return;

        realObject.SetActive(true);
        realObject.transform.SetParent(null);
        gameObject.SetActive(false);
    }



    bool CheckLayers(int layer)
    {
        for (int i = 0; i < checkLayers.Count; ++i)
        {
            if (checkLayers[i].value == layer)
            {
                return true;
            }
        }
        return false;
    }



}
