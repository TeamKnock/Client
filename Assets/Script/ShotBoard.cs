using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBoard : MonoBehaviour {

    HighScoreDungeon manager;

    public void SetScoreManager(HighScoreDungeon _hd) {
        manager = _hd;
    }

    void OnCollisionEnter(Collision col) {
        col.gameObject.SetActive(false);
        manager.AddScore(10);
    }

}
