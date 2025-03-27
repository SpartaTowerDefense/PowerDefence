using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFactory : FactoryBase
{
    private readonly string PATH = $"Scriptable\\BodyDatas\\";
    private readonly string BodySO = $"{nameof(BodySO)}\\";
    private readonly string HeadSO = $"{nameof(HeadSO)}\\";


    private const string Black = nameof(Black);
    private const string Blue = nameof(Blue);
    private const string Green = nameof(Green);
    private const string White = nameof(White);

    private List<BodyData> bodyList = new();
    private List<HeadData> headList = new();

    private void Awake()
    {
        bodyList.Add(ResourceManager.Instance.LoadResource<BodyData>(Black, $"{PATH}{BodySO}{Black}"));
        bodyList.Add(ResourceManager.Instance.LoadResource<BodyData>(Blue, $"{PATH}{BodySO}{Blue}"));
        bodyList.Add(ResourceManager.Instance.LoadResource<BodyData>(Green, $"{PATH}{BodySO}{Green}"));
        bodyList.Add(ResourceManager.Instance.LoadResource<BodyData>(White, $"{PATH}{BodySO}{White}"));

        headList.Add(ResourceManager.Instance.LoadResource<HeadData>($"{PATH}{HeadSO}{Black}"));
        headList.Add(ResourceManager.Instance.LoadResource<HeadData>($"{PATH}{HeadSO}{Blue}"));
        headList.Add(ResourceManager.Instance.LoadResource<HeadData>($"{PATH}{HeadSO}{Green}"));
        headList.Add(ResourceManager.Instance.LoadResource<HeadData>($"{PATH}{HeadSO}{White}"));
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
        BodyData bodyData = bodyList[enumType];
        HeadData headData = headList[enumType];

        // 매개변수로 받은 오브젝트 체킹
        if(obj == null)
            obj = Instantiate(Prefab);  // 매개변수를 못받았을때 새로 생성

        Turret turret = GetComponent<Turret>();

        // 오브젝트 데이터에 덮어씌우기
        turret.Initinalize(bodyData, headData);

        return obj;
    }
}
