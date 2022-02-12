# checkout-system

## Install & Run
Prerequisite:
[Docker](https://www.docker.com/get-started)

1. navigate to /CheckoutWeb/ folder
2. **docker-compose up -d** in terminal to start the database container
3. navigate to https://localhost:44355/swagger in browser

## Details

- **POST** /api/baskets/ - Creates a new customer, will return an Id to use for the basket
- **PUT** /api/baskets/{id}/article-line - Adds a new product item into the customer basket with the {id}
- **GET** /api/baskets/{id} - Retrieves details about the customer, item in his basket and the total price for the products
- **PATCH** /api/baskets/{id} - Updates the state of the basket either as closed or opened & updates the payment details for that customer's basket
