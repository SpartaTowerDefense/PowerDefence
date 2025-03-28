using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFactory : FactoryBase
{
    private readonly string PATH = $"Scriptable\\TurretSo\\";


    private const string Black = nameof(Black);
    private const string Blue = nameof(Blue);
    private const string Green = nameof(Green);
    private const string White = nameof(White);

    private List<TurretData> bodyList = new();

    private void Awake()
    {
        bodyList.Add(ResourceManager.Instance.LoadResource<TurretData>(Black, $"{PATH}{Black}"));
        bodyList.Add(ResourceManager.Instance.LoadResource<TurretData>(Blue, $"{PATH}{Blue}"));
        bodyList.Add(ResourceManager.Instance.LoadResource<TurretData>(Green, $"{PATH}{Green}"));
        bodyList.Add(ResourceManager.Instance.LoadResource<TurretData>(White, $"{PATH}{White}"));
    }

    // 외부에서 클릭시 매개변수를 받아야되는데?
    public override GameObject CreateObject(GameObject obj = null, int enumType = -1)
    {
        if (enumType == -1)
        {
            Debug.Log("OutRange Array");
            return null;
        }

        // 바디 데이터를 받는다, 헤드 데이터를 받는다.
        TurretData bodyData = bodyList[enumType];

        // 매개변수로 받은 오브젝트 체킹
        if(obj == null)
            obj = Instantiate(Prefab);  // 매개변수를 못받았을때 새로 생성

        Turret turret = obj.GetComponent<Turret>();
        //바꾼 부분 
        // 오브젝트 데이터에 덮어씌우기
        turret.Initinalize(bodyData);

        return obj;
    }
}
