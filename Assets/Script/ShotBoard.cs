using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBoard : MonoBehaviour {

    HighScoreDungeon manager;

    public List<LayerMask> checkLayers;

    public void SetScoreManager(HighScoreDungeon _hd) {
        manager = _hd;
    }

    void OnCollisionEnter(Collision col) {
        if (!CheckLayers(1 << col.gameObject.layer))
            return;

        col.gameObject.SetActive(false);
        manager.AddScore(10);
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
