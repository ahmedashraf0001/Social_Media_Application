# Social Media App (API) 📱

A full-stack social media application built with .NET API and React, featuring real-time messaging, notifications, and social interactions similar to Facebook.

## ✨ Features

### Core Social Features
- **User Authentication & Authorization** - JWT-based secure authentication
- **Post Creation & Management** - Create, edit, delete posts with rich content
- **Comment System** - Engage with posts through comments
- **Like System** - Like posts and comments
- **User Following** - Follow/unfollow other users
- **User Profiles** - Comprehensive user profile management

### Real-time Features
- **Live Chat** - Real-time messaging with chat bubbles using SignalR
- **Instant Notifications** - Real-time notifications for:
  - New messages
  - Post likes
  - Comments on posts
  - New followers
- **Live Search** - Real-time search for users and posts

### Advanced Features
- **Pagination** - Efficient data loading with pagination
- **Search Functionality** - Search users and posts with keywords
- **Feed System** - Personalized post feed
- **Responsive Design** - Mobile-friendly UI

## 🛠️ Tech Stack

### Backend
- **.NET 6/7 Web API** - RESTful API development
- **Entity Framework Core** - ORM for database operations
- **SQL Server** - Primary database
- **SignalR** - Real-time communication
- **JWT Authentication** - Secure token-based authentication
- **3-Tier Architecture** - Clean separation of concerns
  - Controllers (Presentation Layer)
  - Services (Business Logic Layer)
  - Repositories (Data Access Layer)
- **Dependency Injection** - Built-in DI container

### Frontend
- **React** - Component-based UI library
- **Redux** - State management
- **Modern React Hooks** - Functional components with hooks
- **Real-time UI Updates** - SignalR integration

## 📋 API Documentation

### Authentication Endpoints
- `POST /api/Auth/register` - User registration
- `POST /api/Auth/login` - User login
- `POST /api/Auth/forgot-password` - Password reset request
- `POST /api/Auth/reset-password` - Password reset

### User Management
- `GET /api/User` - Get current user profile
- `GET /api/User/{userId}` - Get user by ID
- `PUT /api/User/profile` - Update user profile
- `POST /api/User/follow/{userId}` - Follow a user
- `GET /api/User/{userId}/followers` - Get user followers
- `GET /api/User/{userId}/following` - Get users being followed
- `GET /api/User/search` - Search users
- `DELETE /api/User` - Delete user account

### Posts
- `POST /api/Post/Create` - Create a new post
- `GET /api/Post/Feed/{pageNumber}/{pageSize}` - Get paginated feed
- `GET /api/Post/Id/{postId}` - Get specific post
- `GET /api/Post/User/{userId}/{pageNumber}/{pageSize}` - Get user's posts
- `PUT /api/Post/Update` - Update post
- `PUT /api/Post/Like/{postId}` - Like/unlike post
- `GET /api/Post/Search` - Search posts
- `GET /api/Post/{postId}/Likes/{pageNumber}/{pageSize}` - Get post likes
- `DELETE /api/Post/Delete/{postId}` - Delete post

### Comments
- `GET /api/Comment/post/{postId}` - Get post comments
- `POST /api/Comment` - Create comment
- `PUT /api/Comment/{commentId}` - Update comment
- `DELETE /api/Comment/{commentId}` - Delete comment

### Messaging
- `GET /api/Messaging/conversations/Id/{conversationId}/{pageNumber}/{pageSize}/{withMessages}` - Get conversation
- `GET /api/Messaging/conversations/All/{userId}/{pageNumber}/{pageSize}/{withMessages}` - Get all conversations
- `GET /api/Messaging/conversations/inbox/{pageNumber}/{pageSize}` - Get inbox
- `GET /api/Messaging/conversations/Between/{otherUserId}/{pageNumber}/{pageSize}` - Get conversation between users
- `POST /api/Messaging/messages/Send` - Send message
- `PUT /api/Messaging/messages/Edit/{messageId}/{content}` - Edit message
- `PUT /api/Messaging/messages/MarkAsRead/{messageId}` - Mark message as read
- `DELETE /api/Messaging/conversations/Delete/{conversationId}` - Delete conversation
- `DELETE /api/Messaging/messages/Delete/{messageId}` - Delete message
- `GET /api/Messaging/conversations/Search` - Search conversations
- `GET /api/Messaging/messages/Id/{messageId}` - Get message by ID
- `GET /api/Messaging/messages/Unread/{userId}` - Get unread messages
- `GET /api/Messaging/messages/Search` - Search messages

### Notifications
- `GET /api/Notification/Sender/{fromUserId}` - Get notifications from user
- `GET /api/Notification/Receiver` - Get received notifications
- `GET /api/Notification/Unread` - Get unread notifications
- `GET /api/Notification/UnreadCount` - Get unread notification count
- `PUT /api/Notification/MarkAsRead/{notificationId}` - Mark notification as read
- `POST /api/Notification/Create` - Create notification
- `PUT /api/Notification/update` - Update notification
- `DELETE /api/Notification/delete/{notificationId}` - Delete notification

## 🚀 Getting Started

### Prerequisites
- .NET 6/7 SDK
- SQL Server
- Node.js and npm
- Visual Studio or VS Code

### Backend Setup
1. Clone the repository
2. Navigate to the API project directory
3. Update the connection string in `appsettings.json`
4. Run Entity Framework migrations:
   ```bash
   dotnet ef database update
   ```
5. Start the API:
   ```bash
   dotnet run
   ```

## 📱 Screenshots

### API Documentation
![API Endpoints](/Project%20Images/Api-1.png)
*Authentication and Comment endpoints*

![Messaging API](/Project%20Images/Api-2.png)
*Real-time messaging endpoints*

![Notification API](/Project%20Images/Api-3.png)
*Notification and Post management endpoints*

![User API](/Project%20Images/Api-4.png)
*User management and profile endpoints* -->

## 🔧 Configuration

### Database Configuration
Update your `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SocialMediaDb;Trusted_Connection=true;"
  },
  "JWT": {
    "Secret": "your-jwt-secret-key",
    "Issuer": "your-issuer",
    "Audience": "your-audience"
  }
}
```

### SignalR Configuration
The application uses SignalR for real-time features. Hubs are configured for:
- Chat messaging
- Live notifications
- Real-time updates
