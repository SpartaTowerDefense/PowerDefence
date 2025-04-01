using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DefaultCannon : CannonBase
{
    private Transform tp;
    public DefaultCannon(Sprite sprite, Transform tip, CannonController controller) : base(sprite, tip, controller)
    {
        clip = resource.LoadResource<AudioClip>($"{Enums.FireClip}1");
        data.Inintionalize(1, 0, false, 3f);
        controller.DetectEnemy.SetRange(3f);
        tp = tip;
    }

    GameObject bullet = null;

    public override void Fire(Vector3 targetPos)
    {
        base.Fire(targetPos);

        if (time > 0f)
            return;
        //오브젝트 풀에서 객체 가져오기
        bullet = ObjectPoolManager.Instance.GetObject<BulletFactory>(1);
        // 객체에 잇는 스크립트 정보 가져오기
        Bullet bul = bullet.GetComponent<Bullet>();
        // 탄환에 현재 컨트롤러 정보 넘기기
        bul.controller = this.controller;
        bul.SplashRatio = data.SplashRatio;
        // 포지션 동기화
        bullet.transform.position = tp.position;

        //각도구하기
        Vector2 lookPos = targetPos - tp.position;
        float rotz = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, rotz + 90f);

        //발사
        bul.rb.gravityScale = 0f;
        bul.rb.AddForce((targetPos - bullet.transform.position).normalized * bul.bulletSpeed, ForceMode2D.Impulse);

        //발사시 쿨타임 추가
        time = fireColldown;
        //controller.DetectEnemy.SelectEnemy();
    }
}
