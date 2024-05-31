# Matrix API

## Description
This is an API to look for words in a matrix of words.

It was developed using Clean Architecture, Domain Driven Design, CRQS, Dependency injection, Fail Fast pattern and Factory pattern.

## Prequirements

- Visual Studio 2022
- .Net Core 8.0

## Usage
1- Open the solution in Visual Studio
2- Set the project MatrixChallenge.Host as the startup project
3- Run the project
4- In swagger you will find an endpoint called GET ~/Matrix/Words
5- This endpoint receive two string parameters (matrix and wordstream). 
	You must send the words separated by commas and without spaces.
	For example: "abcdc,rgwio,chill,pqnsb,uydxy"

## Projects and layers

### Core
In this folder you will find two projects:

#### MatrixChallenge.Apllication:
This project has the use cases. This API use CQRS so you will find querys and commands for the differents endpoint that it could have.

#### MatrixChallenge.Domain
This project has services that handle the business rules along with extensions methods and classes that take the settings that the services requires.

### Infraestructure
#### MatrixChallenge.Host
This project has the Program.cs and the setting classes.

### Presentation

#### MatrixChallenge.WebAPI
This project has the controllers that expose the endpoins of the API.

### Tests

#### MatrixChallenge.Tests
This projects got the unit tests.



