Feature: Filtering Functionality
  As a user
  I want to filter data on the dashboard
  So that I can find relevant information quickly

@Regression
Scenario: Verify dropdown filter functionality
  Given the user is on the DataHub site
  And user navigates to Dashboard Inventory page
  When the user types "IT " in the dashboard type filter
  Then the autocomplete suggestions should appear
  When the user selects "IT Dashboard" from suggestions
  And clicks Apply Filters button
  Then the "IT Dashboard" info displays correctly

@Regression
Scenario: Verify checkbox filter functionality
  Given the user is on the DataHub site
  And user navigates to Dashboard Inventory page
  When the user selects "Public" checkbox filter
  And clicks Apply Filters button
  Then only the public data displays

@Regression
Scenario: Verify multiple checkbox selection
  Given the user is on the DataHub site
  And user navigates to Data Inventory page
  When the user unchecks "Raw" checkbox filter
  And clicks Apply Filters button 2
  Then verifies data displays correctly
  Then unchecks "Aggregated" checkbox filter
  And clicks Apply Filters button 2
  And verifies no data is returned

@Regression
Scenario: Verify filter clear functionality
  Given the user is on the DataHub site
  And user navigates to Dashboard Inventory page
  When the user selects "Public" checkbox filter
  And the user types "IT " in the dashboard type filter
  And the user selects "IT Dashboard" from suggestions
  And clicks Apply Filters button
  Then the "IT Dashboard" info displays correctly
  When the user clicks on "Clear Filters" button
  Then all filters are reset
  And the original unfiltered data is displayed

@Regression
Scenario: Verify individual filter clear functionality
  Given the user is on the DataHub site
  And user navigates to Dashboard Inventory page
  When the user selects "Public" checkbox filter
  And the user types "IT " in the dashboard type filter
  And the user selects "IT Dashboard" from suggestions
  And clicks Apply Filters button
  Then the "IT Dashboard" info displays correctly
  When the user clicks on the clear button for "Dashboard Type" filter
  And clicks Apply Filters button
  Then only the public data displays

@Regression
Scenario: Verify multi-condition filter functionality
  Given the user is on the DataHub site
  And user navigates to Dashboard Inventory page
  When the user selects "Public" checkbox filter
  And the user selects "Shared Solutions and Performance Improvement (MY)" from "By Office" filter
  And clicks Apply Filters button
  Then only items matching all filter conditions are displayed