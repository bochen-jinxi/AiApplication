using System.Collections.Generic;
using AiApplication.Application.Abstractions.Prompt;
using AiApplication.Domain.AI;

namespace AiApplication.Infrastructure.Prompt.Builders
{
    public class ContractPromptBuilder : IPromptBuilder
    {
        public IReadOnlyList<AiMessage> Build(string systemPrompt, IReadOnlyList<AiMessage> history, string userMessage, IReadOnlyList<string> ragContexts = null, IReadOnlyList<string> toolResults = null)
        {
            throw new System.NotImplementedException();
        }

    }
}
