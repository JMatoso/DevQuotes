# DevQuotes

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE.txt)
[![Build](https://img.shields.io/badge/build-passing-brightgreen.svg)]()
[![API](https://img.shields.io/badge/API-v1.0-blue.svg)](https://api-devquotes.onrender.com/swagger/index.html)

## Executive Summary

DevQuotes is a RESTful API service that provides programming-themed inspirational quotes formatted as code snippets across multiple programming languages. The application delivers code-based motivational content tailored to developers, making inspiration accessible through familiar syntax and programming constructs.

The service addresses the need for developer-specific motivational content by offering quotes that resonate with software engineering professionals. Each quote is presented in the syntax of popular programming languages including C#, Java, JavaScript, Python, and others, creating a unique intersection between motivation and code.

### Key Features

- Random quote generation with optional language-specific filtering
- Support for multiple programming languages (C#, Java, JavaScript, Python, Go, Rust, TypeScript, and more)
- RESTful API with comprehensive Swagger documentation
- Built on clean architecture principles with clear separation of concerns
- SQLite database for efficient data storage and retrieval
- Docker containerization for streamlined deployment
- Comprehensive logging with Serilog
- API versioning support
- CORS-enabled for cross-origin requests

### Problem Statement

Developers often seek motivation and inspiration that aligns with their professional context. Traditional motivational content may not resonate with technical audiences. DevQuotes solves this by presenting inspirational messages in familiar programming syntax, making the content more relatable and engaging for software developers.

## Table of Contents

- [Executive Summary](#executive-summary)
- [Project Structure](#project-structure)
- [System Architecture](#system-architecture)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)
- [Available Commands](#available-commands)
- [API Documentation](#api-documentation)
- [Docker Deployment](#docker-deployment)
- [Contributing](#contributing)
- [License](#license)
- [Maintainers](#maintainers)

## Project Structure

The solution follows Clean Architecture principles with clear separation between layers:

```
DevQuotes/
├── DevQuotes.Api/              # Presentation layer - API controllers and middleware
│   ├── Controllers/            # REST API endpoints
│   ├── Extensions/             # Service registration and pipeline configuration
│   ├── Properties/             # Launch settings and configuration
│   └── wwwroot/                # Static files (HTML, images)
├── DevQuotes.Application/      # Application layer - Business logic and use cases
│   ├── Extensions/             # Application service extensions
│   ├── Mapping/                # Object mapping configurations
│   └── UseCases/               # Application use case implementations
├── DevQuotes.Domain/           # Domain layer - Core business entities
│   └── Entities/               # Domain models
├── DevQuotes.Infrastructure/   # Infrastructure layer - Data access and external services
│   ├── Extensions/             # Infrastructure service extensions
│   ├── Helpers/                # Utility classes (pagination, etc.)
│   ├── Options/                # Configuration options
│   └── Repository/             # Data access implementations
├── DevQuotes.Communication/    # Cross-cutting - DTOs and contracts
│   ├── Requests/               # Request models
│   └── Responses/              # Response models
├── DevQuotes.Shared/           # Cross-cutting - Shared utilities
│   └── Extensions/             # Common extension methods
├── DevQuotes.Exceptions/       # Cross-cutting - Exception definitions
└── DevQuotes.sln               # Solution file
```

### Layer Responsibilities

- **API Layer**: Handles HTTP requests, routing, and response formatting
- **Application Layer**: Contains business logic and orchestrates use cases
- **Domain Layer**: Defines core business entities and domain logic
- **Infrastructure Layer**: Implements data persistence, external service integration, and logging
- **Communication Layer**: Provides data transfer objects for API contracts
- **Shared Layer**: Contains utilities and extensions used across all layers
- **Exceptions Layer**: Defines custom exception types for error handling

## System Architecture

DevQuotes implements a layered architecture based on Clean Architecture principles:

```
┌─────────────────────────────────────────────────────────┐
│                    Presentation Layer                    │
│              (DevQuotes.Api - Controllers)              │
└────────────────────┬────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────┐
│                   Application Layer                      │
│         (DevQuotes.Application - Use Cases)             │
└────────────────────┬────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────┐
│                     Domain Layer                         │
│            (DevQuotes.Domain - Entities)                │
└─────────────────────────────────────────────────────────┘
                     ▲
                     │
┌────────────────────┴────────────────────────────────────┐
│                 Infrastructure Layer                     │
│     (DevQuotes.Infrastructure - Data Access & EF)       │
└─────────────────────────────────────────────────────────┘
```

### Technology Stack

- **Framework**: .NET 8.0
- **ORM**: Entity Framework Core 9.0 (Preview)
- **Database**: SQLite with proxy support
- **Logging**: Serilog with console and exception enrichment
- **API Documentation**: Swashbuckle (Swagger/OpenAPI)
- **API Versioning**: Asp.Versioning.Mvc 8.1.0
- **Functional Programming**: LanguageExt.Core 4.4.9
- **Containerization**: Docker
- **JSON Processing**: Newtonsoft.Json

## Prerequisites

Before setting up the project, ensure you have the following installed:

- **.NET SDK 8.0 or higher** - [Download](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Visual Studio 2022 or later** (optional, for IDE development) - [Download](https://visualstudio.microsoft.com/downloads/)
- **Docker Desktop** (optional, for containerized deployment) - [Download](https://www.docker.com/products/docker-desktop)
- **Git** - [Download](https://git-scm.com/downloads)

### Recommended Tools

- **Visual Studio Code** with C# extension
- **Postman** or similar API testing tool
- **.NET CLI** for command-line operations

## Installation

Follow these steps to set up the development environment:

### Step 1: Clone the Repository

```bash
git clone https://github.com/JMatoso/DevQuotes.git
cd DevQuotes
```

### Step 2: Restore Dependencies

Restore all NuGet packages required by the solution:

```bash
dotnet restore
```

### Step 3: Build the Solution

Compile all projects in the solution:

```bash
dotnet build
```

### Step 4: Verify Installation

Ensure the build completes without errors. The output should indicate successful compilation of all seven projects:

- DevQuotes.Api
- DevQuotes.Application
- DevQuotes.Domain
- DevQuotes.Infrastructure
- DevQuotes.Communication
- DevQuotes.Shared
- DevQuotes.Exceptions

## Configuration

### Environment Variables

The application can be configured using the following environment variables:

| Variable | Description | Default Value | Required |
|----------|-------------|---------------|----------|
| `ASPNETCORE_ENVIRONMENT` | Application environment (Development, Staging, Production) | `Development` | Yes |
| `ASPNETCORE_URLS` | URLs the application listens on | `http://localhost:5047` | No |
| `ConnectionStrings__DefaultConnection` | Database connection string | `Filename=DevQuotes.db;Cache=Shared` | Yes |

### Application Settings

Configuration is managed through `appsettings.json` located in the `DevQuotes.Api` project:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Filename=DevQuotes.db;Cache=Shared"
  }
}
```

### Database Configuration

The application uses SQLite as its database provider. The database file (`DevQuotes.db`) is created automatically in the application directory on first run. Quote data is seeded from `quotes.v2.json` during initial setup.

### Logging Configuration

Serilog is configured for comprehensive logging with the following features:

- Console output with enriched exception details
- Structured logging format
- Configurable log levels per namespace

## Usage

### Running the Application Locally

#### Option 1: Using .NET CLI

Navigate to the API project directory and run:

```bash
cd DevQuotes.Api
dotnet run
```

The application will start and listen on the configured ports (default: `http://localhost:5047` and `https://localhost:7000`).

#### Option 2: Using Visual Studio

1. Open `DevQuotes.sln` in Visual Studio 2022 or later
2. Set `DevQuotes.Api` as the startup project
3. Press `F5` to start debugging or `Ctrl+F5` to run without debugging

#### Option 3: Using Docker

See [Docker Deployment](#docker-deployment) section below.

### Accessing the Application

Once running, access the following endpoints:

- **Swagger UI**: `http://localhost:5047/swagger/index.html`
- **Home Page**: `http://localhost:5047/index.html`
- **API Base URL**: `http://localhost:5047/api/v1/`

### Example API Requests

#### Get a Random Quote

```bash
curl -X GET "http://localhost:5047/api/v1/quotes/random" -H "accept: application/json"
```

#### Get a Random Quote for Specific Language

```bash
curl -X GET "http://localhost:5047/api/v1/quotes/random?code=cs" -H "accept: application/json"
```

#### Get All Supported Languages

```bash
curl -X GET "http://localhost:5047/api/v1/language" -H "accept: application/json"
```

## Available Commands

The following commands are available for common development tasks:

### Build Commands

```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Build in Release mode
dotnet build --configuration Release

# Clean build artifacts
dotnet clean
```

### Run Commands

```bash
# Run the API project
dotnet run --project DevQuotes.Api

# Run with specific environment
dotnet run --project DevQuotes.Api --environment Production

# Run with watch mode (auto-restart on changes)
dotnet watch --project DevQuotes.Api
```

### Docker Commands

```bash
# Build Docker image
docker build -t devquotes .

# Run container
docker run -p 8080:10000 devquotes

# Run with docker-compose
docker-compose up

# Stop docker-compose services
docker-compose down
```

### Testing Commands

```bash
# Run all tests (if test projects exist)
dotnet test

# Run tests with coverage
dotnet test /p:CollectCoverage=true
```

## API Documentation

Comprehensive API documentation is available through Swagger/OpenAPI at runtime:

**Local Development**: `http://localhost:5047/swagger/index.html`

**Production**: `https://api-devquotes.onrender.com/swagger/index.html`

### API Versioning

The API uses versioning to maintain backward compatibility. Current version: `v1.0`

All endpoints are prefixed with: `/api/v1/`

### Response Formats

All responses are returned in JSON format following consistent patterns:

**Success Response**:
```json
{
  "id": "string",
  "content": "string",
  "language": "string",
  "code": "string"
}
```

**Error Response**:
```json
{
  "message": "string",
  "errors": ["string"]
}
```

## Docker Deployment

The application includes full Docker support for containerized deployment.

### Building the Docker Image

```bash
docker build -t devquotes:latest .
```

### Running the Container

```bash
docker run -d -p 10000:10000 --name devquotes-api devquotes:latest
```

### Using Docker Compose

The project includes a `docker-compose.yml` file for simplified deployment:

```bash
docker-compose up -d
```

This will build and start the application, exposing it on port 8080.

### Docker Configuration

The Dockerfile uses multi-stage builds for optimal image size:

1. **Base stage**: Runtime environment using `mcr.microsoft.com/dotnet/aspnet:8.0`
2. **Build stage**: Build environment using `mcr.microsoft.com/dotnet/sdk:8.0`
3. **Publish stage**: Publishes the application
4. **Final stage**: Minimal runtime image with published artifacts

### Cloud Deployment

The application is deployed on Render with the following configuration:

1. Create a new Web Service on Render
2. Connect your GitHub repository
3. Configure environment variables in Render dashboard
4. Deploy using the Render button or manual deployment

[![Deploy to Render](https://render.com/images/deploy-to-render-button.svg)](https://render.com/deploy?repo=https://github.com/JMatoso/DevQuotes)

## Contributing

We welcome contributions to improve DevQuotes. Please follow these guidelines:

### Reporting Issues

1. Check existing issues to avoid duplicates
2. Use the issue template when creating new issues
3. Provide detailed information including:
   - Clear description of the issue
   - Steps to reproduce
   - Expected vs actual behavior
   - Environment details (OS, .NET version, etc.)
   - Relevant logs or error messages

### Submitting Pull Requests

1. Fork the repository
2. Create a feature branch from `main`:
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. Make your changes following the code standards
4. Ensure all builds and tests pass
5. Commit your changes with clear, descriptive messages:
   ```bash
   git commit -m "Add feature: description of changes"
   ```
6. Push to your fork:
   ```bash
   git push origin feature/your-feature-name
   ```
7. Create a Pull Request with:
   - Clear title and description
   - Reference to related issues
   - Summary of changes made
   - Any breaking changes highlighted

### Code Standards

- Follow C# coding conventions and .NET best practices
- Maintain clean architecture principles
- Write clear, self-documenting code
- Add XML documentation comments for public APIs
- Ensure proper error handling and logging
- Keep code DRY (Don't Repeat Yourself)
- Write meaningful commit messages

### Development Workflow

1. Ensure your development environment matches the prerequisites
2. Create a new branch for each feature or bug fix
3. Write code following the established patterns in the project
4. Test your changes locally
5. Update documentation if needed
6. Submit a pull request for review

## License

This project is licensed under the MIT License. See the [LICENSE.txt](LICENSE.txt) file for complete license text.

Copyright (c) 2024 DevQuotes Contributors

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

## Maintainers

### Project Owner

**João Matoso**
- GitHub: [@JMatoso](https://github.com/JMatoso)
- Repository: [DevQuotes](https://github.com/JMatoso/DevQuotes)

### Contact Information

For questions, support, or contributions:

- **Issues**: Submit via [GitHub Issues](https://github.com/JMatoso/DevQuotes/issues)
- **Discussions**: Use [GitHub Discussions](https://github.com/JMatoso/DevQuotes/discussions) for general questions
- **Documentation**: Refer to [API Documentation](https://api-devquotes.onrender.com/swagger/index.html)

### Support

For enterprise support or custom implementations, please contact the maintainers through the repository's issue tracker or discussion board.
