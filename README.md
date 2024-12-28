# Travel and Accommodation Booking Platform

This project is a comprehensive online Travel and Accommodation Booking Platform. The platform allows users to browse, search, and book hotels, with features for secure payments, detailed hotel information, and admin management functionalities. Below is the full documentation to guide you through the project's setup, development, and usage.

---

## **Project Features**

### **1. User Features**

- **Login Page**:
  - Allows user login with a username and password.
- **Home Page**:
  - Central search bar for finding hotels by location and dates.
  - "Featured Deals" section highlighting special offers.
  - "Recently Visited Hotels" showing personalized recommendations.
  - "Trending Destinations" showcasing the most-visited cities.
- **Search Results Page**:
  - Filters for price range, star rating, and amenities.
  - Infinite scroll of hotel listings with brief descriptions and prices.
- **Hotel Details Page**:
  - High-quality image gallery.
  - Detailed hotel information including reviews and nearby attractions.
  - Room availability and "Add to cart" functionality.
- **Secure Checkout**:
  - User and payment information form.
  - Booking confirmation details and email notifications.

### **2. Admin Features**

- **Admin Dashboard**:
  - Manage cities, hotels, and rooms with CRUD operations.
  - Functional search bar and detailed grids for data visualization.
- **Entity Forms**:
  - Forms for creating and updating cities, hotels, and rooms.

---

## **Technical Requirements**

### **1. API Design**

- RESTful API principles with clean, well-documented endpoints.
- Robust error handling and logging.

### **2. Authentication**

- Secure JSON Web Token (JWT) authentication.
- Role-based access control (RBAC) for admin and user functionalities.

### **3. Testing**

- Unit and integration tests for API endpoints.
- Edge case validation and automated test coverage.

### **4. Deployment**

- Containerized with Docker for consistent environments.
- CI/CD pipelines for automated testing and deployment.
- Deployable on cloud platforms like AWS, Azure, or GCP.

---

## **Setup Instructions**

### **1. Prerequisites**

- Install the following tools:
  - Visual Studio with ASP.NET Core framework.
  - SQL Server or any preferred relational database.
  - Docker (optional for containerization).
  - Postman (optional for API testing).

### **2. Clone the Repository**

```bash
git clone <repository_url>
cd travel-booking-platform
```

### **3. Configure the Environment**

- Create a `.env` file with the following variables:
  ```env
  DB_CONNECTION_STRING=your_database_connection_string
  JWT_SECRET=your_jwt_secret_key
  ```

### **4. Database Setup**

- Run the provided migrations to set up the database schema:
  ```bash
  dotnet ef database update
  ```

### **5. Run the Application**

- Start the server:
  ```bash
  dotnet run
  ```
- Access the application at `http://localhost:5000`.

---

## **API Documentation**

### **1. Authentication Endpoints**

- **POST /api/auth/register**:
  - Registers a new user.
- **POST /api/auth/login**:
  - Authenticates the user and returns a JWT.

### **2. User Endpoints**

- **GET /api/hotels**:
  - Retrieves a list of hotels.
- **GET /api/hotels/{id}**:
  - Retrieves detailed information about a specific hotel.
- **POST /api/booking**:
  - Creates a new booking for a user.

### **3. Admin Endpoints**

- **GET /api/admin/hotels**:
  - Retrieves all hotels (admin access required).
- **POST /api/admin/hotels**:
  - Creates a new hotel.
- **PUT /api/admin/hotels/{id}**:
  - Updates an existing hotel.
- **DELETE /api/admin/hotels/{id}**:
  - Deletes a hotel.

---

## **Testing**

### **1. Unit Tests**

- Ensure each API endpoint has a corresponding unit test.
- Use tools like xUnit or NUnit for testing in .NET.

### **2. Integration Tests**

- Validate end-to-end API flows using tools like Postman or Swagger.

### **3. Performance Testing**

- Use tools like JMeter to test the scalability of the APIs under load.

---

## **Deployment**

### **1. Dockerize the Application**

- Build the Docker image:
  ```bash
  docker build -t travel-booking-platform .
  ```
- Run the container:
  ```bash
  docker run -p 5000:5000 travel-booking-platform
  ```

### **2. Set Up CI/CD**

- Use GitHub Actions or Azure DevOps for automating tests and deployments.

### **3. Deploy to Cloud**

- Configure the deployment on AWS, Azure, or GCP using their respective services.
- Ensure proper environment variables are set in the production environment.

---

## **Future Improvements**

- Implement refresh tokens for long-lived sessions.
- Add real-time notifications for bookings.
- Integrate third-party APIs for payment and travel recommendations.
- Enhance search functionality with AI-powered recommendations.

---

## **Contributors**

- Abdullah jamal
- Supervisors : Hiba Khalifa - Basel Alsayed

For more information, feel free to contact the project team!
