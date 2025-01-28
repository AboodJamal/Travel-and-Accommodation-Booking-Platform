# Travel and Accommodation Booking Platform API

Welcome to the **Travel and Accommodation Booking Platform API**, a robust and scalable solution designed to streamline hotel bookings, manage city and hotel information, and enhance guest experiences. This API is built with modern development practices, ensuring flexibility, security, and ease of use for both developers and end-users.

---

## Core Features

### 1. Seamless User Experience
- **User Registration & Login**: Secure and straightforward account creation and authentication for guests.
- **Personalized Guest Services**: Tailored features like booking history, recently visited hotels, and invoice management.

### 2. Advanced Search Capabilities
- **Flexible Hotel Search**: Find hotels by name, room type, price range, or other customizable criteria.
- **Detailed Search Results**: Get comprehensive information about hotels, including amenities, room availability, and pricing.

### 3. Dynamic Content Management
- **Image & Thumbnail Management**: Easily upload, update, or delete images for cities and hotels.
- **Popular Destinations**: Discover trending cities based on user activity and preferences.

### 4. Automated Communication
- **Email Notifications**: Send booking confirmations, invoices, and updates directly to users.
- **Invoice Generation**: Automatically generate and email detailed invoices with booking information, pricing, and hotel details.

### 5. Admin Control Panel
- **Full Entity Management**: Admins can add, update, or delete cities, hotels, rooms, and other entities.
- **Streamlined Operations**: A user-friendly interface simplifies administrative tasks and system maintenance.

---

## API Endpoints Overview

### Authentication
| HTTP Method | Endpoint                         | Description                           |
|-------------|----------------------------------|---------------------------------------|
| POST        | /api/authentication/sign-in     | Authenticate and log in a user.       |
| POST        | /api/authentication/sign-up     | Register a new guest account.         |

### Home
| HTTP Method | Endpoint                                 | Description                         |
|-------------|------------------------------------------|-------------------------------------|
| GET         | /api/home/destinations/trendingCities    | Retrieve trending cities.          |
| GET         | /api/home/search                        | Search for hotels by query.        |
| GET         | /api/home/featuredDeals                | Fetch featured hotel deals.        |

### Cities
| HTTP Method | Endpoint                               | Description                                   |
|-------------|----------------------------------------|-----------------------------------------------|
| GET         | /api/cities                           | Retrieve a list of cities.                   |
| POST        | /api/cities                           | Add a new city.                              |
| GET         | /api/cities/{cityId}                  | Fetch details for a specific city.           |
| DELETE      | /api/cities/{cityId}                  | Delete a city by ID.                         |
| PUT         | /api/cities/{cityId}                  | Update city details.                         |
| PATCH       | /api/cities/{cityId}                  | Partially update city details.               |
| POST        | /api/cities/{cityId}/gallery          | Add images to city gallery.                  |
| GET         | /api/cities/{cityId}/photos           | Fetch city photos.                           |
| PUT         | /api/cities/{cityId}/thumbnail        | Update city thumbnail.                       |
| DELETE      | /api/cities/{cityId}/photos/{photoId} | Delete a city photo.                         |

### Hotels
| HTTP Method | Endpoint                                  | Description                                    |
|-------------|-------------------------------------------|------------------------------------------------|
| GET         | /api/hotels                              | Retrieve all hotels.                          |
| POST        | /api/hotels                              | Add a new hotel.                              |
| GET         | /api/hotels/{hotelId}                   | Fetch details for a specific hotel.           |
| DELETE      | /api/hotels/{hotelId}                   | Delete a hotel by ID.                         |
| PUT         | /api/hotels/{hotelId}                   | Update hotel details.                         |
| GET         | /api/hotels/{hotelId}/availableRooms    | Fetch available rooms in a hotel.             |
| GET         | /api/hotels/{hotelId}/photos           | Fetch hotel photos.                           |
| POST        | /api/hotels/{hotelId}/gallery          | Add images to hotel gallery.                  |
| PUT         | /api/hotels/{hotelId}/thumbnail        | Update hotel thumbnail.                       |
| DELETE      | /api/hotels/{hotelId}/photos/{photoId} | Delete a hotel photo.                         |
| GET         | /api/hotels/{hotelId}/rooms            | Fetch rooms in a hotel.                       |
| POST        | /api/hotels/{hotelId}/rooms            | Add a room to a hotel.                        |
| GET         | /api/hotels/{hotelId}/rooms/{roomId}   | Fetch details of a specific room.             |
| GET         | /api/hotels/{hotelId}/roomTypes        | Fetch room types in a hotel.                  |
| GET         | /api/hotels/{hotelId}/bookings         | Fetch bookings for a hotel.                   |

