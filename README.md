
Web platform for charity auctions to support the Armed Forces of Ukraine. Built with ASP.NET Core and React.

## 📊 Code Quality & Coverage

[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=synapsetron_charity-auction-platform&metric=ncloc&token=759f83b4461d5e7f72f0f061991ec4f61382b60d)](https://sonarcloud.io/summary/new_code?id=synapsetron_charity-auction-platform)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=synapsetron_charity-auction-platform&metric=coverage&token=759f83b4461d5e7f72f0f061991ec4f61382b60d)](https://sonarcloud.io/summary/new_code?id=synapsetron_charity-auction-platform)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=synapsetron_charity-auction-platform&metric=bugs&token=759f83b4461d5e7f72f0f061991ec4f61382b60d)](https://sonarcloud.io/summary/new_code?id=synapsetron_charity-auction-platform)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=synapsetron_charity-auction-platform&metric=vulnerabilities&token=759f83b4461d5e7f72f0f061991ec4f61382b60d)](https://sonarcloud.io/summary/new_code?id=synapsetron_charity-auction-platform)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=synapsetron_charity-auction-platform&metric=security_rating&token=759f83b4461d5e7f72f0f061991ec4f61382b60d)](https://sonarcloud.io/summary/new_code?id=synapsetron_charity-auction-platform)

# 🎯 Charity Auction Platform

[![.NET](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/download)
[![React](https://img.shields.io/badge/React-19.0.0-blue.svg)](https://reactjs.org/)
[![TypeScript](https://img.shields.io/badge/TypeScript-4.9.5-blue.svg)](https://www.typescriptlang.org/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Latest-green.svg)](https://www.postgresql.org/)
[![Docker](https://img.shields.io/badge/Docker-3.8-blue.svg)](https://www.docker.com/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

A modern, full-stack charity auction platform built with **ASP.NET Core 9.0** and **React 19**. This platform enables users to create, participate in, and manage charity auctions with real-time bidding, payment processing, and comprehensive admin controls.

## 🚀 Features

- **🔐 Secure Authentication** - JWT-based auth with Google OAuth integration
- **⚡ Real-time Bidding** - Live auction updates via SignalR
- **💳 Payment Processing** - Support for LiqPay and Fondy payment gateways
- **📊 Admin Dashboard** - Comprehensive auction and user management
- **🌍 Internationalization** - Multi-language support (English/Ukrainian)
- **📱 Responsive Design** - Modern UI with Bootstrap and Framer Motion
- **🤖 Content Moderation** - AI-powered content filtering via Perspective API
- **📧 Email Notifications** - SMTP-based email system
- **📈 Analytics** - Detailed statistics and reporting
- **🔄 Background Jobs** - Automated auction closing with Hangfire

## 🛠️ Technology Stack

### Backend
| **Category**           | **Technology**                                    |
|----------------------|---------------------------------------------------|
| **Framework**         | ASP.NET Core 9.0                                 |
| **Database**          | PostgreSQL with Entity Framework Core            |
| **Authentication**    | JWT Bearer Tokens + ASP.NET Core Identity + Google OAuth  + Facebook Login     |
| **Real-time**         | SignalR with Azure SignalR Service               |
| **Background Jobs**   | Hangfire with PostgreSQL storage                |
| **Payment Gateways**  | LiqPay, Fondy (CloudIpspSDK)                    |
| **Content Moderation**| Google Perspective API                           |
| **Email Service**     | SMTP with SendGrid support                      |
| **API Documentation** | Swagger/OpenAPI 3.0                             |
| **Validation**        | FluentValidation                                |
| **Mapping**           | AutoMapper                                      |
| **Logging**           | Serilog                                         |
| **Testing**           | xUnit with Moq                                 |
| **SonarQube**         | Code review                             |

### Frontend
| **Category**           | **Technology**                                    |
|----------------------|---------------------------------------------------|
| **Framework**         | React 19 with TypeScript                        |
| **Routing**           | React Router DOM 7                              |
| **State Management**  | React Context API                               |
| **UI Framework**      | React Bootstrap 5                               |
| **Styling**           | Bootstrap 5 + Framer Motion                     |
| **HTTP Client**       | Axios                                           |
| **Real-time**         | SignalR Client                                  |
| **Internationalization**| i18next with react-i18next                    |
| **Authentication**    | Google OAuth integration                        |
| **Notifications**     | React Toastify                                 |
| **Charts**            | Recharts                                        |
| **SonarQube**         | Code review                             |

## 📋 Prerequisites

Before running this project, ensure you have the following installed:

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/)
- [Docker & Docker Compose](https://www.docker.com/get-started)
- [PostgreSQL](https://www.postgresql.org/download/) (if not using Docker)
- [Git](https://git-scm.com/)

## 🔧 Installation & Setup

### 1️⃣ Clone the Repository

```bash
git clone https://github.com/yourusername/charity-auction-platform.git
cd charity-auction-platform
```

### 2️⃣ Backend Setup

#### Option A: Using Docker (Recommended)

```bash
cd Backend
# Create .env file (see Environment Variables section below)
cp .env.example .env
# Edit .env with your configuration

# Start all services
docker-compose up -d
```

#### Option B: Local Development

```bash
cd Backend

# Restore dependencies
dotnet restore

# Set up user secrets (see Environment Variables section)
dotnet user-secrets set "JwtSettings:Secret" "your-super-secret-key-here"
# ... add other secrets

# Run database migrations
dotnet ef database update --project CharityAuction.Infrastructure --startup-project CharityAuction.WebAPI

# Start the application
dotnet run --project CharityAuction.WebAPI
```

The backend will be available at:
- **API**: http://localhost:5000
- **Swagger UI**: http://localhost:5000/swagger
- **Hangfire Dashboard**: http://localhost:5000/hangfire

### 3️⃣ Frontend Setup

```bash
cd Frontend

# Install dependencies
npm install

# Create environment file
cp .env.example .env.local
# Edit .env.local with your configuration

# Start development server
npm start
```

The frontend will be available at: http://localhost:3000

## 🔐 Environment Variables

⚠️ **IMPORTANT**: Never commit sensitive information like API keys, passwords, or secrets to version control. Use environment variables or user secrets instead.

### Backend Configuration

Create a `.env` file in the `Backend` directory:

```env
# Database Configuration
POSTGRES_DB=charity_auction
POSTGRES_USER=postgres
POSTGRES_PASSWORD=your_secure_password
PGADMIN_DEFAULT_EMAIL=admin@example.com
PGADMIN_DEFAULT_PASSWORD=admin_password

# Docker Registry (optional)
DOCKER_REGISTRY=your-registry/
```

For local development, configure user secrets in `Backend/CharityAuction.WebAPI`:

```bash
dotnet user-secrets set "Database:DefaultConnection" "Host=localhost;Database=charity_auction;Username=postgres;Password=your_password"
dotnet user-secrets set "JwtSettings:Secret" "your-super-secret-jwt-key-min-32-characters"
dotnet user-secrets set "JwtSettings:Issuer" "CharityAuction"
dotnet user-secrets set "JwtSettings:Audience" "CharityAuctionUsers"
dotnet user-secrets set "JwtSettings:AccessTokenExpirationMinutes" "60"
dotnet user-secrets set "JwtSettings:RefreshTokenExpirationDays" "7"
dotnet user-secrets set "GoogleSettings:ClientId" "your-google-client-id"
dotnet user-secrets set "GoogleSettings:ClientSecret" "your-google-client-secret"
dotnet user-secrets set "EmailSettings:SmtpServer" "smtp.gmail.com"
dotnet user-secrets set "EmailSettings:Port" "587"
dotnet user-secrets set "EmailSettings:SenderName" "Charity Auction"
dotnet user-secrets set "EmailSettings:SenderEmail" "noreply@charityauction.com"
dotnet user-secrets set "EmailSettings:Username" "your-email@gmail.com"
dotnet user-secrets set "EmailSettings:Password" "your-app-password"
dotnet user-secrets set "EmailSettings:UseSSL" "true"
dotnet user-secrets set "LiqPay:PublicKey" "your-liqpay-public-key"
dotnet user-secrets set "LiqPay:PrivateKey" "your-liqpay-private-key"
dotnet user-secrets set "LiqPay:ResultUrl" "https://yourdomain.com/payment/success"
dotnet user-secrets set "LiqPay:ServerUrl" "https://yourdomain.com/api/payment/webhook"
dotnet user-secrets set "Fondy:MerchantId" "your-fondy-merchant-id"
dotnet user-secrets set "Fondy:SecretKey" "your-fondy-secret-key"
dotnet user-secrets set "Fondy:ResultUrl" "https://yourdomain.com/payment/success"
dotnet user-secrets set "Fondy:ServerUrl" "https://yourdomain.com/api/payment/webhook"
dotnet user-secrets set "PerspectiveApi:ApiKey" "your-perspective-api-key"
```

### Frontend Configuration

Create a `.env.local` file in the `Frontend` directory:

```env
REACT_APP_API_URL=http://localhost:5000
REACT_APP_GOOGLE_CLIENT_ID=your-google-client-id
REACT_APP_SIGNALR_URL=http://localhost:5000/auctionHub
```

### 🔑 Where to Get API Keys & Secrets

| **Service** | **Where to Get** | **Purpose** |
|-------------|------------------|-------------|
| **Google OAuth** | [Google Cloud Console](https://console.cloud.google.com/) | User authentication via Google |
| **LiqPay** | [LiqPay Merchant Panel](https://www.liqpay.ua/) | Payment processing for Ukraine |
| **Fondy** | [Fondy Merchant Panel](https://fondy.eu/) | Payment processing for Europe |
| **Perspective API** | [Google AI Studio](https://aistudio.google.com/) | Content moderation |
| **SMTP** | Gmail, SendGrid, or any SMTP provider | Email notifications |

## 📁 Project Structure

```
charity-auction-platform/
├── Backend/                          # ASP.NET Core Backend
│   ├── CharityAuction.WebAPI/       # Main API project
│   ├── CharityAuction.Application/  # Business logic layer
│   ├── CharityAuction.Domain/       # Domain entities
│   ├── CharityAuction.Infrastructure/ # Data access & external services
│   ├── CharityAuction.SignalR/      # Real-time communication
│   ├── CharityAuction.XUnitTest/    # Unit tests
│   └── docker-compose.yml           # Docker orchestration
├── Frontend/                        # React Frontend
│   ├── src/
│   │   ├── components/              # Reusable UI components
│   │   ├── pages/                   # Page components
│   │   ├── api/                     # API client functions
│   │   ├── context/                 # React context providers
│   │   ├── hooks/                   # Custom React hooks
│   │   ├── types/                   # TypeScript type definitions
│   │   └── utils/                   # Utility functions
│   └── public/                      # Static assets
```

## 📚 API Documentation

### Authentication Endpoints

#### Register User
```http
POST /api/v1/auth/register
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "SecurePassword123!",
  "firstName": "John",
  "lastName": "Doe"
}
```

#### Login
```http
POST /api/v1/auth/login
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "SecurePassword123!"
}
```

#### Google OAuth Login
```http
POST /api/v1/auth/login-google
Content-Type: application/json

{
  "idToken": "google-id-token"
}
```

### Auction Endpoints

#### Get All Auctions
```http
GET /api/v1/auctions/all
Authorization: Bearer {token}
```

#### Create Auction
```http
POST /api/v1/auctions
Authorization: Bearer {token}
Content-Type: application/json

{
  "title": "Charity Art Auction",
  "description": "Beautiful artwork for charity",
  "startingPrice": 100.00,
  "endTime": "2024-12-31T23:59:59Z",
  "imageUrl": "https://example.com/image.jpg"
}
```

#### Get Auction by ID
```http
GET /api/v1/auctions/{auctionId}
```

### Bid Endpoints

#### Place Bid
```http
POST /api/bid
Authorization: Bearer {token}
Content-Type: application/json

{
  "auctionId": "auction-guid",
  "amount": 150.00
}
```

#### Get User Bids
```http
GET /api/bid/bid/user
Authorization: Bearer {token}
```

### Payment Endpoints

#### Create LiqPay Payment
```http
POST /api/payment/liqpay
Content-Type: application/json

{
  "auctionId": "auction-guid",
  "amount": 150.00,
  "currency": "UAH",
  "description": "Payment for auction"
}
```

### Admin Endpoints

#### Get Pending Auctions
```http
GET /api/v1/admin/auctions/pending
Authorization: Bearer {token}
```

#### Approve Auction
```http
POST /api/v1/admin/auctions/{auctionId}/approve
Authorization: Bearer {token}
```

### Statistics Endpoints

#### Get User Statistics
```http
GET /api/stats/overview
Authorization: Bearer {token}
```

## 🧪 Testing

### Backend Tests

```bash
cd Backend
dotnet test
```

### Frontend Tests

```bash
cd Frontend
npm test
```

## 🚀 Deployment

### Docker Deployment

```bash
# Build and run with Docker Compose
cd Backend
docker-compose up -d --build
```

### Manual Deployment

1. **Backend**: Publish the WebAPI project
2. **Frontend**: Build the React app with `npm run build`
3. **Database**: Run migrations on production database
4. **Environment**: Configure production environment variables

## 🤝 Contributing

We welcome contributions! Please follow these steps:

1. **Fork** the repository
2. **Create** a feature branch (`git checkout -b feature/amazing-feature`)
3. **Commit** your changes (`git commit -m 'Add amazing feature'`)
4. **Push** to the branch (`git push origin feature/amazing-feature`)
5. **Open** a Pull Request

### Development Guidelines

- Follow the existing code style and conventions
- Write unit tests for new features
- Update documentation as needed
- Ensure all tests pass before submitting PR

## 👥 Authors & Contributors

- **Oleksandr Maslov** - *Main developer* - [synapsetron](https://github.com/synapsetron)

### Contact

- **GitHub**: [@synapsetron](https://github.com/yourusername)
- **Email**: aleksandr.maslov.job@gmail.com

## 🙏 Acknowledgments

- [ASP.NET Core](https://dotnet.microsoft.com/) for the robust backend framework
- [React](https://reactjs.org/) for the modern frontend library
- [Bootstrap](https://getbootstrap.com/) for the responsive UI components
- [SignalR](https://dotnet.microsoft.com/apps/aspnet/signalr) for real-time communication
- [LiqPay](https://www.liqpay.ua/) and [Fondy](https://fondy.eu/) for payment processing

---

⭐ **Star this repository if you find it helpful!**
