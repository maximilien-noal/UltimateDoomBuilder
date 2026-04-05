Feature: Map File Operations
  As a Doom map editor user
  I want to open, edit, and save map files
  So that I can create and modify Doom maps

  Scenario: Open a Doom-format WAD file
    Given a Doom-format WAD file "test.wad" containing map "MAP01"
    When I open the WAD file
    Then the map should load successfully
    And the map should contain vertices, linedefs, sidedefs, sectors, and things

  Scenario: Open a UDMF-format WAD file
    Given a UDMF-format WAD file "test_udmf.wad" containing map "MAP01"
    When I open the WAD file
    Then the map should load successfully
    And the map format should be "UDMF"

  Scenario: Save and reload map preserves all data
    Given a loaded map with known geometry
    When I save the map to a new file
    And I reopen the saved map
    Then all vertices should have identical coordinates
    And all linedefs should have identical properties
    And all sectors should have identical properties

  Scenario: Create a new map
    Given the application is running
    When I create a new map in "Doom" format
    Then the map should be empty
    And the map format should be "Doom"
