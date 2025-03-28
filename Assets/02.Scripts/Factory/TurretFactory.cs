using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretFactory : FactoryBase
{
    private readonly string PATH = $"Scriptable\\TurretSo\\";


    private const string Black = nameof(Black);
    private const string Blue = nameof(Blue);
    private const string Green = nameof(Green);
    private const string Red = nameof(Red);
    private const string White = nameof(White);

    private TurretData[] bodyList = new TurretData[(int)Enums.TurretType.Count];
    private void Awake()
    {
        FactoryManager.Instance.path.Add(typeof(TurretFactory).Name, this);

        bodyList[(int)Enums.TurretType.Black] = (ResourceManager.Instance.LoadResource<TurretData>(Black, $"{PATH}{Black}"));
        bodyList[(int)Enums.TurretType.Blue] = (ResourceManager.Instance.LoadResource<TurretData>(Blue, $"{PATH}{Blue}"));
        bodyList[(int)Enums.TurretType.Green] = (ResourceManager.Instance.LoadResource<TurretData>(Green, $"{PATH}{Green}"));
        bodyList[(int)Enums.TurretType.Red] = (ResourceManager.Instance.LoadResource<TurretData>(Red, $"{PATH}{Red}"));
        bodyList[(int)Enums.TurretType.White] = (ResourceManager.Instance.LoadResource<TurretData>(White, $"{PATH}{White}"));
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
            obj = Instantiate(Prefab, transform);  // 매개변수를 못받았을때 새로 생성

        Turret turret = obj.GetComponent<Turret>();
        CannonController ctrl = obj.GetComponent<CannonController>();

        // 오브젝트 데이터에 덮어씌우기
        turret.Initinalize(bodyData);
        ctrl.Initinalize(bodyData);

        return obj;
    }
}
