using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreDungeon : Dungeon
{

    public GameObject shotBoard;

    [SerializeField]
    int score = 0;

    public int boardCount = 5;

    public List<GameObject> shotBoardList;

    public void Respawn()
    {
        for (int i = 0; i < boardCount; ++i) {
            Vector3 pos = new Vector3(Random.Range(tr.position.x - size.x * 0.5f, tr.position.x + size.x * 0.5f),
                Random.Range(tr.position.y, tr.position.y + size.y),
                Random.Range(tr.position.z - size.z * 0.5f, tr.position.z + size.z * 0.5f));

            GameObject g = Instantiate(shotBoard, pos, Quaternion.identity);
            g.transform.SetParent(tr);

            shotBoardList.Add(g);
            g.transform.GetChild(0).GetComponent<ShotBoard>().SetScoreManager(this);
        }

    }

    void Start()
    {
        Respawn();
    }

    public void AddScore(int _score)
    {
        score += _score;
    }

}
