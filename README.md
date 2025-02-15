# BarNone.Backend
Repository for bar menu system backend code

# Overview
This API is a backend for a cocktail menu website. Its endpoints detail menu items, detail descriptive tags that can be applied to menu items, detail bar inventory, and allow a frontend to take drink orders and save them to the application database. It also provides admin functions, allowing a manager to add and/remove cocktails and ingredients from the database.

# Specification
The API specification is currently built in Swagger, and is hosted here: <a href="https://jpzimmerman.github.io/BarNone.Backend/#/">BarNone API documentation</a>

# Authentication/Authorization
Authentication and authorization support are planned features, and will support Auth0 authentication for an app-user and an admin. Authentication and authorization will be implemented through the use of an API gateway (currently planned to be hosted in AWS), and through decorators on the controllers and endpoints in code.

# Testing
To run this application locally:<br>
1.) Navigate to ./BarNone.API in a terminal application of your choice<br>
2.) Execute the following command: <strong> dotnet run</strong><br>

# Tech stack
<table>
  <tr>
    <td>Language</td>
    <td>C#</td>
  </tr>
  <tr>
    <td>Framework</td>
    <td>.NET 6.0</td>
  </tr>
  <tr>
    <td>CI/CD Pipeline</td>
    <td>CircleCI</td>
  </tr>
</table>
