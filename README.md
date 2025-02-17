# BarNone.Backend

Repository for bar menu system backend code

# Overview

This API is a backend for a cocktail menu website. Its endpoints detail menu items, detail descriptive tags that can be applied to menu items, detail bar inventory, and allow a frontend to take drink orders and save them to the application database. It also provides admin functions, allowing a manager to add and/remove cocktails and ingredients from the database.

# Specification

The API specification is currently built in Swagger, and is hosted here: <a href="https://jpzimmerman.github.io/BarNone.Backend/#/">BarNone API documentation</a>

# Authentication/Authorization

API supports cookie authentication, as well as Google OAuth authentication for an app-user and an admin. Auth0 tokens will be provided through the use of an API gateway (currently planned to be hosted in AWS), and will provide for an admin and app-user context.

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
    <td>.NET 9.0</td>
  </tr>
  <tr>
    <td>CI/CD Pipeline</td>
    <td>CircleCI</td>
  </tr>
</table>
