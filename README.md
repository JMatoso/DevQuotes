# DevQuotes

An API for code based quotes.
See the documentation here: [Swagger](https://api-devquotes.onrender.com/swagger/index.html)

## Table of Contents

- [Tech Stack](#tech-stack)
- [Installation](#installation)
- [Usage](#usage)
- [Deployment](#deployment)

## Tech Stack

- .NET Core 8+
- VS 2022+

## Installation

To get started with the project, follow these steps:

<p>Restore all packages:</p>

1. Clone the repository:
    ```sh
    git clone https://github.com/jmatoso/DevQuotes.git
    cd DevQuotes
    ```

2. Restore dependencies:
    ```sh
    dotnet restore
    ```

## Usage

To start the development server, set <code>DevQuotes.Api</code> as startup project or run:

```sh
dotnet run
```

## Deployment

The site is hosted on Render and the database on Aiven. Follow these steps for deployment:

1. Set up your Render account and create a new web service.
2. Link your GitHub repository to Render.
3. Add the required environment variables in Render's dashboard.
4. Deploy the application directly from the Render dashboard.

<p>or</p>

<a href="https://render.com/deploy?repo=https://github.com/JMatoso/DevQuotes">
    <img src="https://render.com/images/deploy-to-render-button.svg" alt="Deploy to Render" />
</a>
