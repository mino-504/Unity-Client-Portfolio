# Unity Client Portfolio

## 📌 프로젝트 개요
본 프로젝트는 **Unity 2D 기반 싱글 플레이 게임 클라이언트 포트폴리오**입니다.  
그래픽이나 연출보다는 **게임 클라이언트 시스템 설계 및 구현 역량**을 보여주는 것을 목표로 합니다.

무경험 상태에서 시작하여,  
플레이어 제어 → 시스템 분리 → AI → 전투 → 입력 기반 공격 → 데이터 기반 구조로  
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
확장성과 유지보수를 고려한 클라이언트 구조로 개선합니다.

### 구조 설계
Player
├ PlayerInput
├ PlayerMovement
├ PlayerRotation
└ PlayerController

### 설계 포인트
- 입력(Input)과 동작(Behavior)의 명확한 분리
- 컴포넌트 간 의존성 방향 단순화 (순환 참조 없음)
- 기능 확장 시 구조 변경 최소화

---

## Phase 3 – Enemy AI (FSM)

### 목표
적의 행동을 단순 조건문이 아닌 **FSM(Finite State Machine)** 구조로 설계하여  
상태 기반으로 행동이 결정되도록 구현했습니다.

### 구조 설계
Enemy
├ EnemyController
├ EnemyMovement
├ EnemyStateMachine
└ States
├ EnemyIdleState
└ EnemyChaseState

### 구현 내용
- Idle ↔ Chase 상태 전환
- 감지 범위 / 이탈 범위 기반 상태 변경
- 히스테리시스 적용으로 상태 떨림 방지
- Chase 상태에서만 이동 수행

### 설계 포인트
- 상태(State)와 MonoBehaviour 분리
- 상태 추가 시 기존 구조 수정 없이 확장 가능

---

## Phase 4 – Combat System & Interaction

### 목표
플레이어와 적 간의 **전투 상호작용 시스템**을 구현하고,  
AI 상태와 공격 로직이 유기적으로 동작하도록 설계했습니다.

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

### 구현 내용
- Attack 상태 추가 (Chase → Attack → Chase)
- 공격 쿨타임 시스템
- `IDamageable` 인터페이스 기반 공통 피격 처리
- 체력 관리 및 사망 처리

### 설계 포인트
- 공격 판정과 AI 상태 로직 분리
- 시스템 중심 전투 구조 설계

---

## Phase 5 – Player Attack System & Feedback

### 목표
플레이어 공격을 **입력 처리 수준을 넘어 하나의 시스템**으로 확장하고,  
입력 → 판정 → 이펙트 → 쿨타임 흐름이 명확히 드러나도록 구현했습니다.

### 구조 설계
Player
├ PlayerInput
├ PlayerCombat
├ PlayerHealth
└ AttackEffect (Prefab)

### 구현 내용
- 좌클릭 기반 근접 공격
- 공격 쿨타임 관리
- 마우스 방향 기반 공격 방향 계산
- 공격 이펙트 생성 및 자동 제거
- Gizmos를 활용한 공격 범위 시각화

### 설계 포인트
- 입력(Input) → 판정(Logic) → 표현(Effect) 단계 분리
- 이펙트와 판정 로직 분리
- 무기/스킬 시스템으로 확장 가능한 구조

---

## Phase 6 – Data Driven Structure & Physics Integration

### 목표
수치와 설정을 코드에서 분리하여  
**데이터 기반(Data Driven) 구조**로 전투 시스템을 개선하고,  
물리 기반 환경 상호작용을 안정적으로 구현했습니다.

### 구조 설계
Data
├ PlayerAttackData (근접)
└ PlayerRangedAttackData (원거리)

Combat
├ PlayerCombat
├ Projectile
└ IDamageable

### 구현 내용
- ScriptableObject 기반 공격 데이터 분리
- 좌클릭(근접) / 우클릭(원거리) 공격 공존
- Projectile 시스템 구현
- LayerMask 기반 공격 대상/환경 분리
- 벽(Wall)과의 물리 충돌 처리
- Rigidbody2D 기반 이동 구조로 전환
- Idle 상태 진입 시 속도 초기화로 물리 안정화

### 설계 포인트
- **수치/설정(Data)과 로직(Component) 분리**
- 공격 타입 추가 시 코드 수정 최소화
- 환경 오브젝트(벽)와의 상호작용 고려
- 물리 기반 이동과 AI FSM의 충돌 문제 해결 경험

---

## 🛠️ 사용 기술
- Unity 2022.3 LTS
- C#
- Git / GitHub

---

## 📈 개발 로드맵
- Phase 7: UI 및 피드백 시스템
- Phase 8: 코드 정리 및 포트폴리오 완성

---

## 💡 프로젝트 방향성
본 프로젝트는 단순 기능 구현을 넘어,  
**게임 클라이언트 프로그래머로서의 설계 사고 과정**을 보여주는 것을 목표로 합니다.

각 Phase는 실제 개발 흐름을 기준으로 구성되었으며,  
기능 확장 시 구조 변경을 최소화하는 방향으로 설계되었습니다.