### Guests
| HTTP Method | Endpoint                                          | Description                                    |
|-------------|---------------------------------------------------|------------------------------------------------|
| GET         | /api/guests/{guestId}/recentlyvisitedhotels       | Fetch recently visited hotels by a guest.     |
| GET         | /api/guests/recentlyvisitedhotels                 | Fetch recently visited hotels.                |
| GET         | /api/guests/bookings                             | Retrieve bookings for a guest.                |
| POST        | /api/guests/bookings                             | Reserve a room.                               |
| DELETE      | /api/guests/bookings/{bookingId}                 | Cancel a booking.                             |
| GET         | /api/guests/bookings/{bookingId}/invoice         | Fetch booking invoice.                        |

### Reviews
| HTTP Method | Endpoint                        | Description                        |
|-------------|---------------------------------|------------------------------------|
| GET         | /api/reviews/hotels/{hotelId}  | Fetch reviews for a hotel.        |
| POST        | /api/reviews                   | Add a new review.                 |

### Room Amenities
| HTTP Method | Endpoint                        | Description                        |
|-------------|---------------------------------|------------------------------------|
| GET         | /api/roomAmenities             | Fetch all room amenities.         |
| POST        | /api/roomAmenities             | Add a new room amenity.           |
| GET         | /api/roomAmenities/{amenityId} | Fetch details of a specific amenity. |
| DELETE      | /api/roomAmenities/{amenityId} | Delete a room amenity.            |
| PUT         | /api/roomAmenities/{amenityId} | Update a room amenity.            |

### Room Types
| HTTP Method | Endpoint                                  | Description                        |
|-------------|-------------------------------------------|------------------------------------|
| GET         | /api/roomTypes/{roomTypeId}/discounts    | Fetch discounts for a room type. |
| GET         | /api/roomTypes/discounts/{discountId}    | Fetch details of a specific discount. |
| DELETE      | /api/roomTypes/discounts/{discountId}    | Delete a discount.                |
| POST        | /api/roomTypes/discounts                | Add a new discount.               |

---

## Technology Stack

- **Backend**
  - **C#**: Primary programming language.
  - **ASP.NET Core**: Framework for building high-performance APIs.
  - **Entity Framework Core**: ORM for database interactions.
  - **SQL Server**: Relational database for data storage.

- **Image Storage**
  - **Cloudinary Storage**: Scalable cloud storage for images and thumbnails.

- **API Documentation**
  - **Swagger/OpenAPI**: For API specification and interactive documentation.

- **Security**
  - **JWT (JSON Web Tokens)**: Secure authentication and authorization.
  - **HTTPS**: Encrypted communication.
  - **Argon2**: Password hashing for enhanced security.

- **Architecture**
  - **Clean Architecture**: Separation of concerns for maintainability.
  - **CQRS Pattern**: Optimized handling of read and write operations.

---

## Getting Started

### Prerequisites
- **.NET 8 SDK** installed.
- **SQL Server** instance with a database.

### Setup Instructions

1. **Clone the Repository**:
```bash
git clone https://github.com/your-repo/Travel-And-Accommodation-Booking-Platform.git
```

2. **Navigate to the Project Directory**:
```bash
cd Travel-And-Accommodation-Booking-Platform/API
```

3. **Configure `appsettings.json`**:
Update the connection string with your SQL Server details:
```json
{
  "ConnectionStrings": {
    "TAABPCoreDb": "<your_connection_string>"
  }
}
```

4. **Run the API**:
```bash
dotnet run
```

5. **Access the API**:
   - **Swagger UI**: [https://localhost:7056/swagger](https://localhost:7056/swagger)
   - **API Base URL**: [https://localhost:7056](https://localhost:7056)

---

## Contribution Guidelines

We welcome contributions to improve this project! Here’s how you can help:

1. **Report Issues**: Found a bug? Let us know on GitHub.
2. **Suggest Features**: Have an idea? Share it with us.
3. **Code Contributions**: Submit pull requests for new features or fixes.

---

## Contact & Support

For questions, feedback, or support, feel free to reach out:

- **Email**: abood.jamal005@gmail.com
- **GitHub**: [Your GitHub Profile](https://github.com/AboodJamal)

Thank you for exploring the **Travel and Accommodation Booking Platform API**! We hope this tool empowers your travel and hospitality solutions. Let’s build something amazing together! 🚀

