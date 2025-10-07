RainTrackerAPI
A simple .NET 8 Web API to track rain data user specific and store it in PostgresSQL database.
The project is containerized using docker compose for easy setup. 

Features
Add a rain data (POST /api/data) with ItRained and automatic UTC datetime.
Retrieve user-specific rain data (GET /api/data) filtered by x-userId.
x-userId is a required header for all requests.
FluentMigrator runs database migrations and is automatic.
API and PostgresSQL database are docker containerized.

Getting Started
1. Clone the Repository
git clone https://github.com/apekshajoshi87/RainTrackerApi.git/

2. Open a terminal in Project folder

3. Generate and Trust certificate if needed
dotnet dev-certs https --trust

3. Build and start the containers
docker-compose up --build

4. Access the api at http://localhost:8080/swagger/

5. After the api is up and running you can also execute the RainTrackerTest project
dotnet test

Swagger has two endpoints
POST /api/data
Headers:
x-userId is a string and is required
Request Payload Example (datetimestamp property can be from from json body as this is auto calcualted based on UTC):

{
  "itRained": true
}
Errors:
BadRequest 400
Internal Server Error 500

GET /api/data
Headers:
x-userId is a string and is required
Response Example:
[
  {
    "dateTimeStamp": "2025-10-06T22:25:08.364",
    "itRained": true
  },
  {
    "dateTimeStamp": "2025-10-06T22:25:43.029485",
    "itRained": false
  },
  {
    "dateTimeStamp": "2025-10-06T22:25:47.035803",
    "itRained": true
  },
  {
    "dateTimeStamp": "2025-10-06T22:31:12.989778",
    "itRained": true
  },
  {
    "dateTimeStamp": "2025-10-06T22:35:02.027319",
    "itRained": true
  },
  {
    "dateTimeStamp": "2025-10-06T22:35:30.766",
    "itRained": true
  }
]
Errors:
Internal Server Error 500

Troubleshooting
1. /wait-for-postgres.sh not found
Ensure LF line endings and has executabl permissions
dos2unix RainTrackerApi/wait-for-postgres.sh
chmod +x RainTrackerApi/wait-for-postgres.sh

2. pg_isready: not found
Need to make sure dockerfile installs Postgres client
RUN apt-get update && apt-get install -y postgresql-client

3. Ports in use
Change 8080:8080 in docker-compose if conflicting