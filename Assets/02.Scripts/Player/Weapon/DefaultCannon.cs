using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DefaultCannon : CannonBase
{
    private Transform tp;
    public DefaultCannon(Sprite sprite, Transform tip, CannonController controller) : base(sprite, tip, controller)
    {
        data.Inintionalize(1, 3, false);
    }

    public override void Fire(Vector3 targetPos)
    {
        if (time > 0f)
            return;

        GameObject bullet = null;

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

        //발사 쿨타임 추가
        time = fireColldown;
        controller.DetectEnemy.SelectEnemy();
    }
}
