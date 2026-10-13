# GloboTicket

GloboTicket is a multi-tenant event management platform designed to help organizations manage events and ticketing from one shared system. Each organization (tenant) gets its own workspace for organizing its events, while platform administrators can oversee the service across tenants.

The goal is to give event teams a clear place to manage the event lifecycle and give the platform a foundation that can serve multiple organizations without treating their data as one shared workspace.

## Product direction

GloboTicket is being shaped around two levels of management:

- **Platform administration:** manage and oversee tenants and the overall service.
- **Tenant administration:** manage an organization’s users and its event operations within that tenant’s workspace.

The event management domain is intended to cover events, event categories, and tickets. Tenant-aware access and data isolation are central requirements: users should work within the appropriate organization, and tenant data should be kept separate from other tenants’ data.

## Project structure

The solution is split into four .NET projects:

- **`GloboTicket.API`** — ASP.NET Core Web API and HTTP controllers.
- **`GloboTicket.Application`** — application features, request handling, validation, DTOs, and persistence contracts.
- **`GloboTicket.Domain`** — the domain layer, intended for core business concepts and rules.
- **`GloboTicket.Infrastructure`** — persistence and identity infrastructure, including Entity Framework Core and SQL Server configuration.

The API and Application projects use MediatR for request handling. The Application project also includes FluentValidation. The infrastructure layer contains ASP.NET Core Identity setup and JWT bearer token validation configuration.

## Current development status

This repository is an early-stage foundation for the platform. The solution structure, an initial login request and validator, shared result types, and identity/database configuration are present. Event, event-category, ticket, and user controllers are currently placeholders, and the domain entities and tenant-specific data model have not yet been implemented.

As a result, tenant management, tenant isolation, event and ticket workflows, and the planned platform and tenant dashboards are product goals rather than completed features in the current codebase.

## Technology

- .NET 10 and ASP.NET Core Web API
- Entity Framework Core with SQL Server
- ASP.NET Core Identity
- JWT bearer authentication configuration
- MediatR
- FluentValidation
