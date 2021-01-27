# Making Reservations

## How a reservation is made through the API

### Request
```
POST /reservations
Content-Type: application/json


{
  "for": "John Doe",
  "books": [1,2,3,4]
}

```

### Response
```
201 Created
Location: /reservations/42
Content-Type: application/json

{
    "id": 42,
    "for": "John Doe",
    "books": [1,2,3,4],
    "status": "Pending"
}
```

### Alternate Way: Using a "Store" Archetype of a Resource

```
PUT /my-reservations/1
Authorization: bearer 38938983.387783748.89898

PUT /my-reservations/2
Authorization: bearer 38938983.387783748.89898

PUT /my-reservations/3
Authorization: bearer 38938983.387783748.89898

PUT /my-reservations/4
Authorization: bearer 38938983.387783748.89898

POST /my-reservations
Authorization: bearer 38938983.387783748.89898
Content-Type: application/json

{
    "for": "John Doe"
}

201 Created
Location: /reservations/8578
Content-Type: application/json

{

    "id": 8578,
    "for": "John Doe",
    "books": [1,2,3,4],
    "status": "Pending"
}
```

## How the status is checked of that reservation

## The processing of the Reservation