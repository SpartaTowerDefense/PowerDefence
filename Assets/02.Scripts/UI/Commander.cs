using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander
{
    public int health { get; set; }
    public int gold { get; private set; }

    public System.Action buyAction;
    public System.Action die;

    public Commander(int health, int gold)
    {
        this.health = health;
        this.gold = gold;
    }

    public void SubtractHealth(int damage)
    {
        health = Mathf.Max(0, health - damage);
        if(health == 0)
        {
            UIManager.Instance.EndPanel.GetComponent<EndPanel>().isClear = false;
            UIManager.Instance.EndPanel.gameObject.SetActive(true);
        }
            

        if (UIManager.Instance)
            UIManager.Instance.UIDataBinder.SetUIText();
    }

    public void AddGold(int gold)
    {
        this.gold += gold;

        if (UIManager.Instance)
            UIManager.Instance.UIDataBinder.SetUIText();
    }

    public void SubtractGold(int gold)
    {
        this.gold = Mathf.Max(0, this.gold - gold);

        if (UIManager.Instance)
            UIManager.Instance.UIDataBinder.SetUIText();
    }

    public bool CanBuy(int gold)
    {
        if (this.gold > gold)
            return true;
        else
        {
            buyAction?.Invoke();
            return false;
        }
    }
}