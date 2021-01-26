# BidOne
Bid One Coding Test

This is a simple ASP.NET application which allows a user to configure an application with their first name and last name.

This uses the following features:

1. MVC Razor view pages, utilizing TagHelpers and unobtrusive validation using JQuery.
2. Server-side validation via model state.
3. Model binding of the first name and last name values posted to the controller.
4. Dependency injection via constructor of the class that provides JSON storage (JSONUserConfigurationStorage)
5. Async methods for getting/setting the contents of the file.
6. Basic suite of unit tests.

