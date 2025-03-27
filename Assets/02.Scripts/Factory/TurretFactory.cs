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

    // �ܺο��� Ŭ���� �Ű������� �޾ƾߵǴµ�?
    public override GameObject CreateObject(GameObject obj = null, int enumType = -1)
    {
        if (enumType == -1)
        {
            Debug.Log("OutRange Array");
            return null;
        }

        // �ٵ� �����͸� �޴´�, ��� �����͸� �޴´�.
        BodyData bodyData = bodyList[enumType];
        HeadData headData = headList[enumType];

        // �Ű������� ���� ������Ʈ üŷ
        if(obj == null)
            obj = Instantiate(Prefab);  // �Ű������� ���޾����� ���� ����

        Turret turret = GetComponent<Turret>();

        // ������Ʈ �����Ϳ� ������
        turret.Initinalize(bodyData, headData);

        return obj;
    }
}
