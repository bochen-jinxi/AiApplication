using AiApplication.Application.Abstractions.Prompt;
using AiApplication.Application.Prompt;

namespace AiApplication.Infrastructure.Prompt.Repository
{
    public class FilePromptTemplateRepository : IPromptTemplateRepository
    {
        public PromptTemplate GetTemplate(
            string templateName,
            string version)
        {
            return new PromptTemplate();
        }
    }
}
