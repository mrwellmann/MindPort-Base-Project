# User Interface: Info Boxes for VR Builder
## Table of Contents

- [User Interface: Info Boxes for VR Builder](#user-interface-info-boxes-for-vr-builder)
  - [Table of Contents](#table-of-contents)
  - [Introduction](#introduction)
  - [Requirements](#requirements)
  - [Quick Start](#quick-start)
  - [Infobox Prefab](#infobox-prefab)
    - [Introduction](#introduction-1)
    - [Inspector](#inspector)
  - [Optional Infobox Behavior](#optional-infobox-behavior)
    - [Introduction](#introduction-2)
    - [Inspector](#inspector-1)
  - [Mandatory Infobox Condition](#mandatory-infobox-condition)
    - [Introduction](#introduction-3)
    - [Inspector](#inspector-2)
  - [Multiple Choice Box Prefab](#multiple-choice-box-prefab)
    - [Info](#info)
  - [Multiple Choice Wizard](#multiple-choice-wizard)
    - [Info](#info-1)
    - [Inspector](#inspector-3)
      - [Default Layout:](#default-layout)
      - [Vertical Layout:](#vertical-layout)
  - [Multiple Choice Behavior](#multiple-choice-behavior)
    - [Introduction](#introduction-4)
    - [Inspector](#inspector-4)
  - [Multiple Choice Button Condition](#multiple-choice-button-condition)
    - [Introduction](#introduction-5)
    - [Inspector](#inspector-5)
  - [Numerical Pad Prefab](#numerical-pad-prefab)
    - [Info](#info-2)
    - [Inspector](#inspector-6)
  - [Num Pad Visibility Behavior](#num-pad-visibility-behavior)
    - [Introduction](#introduction-6)
    - [Inspector](#inspector-7)
  - [Num Pad Enter Button Condition](#num-pad-enter-button-condition)
    - [Introduction](#introduction-7)
    - [Inspector](#inspector-8)
  - [Num Pad Compare Values Condition](#num-pad-compare-values-condition)
    - [Introduction](#introduction-8)
  - [UI Button Condition](#ui-button-condition)
    - [Introduction](#introduction-9)
    - [Inspector](#inspector-9)
  - [Set Localized Text Behavior](#set-localized-text-behavior)
    - [Introduction](#introduction-10)
    - [Inspector](#inspector-10)
  - [UI Outline Behavior](#ui-outline-behavior)
    - [Introduction](#introduction-11)
    - [Inspector](#inspector-11)
  - [Contact](#contact)

## Introduction
This add-on contains a collection of user interface tools, conditions and behaviors for VR Builder. At the moment, it includes the following prefabs, behaviors and condition.

**Info Box**: Mandatory and Optional Infoboxes to guide, teach and inform users. 

**Multiple Choice Box**: Dynamic Multiple Choice UI that can be set up via wizard.  

**Numerical Pad**: Numerical Pad to simulate various user inputs.

**UI Button**: Condition to acknowledge user interactions inside the UI

**UI Outline**: Behavior to guide users inside the UI via outline animations

## Requirements
This add-on requires VR Builder version 3.0.0 or later to work.


## Quick Start
The easiest way to get started with this add-on is to check out the included demo scenes.

There are five demo scenes included in this package:
- InfoBoxDemo
- MultipleChoiceDemo
- NumPadDemo
- UiButtonDemo
- UiOutlineDemo 

If it is the first time you open a demo scene, you will have to do it through the menu: in`Tools > VR Builder > Demo Scenes > User Interface`. This is necessary as a script will copy the demo courses in the StreamingAssets folder. After the first time, the demo scenes can be opened normally.

Press Play to try out the behaviors and conditions included in this add-on. Each scene contains a demo and set up for a specific use case.
Don't forget to check out the courses for examples on how to use those conditions and behaviors! You can open the Workflow Editor through `Tools > VR Builder > Open Workflow Editor`.

## Infobox Prefab
### Introduction
For Infobox behaviors and conditions you will need an instantiated Infobox prefab in your scene. 
You can create an Infobox via right click in the Hierarchy Inspector and select `VR Builder > UI > Create Infobox`.
Or you just drag the P_InfoBox Prefab directly into scene (can be found under `Assets/MindPort/VR Builder/Add-ons/UserInterface/Demo/StaticAssets/Prefabs/P_InfoBox.prefab`) 

### Inspector
![Demo Image](images/infobox_prefab.png "Infobox Inspector")

**Layout Type**: You can choose between Optional, Mandatory and Info Panel. Using the Infobox in a Optional Infobox Behavior or Mandatory Infobox Condition will set the layout automatically to the right type. Use `Info Panel` for standalone Infoboxes that should be persistently visible in the scene.

**Title Sprite**: Here you can set up the Icon display in the top-left corner of the Infobox. You can easily replace the default info icon sprite with you own image.

**Info Box Size**: Use the slider to increase or decrease the info box size in world space. 

**Use Localization Table**: If you want to use the Unity Localization, enter the string table name here and use keys for all the text fields of the Infobox.

**Title Text** Enter the title text for your Infobox directly or use a Localization Table Key. 

**Detailed Text** Enter the detailed content text for Infobox directly in here or use a Localization Table Key. 

**Image Sprite** Use an Image to be shown in the body of the Infobox below the detailed text. You can also adjust the color of the image directly here.

**Advanced Settings** Here you can see all parts of the Infobox linked to script. You can also adjust the Details Headline and Acknowledge Button (Mandatory Infobox) texts or localization keys.

## Optional Infobox Behavior
### Introduction
This behavior will show an Optional Infobox. To do this it uses an existing Infobox object from the scene and will turn it visible as an Optional Infobox.
The behavior can be found under `UI > Optional Infobox`.

### Inspector
![Inspector Layout](images/infobox_behavior.png "Infobox Behavior")

The **Infobox Behavior** accepts the following parameters:

**Delay (in seconds)**: The Infobox will be shown delayed.

**Hide after Step**: The Infobox will automatically turn invisible after the step. (The user can also dismiss the Infobox via pressing X in the top-right corner).

**Infobox**: The reference of the instantiated Infobox in the scene. If left empty the behavior will try and find a valid Infobox in the scene.

## Mandatory Infobox Condition
### Introduction
This condition will show a Mandatory Infobox with an Acknowledge Button the User has to press to complete the Condition. It uses an existing Infobox object from the scene and will turn it visible as a Mandatory Infobox.
The condition can be found under `UI > Mandatory Infobox`.

### Inspector
![Inspector Layout](images/infobox_condition.png "Infobox Condition")

The **Infobox Condition** accepts the following parameters.

**Infobox**: The reference of the instantiated Infobox in the scene. If left empty the condition will try and find a valid Infobox in the scene.

## Multiple Choice Box Prefab
### Info
For the Multiple Choice behaviors and conditions you will need an instantiated Multiple Choice prefab in your scene. 
You can create an Multiple Choice Box via right click in the Hierarchy Inspector and select `VR Builder > UI > Create Multiple Choice Box`.
Or you just drag the P_MultipleChoice prefab directly into scene (can be found under `Assets/MindPort/VR Builder/Add-ons/UserInterface/Demo/StaticAssets/Prefabs/P_MultipleChoice.prefab`) 

## Multiple Choice Wizard
### Info
For the Multiple Choice behaviors and conditions you can use the multiple choice wizard.
You can open the wizard  via right click in the Hierarchy Inspector and select `VR Builder > UI > Multiple Choice Wizard`.
Or you open it via the Unity Toolbar under `Tools > VR Builder > MultipleChoiceWizard`

### Inspector
![Inspector Layout](images/multiple_choice_wizard.png "Multiple Choice Wizard")

You can enter the question and set the number of answers to your desired count. Then you can enter the answer texts and decide if you want to use the vertical or default layout.

#### Default Layout:
![Inspector Layout](images/multiple_choice_default_layout.png "Default Layout") 
#### Vertical Layout:
![Inspector Layout](images/multiple_choice_vertical_layout.png "Vertical Layout")

## Multiple Choice Behavior
### Introduction
This behavior will allow you to configure or re-configure your basic Multiple Choice Settings.
The behavior can be found under `UI > Multiple Choice Box`.

### Inspector
![Inspector Layout](images/multiplechoice_behavior.png "Multiple Choice Behavior")

The **Multiple Choice Behavior** accepts the following parameters:

**Localization Table (Optional)**: If you want to use the Unity Localization, enter the string table name here and use keys for all the text fields of the Multiple Choice Box.

**Title / Key**: Enter the title for your Multiple Choice Box or use a Localization Table Key.

**Hide Delay (Seconds)**: Keeps the Multiple Choice Box open for the entered amount of seconds (0 for instant close) with the last button pressed highlighted

**Vertical Layout**: Use the vertical layout instead of the default layout.

**Multiple Choice Box**: The reference of the instantiated Multiple Choice Box in the scene. If left empty the behavior will try and find a valid multiple choice box in the scene.  

## Multiple Choice Button Condition
### Introduction
This condition will allow you to set up one answer for your Multiple Choice Box.

### Inspector
![Inspector Layout](images/multiplechoice_conditions.png "Multiple Choice Condition")

The **Multiple Choice Condition** accepts the following parameters:

**Localization Table (Optional)**: If you want to use the Unity Localization, enter the string table name here and use keys for all the text fields of the Multiple Choice Box.

**Button Text / Key**: Enter the answer / button text for your Multiple Choice Box or use a Localization Table Key.

**Text Key is Sprite Resource Path**: Enter the resource path of an image into the "Button Text / Key" field, to display a sprite instead of a text on the Multiple Choice Button
E.g. for using this sprite: "Assets/Resources/Textures/texture01.png" enter "Textures/texture01" into the field.
See also: https://docs.unity3d.com/ScriptReference/Resources.Load.html

**Multiple Choice Box**: The reference of the instantiated Multiple Choice Box in the scene. If left empty the behavior will try and find a valid multiple choice box in the scene.


## Numerical Pad Prefab
### Info
For the Num Pad behaviors and conditions you will need an instantiated Num Pad prefab in your scene. 
You can create a Num Pad via right click in the Hierarchy Inspector and select `VR Builder > UI > Create Num Pad`.
Or you just drag the P_Numpad prefab directly into scene (can be found under `Assets/MindPort/VR Builder/Add-ons/UserInterface/Demo/StaticAssets/Prefabs/P_Numpad.prefab`) 

### Inspector
![Inspector Layout](images/numpad_layout.png "Num Pad")


## Num Pad Visibility Behavior
### Introduction
This behavior will allow you to activate and set your Num Pad visible in the scene.
The behavior can be found under `UI > Num Pad`.

### Inspector
![Inspector Layout](images/numpad_behavior.png "Num Pad Visibility Behavior")

The **Num Pad Behavior** accepts the following parameters:

**Num Pad**: The reference of the instantiated Num Pad in the scene. If left empty the behavior will try and find a valid Num Pad in the scene.  

**Visibility**: Will show (or hide) the Num Pad. 


## Num Pad Enter Button Condition
### Introduction
This condition will be completed if a user presses the enter button of the Num Pad.
The condition can be found under `UI > Num Pad Enter Button`.

The **Num Pad Enter Button Condition** accepts the following parameters:

**Num Pad**: The reference of the instantiated Num Pad in the scene. If left empty the behavior will try and find a valid Num Pad in the scene.  

### Inspector
![Inspector Layout](images/numpad_conditions.png "Num Pad Conditions")


## Num Pad Compare Values Condition
### Introduction
This condition will be completed if the entered Num Pad value fits the Right Operant depending on the selected Operator setting.
(This condition works very much like the Compare Numbers Condition of the States and Data Add-On.)
The condition can be found under `States and Data > Compare Num Pad Numbers`.

The **Num Pad Compare Values Condition** accepts the following parameters:

**Num Pad**: The reference of the instantiated Num Pad in the scene. If left empty the behavior will try and find a valid Num Pad in the scene. 

`Right Operand` can either be another data property or a constant value entered in the inspector.
`Operator` defines the type of operation to perform. 


## UI Button Condition
### Introduction
This condition will be completed if the user clicks the selected Ui Element. This will work for Buttons as well as for Toggles.
The condition can be found under `UI > Button Click`.

### Inspector
![Inspector Layout](images/ui_button_condition.png "UI Button Condition")

The **UI Button Condition** accepts the following parameters:

**UI Button**:  Use your UI Element (Button or Toggle) in here.


## Set Localized Text Behavior
### Introduction
Dynamically set the text of an UI element (TextMesh, TextMeshPro, also 3D Text is supported) with Unity Localization support.
The behavior can be found under `UI > Set Localized Text`.

### Inspector
![Inspector Layout](images/ui_localized_text.png "Set Localized Text")

The **Use Localized Text Behavior** accepts the following parameters:

**Localization Table (Optional)**: If you want to use the Unity Localization, enter the string table name here and use a key for the text element.

**Button Text / Key**: Enter the text for your UI element or use a Localization Table Key.

**Append Text**: Text will not replace the old text, but will be appended.

**Text Component**: EThe Text Component (TextMesh, textMeshPro, UI Text)


## UI Outline Behavior
### Introduction
Add an pulsating outline to a UI element to gain user attention.
The behavior can be found under `UI > Outline`.

### Inspector
![Inspector Layout](images/ui_outline.png "Ui Outline")

The **Outline Behavior** accepts the following parameters:

**Animation Delay**: Duration of the outline pulse animation.

**Outline Border Size**: Thickness / size of the outline.

**Effect Color**: Desired outline color.

**Reset after Step**: Outline will be disabled again after the step (or stay active)

**Outline Element**: UI Element you want to use the outline for. 


## Contact
If you have any issues, please contact [asset.support@lefx.de](mailto:asset.support@lefx.de). We’d love to get your feedback, both positive and constructive. By sharing your feedback you help us improve - thank you in advance!

If you have further questions to the VR Builder Toolset in general - please consider joining the official [Discord server](http://community.mindport.co) for quick support from the developer and fellow users. Suggest and vote on new ideas to influence the future of the VR Builder.

Make sure to review our [asset](https://assetstore.unity.com/packages/slug/265432) if you like it. It will help us immensely.