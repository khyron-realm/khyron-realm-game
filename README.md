<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="https://github.com/target-software/Unlimited-Game-MiningGame">
    <img src="images/logo.png" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center">UNLIMITED </h3>

  <p align="center">
    README for the Unlimited game [Mining game]
    <br />
    <a href="https://github.com/target-software/Unlimited-Game-MiningGame"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/target-software/Unlimited-Game-MiningGame">View Demo</a>
    ·
    <a href="https://github.com/target-software/Unlimited-Game-MiningGame/issues">Report Bug</a>
    ·
    <a href="https://github.com/target-software/Unlimited-Game-MiningGame/issues">Request Feature</a>
  </p>
</p>



<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#license">License</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

<img src="images/img3.jpg" alt="Logo" width="1000" height="400">

Unlimited Game ... The next 2D social casual game with an interconnected economy system that teach users basic finance and selling skils.

### Built With

* [Unity](https://unity.com/)
<!-- GETTING STARTED -->
## Getting Started

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/your_username_/Project-Name.git
   ```
2. Open project in Unity



<!-- USAGE EXAMPLES -->
## Usage

More detailes can be found on [Google Drive](https://docs.google.com/document/d/1CHdDfEm5BDM8vAbeubNgLF-Et8YwMgCbreD4CC6dSfo/edit)


<!-- ROADMAP -->
## Roadmap

See the [open issues](https://github.com/target-software/Unlimited-Game-MiningGame/issues) for a list of proposed features (and known issues).



<!-- CONTRIBUTING -->
## Project structure

```bash
< PROJECT ROOT >
   |
   |-- Animations                               
   |-- Editor   
   |-- Fonts 
   |-- Materials
      | -- Shaders
   |-- Meshes
   |-- Plugins
      | -- RainbowAssets
   |-- Prefabs
      | -- Buttons
      | -- Icons
      | -- Robot
   |-- Resources
      | -- Blocks
      | -- Buttons
      | -- IconFolders
      | -- Map
      | -- Panels
      | -- Resources
      | -- Robots
      | -- UI
   |-- Scenes
   |-- Scriptable Objetcs
      | -- Mine Resources
      | -- Player
      | -- Prices
      | -- Robots
   |-- Scripts
      | -- Auxiliary
          | -- AuxiliaryMethods.cs              #Auxiliary methods
      | -- Camera movement
          | -- PanPinch.cs                      #Script for moving the camera using touch inpun 
      | -- Canvas
          | -- Bars
              | -- ProgressBar.cs               #Progress ba handler, manage visual state of the progress bar
          | -- Panels
              | -- ClosePanelUsingBackGround.cs #Closes selected pabel using the background
              | -- OpenPanel.cs                 #Open the panel that is selected for the curent gameObject + handle animations for opening the panel
          | -- CanvasHelper.cs                  #Resize the UI to fit inside the phone safe zone
          | -- ChangeScreen.cs                  #Activate one Gameobject and dezactivate the other one -- used for changing screens from hq to map
          | -- RobotsManagerUI.cs               #Used to instantiate buttons for all robots in the desired canvas
      | -- GameErrors
          | -- RaiseGameError.cs                #Displays on the screen the error that happened {Not enough resources etc}
      | -- Manager Scripts
          | -- DataStorageDuringGameplay
              | -- ResourcesOperations.cs
              | -- StatBarsOperations.cs
              | -- StatsOperaions.cs
              | -- StoreDataPlayerStats.cs
              | -- StoreDaraResources.cs
          | -- RobotHandlers
              | -- RobotLevelingUp.cs
              | -- RobotManager.cs
              | -- RobotPLayerProgress.cs
              | -- RobotsUnlocking.cs
          | -- GameManager                      #Manage game
      | -- Mine Generation                      #In review
          | -- ScriptableObjects
              | -- MinePatterns.cs
              | -- MineResources.cs
          | -- Values Generation
              | -- GridGeneratePositions.cs
              | -- GridHiddenValues.cs
              | -- GridVisibleValues.cs
          | -- MineGenerator.cs
      | -- Resources          
          | -- ConvertResources.cs               #Used for converting resources into energy
      | -- Robot Related
          | -- RobotBuilding
              | -- BuildRobots.cs                #Coroutine with the logic of building the robots
              | -- ManageIcons.cs                #Manage icons that will apear in the right in the building phase
              | -- ManageIconsDuringTraining.cs  #Manage icons that will apear in the right in the building phase 
              | -- PayRobots.cs                  #Is doing the payment for each robot [Pay,refund]
              | -- StoreTrainRobots.cs           #Keep track of robots in building and robots already built
              | -- StoreTrainRobotsOperations.cs #Operations of adding and removing robots from the queue
          | -- RobotUpgrading
              | -- UpgradeRobots.cs              #Upgrade robots logic
          | -- HallOfFameInstantiateRobots.cs    #Instantiate panel with robots and 
          | -- ObjectPooling.cs                  #Takes care of 30 gameObjects by activating and dezactivating them based on the need
      | -- Scriptable Objetcts
          | -- Mine
              | -- MinePlacingBlocks.cs
          | -- Player
              | -- LevelsThresholds.cs
          | -- Resources
              | -- GameResources.cs
              | -- PriceToBuildOrUpgrade.cs
          | -- Robots
              | -- Robot.cs
              | -- RobotLevel.cs
              | -- StatusRobot.cs
      | -- Tiles                                  #In review
      | -- Timer
          | -- Timer.cs                           #Keeps track of the time during a procces
      | -- Touch
          | -- UserTouch.cs                       #Fundamental touch operations simplifiend in methods
   |-- Sounds
   |-- Tiles
  ************************************************************************
```
## Used Plugins and Software

1. [Rainbow Folders 2](https://assetstore.unity.com/packages/tools/utilities/rainbow-folders-2-143526)
2. [DotWeen](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676)

<!-- LICENSE -->
## License

Project Template adapted from [Othneil Drew](https://github.com/othneildrew) / [Best-README-Template](https://github.com/othneildrew/Best-README-Template).


<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[product-screenshot]: images/screenshot.png
