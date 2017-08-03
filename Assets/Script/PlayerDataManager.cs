using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 클라이언트 유저의 정보가 저장되는 매니저 입니다. 
/// 유저 데이터 외의 정보도 함께 관리 됩니다.
/// </summary>
public class PlayerDataManager : MonoBehaviour {
   
    public static PlayerDataManager instance;

    public User my;

    public void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);

        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }
}
