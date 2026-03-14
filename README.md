# SmartStay


SmartStay is an AI-powered booking platform developed using **C# and .NET**.  
The system allows users to search and reserve rooms while using **Artificial Intelligence to recommend the best rooms and predict reservation cancellations**.

This project demonstrates how **machine learning can be integrated into a real-world booking system**.

---

# Overview

SmartStay combines a traditional booking platform with intelligent decision support features.

Users can browse rooms, make reservations, and manage their bookings while AI components help improve the experience through:

- intelligent room recommendations
- cancellation prediction
- smart search
- automated support assistance

The goal of the project is to explore how AI can improve **user experience and system efficiency** in booking platforms.

---

# Key Features

## User Management

- User registration
- Secure login and authentication
- User profile management
- Booking history
- JWT-based authentication

---

## Room Management

Administrators can manage rooms within the system.

Features include:

- Create rooms
- Update room information
- Delete rooms
- Manage room availability
- Define room capacity
- Upload room images
- Manage amenities (WiFi, balcony, AC, etc.)

Room attributes include:

- capacity
- price
- amenities
- room type
- rating
- availability

---

## Booking System

Core functionality of the platform.

Users can:

- Search available rooms
- Create reservations
- Modify reservations
- Cancel reservations
- View reservation history

The system automatically handles:

- availability validation
- date conflict detection
- price calculation

---

## Smart Room Recommendation (AI)

The system recommends the most suitable rooms based on **user preferences**.

User preferences may include:

- budget
- number of guests
- preferred amenities
- room rating
- room type

The AI recommendation engine calculates a **match score** for each room and returns the best options.

Example result:

1. Deluxe Room — 92% match  
2. Sea View Room — 88% match  
3. Standard Room — 81% match  

The recommendation score is calculated using factors such as:

- price compatibility
- amenities match
- room capacity
- room rating
- popularity

---

## Cancellation Prediction (AI)

A machine learning model predicts the **probability that a reservation will be cancelled**.

This allows administrators to detect risky reservations and take preventive actions.

Example prediction:

Cancellation probability: **72%**

Features used by the model:

- days until check-in
- reservation price
- booking duration
- user cancellation history
- room type
- season

The prediction model is implemented using **ML.NET**.

---

## Smart Search

Advanced filtering system for discovering rooms.

Filters include:

- price range
- number of guests
- amenities
- rating
- availability dates

Sorting options:

- price
- rating
- AI recommendation score

---

## Reviews and Ratings

After completing a stay, users can leave feedback.

Features include:

- room rating
- written reviews
- average rating calculation

Ratings are also used as an input factor for the **AI recommendation system**.

---

## AI Customer Support Assistant

An integrated assistant helps users solve common issues related to bookings.

Examples of supported questions:

- how to cancel a reservation
- how to change reservation dates
- booking issues
- account problems

The assistant provides automated responses based on system documentation.

---

# AI Components

## Room Recommendation Engine

The recommendation engine calculates a score for each room based on multiple factors.

Example scoring formula:

Score =  
0.35 × Price Match  
0.25 × Amenities Match  
0.20 × Capacity Match  
0.10 × Room Rating  
0.10 × Popularity  

Rooms with the highest score are recommended to the user.

---

## Cancellation Prediction Model

Machine learning model used to estimate the likelihood of reservation cancellation.

Model type:

Binary Classification

Technology used:

ML.NET

Purpose:

Predict whether a reservation is likely to be cancelled.

---

# Technology Stack

## Backend

- ASP.NET Core Web API
- Entity Framework Core

## Frontend

- Blazor
  
## Database

- PostgreSQL

## Artificial Intelligence

- ML.NET
- OpenAI API (optional)

## Authentication

- JWT Authentication

  ```mermaid
erDiagram

    USERS {
        int Id PK
        string Email
        string PasswordHash
        string FirstName
        string LastName
        string Role
        datetime CreatedAt
        bool IsActive
    }

    ROOMS {
        int Id PK
        string Name
        string Description
        int Capacity
        decimal PricePerNight
        int Size
        string BedType
        float AverageRating
        datetime CreatedAt
    }

    AMENITIES {
        int Id PK
        string Name
    }

    ROOM_AMENITIES {
        int RoomId FK
        int AmenityId FK
    }

    BOOKINGS {
        int Id PK
        int UserId FK
        int RoomId FK
        date CheckInDate
        date CheckOutDate
        decimal TotalPrice
        string Status
        datetime CreatedAt
    }

    PAYMENTS {
        int Id PK
        int BookingId FK
        decimal Amount
        string PaymentMethod
        string PaymentStatus
        datetime PaidAt
    }

    REVIEWS {
        int Id PK
        int UserId FK
        int RoomId FK
        int Rating
        string Comment
        datetime CreatedAt
    }

    CANCELLATION_LOGS {
        int Id PK
        int BookingId FK
        datetime CancelledAt
        int DaysBeforeCheckin
        string Reason
    }

    USERS ||--o{ BOOKINGS : makes
    ROOMS ||--o{ BOOKINGS : reserved
    BOOKINGS ||--|| PAYMENTS : has
    USERS ||--o{ REVIEWS : writes
    ROOMS ||--o{ REVIEWS : receives
    ROOMS ||--o{ ROOM_AMENITIES : contains
    AMENITIES ||--o{ ROOM_AMENITIES : assigned
    BOOKINGS ||--o{ CANCELLATION_LOGS : logs


