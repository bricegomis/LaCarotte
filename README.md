# La Carotte 🥕

La Carotte is an innovative task management application that leverages **gamification** to transform how you handle your daily activities. By using a smart point system, La Carotte motivates you to tackle even the most unpleasant tasks to earn attractive rewards. 

## 🎯 Objective

The primary goal of La Carotte is to make task management fun and engaging. By linking undesirable tasks to reward points, the app encourages users to balance their efforts between difficult work and enjoyable activities.

## ✨ Features

- **Task Management with Gamification**: Turn your tasks into challenges by earning points for completing tough tasks and spending them on rewards.
- **Reward System**: Use the accumulated points to unlock pleasant tasks or personalized rewards.

## 🚀 Technologies Used

- **Frontend**:
  - **Vue.js**: A progressive JavaScript framework for building user interfaces.
  - **Quasar Framework**: A powerful Vue.js-based framework for developing modern applications.
  - **Vite**: A fast build tool for Vue.js applications.
  - **Pinia**: State management in Vue.js with persistence via `localStorage`.
- **Backend**:
  - **.NET 8**: A robust and performant API built with .NET 8
  - **Docker**: Containerization of the application for easy deployment and scalability.  

## 🌟 Usage

1. **Create tasks**: Add tasks and specify the points they earn or cost.
2. **Complete tasks**: Finish unpleasant tasks to accumulate points.
3. **Spend points**: Use your points to perform enjoyable tasks or unlock rewards.

## 🗂️ Project Structure

- **la-carotte/**
  - **backend/**                 # Backend code for the .NET Core API
    - **Controllers/**           # API controllers
    - **Services/**              # Task and point management services
    - ...                        # Other directories and files
  - **frontend/**                # Frontend code with Vue.js, Quasar, and Vite
    - **src/**
      - **components/**         # Vue.js components
      - **stores/**             # Pinia store
      - **pages/**              # Application pages
      - ...                    # Other directories and files
    - **public/**                # Static files
    - ...                        # Other directories and files
  - **docker-compose.yml**       # Docker configuration for the backend

## 📄 License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.
