# AiApplication 项目架构

## 1. 项目概览

**解决方案**：`AiApplication.sln`
**目标框架**：.NET Core 3.1 (`netcoreapp3.1`)
**架构风格**：Clean Architecture（整洁架构）+ DDD（领域驱动设计）

项目采用分层架构，依赖方向从外向内：`Api → Infrastructure → Application → Domain`，其中 `Shared` 为公共工具层。各层通过抽象接口解耦，遵循依赖倒置原则。

## 2. 项目依赖关系

```
AiApplication.Api  (Web 入口)
├── AiApplication.Application
└── AiApplication.Infrastructure
    ├── AiApplication.Application
    └── AiApplication.Domain

AiApplication.Application  (应用层)
└── AiApplication.Domain

AiApplication.Domain       (领域层，无依赖)

AiApplication.Shared       (公共层，无依赖)
```

## 3. 分层架构

### 3.1 AiApplication.Api（API 层 / 表示层）
**项目文件**：`src/AiApplication.Api/AiApplication.Api.csproj`
**SDK**：`Microsoft.NET.Sdk.Web`
**职责**：HTTP 入口、路由、请求/响应 DTO 转换、中间件管道

```
src/AiApplication.Api/
├── AiApplication.Api.csproj
├── AiApplication.Api.csproj.user
├── Program.cs                              # 应用入口
├── Startup.cs                              # 启动配置（服务注册、中间件管道）
├── appsettings.json                        # 应用配置
├── appsettings.Development.json            # 开发环境配置
├── Properties/
│   └── launchSettings.json                 # 启动配置
├── Controllers/
│   ├── ChatController.cs                   # 聊天控制器
│   ├── HealthController.cs                 # 健康检查控制器
│   └── KnowledgeController.cs              # 知识库控制器
├── Models/
│   ├── ChatRequestDto.cs                   # 聊天请求 DTO
│   ├── ChatResponseDto.cs                  # 聊天响应 DTO
│   └── ErrorResponseDto.cs                 # 错误响应 DTO
├── Middleware/
│   ├── CorrelationIdMiddleware.cs          # 关联 ID 中间件
│   ├── ExceptionHandlingMiddleware.cs      # 异常处理中间件
│   └── RequestLoggingMiddleware.cs         # 请求日志中间件
└── Extensions/
    ├── ApplicationBuilderExtensions.cs     # IApplicationBuilder 扩展
    ├── ServiceCollectionExtensions.cs      # IServiceCollection 扩展
    └── WebApplicationExtensions.cs         # WebApplication 扩展
```

### 3.2 AiApplication.Application（应用层）
**项目文件**：`src/AiApplication.Application/AiApplication.Application.csproj`
**职责**：用例编排、应用服务、抽象接口定义（端口）
**依赖**：`AiApplication.Domain`、`Microsoft.Extensions.DependencyInjection.Abstractions`、`Microsoft.Extensions.Logging.Abstractions`

