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
    private const string White = nameof(White);

    private List<TurretData> bodyList = new();

    private void Awake()
    {
        FactoryManager.Instance.path.Add(typeof(TurretFactory).Name, this);

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
            obj = Instantiate(Prefab, transform);  // �Ű������� ���޾����� ���� ����

        Turret turret = obj.GetComponent<Turret>();
        CannonController ctrl = obj.GetComponent<CannonController>();

        // ������Ʈ �����Ϳ� ������
        turret.Initinalize(bodyData);
        ctrl.Initinalize(bodyData);

        return obj;
    }
}
