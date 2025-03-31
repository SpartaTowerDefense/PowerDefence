using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float Money { get; private set; } = 0f;

    /// <summary>
    /// 돈을 추가하는 함수
    /// </summary>
    public void PlusMoney(float money)
    {
        Money += money;
    }

    /// <summary>
    /// 매개변수로 넘겨준 돈보다 게임매니저의 있는 돈이 더많은지 체크하는 함수
    /// </summary>
    public bool CanBuy(float money)
    {
        return Money > money;
    }

    /// <summary>
    /// 게임매니저의 돈을 매개변수만큼 소비시키는 함수
    /// </summary>
    public void MinusMoney(float money)
    {
        Money -= money;
    }
}