```
src/AiApplication.Application/
├── AiApplication.Application.csproj
├── Abstractions/                           # 抽象接口（端口）
│   ├── AI/
│   │   ├── IAiClient.cs                    # AI 客户端接口
│   │   ├── IAiClientProvider.cs            # AI 客户端提供者接口
│   │   ├── IAiHttpTransport.cs             # AI HTTP 传输接口
│   │   └── IAiResponseParser.cs            # AI 响应解析器接口
│   ├── Common/
│   │   └── IClock.cs                       # 时钟接口
│   ├── Mcp/
│   │   ├── IMcpClient.cs                   # MCP 客户端接口
│   │   ├── IMcpToolExecutor.cs             # MCP 工具执行器接口
│   │   └── IMcpToolRegistry.cs             # MCP 工具注册表接口
│   ├── Prompt/
│   │   ├── IPromptBuilder.cs               # 提示词构建器接口
│   │   ├── IPromptExampleRepository.cs     # 提示词示例仓储接口
│   │   ├── IPromptRenderer.cs              # 提示词渲染器接口
│   │   └── IPromptTemplateRepository.cs    # 提示词模板仓储接口
│   ├── Rag/
│   │   ├── IDocumentChunker.cs             # 文档分块器接口
│   │   ├── IDocumentLoader.cs              # 文档加载器接口
│   │   ├── IEmbeddingService.cs            # 嵌入服务接口
│   │   ├── IRetriever.cs                   # 检索器接口
│   │   └── IVectorStore.cs                 # 向量存储接口
│   ├── Serialization/
│   │   └── IJsonSerializer.cs              # JSON 序列化器接口
│   ├── Tools/
│   │   ├── ITool.cs                        # 工具接口
│   │   ├── IToolExecutor.cs                # 工具执行器接口
│   │   └── IToolRegistry.cs                # 工具注册表接口
│   └── Weather/
│       └── IWeatherService.cs              # 天气服务接口
├── Chat/                                   # 聊天用例
│   ├── ChatContext.cs                      # 聊天上下文
│   ├── ChatMessage.cs                      # 聊天消息
│   ├── ChatRequest.cs                      # 聊天请求
│   ├── ChatResponse.cs                     # 聊天响应
│   ├── ChatRole.cs                         # 聊天角色
│   ├── ChatService.cs                      # 聊天服务实现
│   └── IChatService.cs                     # 聊天服务接口
├── Common/
│   ├── Result.cs                           # 操作结果
│   └── ResultT.cs                          # 泛型操作结果
├── Contracts/                              # 合同审查用例
│   ├── ContractReviewResult.cs             # 合同审查结果
│   └── Http/
│       ├── AiHttpRequest.cs                # AI HTTP 请求
│       └── AiHttpResponse.cs               # AI HTTP 响应
├── DependencyInjection/
│   └── ApplicationServiceExtensions.cs     # 应用层服务注册
├── Mcp/                                    # MCP（Model Context Protocol）用例
│   ├── McpRequest.cs                       # MCP 请求
│   ├── McpResult.cs                        # MCP 结果
│   ├── McpService.cs                       # MCP 服务
│   └── McpToolCall.cs                      # MCP 工具调用
├── Prompt/                                 # 提示词用例
│   ├── PromptDocument.cs                   # 提示词文档
│   ├── PromptExample.cs                    # 提示词示例
│   ├── PromptParameter.cs                  # 提示词参数
│   ├── PromptRenderer.cs                   # 提示词渲染器
│   ├── PromptRequest.cs                    # 提示词请求
│   ├── PromptResult.cs                     # 提示词结果
│   ├── PromptSection.cs                    # 提示词段落
│   └── PromptTemplate.cs                   # 提示词模板
├── Rag/                                    # RAG（检索增强生成）用例
│   ├── DocumentChunk.cs                    # 文档块
│   ├── RagQuery.cs                         # RAG 查询
│   ├── RagResult.cs                        # RAG 结果
│   ├── RagService.cs                       # RAG 服务
│   └── RetrievedDocument.cs                # 检索到的文档
├── Serialization/
│   └── JsonDeserializeResult.cs            # JSON 反序列化结果
├── Tools/                                  # 工具用例
│   ├── ToolCall.cs                         # 工具调用
│   ├── ToolCallRequest.cs                  # 工具调用请求
│   ├── ToolCallResult.cs                   # 工具调用结果
│   ├── ToolDefinition.cs                   # 工具定义
│   ├── ToolExecutor.cs                     # 工具执行器
│   ├── ToolResult.cs                       # 工具结果
│   ├── ToolService.cs                      # 工具服务
│   └── WeatherTool.cs                      # 天气工具实现
└── Weather/                                # 天气用例
    ├── WeatherRequest.cs                   # 天气请求
    └── WeatherResult.cs                    # 天气结果
```

### 3.3 AiApplication.Domain（领域层）
**项目文件**：`src/AiApplication.Domain/AiApplication.Domain.csproj`
**职责**：领域实体、值对象、领域服务、领域异常
**依赖**：无（纯领域层）

```
src/AiApplication.Domain/
├── AiApplication.Domain.csproj
├── AI/                                     # AI 领域模型
│   ├── AiCompletion.cs                     # AI 补全
│   ├── AiMessage.cs                        # AI 消息
│   ├── AiModel.cs                          # AI 模型
│   ├── AiProviderEnum.cs                   # AI 提供商枚举
│   ├── AiProviderException.cs              # AI 提供商异常
│   ├── AiRoleEnum.cs                       # AI 角色枚举
│   └── TokenUsage.cs                       # Token 使用统计
├── Common/
│   ├── Error.cs                            # 错误载体
│   └── Result.cs                           # 操作结果
├── Conversation/                           # 对话领域
│   ├── Conversation.cs                     # 对话聚合根
│   ├── ConversationId.cs                   # 对话 ID（值对象）
│   └── ConversationMessage.cs              # 对话消息
├── Knowledge/                              # 知识库领域
│   ├── Document.cs                         # 文档
│   ├── DocumentChunk.cs                    # 文档块
│   ├── DocumentId.cs                       # 文档 ID（值对象）
│   └── KnowledgeSource.cs                  # 知识来源
└── Prompt/                                 # 提示词领域
    ├── PromptId.cs                         # 提示词 ID（值对象）
    └── PromptVersion.cs                    # 提示词版本
```

### 3.4 AiApplication.Infrastructure（基础设施层）
**项目文件**：`src/AiApplication.Infrastructure/AiApplication.Infrastructure.csproj`
**职责**：外部服务适配器实现（AI 客户端、数据库、HTTP、RAG、MCP 等）
**依赖**：`AiApplication.Application`、`AiApplication.Domain`、`Microsoft.EntityFrameworkCore`、`Microsoft.Extensions.Http`、`Microsoft.Extensions.Options` 等

