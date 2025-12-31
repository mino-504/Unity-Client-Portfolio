# Unity Client Portfolio

## 📌 프로젝트 개요
본 프로젝트는 **Unity 2D 기반 싱글 플레이 게임 클라이언트 포트폴리오**입니다.  
그래픽이나 연출보다는 **게임 클라이언트 시스템 설계 및 구현 역량**을 보여주는 것을 목표로 합니다.

무경험 상태에서 시작하여,  
플레이어 제어 → 시스템 분리 → AI → 전투 → UI → 데이터 기반 구조로  
**클라이언트 구조를 단계적으로 확장하는 과정**을 기록합니다.

---

## Phase 1 – Player Control System
### 구현 기능
- 키보드 입력(WASD)을 이용한 플레이어 이동
- `Time.deltaTime`을 사용한 프레임 독립 이동
- 마우스 위치를 기준으로 한 플레이어 회전
- Screen 좌표 → World 좌표 변환 처리

### 핵심 스크립트
- `PlayerController.cs`

### 주요 구현 포인트
- Update 루프 기반 입력 처리 구조
- 입력(Input) → 로직 → 결과 흐름 이해
- 대각선 이동 시 속도 보정을 위한 벡터 정규화
- `Mathf.Atan2`를 활용한 2D 회전 처리

---

## Phase 2 – Player System Separation

### 목표
플레이어 로직을 하나의 스크립트에서 처리하는 구조에서 벗어나,  
**입력, 이동, 회전을 책임 단위로 분리**하여  
확장성과 유지보수를 고려한 클라이언트 구조로 개선하는 것을 목표로 합니다.

---

### 구조 설계
플레이어는 단일 클래스가 아닌, 여러 컴포넌트의 조합으로 구성됩니다.

Player
├ PlayerInput : 입력 수집 전담
├ PlayerMovement : 이동 처리 전담
├ PlayerRotation : 회전 처리 전담
└ PlayerController : 플레이어 구성 관리

각 컴포넌트는 **하나의 책임만 가지며**,  
필요한 데이터만 참조하고 구현 로직에는 직접 관여하지 않습니다.

---

### 주요 구현 내용

#### PlayerInput
- 키보드 및 마우스 입력 수집
- 이동 입력과 마우스 월드 좌표를 값으로 제공
- 이동/회전 로직과 완전히 분리된 입력 전담 컴포넌트

#### PlayerMovement
- `PlayerInput`에서 제공하는 이동 입력 값 참조
- 방향 벡터 정규화를 통한 대각선 이동 속도 보정
- `Time.deltaTime` 기반 프레임 독립 이동 처리

#### PlayerRotation
- `PlayerInput`에서 제공하는 마우스 월드 좌표 참조
- `Mathf.Atan2`를 사용한 2D 회전 각도 계산
- Z축 기준 회전 처리

#### PlayerController
- 직접적인 이동/회전 로직을 포함하지 않는 관리자 역할
- `RequireComponent`를 사용하여 필수 컴포넌트 구성 보장

---

### 설계 포인트
- 입력(Input)과 동작(Behavior)의 명확한 분리
- 컴포넌트 간 의존성 방향 단순화 (순환 참조 없음)
- 기능 확장(전투, AI, 스킬) 시 구조 변경 최소화 가능

---

## Phase 3 – Enemy AI (FSM)

### 목표
적의 행동을 단순 조건문이 아닌 **FSM(Finite State Machine)** 구조로 설계하여,  
상태 기반으로 행동이 결정되도록 구현했습니다.

---

### 구조 설계
Enemy는 Controller(관리자)와 Movement(이동 처리)를 분리하고,  
상태 및 상태 전환은 순수 C# 클래스 기반으로 구성했습니다.

Enemy
├ EnemyController
├ EnemyMovement
├ EnemyStateMachine
└ States
├ EnemyIdleState
└ EnemyChaseState

---

### 구현 내용
- **Idle ↔ Chase 상태 전환**
  - 플레이어가 감지 범위(`detectRange`) 안으로 들어오면 Chase 전환
  - 플레이어가 이탈 범위(`loseRange`) 밖으로 나가면 Idle 복귀
- **Hysteresis 적용**
  - `detectRange < loseRange` 설정으로 상태 떨림 방지
- **상태 기반 이동 처리**
  - Chase 상태에서만 이동 수행
  - Idle 상태에서는 이동 정지

---

### 설계 포인트
- 상태(State)와 Unity 컴포넌트(MonoBehaviour)를 분리
- 상태 추가 시 기존 구조 수정 없이 확장 가능

---

## Phase 4 – Combat System & Interaction

### 목표
플레이어와 적 간의 **전투 상호작용 시스템**을 구현하고,  
AI 상태와 공격 로직이 유기적으로 연결되도록 설계했습니다.

---

### 구조 설계
Enemy
├ EnemyController
├ EnemyMovement
├ EnemyAttack
├ EnemyHealth
└ States
├ EnemyIdleState
├ EnemyChaseState
└ EnemyAttackState

---

### 구현 내용
- **Attack 상태 추가**
  - 공격 범위 진입 시 Chase → Attack 전환
  - 이탈 범위 초과 시 Attack → Chase 복귀
- **공격 쿨타임 시스템**
  - 일정 시간 간격으로만 공격 가능
- **IDamageable 인터페이스 도입**
  - Player / Enemy 공통 피격 처리 구조
- **피격 처리**
  - 체력 감소
  - 체력 0 이하 시 오브젝트 비활성화
- **물리 충돌 안정화**
  - Rigidbody2D를 Kinematic으로 설정하여
    비물리 이동 구조에서 발생하던 밀림 및 판정 오류 해결

---

### 설계 포인트
- 공격 판정과 AI 상태 로직 분리
- 물리 엔진 의존 최소화
- 시스템 중심 전투 구조 설계

---

## 🛠️ 사용 기술
- Unity 2022.3 LTS
- C#
- Git / GitHub

---

## 📈 개발 로드맵
- Phase 5: 플레이어 공격 및 UI 피드백
- Phase 6: 데이터 기반 구조 및 포트폴리오 정리

---

## 💡 프로젝트 방향성
본 프로젝트는 단순한 기능 구현을 넘어,  
**게임 클라이언트 프로그래머로서의 사고 과정과 구조 설계 능력**을 보여주는 것을 목표로 합니다.

각 단계는 실제 개발 흐름과 유사하게 설계되었으며,  
기능 확장 시 구조 변경을 최소화하는 방향으로 구현됩니다.