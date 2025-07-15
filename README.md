# Social Media App

A full-stack social media application built with modern web technologies, featuring real-time interactions, comprehensive user engagement tools, and a sleek, responsive design.

## 🚀 Overview

This project is a complete social media platform that replicates the core functionality of popular social networks like Facebook. It provides users with the ability to connect, share content, and engage with others in real-time through posts, comments, likes, and direct messaging.

## ✨ Key Features

- **📝 Post Management**: Create, edit, and delete posts with rich content
- **💬 Real-time Messaging**: Instant chat with message bubbles and typing indicators
- **🔔 Live Notifications**: Real-time notifications for likes, comments, follows, and messages
- **👥 User Interactions**: Like, comment, and follow other users
- **🔍 Smart Search**: Real-time search functionality for users and posts
- **🔐 Secure Authentication**: JWT-based authentication and authorization
- **📱 Responsive Design**: Mobile-first approach with modern UI/UX

## 🛠️ Technology Stack

### Backend
- **.NET Core API** - RESTful web API
- **Entity Framework Core** - ORM for database operations
- **SQL Server** - Primary database
- **SignalR** - Real-time communication hub
- **JWT Authentication** - Secure token-based authentication

### Frontend
- **React** - Component-based UI library
- **Redux** - State management
- **Modern CSS** - Responsive and interactive design
- **Real-time Integration** - SignalR client for live updates

## 🏗️ Architecture

The project follows a clean architecture pattern with clear separation of concerns:

```
SocialMediaApp/
├── SocialMediaApp.API/          # Backend API
│   ├── Controllers/
│   ├── Models/
│   ├── Services/
│   ├── Hubs/                   # SignalR hubs
│   └── Data/                   # Entity Framework context
└── SocialMediaApp.UI/          # Frontend React app
    ├── src/
    │   ├── components/
    │   ├── store/              # Redux store
    │   ├── services/
    │   └── utils/
    └── public/
```

## 🌟 Core Functionality

### User Authentication & Authorization
- Secure user registration and login
- JWT token-based authentication
- Role-based access control
- Session management

### Social Interactions
- Create and share posts with rich content
- Like and unlike posts
- Comment on posts with nested replies
- Follow/unfollow users
- Real-time activity feeds

### Messaging System
- One-on-one chat functionality
- Real-time message delivery
- Message status indicators
- Chat history persistence

### Notification System
- Real-time notifications for:
  - New followers
  - Post likes and comments
  - Direct messages
  - System alerts

### Search & Discovery
- Real-time search for users and posts
- Keyword-based filtering
- Instant search results
- Search history

## 🚀 Getting Started

### Prerequisites
- .NET 6.0 or higher
- Node.js (v14 or higher)
- SQL Server
- Visual Studio or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/social-media-app.git
   cd social-media-app
   ```

2. **Setup Backend**
   ```bash
   cd SocialMediaApp.API
   dotnet restore
   dotnet ef database update
   dotnet run
   ```

3. **Setup Frontend**
   ```bash
   cd SocialMediaApp.UI
   npm install
   npm start
   ```

4. **Configure Database**
   - Update connection string in `appsettings.json`
   - Run Entity Framework migrations

## 📁 Project Structure

This repository contains two main directories:

- **`SocialMediaApp.API/`** - Backend API built with .NET Core
- **`SocialMediaApp.UI/`** - Frontend React application

Each directory contains its own detailed README with specific setup instructions and documentation.

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request
