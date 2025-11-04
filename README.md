# JWT Authentication API

This is a .NET Core API that provides JWT (JSON Web Token) based authentication functionality. It includes endpoints for user registration, login, and token refresh.

## Installation

1. Clone the repository:
```
git clone https://github.com/your-username/jwt-auth-api.git
```

2. Navigate to the project directory:
```
cd jwt-auth-api
```

3. Restore the NuGet packages:
```
dotnet restore
```

4. Update the connection string in the `appsettings.json` file to match your database configuration.

5. Apply the database migrations:
```
dotnet ef database update
```

6. Build and run the application:
```
dotnet run
```

The API will be available at `https://localhost:5001/api`.

## Usage

### Registration
To register a new user, send a POST request to the `/api/auth/register` endpoint with the following JSON payload:

```json
{
  "username": "your-username",
  "password": "your-password"
}
```

### Login
To log in, send a POST request to the `/api/auth/login` endpoint with the following JSON payload:

```json
{
  "username": "your-username",
  "password": "your-password"
}
```

The response will include an access token and a refresh token.

### Refresh Token
To refresh the access token, send a POST request to the `/api/auth/refresh-token` endpoint with the following JSON payload:

```json
{
  "refreshToken": "your-refresh-token"
}
```

### Authorized Endpoints
To access the authorized endpoints, include the access token in the `Authorization` header of your requests:

```
Authorization: Bearer your-access-token
```

The API includes two authorized endpoints:
- `/api/auth/auth-endpoint`: Accessible to all authenticated users.
- `/api/auth/admin-endpoint`: Accessible only to users with the "Admin" role.

## API

The API includes the following endpoints:

| Endpoint | HTTP Method | Description |
| --- | --- | --- |
| `/api/auth/register` | POST | Register a new user. |
| `/api/auth/login` | POST | Log in and receive an access token and a refresh token. |
| `/api/auth/refresh-token` | POST | Refresh the access token using the refresh token. |
| `/api/auth/auth-endpoint` | GET | Accessible to all authenticated users. |
| `/api/auth/admin-endpoint` | GET | Accessible only to users with the "Admin" role. |

## Contributing

1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Implement your changes.
4. Test your changes.
5. Submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE).

## Testing

To run the tests, execute the following command:

```
dotnet test
```

This will run the unit tests for the application.
