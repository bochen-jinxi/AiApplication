using System;
using System.Collections.Generic;

namespace AiApplication.Application.Prompt
{
    public sealed class PromptTemplate
    {
        public string Role { get; set; } = "";
  public   string Name { get; set; }

    public   string Version { get; set; }
        public string Task { get; set; } = "";

        public string Context { get; set; } = "";

        public string Constraints { get; set; } = "";

        public string OutputFormat { get; set; } = "";
public   IReadOnlyList<PromptSection> Sections
    {
        get;
        set;
    }

     public IReadOnlyList<PromptExample>
        Examples
    {
        get;
        set;
    }
    = Array.Empty<PromptExample>();
        public string Build(PromptTemplate template)
        {
            return
$@"角色：

{template.Role}

任务：

{template.Task}

上下文：

{template.Context}

约束：

{template.Constraints}

输出格式：

{template.OutputFormat}";
        }
    }
}
