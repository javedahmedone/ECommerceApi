# 🔐 Authentication APIs

This module provides user authentication functionalities including **Register** and **Login** using JWT-based authentication.

---

## 1 Register User
Creates a new user account with email, name, password, and assigned role.
### **Endpoint** - {{baseUrl}}/api/auth/Register

### **Method Type** -  POST

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

### **Method Type** -  POST
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




## 3 Add Category

### **Endpoint**
{{baseUrl}}/api/admin/categories/Category
### **Method Type** -  POST


### **📥 Request Body**
```json
{
  "name": "Electronic",
  "description": "This is Electronic"
}
```

### **Response

200 / 201 (successful creation — depends on your implementation)
```json
{
    "id": 3,
    "name": "Electronic",
    "description": "This is Electronic",
    "createdDate": "0001-01-01T00:00:00",
    "updatedDate": null
}
```



## 3 Get Category

### **Endpoint**
{{baseUrl}}/api/admin/categories
### **Method Type** -  GET


### **Response

200 / 201 (successful creation — depends on your implementation)
```json
[
    {
        "id": 1,
        "name": "Fashion & Apparel",
        "description": "Men’s, women’s, and kids’ clothing, footwear, and fashion accessories.",
        "createdDate": "2025-11-29T10:30:57.24028Z",
        "updatedDate": null
    }
]
```




