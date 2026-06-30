# JWT Authentication Exercise

A simple ASP.NET Core Web API exercise demonstrating JWT authentication and authorization.

This project is intended for learning purposes. It uses a hardcoded username and password instead of a database or an identity provider.

## Features

* JWT token generation
* JWT Bearer authentication
* Protected endpoint using `[Authorize]`
* Swagger UI with Bearer token support
* Secret management using .NET User Secrets

## Requirements

* .NET 10 SDK
* Git Bash with OpenSSL, or PowerShell

## Configuration

The public JWT settings are stored in `appsettings.json`:

```json
{
  "JwtSettings": {
    "Issuer": "JwtAuthenticationApi",
    "Audience": "JwtAuthenticationClients"
  }
}
```

The JWT secret must not be committed to Git. Store it locally using .NET User Secrets.

## Create the JWT secret with Git Bash

Open Git Bash in the project directory.

Initialize User Secrets:

```bash
dotnet user-secrets init
```

Generate a random 32-byte secret and save it:

```bash
dotnet user-secrets set "JwtSettings:Secret" "$(openssl rand -base64 32)"
```

Verify that the secret was saved:

```bash
dotnet user-secrets list
```

You should see:

```text
JwtSettings:Secret = ...
```

## Create the JWT secret with PowerShell

Open PowerShell in the project directory.

Initialize User Secrets:

```powershell
dotnet user-secrets init
```

Generate a random 32-byte secret:

```powershell
$bytes = New-Object byte[] 32
$rng = [System.Security.Cryptography.RandomNumberGenerator]::Create()
$rng.GetBytes($bytes)
$secret = [Convert]::ToBase64String($bytes)
$rng.Dispose()
```

Save the generated secret:

```powershell
dotnet user-secrets set "JwtSettings:Secret" "$secret"
```

Verify that the secret was saved:

```powershell
dotnet user-secrets list
```

## Run the application

Restore the NuGet packages:

```bash
dotnet restore
```

Start the API:

```bash
dotnet run
```

Open Swagger UI:

```text
https://localhost:<port>/swagger
```

Use the HTTPS port shown in the terminal when the application starts.

## Test login

Send a `POST` request to:

```text
/api/Auth/login
```

Request body:

```json
{
  "username": "admin",
  "password": "password"
}
```

A successful request returns a JWT token:

```json
{
  "token": "your-jwt-token"
}
```

The username and password are hardcoded because this is a simple authentication exercise. A real application should validate users against a database or an external identity provider.

## Test the protected endpoint

Copy the token returned by the login endpoint.

In Swagger UI:

1. Click **Authorize**.
2. Paste the JWT token.
3. Click **Authorize**.
4. Call the protected endpoint:

```text
GET /api/SecureData
```

A valid token returns a response similar to:

```json
{
  "message": "Congratulations admin, you have reached a protected endpoint!"
}
```

Calling the protected endpoint without a valid token returns:

```text
401 Unauthorized
```

## Security notice

This project is an educational exercise and is not production-ready.

For a production application:

* Do not use hardcoded credentials.
* Store users and securely hashed passwords in a database.
* Do not commit secrets to source control.
* Use a proper identity provider or ASP.NET Core Identity.
* Configure suitable token expiration and refresh-token handling.
* Use HTTPS.
