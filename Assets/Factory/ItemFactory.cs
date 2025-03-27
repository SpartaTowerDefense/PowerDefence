using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : FactoryBase
{
    // 데이터 프리펩
    private List<EquipData> dataList = new();

    private const string path = "Scriptable\\Item";
    // 아이템 생성에 필요한것
    // 아이템 데이터, 아이템 프리펩 ( 원본 객체 )

    // 리소스 매니저에서 프리펩을 가져오고
    // 그 프리펩 안에 있는 Item 스크립트에 접근
    // 접근 후, ItemData 교체

    // 아이템 데이터관한 것들은 어웨이크에서 데이터 로드
    private void Awake()
    {
        dataList.Add(ResourceManager.Instance.LoadResource<WeaponData>("Hammer", $"{path}\\Hammer"));
        dataList.Add(ResourceManager.Instance.LoadResource<WeaponData>("WoodSword", $"{path}\\WoodSword"));
        dataList.Add(ResourceManager.Instance.LoadResource<ArmorData>("RockArmor", $"{path}\\RockArmor"));
        dataList.Add(ResourceManager.Instance.LoadResource<ArmorData>("WoodArmor", $"{path}\\WoodArmor"));
    }

    private void Start()
    {
        FactoryManager.Instance.path[typeof(ItemFactory).Name] = this;
    }

    // 매개변수로 받는 오브젝트가 있으면 그걸 씌우기
    public override GameObject CreateObject(GameObject obj = null)
    {
        EquipData newData;
        Type type;
        if ((newData = GetRandomItemData(out type)) == null)
            return null;


        if(obj == null)
            obj = Instantiate(Prefab, this.transform);

        obj.GetComponent<Item>().itemData = newData;

        return obj;
    }

    private EquipData GetRandomItemData(out Type type)
    {
        if (dataList.Count > 0)
        {
            int rand = UnityEngine.Random.Range(0, dataList.Count);

            type = dataList[rand].GetType();
            return dataList[rand];
        }

        type = null;
        return null;
    }
}
