using System.Collections.Generic;
using AiApplication.Application.Abstractions.Prompt;
using AiApplication.Application.Prompt;

namespace AiApplication.Infrastructure.Prompt.Repository
{
    public sealed class MemoryPromptTemplateRepository
        : IPromptTemplateRepository
    {
        private readonly Dictionary<string, PromptTemplate> _templates;

        public MemoryPromptTemplateRepository()
        {
            _templates = new Dictionary<string, PromptTemplate>();

            var template = new PromptTemplate
            {
                Name = "Hydrology",
                Version = "v1",
                Sections = new List<PromptSection>
                {
                    new PromptSection
                    {
                        Name = "Role",
                        Content =
@"
你是一名具有二十年以上经验的国家防汛专家。
"
                    },

                    new PromptSection
                    {
                        Name = "Task",
                        Content =
@"
分析洪水风险。
"
                    }
                }
            };

            _templates.Add(
                $"{template.Name}:{template.Version}",
                template);
        }

        public PromptTemplate GetTemplate(
            string templateName,
            string version)
        {
            return _templates[
                $"{templateName}:{version}"
            ];
        }
    }
}
