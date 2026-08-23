using AiApplication.Application.Prompt;

namespace AiApplication.Application.Abstractions.Prompt
{
    public interface IPromptTemplateRepository
    {
        PromptTemplate GetTemplate(
        string templateName,
        string version);
    }
}
