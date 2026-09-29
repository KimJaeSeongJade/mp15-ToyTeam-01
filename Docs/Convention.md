# 컨벤션

> 파일명·스크립트명·공유 파일명은 그라운드룰의 `자료 형식` 을 따른다
> 가볍게 시작하고, 불편한 점이 생기면 회의에서 보완한다

## 1. 코드 규칙

| 대상 | 형식 | 예시 |
| --- | --- | --- |
| 메서드 | `PascalCase` | `TakeDamage()` |
| private 필드 | `_camelCase` | `_currentHp` |
| public 필드 | `PascalCase` | `Health` |
| 프로퍼티 | `PascalCase` | `Health` |
| 상수 | `UPPER_SNAKE_CASE` | `MAX_HEALTH` |

- 인스펙터에 노출하려고 필드를 `public` 으로 두지 않는다
- 인스펙터 노출이 필요하면 `[SerializeField] private` 을 사용한다
- 중괄호는 새 줄에서 시작한다

## 2. 커밋 양식

형식: `[말머리] 작업 내용`

| 말머리 | 의미 | 예시 |
| --- | --- | --- |
| `Feat` | 새 기능 | `[Feat] 과열 시스템 추가` |
| `Fix` | 버그 수정 | `[Fix] 적이 죽지 않는 문제 수정` |
| `Asset` | 에셋 추가/변경 | `[Asset] 총기 모델 추가` |
| `Prefab` | 프리팹 작성, 수정 | `[Prefab] 프리팹 수정` |
| `Docs` | 문서 작성, 수정 | `[Docs] 컨벤션 수정` |

- 한 커밋에는 한 가지 작업만 담는다

## 3. 브랜치

| 브랜치 | 용도 |
| --- | --- |
| `main` | 안정된 결과물 (직접 커밋 금지) |
| `feature-영문이름축약-기능(PascalCase)` | 개인 작업 (예: `feature-YH-PlayerMove`) |

- `main` 에는 절대 직접 커밋하지 않는다 → 모든 작업은 PR 로 올린다
- 병합이 끝난 브랜치는 본인이 확인 후 삭제한다

## 4. PR · 머지

### PR 규칙

- PR 을 올릴 때 **리뷰어 1명~2명**을 지정한다
- 리뷰어는 코드 리뷰를 남기고 **Approve** 한다
- **자신의 PR 은 자신이 머지하지 않는다** (승인받으려고 올린 것이므로)

### 머지 규칙

| 상황 | 머지하는 사람 |
| --- | --- |
| 일반 PR | 지정된 리뷰어 **모두 승인 후**, 마지막으로 승인한 리뷰어 |
| 충돌(Conflict) 난 PR | 팀장 |

⚠️ 회의 결정 필요

- 리뷰어 지정 방식
- 머지 방식 : 일반 머지 / `Squash and merge`
- 씬별 담당자

## 5. 씬·프리팹 수정 규칙

> 씬 파일은 충돌이 나면 합치기 어려우므로, 충돌이 나지 않게 하는 것이 목표이다

### 씬

- 씬은 담당자만 수정한다
- 수정 시작 전에 Slack에 알리고, 커밋 후 끝났음을 알린다
- 다른 사람의 씬에 넣을 오브젝트는 **프리팹으로 만들어서** 전달한다

### 프리팹

- 프리팹을 **수정하거나 삭제할 때는 반드시 사전에 Slack에 보고한다**
- 보고 내용: 대상 프리팹, 수정·삭제 사유
- 삭제 전에는 다른 씬·프리팹에서 참조 중인지 확인한다

## 6. 프로젝트 세팅

| 항목 | 내용 |
| --- | --- |
| Unity 버전 | `2022.3.62f3` |
| Asset Serialization | `Force Text` |
| Meta Files | `Visible Meta Files` |
| `.gitignore` | Unity 템플릿 사용, +`Imports` 폴더 |
| 용량 큰 파일 | 사용 여부를 회의에서 결정 |

- `.meta` 파일은 반드시 함께 커밋한다
- `Library/`, `Temp/`, `Logs/`, `obj/` 폴더는 커밋하지 않는다

## 7. 에셋 폴더 구조

```
Assets
├─ Scenes
├─ Scripts
├─ Prefabs
├─ Art
│   ├─ Models
│   ├─ Textures
│   ├─ Materials
│   └─ Animations
├─ Audio
│   ├─ BGM
│   └─ SFX
└─ Imports  (외부 에셋)
```

- 외부 에셋은 `Imports` 폴더에만 넣고, 팀이 만든 에셋과 섞지 않는다
- 레포가 public 이므로 `Imports` 는 git 에 올리지 않는다 (라이선스 보호)
- 외부 에셋은 에셋 담당자가 `Imports.unitypackage` 로 묶어 Slack-자료-GoogleDrive에 공유한다
  - 에셋이 추가되면 패키지를 갱신하고 Slack 에 공지 → 팀원은 다시 임포트
- `Imports` 안의 파일은 직접 수정하지 않는다
  - 수정이 필요하면 `Art/` 로 복사해서 수정한다 (수정본은 git 으로 공유됨)
- 에셋을 추가하면 `AssetInfo.md` 에 이름·제작자·라이선스·링크를 기록한다