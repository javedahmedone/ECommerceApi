# 🔐 Authentication APIs

This module provides user authentication functionalities including **Register** and **Login** using JWT-based authentication.

---

## 1 Register User
Creates a new user account with email, name, password, and assigned role.
{{baseUrl}}/api/auth/Register
### **Endpoint**
### **Description**

### **📥 Request Body**
```json
{
  "email": "guestUser@gmail.com",
  "name": "Guest user",
  "password": "Guest@123",
  "role": 2  //1 for admin , 2 for customer
}
```

### **Response

200 / 201 (successful creation — depends on your implementation)
```json
{
  "userId": 4,
  "role": 2,
  "email": "guestUser@gmail.com"
}
```


## 2 Login User

### **Endpoint**
{{baseUrl}}/api/auth/Login

### **Description**
Login user

### **📥 Request Body**
```json
{
  "email": "guestUser@gmail.com",
  "name": "Guest user",
  "password": "Guest@123",
  "role": 2
}
```

### **Response

200 / 201 (successful creation — depends on your implementation)
```json
{
    "token": "JWT TOKEN",
    "userId": 4,
    "role": 2,
    "email": "guestUser@gmail.com"
}
```

