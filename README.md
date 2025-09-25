# 🚪 Elevator Manager System

[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com/)  
[![Tests](https://img.shields.io/badge/tests-NUnit-green)](https://nunit.org/)  
[![Docker](https://img.shields.io/badge/docker-ready-blue)](https://www.docker.com/)  

---

## 📖 Overview
The **Elevator Manager System** is a C# console application that simulates how elevators are dispatched in a building.

It models real-world elevator behavior, ensuring that:  
- The **closest idle elevator** services requests.  
- **Capacity limits** are respected (multiple trips if needed).  
- **Invalid requests** (out of range floors) are safely rejected.  
- Elevators return to **Idle state** when finished.  

This project also includes a **test suite** (NUnit), and is fully **containerized with Docker** for easy deployment.  

---

### Features
- 🚇 Multiple elevators with configurable **capacity** and **speed**.  
- 🛗 Dispatches the **closest idle elevator** to the requested floor.  
- 👥 Handles **over-capacity** (multiple trips for waiting passengers).  
- 🚫 Rejects invalid floor requests (outside max floors).  
- 💻 Console-driven (enter floor + number of passengers).  
- ✅ NUnit test suite with regression and overload tests.  
- 🐳 Fully containerized with Docker.  

---

## ⚙️ Requirements
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later  
- [Docker](https://docs.docker.com/get-docker/) (for containerized execution)  

---

## ▶️ Running the Application

### 1. Clone the repository
```bash
git clone https://github.com/zaKIMnoon/ElevatorChallenge.git
cd ElevatorChallenge

windows (Command Line/Powershell)
git clone https://github.com/zaKIMnoon/ElevatorChallenge.git
cd ElevatorChallenge

### 2. Run the project
dotnet run --project ElevatorChallenge

### 3. Console Usage
Enter command (request/shutdown)
request 5 8   # Request elevator to 5th floor with 8 people waiting
shutdown      # Stop the application

## ▶️ Running the Tests
### 1. Clone the repository
```bash
git clone https://github.com/zaKIMnoon/ElevatorChallenge.git
cd ElevatorChallenge