# Pet Shop Application

Welcome to the Pet Shop Application repository! This project is a web application designed to manage a virtual pet shop, including features for browsing animals, adding comments, user management, and more.

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Setup Instructions](#setup-instructions)
- [Project Structure](#project-structure)
- [Future Enhancements](#future-enhancements)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Animal Catalog**: Browse and search through a catalog of animals.
- **User Registration and Login**: Secure registration and login with role-based access control.
- **Role Management**: Admins and moderators can manage user roles.
- **Comments**: Users can add comments to animal profiles.
- **Admin Dashboard**: Admins have full control over the application, including user management and content approval.
- **Responsive Design**: Optimized for both desktop and mobile devices.

## Technologies Used

- **Backend**: ASP.NET Core
- **Frontend**: Bootstrap, jQuery
- **Database**: MySQL
- **ORM**: Entity Framework Core
- **Authentication**: ASP.NET Identity
- **Unit Testing**: MSTest

## Setup Instructions

### Prerequisites

- .NET 8.0 SDK
- MySQL Server
- Visual Studio Code or Visual Studio 2022

### Steps

1. **Clone the Repository**:
    \`\`\`bash
    git clone https://github.com/yourusername/petshop-application.git
    cd petshop-application
    \`\`\`

2. **Set Up the Database**:
    - Create a MySQL database and update the connection string in \`appsettings.json\`.

3. **Run Migrations**:
    \`\`\`bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    \`\`\`

4. **Run the Application**:
    \`\`\`bash
    dotnet run
    \`\`\`

5. **Seed the Database** (Optional):
    - Use the provided SQL script to seed initial data into the database.

## Project Structure


├── Controllers<br />
&emsp;├── AccountController.cs<br />
&emsp;├── AdminController.cs<br />
&emsp;├── AnimalController.cs<br />
&emsp;├── CatalogController.cs<br />
&emsp;├── HomeController.cs<br />
&emsp;└── UserManagementController.cs<br />
├── Models<br />
&emsp;├── ApplicationDbContext.cs<br />
&emsp;├── Animal.cs<br />
&emsp;├── Category.cs<br />
&emsp;├── Comment.cs<br />
&emsp;├── EditUser.cs<br />
&emsp;├── Login.cs<br />
&emsp;├── Manage.cs<br />
&emsp;├── Register.cs<br />
&emsp;└── ApplicationUser.cs<br />
├── Services<br />
&emsp;└── FileService.cs<br />
├── Helper<br />
&emsp;└── PaginationHelper.cs<br />
├── ViewModels<br />
&emsp;├── RegisterViewModel.cs<br />
&emsp;├── LoginViewModel.cs<br />
&emsp;├── UserEditViewModel.cs<br />
&emsp;└── CommentViewModel.cs<br />
├── Views<br />
&emsp;├── Account<br />
&emsp;├── Admin<br />
&emsp;├── Catalog<br />
&emsp;└── UserManagement<br />
&emsp;└── wwwroot<br />
&emsp;&emsp;├── css<br />
&emsp;&emsp;├── img<br />
&emsp;&emsp;└── js<br />


## Future Enhancements

- **Advanced Search**: Implement more advanced search filters.
- **Notifications**: Add a notification system for user activities.
- **Animal Adoption**: Implement features for virtual adoption.
- **Enhanced Security**: Add features like two-factor authentication.
- **User Profiles**: Allow users to create and manage their profiles.

## Contributing

We welcome contributions from the community! Please follow these steps:

1. Fork the repository.
2. Create a new branch (\`git checkout -b feature-branch\`).
3. Make your changes.
4. Commit your changes (\`git commit -m 'Add some feature'\`).
5. Push to the branch (\`git push origin feature-branch\`).
6. Create a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

Feel free to expand this README with more details as the project grows. If you have any questions or need further assistance, please open an issue in this repository. Happy coding!
