# ✅ TodoApp — Modern Todo List Web Application

A beautiful, modern, and responsive Todo List application built with **ASP.NET Core MVC (.NET 10)** and **Entity Framework Core**, featuring a stunning dark glassmorphism UI.

![TodoApp Screenshot](docs/screenshot.png)

## ✨ Features

- ➕ **Add** new todo items with optional descriptions
- ✏️ **Edit** existing todo items
- 🗑️ **Delete** todo items with confirmation dialog
- ✅ **Toggle** completion status (mark as done / not done)
- 🔍 **Filter** todos by All / Active / Completed
- 🧹 **Clear** all completed todos at once
- 📱 **Responsive** UI that works on mobile, tablet, and desktop
- 📝 **Validation** for required title field
- 🌙 **Beautiful dark glassmorphism UI** with gradient animations
- 🎭 **Nice empty state** illustrations when there are no todos
- 🌱 **Seed data** pre-loaded for demo purposes

## 🛠️ Tech Stack

| Technology | Purpose |
|-----------|---------|
| [ASP.NET Core MVC](https://docs.microsoft.com/aspnet/core/) (.NET 10) | Web framework |
| [Entity Framework Core](https://docs.microsoft.com/ef/core/) | ORM / Data access |
| [PostgreSQL](https://www.postgresql.org/) (Primary) | Database (via [Neon](https://neon.tech/)) |
| [SQL Server](https://www.microsoft.com/sql-server) (Fallback) | Alternative database |
| [Bootstrap 5](https://getbootstrap.com/) | UI styling framework |
| [Bootstrap Icons](https://icons.getbootstrap.com/) | Icon library |
| [Google Fonts (Inter)](https://fonts.google.com/specimen/Inter) | Typography |

### Database Provider Strategy

The app supports **dual database providers** with dynamic switching:

- **Primary**: PostgreSQL (hosted on [Neon](https://neon.tech/) free tier)
- **Fallback**: SQL Server (for local development or Azure hosting)

The provider is selected via the `DatabaseProvider` setting in `appsettings.json` or environment variables.

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- PostgreSQL (local or [Neon](https://neon.tech/) free tier) **or** SQL Server (LocalDB or full)

### 1. Clone the Repository

```bash
git clone https://github.com/AhmedSamiir51/TodoApp.git
cd TodoApp
```

### 2. Configure the Connection String

#### Option A: PostgreSQL (Recommended)

Set `DatabaseProvider` to `"PostgreSQL"` in `appsettings.json` and update the connection string:

```json
{
  "DatabaseProvider": "PostgreSQL",
  "ConnectionStrings": {
    "PostgreSQL": "Host=your-host.neon.tech;Database=neondb;Username=neondb_owner;Password=YOUR_PASSWORD;SSL Mode=Require;Trust Server Certificate=true"
  }
}
```

Or use environment variables (recommended for production):

```bash
# Windows PowerShell
$env:ConnectionStrings__PostgreSQL = "Host=your-host.neon.tech;Database=neondb;Username=neondb_owner;Password=YOUR_PASSWORD;SSL Mode=Require;Trust Server Certificate=true"

# Linux / macOS
export ConnectionStrings__PostgreSQL="Host=your-host.neon.tech;Database=neondb;Username=neondb_owner;Password=YOUR_PASSWORD;SSL Mode=Require;Trust Server Certificate=true"
```

#### Option B: SQL Server

Set `DatabaseProvider` to `"SqlServer"`:

```json
{
  "DatabaseProvider": "SqlServer",
  "ConnectionStrings": {
    "SqlServer": "Server=(localdb)\\mssqllocaldb;Database=TodoApp;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### 3. Run the Application

```bash
dotnet run
```

The app will automatically:
1. Apply EF Core migrations
2. Seed sample todo data
3. Start listening on `http://localhost:5208`

### 4. Apply Migrations Manually (Optional)

```bash
# Add a new migration
dotnet ef migrations add MigrationName

# Apply migrations
dotnet ef database update

# Remove last migration
dotnet ef migrations remove
```

## 📁 Project Structure

```
TodoApp/
├── Controllers/
│   ├── HomeController.cs       # Default home controller
│   └── TodoController.cs       # Todo CRUD operations
├── Data/
│   └── AppDbContext.cs          # EF Core DbContext with seed data
├── Migrations/                  # EF Core migrations
├── Models/
│   ├── ErrorViewModel.cs        # Error model
│   ├── TodoItem.cs              # Todo entity model
│   └── TodoViewModel.cs         # View model for todo list
├── Views/
│   ├── Shared/
│   │   └── _Layout.cshtml       # Main layout (dark theme)
│   └── Todo/
│       ├── Index.cshtml         # Main todo list view
│       └── Edit.cshtml          # Edit todo view
├── wwwroot/
│   ├── css/
│   │   └── todo.css             # Custom glassmorphism styles
│   └── js/
│       └── todo.js              # Client-side interactivity
├── appsettings.json             # Configuration
├── Dockerfile                   # Docker deployment
├── Program.cs                   # App entry point & DI config
└── TodoApp.csproj               # Project file
```

## 🌐 Deployment

### Option 1: Render.com + Neon PostgreSQL (Recommended — Free)

#### Step 1: Set up Neon PostgreSQL

1. Go to [neon.tech](https://neon.tech/) and create a free account
2. Create a new project
3. Copy the connection string from the dashboard

#### Step 2: Deploy to Render

1. Push your code to GitHub
2. Go to [render.com](https://render.com/) and create a free account
3. Click **New+** → **Web Service**
4. Connect your GitHub repository
5. Configure:
   - **Environment**: Docker
   - **Instance Type**: Free
6. Add environment variables:
   - `DatabaseProvider` = `PostgreSQL`
   - `ConnectionStrings__PostgreSQL` = `Host=your-host.neon.tech;Database=neondb;Username=neondb_owner;Password=YOUR_PASSWORD;SSL Mode=Require;Trust Server Certificate=true`
7. Click **Create Web Service**

### Option 2: Azure App Service + Azure SQL

1. Create an [Azure](https://portal.azure.com/) free account
2. Create an Azure SQL Database (free tier: 32GB, 100K vCores)
3. Create an App Service (F1 free tier)
4. Set the connection string in App Service Configuration:
   - `DatabaseProvider` = `SqlServer`
   - `ConnectionStrings__SqlServer` = `Server=your-server.database.windows.net;Database=TodoApp;User Id=your-user;Password=YOUR_PASSWORD;`
5. Deploy via GitHub Actions or `dotnet publish`

### Option 3: Railway

1. Create a [Railway](https://railway.app/) account
2. New Project → Deploy from GitHub repo
3. Add a PostgreSQL service
4. Set environment variables with the Railway PostgreSQL connection string
5. Deploy

## 🔒 Security

- ❌ **No hardcoded secrets** — connection strings use environment variables in production
- ✅ **CSRF protection** — all forms use `@Html.AntiForgeryToken()`
- ✅ **Input validation** — server-side validation with Data Annotations
- ✅ **Parameterized queries** — EF Core prevents SQL injection

## 📄 License

This project is open source and available under the [MIT License](LICENSE).

## 🙏 Acknowledgments

- [ASP.NET Core](https://docs.microsoft.com/aspnet/core/) — Microsoft's web framework
- [Neon](https://neon.tech/) — Serverless PostgreSQL
- [Bootstrap](https://getbootstrap.com/) — CSS framework
- [Bootstrap Icons](https://icons.getbootstrap.com/) — Icon set