```
src/AiApplication.Infrastructure/
├── AiApplication.Infrastructure.csproj
├── AI/                                     # AI 客户端实现
│   ├── AiClientFactory.cs                  # AI 客户端工厂
│   ├── AiClientProvider.cs                 # AI 客户端提供者实现
│   ├── Http/
│   │   └── AiHttpTransport.cs              # AI HTTP 传输实现
│   ├── OpenAI/                             # OpenAI 实现
│   │   ├── OpenAiClient.cs                 # OpenAI 客户端
│   │   ├── OpenAiOptions.cs                # OpenAI 配置
│   │   ├── OpenAiMessageMapper.cs          # 消息映射器
│   │   ├── OpenAiRequestMapper.cs          # 请求映射器
│   │   ├── OpenAiResponseMapper.cs         # 响应映射器
│   │   └── Dtos/
│   │       ├── OpenAiChatRequest.cs        # OpenAI 聊天请求 DTO
│   │       ├── OpenAiChatResponse.cs       # OpenAI 聊天响应 DTO
│   │       ├── OpenAiChoice.cs             # OpenAI 选择项 DTO
│   │       ├── OpenAiMessage.cs            # OpenAI 消息 DTO
│   │       └── OpenAiUsage.cs              # OpenAI 用量 DTO
│   ├── Claude/                             # Anthropic Claude 实现
│   │   ├── ClaudeClient.cs                 # Claude 客户端
│   │   ├── ClaudeOptions.cs                # Claude 配置
│   │   ├── ClaudeRequestMapper.cs          # Claude 请求映射器
│   │   └── ClaudeResponseMapper.cs         # Claude 响应映射器
│   └── DeepSeek/                           # DeepSeek 实现
│       ├── DeepSeekClient.cs               # DeepSeek 客户端
│       ├── DeepSeekOptions.cs              # DeepSeek 配置
│       ├── DeepSeekRequestMapper.cs        # DeepSeek 请求映射器
│       └── DeepSeekResponseMapper.cs       # DeepSeek 响应映射器
├── DependencyInjection/
│   └── InfrastructureServiceExtensions.cs  # 基础设施层服务注册
├── Http/                                   # HTTP 工具
│   ├── HttpClientExtensions.cs             # HttpClient 扩展
│   └── HttpResilienceExtensions.cs         # HTTP 弹性扩展
├── MCP/                                    # MCP（Model Context Protocol）实现
│   ├── McpClient.cs                        # MCP 客户端
│   ├── McpOptions.cs                       # MCP 配置
│   ├── McpToolExecutor.cs                  # MCP 工具执行器
│   └── McpToolRegistry.cs                  # MCP 工具注册表
├── Persistence/
│   └── AppDbContext.cs                     # EF Core 数据库上下文
├── Prompt/                                 # 提示词基础设施
│   ├── Builders/                           # 提示词构建器
│   │   ├── ContractPromptBuilder.cs        # 合同提示词构建器
│   │   ├── GeneralChatPromptBuilder.cs     # 通用聊天提示词构建器
│   │   └── HydrologyPromptBuilder.cs       # 水文提示词构建器
│   ├── Rendering/                          # 提示词渲染器
│   │   ├── MarkdownPromptRenderer.cs       # Markdown 渲染器
│   │   ├── PromptRenderer.cs               # 提示词渲染器
│   │   └── XmlPromptRenderer.cs            # XML 渲染器
│   ├── Repository/                         # 提示词仓储
│   │   ├── FilePromptTemplateRepository.cs # 文件提示词模板仓储
│   │   └── MemoryPromptTemplateRepository.cs # 内存提示词模板仓储
│   └── Templates/                          # 提示词模板（YAML）
│       ├── Contract/
│       │   ├── v1.yaml
│       │   └── v2.yaml
│       ├── General/
│       │   └── v1.yaml
│       └── Hydrology/
│           ├── v1.yaml
│           └── v2.yaml
├── RAG/                                    # RAG（检索增强生成）实现
│   ├── Chunking/
│   │   └── TextChunker.cs                  # 文本分块器
│   ├── Documents/                          # 文档加载器
│   │   ├── PdfDocumentLoader.cs            # PDF 文档加载器
│   │   ├── TextDocumentLoader.cs           # 文本文档加载器
│   │   └── WordDocumentLoader.cs           # Word 文档加载器
│   ├── Embedding/
│   │   ├── EmbeddingOptions.cs             # 嵌入配置
│   │   └── OpenAiEmbeddingService.cs       # OpenAI 嵌入服务
│   ├── Retrieval/
│   │   └── VectorRetriever.cs              # 向量检索器
│   └── VectorStore/
│       ├── MilvusOptions.cs                # Milvus 配置
│       └── MilvusVectorStore.cs            # Milvus 向量存储
├── Serialization/
│   └── SystemTextJsonSerializer.cs         # System.Text.Json 序列化器实现
├── Tools/                                  # 工具实现
│   ├── DatabaseTool.cs                     # 数据库工具
│   ├── SearchTool.cs                       # 搜索工具
│   └── ToolRegistry.cs                     # 工具注册表实现
└── Weather/
    └── WeatherService.cs                   # 天气服务实现
```

