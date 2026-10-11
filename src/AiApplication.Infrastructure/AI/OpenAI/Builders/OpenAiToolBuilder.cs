using AiApplication.Application.Models.OpenAI;
using AiApplication.Application.Tools.Models;
using AiApplication.Application.Tools.Schema;
using System.Collections.Generic;

namespace AiApplication.Infrastructure.AI.OpenAI.Builders
{
    /// <summary>
    /// OpenAI Tool Builder。
    /// </summary>
    public    class OpenAiToolBuilder  
    {
        public   IList<OpenAiTool> Build(IList<ToolDefinition> tools)
        {
            List<OpenAiTool> result = new List<OpenAiTool>();

            if (tools == null || tools.Count == 0)
            {
                return result;
            }

            foreach (ToolDefinition tool in tools)
            {
                OpenAiTool openAiTool = new OpenAiTool
                {
                    Type = "function",
                    Function = new OpenAiFunction
                    {
                        Name = tool.Name,
                        Description = tool.Description,
                        Parameters = BuildParameters(tool)
                    }
                };

                result.Add(openAiTool);
            }

            return result;
        }
         

        private static OpenAiFunctionParameters BuildParameters(ToolDefinition tool)
        {

            var toolSchema=ToolSchemaBuilder.Build(tool.RequestType);

            OpenAiFunctionParameters parameters = new OpenAiFunctionParameters
            {
                Type = toolSchema.Type,
                Properties = toolSchema.Properties,
                Required = toolSchema.Required
            };
            return parameters;
        }
    }
}