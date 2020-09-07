Feature: Tracker
	Test for Tracker controller in TrackerApplication.WebApi

Scenario: Given that the service just started Retrieve should return zero items
	When I send a POST request to api/tracker/retrieve
	Then StatusCode should be 200
	And property type should be 'Succeed'
	And property data should be an empty array

Scenario: Given that data with format1 is saved to the service Retrieve should return two items
	Given I save format 1 tracker data from file 'TrackerDataFoo1.json'
	When I send a POST request to api/tracker/retrieve
	Then StatusCode should be 200
	And property type should be 'Succeed'
	And property data should be the complex-element array
	| companyId:key | companyName | trackerId:key | trackerName | firstCrumbDtm:DateTime | lastCrumbDtm:DateTime | tempCount:Number | avgTemp:Number | humidityCount:Number | avgHumdity:Number |
	| 1             | Foo1        | 1             | ABC-100     | 2020-08-17T10:35:00    | 2020-08-17T10:45:00   | 3                | 23.15          | 3                    | 81.5              |
	| 1             | Foo1        | 2             | ABC-200     | 2020-08-18T10:35:00    | 2020-08-18T10:45:00   | 3                | 24.15          | 3                    | 82.5              |

Scenario: Given that data with format1 and format2 are is saved to the service Retrieve should return four items
	Given I save format 1 tracker data from file 'TrackerDataFoo1.json'
	And I save format 2 tracker data from file 'TrackerDataFoo2.json'
	When I send a POST request to api/tracker/retrieve
	Then StatusCode should be 200
	And property type should be 'Succeed'
	And property data should be the complex-element array
	| companyId:key | companyName | trackerId:key | trackerName | firstCrumbDtm:DateTime | lastCrumbDtm:DateTime | tempCount:Number | avgTemp:Number | humidityCount:Number | avgHumdity:Number |
	| 1             | Foo1        | 1             | ABC-100     | 2020-08-17T10:35:00    | 2020-08-17T10:45:00   | 3                | 23.15          | 3                    | 81.5              |
	| 1             | Foo1        | 2             | ABC-200     | 2020-08-18T10:35:00    | 2020-08-18T10:45:00   | 3                | 24.15          | 3                    | 82.5              |
	| 2             | Foo2        | 1             | XYZ-100     | 2020-08-18T10:35:00    | 2020-08-18T10:45:00   | 3                | 33.15          | 3                    | 91.5              |
	| 2             | Foo2        | 2             | XYZ-200     | 2020-08-19T10:35:00    | 2020-08-19T10:45:00   | 3                | 43.15          | 3                    | 92.5              |