using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopDescription : Shop
{
    [SerializeField] TextMeshProUGUI price;
    [SerializeField] TextMeshProUGUI description;

    void TextSet(TurretData turretData)
    {
        price.text = turretData.Price.ToString();
    }
}
