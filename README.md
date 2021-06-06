# RushHour

## Overview
Rush Hour is appointment scheduling software. It can be used in a variety of areas, i.e. medical services, beauty and wellness, sport.  
## Specifications  
The application allows clients to make appointments for a given set of activities. The RESTful API is installed on customers’ dedicated servers and is configured depending on the customer needs and area of operation.  
## Entities  
### User  
Represents user information. It consists of:  
First name;  
Last name;  
Email;  
Password;  
Roles;  
Appointments;  
### Role
Used to create different types of users with their own level of access to the application features. It consists of:  
Name;
Users;
### Activity
Represents an activity in a certain area. It consists of:  
Name;  
Duration;  
Price;  
Appointments;    
### Appointment  
Represents the user appointment. The many-to-many relationship allows a user to include one or more activities in their appointment. It consists of:  
Start date;  
End date;  
User;  
Activities;    
## User Roles
### Administrator
As an administrator I would like to be able to create, read, update and delete appointments. And to create, read, update and delete activities.
### User
As a user I would like to be able to create, read, update, cancel my appointments.
## Milestones
1. Authentication & Authorization  
Log in and sign up. Roles management.
2. Activities management  
Activities CRUD by administrator.  
3. Appointments management  
Appointments management by admin and by user.  
4. Unit Testing 


