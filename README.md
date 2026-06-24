# TextSense

Blazor Server app built with .NET 9 that generates text with an OpenAI chat model and instantly analyzes it for sentiment and key phrases using Azure AI Language. Pick a model, write or choose a prompt, and see the generated text together with its sentiment breakdown and extracted key phrases in a single pass.

> Portfolio project focused on clean .NET architecture: typed configuration, injectable services behind interfaces, a strict result type and a thin Blazor UI over two real AI integrations.

## Highlights

- Single-pass workflow: generate with OpenAI, then analyze the result with Azure AI Language.
- Model picker driven entirely by configuration — add or remove OpenAI models without touching code.
- Sentiment scoring with positive/neutral/negative confidence breakdowns and extracted key phrases.
- Service layer behind interfaces (`ITextGenerationService`, `ITextAnalysisService`, `IPromptLibrary`) instead of API calls inside components.
- Strongly-typed options bound from configuration via `IOptions<T>`.
- Uniform `ServiceResult<T>` for success/failure so the UI never parses error strings.
- Typed, named `HttpClient`s with auth headers configured at composition root.
- Secrets kept out of source control through .NET user secrets.

## Tech Stack

- .NET 9 / ASP.NET Core
- Blazor Server (interactive server components)
- OpenAI Chat Completions API
- Azure AI Language (`analyze-text`: sentiment + key phrase extraction)
- `IHttpClientFactory` with named, typed clients
- Options pattern (`IOptions<T>`) for configuration binding
- Scoped CSS for component-level styling
- Bootstrap for layout primitives

## Architecture

The UI is intentionally thin. Components depend only on interfaces; all HTTP and AI logic lives in the service layer, configured once at the composition root.

```text
TextSense/
  Program.cs                       composition root: options, HTTP clients, DI
  appsettings.json                 endpoints, models, sample prompts (no secrets)

  Configuration/
    OpenAiOptions.cs               endpoint, default + available models, max tokens
    AzureLanguageOptions.cs        endpoint, API version, default language
    PromptOptions.cs               sample prompt list

  Services/
    ITextGenerationService.cs      generation contract
    OpenAiTextGenerationService.cs OpenAI chat completions integration
    ITextAnalysisService.cs        analysis contract
    AzureTextAnalysisService.cs    Azure Language sentiment + key phrases
    IPromptLibrary.cs              sample prompt contract
    PromptLibrary.cs               configuration-backed prompts
    ServiceResult.cs               success/failure result type
    TextAnalysis.cs                domain analysis result

  Models/
    OpenAi/                        chat completion request/response DTOs
    AzureLanguage/                 analyze-text request/response DTOs

  Components/
    Pages/Home.razor               landing page
    Pages/Playground.razor         generate + analyze workflow and UI
    Layout/                        app shell and navigation
```

## Core Features

### Text generation

- Select from configurable OpenAI models (defaults to `gpt-4o-mini`).
- Start from a sample prompt or write your own.
- Token limit and endpoint driven by configuration.
- Failures surface as friendly messages; raw API errors are logged, not shown.

### Text analysis

- Sentiment classification (positive / neutral / negative / mixed).
- Per-sentiment confidence scores rendered as bars.
- Key phrase extraction shown as chips.
- Both Azure calls share one private helper — no duplication across the UI.

### UI

- Clean, responsive Blazor Server page with scoped CSS.
- Loading state with an inline spinner.
- Separate, non-blocking handling of generation and analysis errors.

## Getting Started

### Prerequisites

- .NET 9 SDK
- An OpenAI API key
- An Azure AI Language resource (key + endpoint)

### 1. Configure secrets

Secrets are read via .NET user secrets, so they never enter source control:

```bash
dotnet user-secrets init --project TextSense
dotnet user-secrets set "OpenAI:ApiKey" "sk-..." --project TextSense
dotnet user-secrets set "AzureLanguage:ApiKey" "<azure-key>" --project TextSense
dotnet user-secrets set "AzureLanguage:Endpoint" "https://<your-resource>.cognitiveservices.azure.com/" --project TextSense
```

Non-secret defaults (endpoints, model list, sample prompts) live in `appsettings.json` and can be edited freely.

### 2. Run the app

```bash
dotnet run --project TextSense/TextSense.csproj
```

Open the ASP.NET Core development URL, then navigate to `/playground`.

## What This Project Demonstrates

- Refactoring tutorial-grade code into a clean, layered Blazor application.
- A thin UI layer that depends on interfaces rather than HTTP plumbing.
- Configuration-driven behavior (models, prompts, endpoints) instead of hardcoded values.
- Consistent error handling with a single result type across services.
- Safe secret management with .NET user secrets.
- Integrating two distinct AI providers into one coherent workflow.

## Roadmap

- Streaming token-by-token generation.
- Unit tests for the service layer with a mocked `HttpClient`.
- Selectable analysis language and additional Azure Language skills.
- Generation history with copy-to-clipboard.
- Public demo deployment and README screenshots.
