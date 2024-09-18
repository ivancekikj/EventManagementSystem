# Functional description

A system for purchasing tickets for events. Users can register as consumers or administrators. Consumers can add tickets to their shopping cart, purchase tickets and view personal order details. Administrators manage information about events, schedules and tickets. Additionally, they can view all order details, export order invoices and import events from spreadsheets. The app also displays information about events from a partner event management system.

# Architecture

The system is coded as an ASP.NET Core MVC web application in .NET 8 with the Onion architecture and uses an SQL Server database. It also communicates with an external web application by using an API controller.
