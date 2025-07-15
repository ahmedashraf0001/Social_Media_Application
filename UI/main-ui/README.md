# Social Media App - Frontend (React)

A modern, responsive React application for the social media platform with real-time features and intuitive user interface.

## 🎨 UI Overview

The frontend is built with React and Redux, featuring a clean, modern design that provides an excellent user experience across all devices. The interface includes real-time updates, smooth animations, and an intuitive navigation system.

## 🚀 Technologies Used

- **React** - Component-based UI library
- **Redux** - Predictable state container
- **SignalR Client** - Real-time communication
- **CSS3** - Modern styling with animations
- **JavaScript ES6+** - Modern JavaScript features

## 📱 Features & Screenshots

### 1. Main Feed & Post Creation
The main dashboard displays the user's feed with posts from followed users. Users can create new posts with text content and engage with existing posts.

![Main Feed](/Project%20Images/UI/UI-1.png)

**Key Features:**
- Clean, card-based post layout
- Real-time post updates
- Inline post creation
- Video, photo, and article post types
- Engagement metrics (likes, comments)

### 2. Post Details & Interaction
Detailed post view with enhanced interaction capabilities and rich content display.

![Post Details](/Project%20Images/UI/UI-2.png)

**Key Features:**
- Detailed post view with full content
- Interactive comment system
- Real-time engagement updates
- Post sharing capabilities
- Enhanced content formatting
- User interaction tracking

### 3. Real-time Notifications
Users receive instant notifications for all social interactions, keeping them engaged and informed.

![Notifications](/Project%20Images/UI/UI-3.png)

**Key Features:**
- Real-time notification updates
- Multiple notification types:
  - New followers
  - Post likes
  - Comments on posts
  - Direct messages
- Notification badges and indicators
- Mark as read functionality

### 4. User Profiles
Comprehensive user profiles with customizable information and activity tracking.

![User Profile](/Project%20Images/UI/UI-4.png) 

**Key Features:**
- Customizable profile information
- Cover photo and avatar support
- Follower/following counts
- Location and bio information
- Activity timeline
- Profile editing capabilities

### 5. Post Creation Interface
Intuitive post creation with multiple content types and formatting options.

![Post Creation](/Project%20Images/UI/UI-5.png)

**Key Features:**
- Rich text editor
- Character count (2,000 limit)
- Media attachment support
- Post privacy settings
- Draft saving
- Preview functionality

### 6. Search Functionality
Real-time search across users and posts with instant results.

![Search](/Project%20Images/UI/UI-6.png)

**Key Features:**
- Real-time search results
- User and post filtering
- Search history
- Keyboard navigation
- Search suggestions

## 🛠️ Installation & Setup

### Prerequisites
- Node.js (v14 or higher)
- npm or yarn package manager

### Getting Started

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/social-media-app.git
   cd social-media-app/SocialMediaApp.UI
   ```

2. **Install dependencies**
   ```bash
   npm install
   ```

3. **Configure environment variables**
   Create a `.env` file in the root directory:
   ```env
   REACT_APP_API_URL=http://localhost:5000/api
   REACT_APP_SIGNALR_URL=http://localhost:5000/chatHub
   ```

4. **Start the development server**
   ```bash
   npm start
   ```

5. **Build for production**
   ```bash
   npm run build
   ```

## 🎯 Key Components

### Authentication
- Login/Register forms with validation
- JWT token management
- Protected routes
- Automatic token refresh

### Feed Management
- Post creation and editing
- Real-time post updates
- Infinite scrolling
- Post filtering and sorting

### Social Features
- Like/unlike functionality
- Comment system with replies
- Follow/unfollow users
- User discovery

### Real-time Features
- Live notifications
- Instant messaging
- Online status indicators
- Typing indicators

### Search System
- Real-time search results
- Debounced search queries
- Search history
- Advanced filtering

## 🔧 State Management

The application uses Redux for state management to handle the application's global state efficiently across all components.

## 📱 Responsive Design

The application is fully responsive and works seamlessly across:
- Desktop computers
- Tablets
- Mobile phones
- Various screen sizes and orientations

## 🔄 Real-time Features

The application integrates with SignalR for real-time functionality:
- Live notifications
- Instant messaging
- Real-time post updates
- Online user status
- Typing indicators

## 📝 Available Scripts

- `npm start` - Start development server
- `npm run build` - Build for production
- `npm test` - Run tests
- `npm run lint` - Run ESLint
- `npm run format` - Format code with Prettier

