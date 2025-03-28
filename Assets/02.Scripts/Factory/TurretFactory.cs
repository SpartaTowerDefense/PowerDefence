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
