# blazor-showcase-diagram-collaborative-editing

A Blazor Server application showcasing real-time collaborative diagram editing powered by SignalR and Redis, built using Syncfusion Blazor components.

## Overview

This showcase demonstrates a multi-user diagram builder where multiple clients can simultaneously create and edit diagrams in real time. Changes made by any user are instantly broadcast to all other connected users in the same room, with conflict detection and resolution handled server-side.

## Features

- **Real-time collaboration** via ASP.NET Core SignalR
- **Room-based sessions** allowing multiple independent diagram workspaces
- **Live cursor and selection tracking** across all connected users
- **Diagram state persistence** using Redis as the backing store
- **Syncfusion Blazor Diagram** component for rich diagramming capabilities

## Prerequisites

- .NET 8.0 SDK or later
- A running Redis instance
- A valid Syncfusion license key

## Configuration

Update the Redis connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "Redis": "your-redis-connection-string"
}
```

## Getting Started

1. Clone the repository.
2. Configure the Redis connection string as described above.
3. Run the `DiagramCollaborativeEditing` project.
4. Open multiple browser tabs to experience live collaborative editing.

## License

This project uses Syncfusion Blazor components, which require a valid license.