Feature: Home Page
As a user
I want DataHub home page to load correctly and verify the data displayed. 

@Regression
Scenario: Verify DataHub loads correctly

	Given the user is on the DataHub site
	When the user verifies the header and footer are displayed
	And Dashboard Inventory and Data Inventory are available
	Then user can click Explore Dashboard Inventory
	And verify the filters and buttons exist on the page
	When user selects "Acquisitions Workforce Chase Lists" and Private filters
	And clicks Apply Filters button
	Then the data reflects changes