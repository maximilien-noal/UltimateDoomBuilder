Feature: Cross-Platform Behavior
  As a cross-platform user
  I want platform-specific behavior to work correctly
  So that the application works on Linux, Windows, and macOS

  Scenario: Platform settings are stored and retrieved
    Given the platform settings service is initialized
    When I store a setting "testkey" with value "testvalue"
    Then retrieving "testkey" should return "testvalue"

  Scenario: File lock detection works
    Given a locked file exists
    When I check if the file is locked
    Then the file lock checker should report it as locked

  Scenario: Native library loads on current platform
    Given the native BuilderNative library is available
    When I attempt to load the native library
    Then the library should load without errors
