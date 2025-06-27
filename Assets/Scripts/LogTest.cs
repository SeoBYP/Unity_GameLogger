using UnityEngine;
using Core.Logging;

public class LogTest : MonoBehaviour
{
    void Start()
    {
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
    }
}