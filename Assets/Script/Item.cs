using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템의 최상위 클래스 입니다.
/// </summary>
[System.Serializable]
public class Item
{
    /// <summary>
    /// 이름
    /// </summary>
    public string name;
    /// <summary>
    /// 고유 아이디
    /// </summary>
    public string id;
    /// <summary>
    /// 설명
    /// </summary>
    [TextArea]
    public string context;
    /// <summary>
    /// 아이콘
    /// </summary>
    public Sprite icon;

}

/// <summary>
/// 아이템 옵션을 추가 및 삭제 합니다.
/// </summary>
[System.Serializable]
public class ItemOption
{
    /// <summary>
    /// 옵션 리스트
    /// </summary>
    public List<Status> optionList;

    public Status FindStatus(StatusCategory _status)
    {
        for (int i = 0; i < optionList.Count; ++i)
        {
            if (optionList[i].category == _status)
                return optionList[i];
        }
        return null;

    }



}