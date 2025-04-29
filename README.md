# 💼 E-Commerce Microservices Architecture with ASP.NET Core

This project is a fully functional **E-commerce backend** built using **.NET microservices architecture**. It includes modular, scalable APIs that communicate through an API Gateway, with support for authentication, product management, and order processing.

---

## 🛆 Microservices

### 1. **API Gateway**
- Central entry point for all clients.
- Handles routing, rate limiting, caching using ocelot, and retries (via Polly).

### 2. **Authentication API**
- Handles user registration, login, and JWT-based authentication.
- Secures other services via access tokens.

### 3. **Product API**
- CRUD operations for products.
- Manages categories, stock, and product details.

### 4. **Order API**
- Manages customer orders.
- Handles order creation, tracking, and history.

---

## 🛠️ Tech Stack

- **ASP.NET Core 8.0**
- **Entity Framework Core**
- **JWT Authentication**
- **API Gateway (YARP or Ocelot)**
- **Polly (Resilience and Retry Policies)**
- **Caching (In-Memory / Redis)**
- **Rate Limiting (Middleware or Gateway)**
- **Docker** (Optional for containerization)
- **Swagger/OpenAPI**

---

## 📂 Project Structure (Clean Architecture)

```

  APiGateway
  Authentication_API_Solution
  ProductAPISolution
  OrderAPiSolution
  E-Commerce.SharedLibrary 
```

Each service follows a **Clean Architecture** approach with:

- `Domain` (Entities, Value Objects)
- `Application` (Business Rules, Interfaces)
- `Infrastructure` (Database, External Services)
- `Presentation/API` (Controllers, DTOs)

---

## 🔐 Authentication Flow

1. User registers or logs in via Authentication API.
2. A **JWT token** is issued and sent in future requests to other services.
3. API Gateway validates the token and forwards the request.

---

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/Mohamed-Ramadan9/ecommerce-microservices.git
cd ecommerce-microservices
```

### 2. Update Configuration

- Set up connection strings in each service's `appsettings.json`
- Update JWT secret keys and issuer/audience settings

### 3. Run Services

You can start each service individually or use Docker (if configured).

```bash
cd src/ProductApi
dotnet run
```

Repeat for other services.

### 4. Access Swagger Docs

-Each API exposes Swagger UI at:

-http://localhost:{port}/swagger

-Note: If you want to test an API independently using Swagger, make sure to remove the line app.UseMiddleware<ListenToOnlyApiGateway>(); located in namespace E_Commerce.SharedLibrary.Dependency_Injection in static class SharedServicesContainer 

---

## 📈 Features

- ✅ Modular microservices
- ✅ Clean Architecture pattern
- ✅ Secure JWT auth
- ✅ Rate limiting and resiliency
- ✅ Caching with fallback strategies
- ✅ OpenAPI (Swagger) documentation

---

## 🤝 Contributing

  Pull requests are welcome. For major changes, open an issue first to discuss what you’d like to change.

---

## 📝 License

This project is licensed under the MIT License.

