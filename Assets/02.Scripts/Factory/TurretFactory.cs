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

    // �ܺο��� Ŭ���� �Ű������� �޾ƾߵǴµ�?
    public override GameObject CreateObject(GameObject obj = null, int enumType = -1)
    {
        if (enumType == -1)
        {
            Debug.Log("OutRange Array");
            return null;
        }

        // �ٵ� �����͸� �޴´�, ��� �����͸� �޴´�.
        TurretData bodyData = bodyList[enumType];

        // �Ű������� ���� ������Ʈ üŷ
        if(obj == null)
            obj = Instantiate(Prefab);  // �Ű������� ���޾����� ���� ����

        Turret turret = GetComponent<Turret>();

        // ������Ʈ �����Ϳ� ������
        turret.Initinalize(bodyData);

        return obj;
    }
}
