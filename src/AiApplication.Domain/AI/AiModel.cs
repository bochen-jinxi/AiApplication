namespace AiApplication.Domain.AI
{
    /// <summary>
    /// AI 模型标识。封装模型名称与所属提供商，便于在调用链中传递。
    /// </summary>
    public sealed class AiModel
    {
        public string Name { get; }

        public AiProvider Provider { get; }

        public AiModel(string name, AiProvider provider)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new System.ArgumentException("AI 模型名称不能为空。", nameof(name));
            }

            Name = name;
            Provider = provider;
        }

        public override string ToString() => $"{Provider}/{Name}";

        public static AiModel OpenAI(string name) => new AiModel(name, AiProvider.OpenAI);

        public static AiModel Claude(string name) => new AiModel(name, AiProvider.Claude);

        public static AiModel DeepSeek(string name) => new AiModel(name, AiProvider.DeepSeek);
    }
}
