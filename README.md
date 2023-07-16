# Project Name

Backend-Application for Innoloft

## Description

This repository contains my solution for the Innoloft assessment. The goal of the assessment was to develop a minified version of the Events module API. The API allows users to perform CRUD operations on events, manage invitations, and interact with user data.

## Table of Contents

- [Implementation Details](#implementation-details)
- [Setup](#setup)
- [Endpoints](#endpoints)
- [Note](#note)

## Implementation Details

In this project, I have used the repository-service-controller pattern with a domain-rich model approach. This design pattern helps separate concerns and maintain a clean architecture.

To improve performance and scalability, I have implemented asynchronous methods throughout the API. Asynchronous programming allows for non-blocking operations, enabling better utilization of system resources and improved responsiveness.

Caching has been implemented to optimize the retrieval of frequently accessed data. By caching certain data, we can avoid repetitive database queries and reduce response times. However, due to time constraints, I did not implement resource-locking techniques like using semaphores. Resource locking would be beneficial in scenarios where multiple users access the same data simultaneously to ensure data consistency and prevent conflicts.

I have included unit testing for one case to demonstrate the testing approach. Given more time, I would expand the test coverage by writing additional unit tests to validate different scenarios and ensure the robustness of the system.

In addition to more unit tests, I would add comments and logging throughout the project to improve code understanding and maintainability. Comments provide clarity and help other developers comprehend the code, while logging allows for easier troubleshooting and debugging during runtime.

## Setup

To run the project locally, follow these steps:

1. Clone the repository to your local machine.
2. Ensure you have the necessary dependencies installed (ASP.NET Core, Docker, etc.).
3. Update the connection string in the `appsettings.json` file to point to your MySQL database.
4. Run the necessary database migrations to set up the required tables and schema.
5. Build and run the project.

## Endpoints

The API provides the following endpoints:

### Events

- `GET /api/events` - Retrieves a paginated list of events.
- `POST /api/events` - Creates a new event.
- `GET /api/events/{eventId}` - Retrieves details of a specific event.
- `PUT /api/events/{eventId}` - Updates an existing event.
- `DELETE /api/events/{eventId}` - Deletes an event.
- `GET /api/events/{eventId}/participants` - Retrieves a list of participants for a specific event.
- `POST /api/events/{eventId}/participants` - Registers a participant for an event.

### Invitations

- `GET /api/invitations` - Retrieves a paginated list of invitations.
- `POST /api/invitations` - Creates a new invitation.
- `GET /api/invitations/{invitationId}` - Retrieves details of a specific invitation.
- `PUT /api/invitations/{invitationId}` - Updates an existing invitation.
- `DELETE /api/invitations/{invitationId}` - Deletes an invitation.

### Users

- `GET /api/users` - Retrieves a paginated list of users.
- `POST /api/users` - Creates a new user.
- `GET /api/users/{userId}` - Retrieves details of a specific user.
- `PUT /api/users/{userId}` - Updates an existing user.
- `DELETE /api/users/{userId}` - Deletes a user.


## Note

To create a user account and participate in an event, follow these steps:

1. Create a user account using the users API by sending a POST request to `/api/users` with valid IDs from 1 to 10.
2. After successfully creating a user account, create an event using the events API by sending a POST request to `/api/events`.
3. Use the IDs of the created user account and event to participate in the event. Send a POST request to `/api/events/{eventId}/participants` with the appropriate parameters.

Please note that the user account will be populated with information from the provided API at [https://jsonplaceholder.typicode.com/users/1](https://jsonplaceholder.typicode.com/users/1).


