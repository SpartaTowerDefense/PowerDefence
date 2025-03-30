using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurTurretInfo : Shop
{
    Image image;
    [SerializeField] private TextMeshProUGUI cannonLv;
    [SerializeField] private TextMeshProUGUI bodyLv;

    void SetCurTurretInfo()
    {
        Turret turret = UIManager.Instance.curTurret;
        //image.sprite = turret.bodySpr;
        //cannonLv.text = turret.
        bodyLv.text = turret.TurretStat.Level.ToString();
    }
}
