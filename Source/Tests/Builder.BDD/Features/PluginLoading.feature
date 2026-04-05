Feature: Plugin Loading
  As a Doom map editor
  I want all built-in plugins to load successfully
  So that all editing features are available

  Scenario: All built-in plugins load
    Given the application is initialized
    Then the following plugins should be available:
      | Plugin Name          |
      | BuilderModes         |
      | BuilderEffects       |
      | ColorPicker          |
      | CommentsPanel        |
      | TagExplorer          |
      | NodesViewer          |
      | AutomapMode          |
      | BlockmapExplorer     |
      | SoundPropagationMode |
      | StairSectorBuilder   |
      | TagRange             |
      | ThreeDFloorMode      |
      | VisplaneExplorer     |
      | USDF                 |
      | UDBScript            |
