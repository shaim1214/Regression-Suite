Feature: Mega Menu Navigation
  As a user
  I want to use the mega menu for navigation
  So that I can access different areas of the application

@Regression
Scenario: Verify mega menu click navigation
  Given the user is on the DataHub site
  When the user clicks on the About menu item
  Then the About Data Hub page should load properly
  And the page title should contain "Streamlining the process for your most essential data resources."

@Regression
Scenario: Verify mega menu hover functionality
  Given the user is on the DataHub site
  When the user hovers over the Policy Areas Menu
  Then the dropdown menu should appear
  And the dropdown countains "Office of Government-wide Policy" option
  And the dropdown should contain  "Asset and Transportation Management" options
  When the user clicks on "Acquisition Policy" option
  Then the "Office of Acquisition Policy" page should load properly