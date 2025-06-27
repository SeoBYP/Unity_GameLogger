using System.Text;

namespace Core.Logging
{
    public readonly struct GameLogBuilder
    {
        private readonly StringBuilder _builder;

        public GameLogBuilder(StringBuilder builder)
        {
            _builder = builder;
        }

        public GameLogBuilder Append(string text)
        {
            _builder.Append(text);
            return this;
        }

        public GameLogBuilder AppendLine(string text = "")
        {
            _builder.AppendLine(text);
            return this;
        }

        public GameLogBuilder AppendFormat(string format, params object[] args)
        {
            _builder.AppendFormat(format, args);
            return this;
        }

        public void LogInfo() => GameLog.Log(_builder.ToString());
        public void LogWarning() => GameLog.LogWarning(_builder.ToString());
        public void LogError() => GameLog.LogError(_builder.ToString());

        public void LogException(System.Exception ex)
            => GameLog.Exception(ex, _builder.ToString());
    }
}