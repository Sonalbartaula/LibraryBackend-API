# Library Management System Backend

## Installation

1. Clone the repository:
```
git clone https://github.com/your-username/LibraryBackend-API.git
```
2. Navigate to the project directory:
```
cd LibraryBackend-API
```
3. Install the required dependencies:
```
dotnet restore
```
4. Build the project:
```
dotnet build
```
5. Run the application:
```
dotnet run
```

## Usage

The Library Management System provides the following features:

1. **Book Management**:
   - Add new books
   - Edit book details
   - Delete books
   - Search for books based on title, author, ISBN, and category

2. **Student Management**:
   - Add new students
   - Edit student details
   - Delete students
   - Search for students based on name, ID, type, and status

3. **Transactions**:
   - Check out books
   - Return books
   - View active loans
   - View transaction history

## API

The application exposes the following API endpoints:

### Authentication
- `POST /api/auth/register`: Register a new user
- `POST /api/auth/login`: Login and obtain access and refresh tokens
- `POST /api/auth/refresh-token`: Refresh access token using a valid refresh token
- `GET /api/auth/auth-endpoint`: Endpoint that requires authentication
- `GET /api/auth/admin-endpoint`: Endpoint that requires admin authorization

### Books
- `GET /api/books`: Get all books
- `POST /api/books/addbooks`: Add a new book (requires admin authorization)
- `PUT /api/books/edit/{id}`: Update a book (requires admin authorization)
- `DELETE /api/books/delete/{id}`: Delete a book (requires admin authorization)
- `GET /api/books/search`: Search for books based on various criteria

### Students
- `GET /api/students/student`: Get all students
- `POST /api/students/addstudent`: Add a new student (requires admin authorization)
- `PUT /api/students/editstudent`: Update a student (requires admin authorization)
- `DELETE /api/students/deletestudent/{id}`: Delete a student (requires admin authorization)
- `GET /api/students/search`: Search for students based on various criteria

### Transactions
- `POST /api/transactions/checkout`: Check out a book (requires admin or staff authorization)
- `GET /api/transactions/active`: Get all active loans
- `PUT /api/transactions/return/{id}`: Return a book (requires admin or staff authorization)
- `GET /api/transactions/history`: Get the transaction history

## Contributing

1. Fork the repository
2. Create a new branch: `git checkout -b feature/your-feature`
3. Make your changes and commit them: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin feature/your-feature`
5. Submit a pull request

## License

This project is licensed under the [MIT License](LICENSE).

## Testing

To run the tests, execute the following command:

```
dotnet test
```

This will run the unit tests for the application.
