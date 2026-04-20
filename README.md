# ⚔️ PowerDefence

> Unity 2022.3으로 개발한 2D 탑디펜스 게임 (공부용 프로젝트)

---

## 📌 프로젝트 개요

적이 웨이포인트 경로를 따라 이동해 오면, 플레이어가 터렛을 배치·업그레이드하며 막아내는 **2D 탑디펜스 게임**입니다.  
디자인 패턴(Singleton, Factory, Object Pool)과 Unity의 ScriptableObject 활용 등 게임 개발 기초를 공부하기 위해 제작되었습니다.

---

## 🎮 게임 플레이

| 항목 | 내용 |
|------|------|
| 장르 | 2D 탑디펜스 (Top-Down) |
| 스테이지 | 2개 |
| 시작 자원 | HP 20 / Gold 5,000 |
| 목표 | 적이 목적지에 도달하기 전에 모두 처치 |
| 게임 오버 | HP 0 (적이 목적지 도달 시 HP 감소) |

### 기본 흐름

1. 골드로 터렛을 구매해 타일 맵에 배치
2. 배치 전 회전(Rotate) 가능
3. 적 처치 시 골드 획득 → 터렛 업그레이드
4. 모든 웨이브 격파 → 스테이지 클리어

---

## 🗼 터렛 시스템

### 터렛 종류 (5가지)

| 타입 | 특성 |
|------|------|
| Black | 기본 공격 |
| Blue | 적 빙결(이동속도 감소) |
| Red | 도트 화염 데미지 (Burning) |
| Green | 처치 시 코인 보너스 |
| White | 넉백 효과 |

### 캐논(무기) 업그레이드 경로

| 레벨 | 캐논 종류 | 특징 |
|------|----------|------|
| 1 | Default Cannon | 단발, 사거리 3 |
| 2 | Triple Cannon | 3발, 사거리 2 |
| 3 | Splash Cannon | 3발 + 범위 피해 |
| 4 | Penetration Cannon | 3발, 관통 |
| 5 | Melee Cannon | 근접 고데미지 |

- 터렛 본체와 캐논은 **각각 독립적으로 레벨업** 가능
- 업그레이드 비용은 기존 가격 × 1.2배 증가

---

## 👾 적 시스템

적은 `SpawnPatternData`(ScriptableObject)에 정의된 웨이브 패턴에 따라 스폰됩니다.

### 상태 이상

| 상태 | 설명 |
|------|------|
| Frozen | 이동속도 감소, 파란색 |
| Burning | 도트 데미지, 빨간색 |
| Knockback | 뒤로 밀려남 |
| Dead | 처치 완료 |

---

## 🏗️ 아키텍처

### 사용된 디자인 패턴

| 패턴 | 적용 대상 |
|------|----------|
| Singleton | GameManager, UIManager, DataManager, AudioManager, ResourceManager, FactoryManager, ObjectPoolManager |
| Factory | BulletFactory, EnemyFactory, TurretFactory |
| Object Pool | 총알·적 오브젝트 재사용으로 성능 최적화 |
| ScriptableObject | TurretData, EnemyData, StagePatternData |

### 폴더 구조

```
Assets/
├── 01.Scenes/          # 게임 씬 (MainScene, Test)
├── 02.Scripts/         # C# 스크립트
│   ├── Bullets/        # 투사체 로직
│   ├── Enemy/          # 적 AI, 스폰, 웨이포인트
│   ├── Factory/        # 팩토리 & 오브젝트 풀
│   ├── Player/         # Commander (플레이어 자원 관리)
│   ├── Scriptable/     # ScriptableObject 정의
│   ├── Singleton/      # 싱글톤 매니저들
│   ├── Tank/           # 터렛 & 캐논 컨트롤러
│   └── UI/             # UI 매니저 & 패널
├── 03.Sprites/
├── 04.Prefabs/
├── 05.Animations/
├── 06.Audios/
└── Resources/          # 런타임 로드 에셋
```

---

## 🛠️ 기술 스택

| 항목 | 내용 |
|------|------|
| 엔진 | Unity 2022.3.17f1 |
| 언어 | C# |
| 애니메이션 | DOTween (Demigiant) |
| UI 텍스트 | TextMesh Pro |
| 입력 | Unity New Input System |
| 저장 | JSON 직렬화 (DataManager) |

---

## 💾 저장 시스템

`DataManager` 싱글톤이 JSON으로 게임 데이터를 저장·불러옵니다.

```csharp
// 저장 항목
SaveData {
    int gold;   // 보유 골드
    int stage;  // 현재 스테이지
}
```

## 📚 학습 목표

- Unity 게임 개발 기초 (씬 관리, 타일맵, 물리)
- C# 디자인 패턴 실습 (Singleton, Factory, Object Pool)
- ScriptableObject를 이용한 데이터 분리
- 오브젝트 풀링을 통한 성능 최적화
- UI 시스템 및 이벤트 연동

---

## 👥 팀

스파르타 탑디펜스 팀 프로젝트
