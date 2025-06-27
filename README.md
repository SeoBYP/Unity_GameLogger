# 🔥 Unity ZLogger Logging System

Unity에서 **고성능**, **Zero Allocation**, **구조적 로깅**을 실현하기 위해 [ZLogger](https://github.com/Cysharp/ZLogger)를 기반으로 구축한 **일관된 로그 시스템**입니다.
에디터/파일 동시 출력, `GameLog` 단일 진입점, Assert, 로그 빌더 등 실전 게임 개발에 적합한 기능을 제공합니다.

---

## 🚀 주요 기능

| 기능                            | 설명                                                                                           |
| ------------------------------- | ---------------------------------------------------------------------------------------------- |
| ✅ GameLog 단일 진입점          | `GameLog.Log()`, `GameLog.LogWarning()`, `GameLog.LogError()` 등으로 일관성 있는 로깅 API 제공 |
| ✅ Unity 콘솔 + 로그 파일 출력  | 콘솔과 파일을 동시에 출력 (RollingFile 지원)                                                   |
| ✅ ZLogger 기반 고성능 로그     | C# 10 Interpolated String + UTF8 직렬화, Zero Allocation                                       |
| ✅ Zero GC 로그 빌더            | `GameLog.Builder()`를 통해 `Append`, `AppendFormat` 체이닝 빌더 지원                           |
| ✅ Assert / WarnIf / DevOnlyLog | 조건 기반 출력 및 에디터 전용 디버그 지원                                                      |
| ✅ 예외 핸들링 지원             | `GameLog.Exception()` 을 통해 예외 메시지 + 예외 로그 출력                                     |
| ✅ 구조적 로깅(JSON) 가능       | `UseJsonFormatter()` 설정 시 구조적 로그로 확장 가능                                           |
| ✅ 유닛 테스트 및 운영툴 확장   | `ILogger<T>` 호환 구조, `InMemory`, `LogProcessor` 등 확장성 확보                              |

---

## 🎯 왜 만들었는가?

Unity의 `Debug.Log()`는 간단하지만 다음과 같은 단점이 존재합니다:

- **할당(Allocation)이 많고 성능이 낮음**
- **로그 파일 출력 미지원**
- **로그 출력 구조가 불일치하고 테스트 어려움**
- **운영환경 확장성 부족 (JSON/Slack/HUD 등)**

이를 개선하기 위해 ZLogger 기반으로 다음을 구현했습니다:

> ✅ `GameLog` 단일 진입점 + `ZLogger`의 고성능 로그 출력
> ✅ 콘솔/파일 로그 동시 출력
> ✅ Assert, DevOnlyLog, WarnIf 등 실전 유틸 통합
> ✅ 추후 Slack, HUD 연동 등을 고려한 확장 구조

---

## 🧱 사용 예시

```csharp
GameLog.Log("🎮 게임 시작됨");
GameLog.LogWarning("⚠️ 경고: 프레임 드랍 발생");
GameLog.LogError("❌ 에러: 리소스 로드 실패");

GameLog.Builder()
    .Append("게임 시작됨 | ")
    .AppendFormat("플레이어 수: {0}", 12)
    .AppendLine()
    .Append("시간: ").Append(System.DateTime.Now.ToString("HH:mm:ss"))
    .LogInfo();

GameLog.Builder()
    .AppendLine("치명적인 오류 발생!")
    .AppendLine("데이터베이스 연결 실패")
    .LogError();

try
{
    throw new System.Exception("DB 연결 실패");
}
catch (System.Exception ex)
{
    GameLog.Exception(ex, "게임 종료 처리 중 오류 발생");
}

GameLog.Assert(hp > 0, "HP가 0 이하입니다!");
GameLog.WarnIf(enemyCount > 100, "적 수치 초과!");
GameLog.DevOnlyLog(Application.isEditor, "에디터 전용 디버그 로그");
```

---

## 🔮 확장 가능성

- `UseJsonFormatter()` → 구조적 로깅(JSON)
- `AddZLoggerInMemory()` → 게임 내 디버그 HUD
- `AddZLoggerLogProcessor()` → Slack/Discord 연동
- `ZLoggerMessage` Source Generator 지원 (Unity 2022.3.12 이상)
