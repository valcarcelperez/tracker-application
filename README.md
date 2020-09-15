# tracker-application

This is a Visual Studio solution that demonstrates different type of projects and technologies.

The solution contains two applications: a WebApi and a WinForm.

The WebApi implements endpoints for receiving temperature and humidity data in three different formats and merges the data into a common collection with a standardized tracker data format. WebApi implements another endpoint to receive the data in the standardized format.

The WinForm application uses a HttpClient to request standardized tracker data from the WebApi. The data received is displayed in a data grid.

## Concepts and technologies demonstrated in the WebApi application
- ASP.NET Core 3.1.
- Dependency Injection Pattern.
- Singleton Pattern.
- Exception Handling.
- OOP Concepts.
- LINQ.
- Thread synchronization when using a shared resource.
- Service Contract Design.
- Swagger-UI.
- Automated User Acceptance Test using SpecFlow.

## Concepts and technologies demonstrated in the WinForm application
- .NET Core 3.1.
- Exception Handling.
- Loading configuration from a configuration file.
- Using a HttpClient to request data from the WebApi.
- Timer to execute the requests.
- Mechanism to control a process from the UI Thread.
- Cross-Thread calls.
- Logging

## Example of test
```
Scenario: Given that data with format1 is saved to the service Retrieve should return two items
	Given I save format 1 tracker data from file 'TrackerDataFoo1.json'
	When I send a POST request to api/tracker/retrieve
	Then StatusCode should be 200
	And property type should be 'Succeed'
	And property data should be the complex-element array
	| companyId:key | companyName | trackerId:key | trackerName | firstCrumbDtm:DateTime | lastCrumbDtm:DateTime | tempCount:Number | avgTemp:Number | humidityCount:Number | avgHumdity:Number |
	| 1             | Foo1        | 1             | ABC-100     | 2020-08-17T10:35:00    | 2020-08-17T10:45:00   | 3                | 23.15          | 3                    | 81.5              |
	| 1             | Foo1        | 2             | ABC-200     | 2020-08-18T10:35:00    | 2020-08-18T10:45:00   | 3                | 24.15          | 3                    | 82.5              |

```

## Projects

Project Name | Description
-|-
TrackerApplication.Contracts | Defines the models and contracts used by the WebApi.
TrackerApplication.Domain | Define classes for converting data from different formats to a standardized format.
TrackerApplication.Domain.Tests | Unit tests for the classes that convert the data. 
TrackerApplication.WebApi | WebApi application.
TrackerApplication.WebApi.Tests | User acceptance tests using SpecFlow. When the test execute the WebApi is hosted, http requests are sent, and the received data is compared with the expected values.
TrackerApplication.Services | Defines the service used by the controllers. Converts data from different formats and store the standardized data in a repository.
TrackerApplication.Repositories | Repository used by the service to store standardized data.
TrackerApplication.WinForm | WinForm application. Requests and displays data.
TrackerApplication.Client | Defines the class that encapsulates data requesting process.

