using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander
{
    public int health { get; private set; }
    public int gold { get; private set; }
    public int wave { get; private set; } = 1;

    public Commander(int health, int gold)
    {
        this.health = health;
        this.gold = gold;
    }

    public void SubtractHealth(int damage)
    {
        health = Mathf.Max(0, health - damage);
    }

    public void AddGold(int gold)
    {
        this.gold += gold;
    }

    public void SubtractGold(int gold)
    {
        this.gold = Mathf.Max(0, this.gold - gold);
    }

    public bool CanBuy(int gold)
    {
        return this.gold >= gold;
    }
}
