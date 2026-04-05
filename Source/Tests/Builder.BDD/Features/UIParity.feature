Feature: UI Parity - Avalonia Windows
  As a cross-platform user
  I want all dialogs to open and display correctly in Avalonia
  So that the cross-platform version has feature parity with WinForms

  Scenario: Main window opens with correct layout
    Given the Avalonia application is running in headless mode
    When the main window is displayed
    Then the main menu should contain "File", "Edit", "View", "Mode", "Tools", "Help"
    And the toolbar should be visible
    And the status bar should display "Ready"
    And the rendering area should be present

  Scenario: About dialog opens
    Given the Avalonia application is running in headless mode
    When I open the About dialog
    Then the dialog should display the application name
    And the dialog should display the version number
