# HairBooking API

HairBooking API är ett API skapat med ASP.NET Core för en frisörsalong. Där data gällande bokningar, kategorier, kunder, frisörer, bilder och kunder kan hanteras med GET, GET/id,
POST, PUT pch DELETE



## Endpoints

### Bokningar (Booking)

- GET `/api/Booking`: Hämtar alla bokningar.
- POST `/api/Booking`: Skapar en ny bokning.
- GET `/api/Booking/{id}`: Hämtar en specifik bokning med angivet ID.
- PUT `/api/Booking/{id}`: Uppdaterar en befintlig bokning med angivet ID.
- DELETE `/api/Booking/{id}`: Raderar en befintlig bokning med angivet ID.

### Kategorier (Category)

- GET `/api/Category`: Hämtar alla kategorier.
- POST `/api/Category`: Skapar en ny kategori.
- GET `/api/Category/{id}`: Hämtar en specifik kategori med angivet ID.
- PUT `/api/Category/{id}`: Uppdaterar en befintlig kategori med angivet ID.
- DELETE `/api/Category/{id}`: Raderar en befintlig kategori med angivet ID.

### Kund (Customer)

- GET `/api/Customer`: Hämtar alla kunder.
- POST `/api/Customer`: Skapar en ny kund.
- GET `/api/Customer/{id}`: Hämtar en specifik kund med angivet ID.
- PUT `/api/Customer/{id}`: Uppdaterar en befintlig kund med angivet ID.
- DELETE `/api/Customer/{id}`: Raderar en befintlig kund med angivet ID.
- POST `/api/Customer/login`: Loggar in en kund.

### Hairdresser

- GET `/api/Hairdresser`: Hämtar alla frisörer.
- POST `/api/Hairdresser`: Skapar en ny frisör.
- GET `/api/Hairdresser/{id}`: Hämtar en specifik frisör med angivet ID.
- PUT `/api/Hairdresser/{id}`: Uppdaterar en befintlig frisör med angivet ID.
- DELETE `/api/Hairdresser/{id}`: Raderar en befintlig frisör med angivet ID.

### Bild (Image)

- GET `/api/Image`: Hämtar alla bilder.
- POST `/api/Image`: Lägger till en ny bild.
- GET `/api/Image/{id}`: Hämtar en specifik bild med angivet ID.
- PUT `/api/Image/{id}`: Uppdaterar en befintlig bild med angivet ID.
- DELETE `/api/Image/{id}`: Raderar en befintlig bild med angivet ID.

### Tid (Time)

- GET `/api/Time`: Hämtar alla tider.
- POST `/api/Time`: Lägger till en ny tid.
- GET `/api/Time/{id}`: Hämtar en specifik tid med angivet ID.
- PUT `/api/Time/{id}`: Uppdaterar en befintlig tid med angivet ID.
- DELETE `/api/Time/{id}`: Raderar en befintlig tid med angivet ID.

  ### Inloggning och registrering
 - POST `/api/Login`: Loggar in.
 -  - POST `/api/LRegister`: Registrera inloggning.
