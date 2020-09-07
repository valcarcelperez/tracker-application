Feature: Tracker
	Test for Tracker controller in TrackerApplication.WebApi

Scenario: Given that the service just started Retrieve should return zero items
	When I send a POST request to api/tracker/retrieve
	Then StatusCode should be 200
	