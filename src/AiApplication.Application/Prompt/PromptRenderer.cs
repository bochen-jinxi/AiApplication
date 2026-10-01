//using System.Collections.Generic;
//using System.Text;
//using AiApplication.Application.Abstractions.Prompt;

//namespace AiApplication.Application.Prompt
//{
//public sealed class PromptRenderer
//    : IPromptRenderer
//{

//        public string Render(PromptDocument document, IReadOnlyList<PromptParameter> parameters)
//        {
//            var builder = new StringBuilder();

//            foreach(var section in document.Sections)
//            {
//                var content = section.Content;

//                foreach(var parameter in parameters)
//                {
//                    content = content.Replace("{{" + parameter.Name + "}}", parameter.Value);
//                }

//                builder.AppendLine(content);
//            }

//            return builder.ToString();
//        }
//        // public string Render(
//        //     PromptDocument document)
//        // {
//        //     var builder =
//        //         new StringBuilder();

//        //     foreach(var section
//        //         in document.Sections)
//        //     {
//        //         builder.AppendLine(
//        //             $"## {section.Name}");

//        //         builder.AppendLine(
//        //             section.Content);

//        //         builder.AppendLine();
//        //     }

//        //     return builder.ToString();
//        // }
//    }

//}
