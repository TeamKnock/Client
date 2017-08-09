using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public List<GameObject> dungeonList;

    public Dungeon currentDungeon;
    public GameObject currentMap;
    public List<Dungeon> stackDungeon;
    public List<int> dungeonPointIndexList;
    public List<MapDirection> queueDir = new List<MapDirection>();

    public int maxRooms = 500;

    bool isMakeRun = false;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine(MakeDungeon(true));
    }


    IEnumerator MakeDungeon(bool isFirst)
    {
        isMakeRun = true;
        bool isEnd = true;
        bool isFail = false;
        int cnt = 0, infinityLoopCheck = 0,totalCnt = 0;
        GameObject map;
        while (isEnd)
        {
            RETRY:

            dungeonPointIndexList.Clear();
            ++infinityLoopCheck;

            if (infinityLoopCheck > 100)
            {
                isEnd = false;
                print("경우의 수가 존재하지 않습니다. 루프를 중지합니다.");
                if (totalCnt == 0)
                    print("방이 하나도 생성되지 못합니다.");
                break;

            }
            
            isFail = false;
            map = Instantiate(dungeonList[Random.Range(0, dungeonList.Count)]);
            map.name = "Room" + totalCnt;
            map.SetActive(false);

            if (currentDungeon != null)
            {
                int index = 0;
                int loopCnt = 0;
                LastPoint lastPoint = null;
                do
                {
                    if (loopCnt > 0)
                        dungeonPointIndexList.Add(index);

                    if (loopCnt > currentDungeon.lastPointList.Count)
                    {
                        if (stackDungeon.Count > 1)
                        {
                            stackDungeon.Remove(currentDungeon);
                            Destroy(currentDungeon.gameObject);
                            currentDungeon = stackDungeon[stackDungeon.Count - 1];
                            currentMap = currentDungeon.gameObject;
                            dungeonPointIndexList.Clear();
                            --cnt;
                            loopCnt = 0;
                        }
                        else {
                            isFail = true;
                            break;
                        }
                    }
                    index = Random.Range(0, currentDungeon.lastPointList.Count);
                    lastPoint = currentDungeon.lastPointList[index];
                    ++loopCnt;
                } while (CheckLastPoint(index, lastPoint, currentDungeon, map.GetComponent<Dungeon>())); // 


                if (!map.GetComponent<Dungeon>().isAlreadyDir(lastPoint.direction) || isFail)
                {

                    Destroy(map);
                    goto RETRY;
                }

                if (isEnd)
                {
                    map.transform.position = currentMap.transform.position + lastPoint.ConvertVector3() * (currentDungeon.GetSize(lastPoint.direction) * 0.5f + map.GetComponent<Dungeon>().GetSize(lastPoint.direction) * 0.5f);
                    map.GetComponent<Dungeon>().FindLastPointAndRemove(lastPoint.direction);
                    lastPoint.Remove();

                    queueDir.Add(lastPoint.direction);

                    if (queueDir.Count > 0)
                        queueDir.RemoveAt(0);

                }
            }

            if (isEnd)
            {
                currentDungeon = map.GetComponent<Dungeon>();
                currentMap = map;
                stackDungeon.Add(currentDungeon);
            }

            ++cnt;
            ++totalCnt;
            infinityLoopCheck = 0;
            map.SetActive(true);

            if (cnt > maxRooms - 1)
            {
                isEnd = false;
                print("is all made");
            }

            yield return new WaitForFixedUpdate();


        }

        stackDungeon.Clear();

        if (isFirst)
            PlayGame();


        isMakeRun = false;
    }

    bool FindIndex(int index)
    {
        for (int i = 0; i < dungeonPointIndexList.Count; i++)
        {
            if (dungeonPointIndexList[i] == index)
                return true;
        }
        return false;
    }

    bool FindDir(MapDirection _dir)
    {
        if (queueDir.Count == 0)
            return false;

        for (int i = 0; i < queueDir.Count; i++)
        {
            if (queueDir[i] == _dir)
                return true;
        }
        return false;
    }

    bool CheckLastPoint(int _index, LastPoint _point, Dungeon _currentDungeon, Dungeon _nextDungeon)
    {
        if (!FindIndex(_index))
        {
            if (!_point.CheckDir())
            {
                if (!FindDir(_point.direction))
                {
                    if (!currentDungeon.CheckRoomSize(_point, _currentDungeon, _nextDungeon))
                    {
                        return false;
                    }
                    else
                        return true;
                }
                else
                    return true;
            }
            else
                return true;
        }
        else
            return true;

    }

    void PlayGame()
    {
        CameraController.instance.SetCursor();
    }

    public void StartMakeDungeon()
    {
        if (!isMakeRun)
            StartCoroutine(MakeDungeon(false));
    }

}
