//using AiApplication.Application.Abstractions.Prompt;
//using AiApplication.Application.Chat;
//using AiApplication.Application.Prompt;

//namespace AiApplication.Infrastructure.Prompt.Builders
//{
//    public sealed class HydrologyPromptBuilder
//        : IPromptBuilder
//    {

//    private readonly IPromptTemplateRepository _repository;
//   public HydrologyPromptBuilder(IPromptTemplateRepository repository)
//    {
//        _repository = repository;
//    }

//        public ChatMessage BuildSystemPrompt(PromptRequest request)
//        {
//            return new ChatMessage
//            {
//                Role = ChatRole.System,
//                Content =
//@"你是一名具有二十年以上经验的水文专家。

//你的职责包括：

//1. 分析降雨数据。
//2. 分析河流水位。
//3. 分析洪水风险。
//4. 优先依据提供的RAG知识库内容回答。
//5. 如果提供了MCP实时数据，应优先引用实时数据。
//6. 如果知识不足，请明确说明不知道，不允许编造事实。
//7. 输出内容必须专业、准确、清晰。"
//            };
//        }

//        public ChatMessage BuildUserPrompt(PromptRequest request)
//        {
//            return new ChatMessage
//            {
//                Role = ChatRole.User,
//                Content = request.UserQuestion
//            };
//        }

//   public PromptDocument Build(PromptRequest request)
//    {
//        var template = _repository.GetTemplate("Hydrology", "v1");

//        var document = new PromptDocument();

//        foreach (var section in template.Sections)
//        {
//            document.Sections.Add(section);
//        }

//   foreach (var example in template.Examples)
//    {
//        document.Sections.Add(new PromptSection
//            {
//                Name = "Example",
//                Content =
//$@"示例输入：

//{example.Input}

//示例输出：

//{example.Output}"
//            });
//    }

//        return document;
//    }
////          public PromptDocument Build(
////         PromptRequest request)
////     {
////         var document =
////             new PromptDocument();

////         document.Sections.Add(
////             new PromptSection
////             {
////                 Name = "Role",
////                 Content =
//// @"
//// 你是一名具有二十年以上经验的水文专家。
//// "
////             });

////         document.Sections.Add(
////             new PromptSection
////             {
////                 Name = "Task",
////                 Content =
//// @"
//// 根据用户问题分析洪水风险。
//// "
////             });

////         document.Sections.Add(
////             new PromptSection
////             {
////                 Name = "Constraint",
////                 Content =
//// @"
//// 不得编造事实。

//// 知识不足必须明确说明。
//// "
////             });

////         return document;
////     }
//    }
//}
