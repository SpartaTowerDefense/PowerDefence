using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUpgrade : Shop
{
    [SerializeField] TextMeshProUGUI description;

    public void UpgradeCannon()
    {
        CannonController cannonController = UIManager.Instance.curTurret.GetComponent<CannonController>();
    }

    public void UpgaradeBody()
    {

    }

}