### 3.5 AiApplication.Shared（公共层）
**项目文件**：`src/AiApplication.Shared/AiApplication.Shared.csproj`
**职责**：跨层共享的常量、扩展方法、工具类
**依赖**：无

```
src/AiApplication.Shared/
├── AiApplication.Shared.csproj
├── Class1.cs                               # 占位类
├── Constants/
│   ├── AiConstants.cs                      # AI 常量
│   └── PromptConstants.cs                  # 提示词常量
├── Extensions/
│   ├── EnumExtensions.cs                   # 枚举扩展方法
│   └── StringExtensions.cs                 # 字符串扩展方法
└── Serialization/
    └── JsonDefaults.cs                     # JSON 默认配置
```

## 4. 测试项目

```
tests/
├── AiApplication.UnitTests/                # 单元测试
│   ├── AiApplication.UnitTests.csproj
│   └── UnitTest1.cs
├── AiApplication.IntegrationTests/         # 集成测试
│   ├── AiApplication.IntegrationTests.csproj
│   └── IntegrationTest1.cs
└── AiApplication.ArchitectureTests/        # 架构测试（NetArchTest）
    ├── AiApplication.ArchitectureTests.csproj
    └── ArchitectureTest1.cs
```

**测试框架**：xUnit 2.4.0 + Microsoft.NET.Test.Sdk 16.5.0 + coverlet.collector 1.2.0
**架构测试**：NetArchTest.Rules 1.3.0（用于验证分层依赖规则）

## 5. 配置文件

```
src/AiApplication.Api/
├── appsettings.json                        # 主配置（AI 提供商、连接字符串等）
└── appsettings.Development.json            # 开发环境配置
```

## 6. IDE / 工具配置

```
.vscode/
├── launch.json                             # 调试启动配置
├── settings.json                           # 工作区设置
└── tasks.json                              # 任务配置

.codeartsdoer/
└── .gitignore                              # CodeArts Doer 忽略配置
```

## 7. 关键架构特征

| 特征 | 说明 |
|------|------|
| **分层架构** | Domain → Application → Infrastructure → Api，依赖方向向内 |
| **依赖倒置** | Infrastructure 依赖 Application 的抽象接口（Abstractions） |
| **端口适配器** | Application.Abstractions 定义端口，Infrastructure 实现适配器 |
| **多 AI 提供商** | 支持 OpenAI、Claude、DeepSeek，通过 `IAiClientProvider` 路由 |
| **RAG 支持** | 文档加载、分块、嵌入、向量存储（Milvus）、检索 |
| **MCP 协议** | Model Context Protocol 客户端与工具执行 |
| **提示词工程** | YAML 模板 + 构建器 + 渲染器（Markdown/XML） |
| **工具调用** | `ITool` / `IToolRegistry` / `IToolExecutor` 工具体系 |
| **EF Core** | `AppDbContext` 持久化 |
| **中间件管道** | 关联 ID、异常处理、请求日志 |

## 8. 主要 NuGet 依赖

| 项目 | 包 | 版本 |
|------|-----|------|
| Api | Swashbuckle.AspNetCore | 5.6.3 |
| Application | Microsoft.Extensions.DependencyInjection.Abstractions | 3.1.32 |
| Application | Microsoft.Extensions.Logging.Abstractions | 3.1.32 |
| Infrastructure | Microsoft.Extensions.DependencyInjection.Abstractions | 3.1.32 |
| Infrastructure | Microsoft.Extensions.Configuration.Abstractions | 3.1.32 |
| Infrastructure | Microsoft.Extensions.Configuration.Binder | 3.1.32 |
| Infrastructure | Microsoft.Extensions.Options | 3.1.32 |
| Infrastructure | Microsoft.Extensions.Options.ConfigurationExtensions | 3.1.32 |
| Infrastructure | Microsoft.Extensions.Http | 3.1.32 |
| Infrastructure | Microsoft.EntityFrameworkCore | 3.1.32 |
| ArchitectureTests | NetArchTest.Rules | 1.3.0 |
| *Tests | xunit | 2.4.0 |
| *Tests | Microsoft.NET.Test.Sdk | 16.5.0 |
| *Tests | coverlet.collector | 1.2.0 |
