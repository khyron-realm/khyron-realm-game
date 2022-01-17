<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="https://github.com/khyron-realm/khyron-realm-game">
    <img src="Images/logo.png" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center">Khyron Realm - Game</h3>

  <p align="center">
    <a href="https://khyron-realm.com/docs/description"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/khyron-realm/khyron-realm-game">View Demo</a>
    ·
    <a href="https://github.com/khyron-realm/khyron-realm-game/issues">Report Bug</a>
    ·
    <a href="https://github.com/khyron-realm/khyron-realm-game/issues">Request Feature</a>
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
    <li><a href="#references">References</a></li>
  </ol>
</details>


<!-- ABOUT THE PROJECT -->
## About The Project

<img src="images/game.png" alt="Game">

The repository contains the Khyron Realm Game made with Unity using Darkrift 2 Networking.

### Built With

* [Unity](https://unity.com/)


<!-- GETTING STARTED -->
## Getting Started

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/khyron-realm/khyron-realm-game.git
   ```
2. Open project in **Unity**


<!-- USAGE EXAMPLES -->
## Usage

More detailes can be found on [Wiki](https://khyron-realm.com/docs/description)

<!-- ROADMAP -->
## Roadmap

See the [open issues](https://github.com/khyron-realm/khyron-realm-game/issues) for a list of proposed features (and known issues).


<!-- CONTRIBUTING -->
## Project structure

```bash
< PROJECT ROOT >
    |
    |-- Animations                               
    |-- Editor   
    |-- Fonts 
    |-- Materials
    |    | -- Shaders
    |
    |-- Meshes
    |-- Plugins
    |    | -- RainbowAssets
    |
    |-- Prefabs
    |    | -- Buttons
    |    | -- Icons
    |    | -- Robot
    |
    |-- Resources
    |    | -- Blocks
    |    | -- Buttons
    |    | -- IconFolders
    |    | -- Map
    |    | -- Panels
    |    | -- Resources
    |    | -- Robots
    |    | -- UI
    |
    |-- Scenes
    |
    |-- Scriptable Objects
    |    | -- Mine Resources
    |    | -- Player
    |    | -- Prices
    |    | -- Robots
    |
    |-- Scripts
    |    |-- Auxiliary Methods
    |    |    |-- AuxiliaryMethods.cs                   # Auxiliary methods that have universal meaning
    |    |    |-- ObjectPooling.cs                      # Pools objects for different purposes 
    |    |
    |    |-- Bidding   
    |    |   |-- ActivateScanning.cs                    # Activates the scanning procedure and handle inputs 
    |    |    |-- ScanMine.cs                           # It is responsable for discovering one area as user requested
    |    |    |-- StartAuction.cs                       # Start the auction and takes care of time
    |    |
    |    |-- Camera Actions
    |    |    |-- PanPinch.cs                           # Script for moving the camera using touch input 
    |    |    |-- UserTouch.cs                          # Fundamental touch operations simplifiend in methods
    |    |
    |    |-- Canvas
    |    |    |-- CanvasHelper.cs                       # Helps to resize the canvas to fit in safe area of the phone
    |    |
    |    |-- Convert
    |    |    |-- ConvertResources.cs                   # Used for converting resources into energy [Converting floor]
    |    |
    |    |-- GameErrors
    |    |    |-- RaiseGameError.cs                     # Displays on the screen the error that happened {Not enough resources etc}
    |    |
    |    |-- Manager Scripts
    |    |    |-- GameManager                           # Manage game settings during gameplay
    |    |
    |    |-- Mine                      
    |    |    |-- Mine Creation Tools
    |    |        |-- ShapeTheMine.cs                   # Tool used to shape the mine [Only in unity editor used]
    |    |    |-- Values Generation
    |    |        |-- GridGeneratePositions.cs          # Generate blocks for the whole 2d array in the map [Only in unity editor used]
    |    |        |-- GridHiddenValues.cs               # Generate the hidden values for the blocks in the mine
    |    |        |-- GridVisibleValues.cs              # Generate the visible values for the blocks in the mine
    |    |    |-- MineEnergyEstimates.cs                # An estimated price of the mine in energy
    |    |    |-- MineGenerator.cs                      # Generates the mine [Instantiate tilemap]
    |    |    |-- MineTouched.cs                        # Detect if mine is touched and Invokes an event
    |    |    |-- RefreshMineValues.cs                  # Refresh the seeds and coefficients for mine generation
    |    |    |-- ShowMineDetails.cs                    # Shows more buttons and details about the mine [Enter button, refresh button] 
    |    |
    |    |-- Networking
    |    |    |-- Auction
    |    |    |    | -- AuctionRoom.cs                  # Auction room structure
    |    |    |    | -- AuctionManager.cs               # Auction Manager for handling the auction
    |    |    |    | -- Bid.cs                          # Bid structure
    |    |    |    | -- Player.cs                       # Player structure
    |    |    |-- Chat
    |    |    |    | -- ArrayPrefs.cs                   # Array methods  
    |    |    |    | -- ChatGroup.cs                    # Chat group structure
    |    |    |    | -- ChatManager.cs                  # Chat Manager for handling the chat
    |    |    |    | -- ChatMessage.cs                  # Chat message structure
    |    |    |    | -- Filter.cs                       # Chat filter
    |    |    |    | -- MessageType.cs                  # Message type  
    |    |    |-- Friends
    |    |    |    | -- FriendsManager.cs               # Friends Manager for handling friends
    |    |    |-- Headquarters
    |    |    |    | -- BuildTask.cs                    # Build task structure
    |    |    |    | -- HeadquartersManager.cs          # Headquarters Manager for handling hq
    |    |    |    | -- PlayerData.cs                   # Player data structure
    |    |    |    | -- Resource.cs                     # Resource structure
    |    |    |    | -- Robot.cs                        # Robot structure
    |    |    |-- Launcher
    |    |    |    | -- DarkriftServerConnection.cs     # Methods for connecting to the server
    |    |    |    | -- NetworkManager.cs               # Manager for the client connection
    |    |    |    | -- Singleton.cs                    # Singleton class for the network manager
    |    |    |-- Login
    |    |    |    | -- LoginManager.cs                 # Manager for authenticating the user
    |    |    |    | -- Rsa.cs                          # Login types class
    |    |    |    | -- Rsa.cs                          # RSA encryption method
    |    |    |-- Mines
    |    |    |    | -- Mine.cs                         # Mine structure
    |    |    |    | -- MineGenerator.cs                # Mine generator
    |    |    |    | -- MinePlugin.cs                   # Mine Plugin for handling the mines
    |    |    |    | -- MineScan.cs                     # Mine scan structure
    |    |    |    | -- ResourcesData.cs                # Resources data structure
    |    |    |-- MongoDBConnector
    |    |    |    | -- DataLayer.cs                    # Data layer for MongoDB database
    |    |    |    | -- MongoDBPlugin.cs                # MongoDB Plugin for handling MongoDB
    |    |    |-- Tags
    |    |    |    | -- AuctionTags.cs                  # Tags for auction rooms
    |    |    |    | -- ChatTags.cs                     # Tags for game messages
    |    |    |    | -- FriendsTags.cs                  # Tags for login messages
    |    |    |    | -- HeadquartersTags.cs             # Tags for headquarters messages
    |    |    |    | -- LoginTags.cs                    # Tags for login messages
    |    |    |    | -- MineTags.cs                     # Tags for mines
    |    |    |    | -- Tags.cs                         # Tags structure    
    |    |
    |    |-- Panels     
    |    |    |-- BidsDisplayUI.cs                      # Displays the bids in the AuctionScene
    |    |    |-- ChangeScreen.cs                       # Used to change screens in the same scene [HQ -- > Map --> HQ]
    |    |    |-- ClosePanelUsingBackground.cs          # Used to close any panel touching the background [Outside of panel]
    |    |    |-- Confirm.cs                            # Confirmation panel used to make further verification of user decisions 
    |    |    |-- OpenPanel.cs                          # Open the desired panel if gameObject is touched
    |    |    |-- ProgressBar.cs                        # Handle progress bar values and adjust the visuals for it 
    |    |
    |    |-- Pay Operations
    |    |    |-- PayRobots.cs                          # Used to make a transaction or refund regarding robots [Build robot --> pay energy]
    |    |
    |    |-- Robot 
    |    |    |-- DeployRobots.cs                       #
    |    |    |-- HallOfFameInstantiateRobots.cs        # Instantiate panel with robots and
    |    |    |-- RobotManagerUIForMine.cs              # Instantiate buttons in the mine with all trained robots
    |    |    |-- RobotLevelingUp.cs                    # Takes care of robots leveling up
    |    |    |-- RobotManager.cs                       #
    |    |    |-- RobotPlayerProgress.cs                # Struct that store the level and state [lock/unlocked] of the robot
    |    |    |-- RobotsUnlocking.cs                    # Unlocks the robot for user
    |    |
    |    |-- Save
    |    |    |-- Persistent Data Across Scenes
    |    |        |-- GetMineGenerationData.cs          # Static class with data that persist across scenes
    |    |        |-- GetRobotsTrained.cs               # Static class with data that persist across scenes
    |    |        |-- GetTimeTillAuctionEnds.cs         # Static class with data that persist across scenes
    |    |    |-- Serializable Class For Storage Data   
    |    |        |-- MineData.cs                       # Serializable class that have the data about the mine | Used to be save in binary format
    |    |        |-- TimeData.cs                       # Serializable class that have data about the auction time | Used to be save in binary format
    |    |    |-- Values
    |    |        |-- ISaveOperations.cs                # Interface for basic SAVE and LOAD operations of any data
    |    |        |-- MineValues.cs                     #
    |    |        |-- TimeValues.cs                     #
    |    |    |-- SaveSystem.cs                         # Saves and Load data
    |    |
    |    |
    |    |-- Scenes Management                      
    |    |    |-- ChangeScenes.cs                       #
    |    |
    |    |-- Scriptable Objetcts        
    |    |    |-- Mine                     
    |    |        |-- MineResources.cs                  #
    |    |        |-- MineShape.cs                      # Create SO that stores the shape of the mine
    |    |    |-- Player
    |    |        |-- LevelsThresholds.cs               # SO with all the levels threshold in xp for leveling up
    |    |    |-- Resources
    |    |        |-- GameResources.cs                  #
    |    |        |-- PriceToBuildOrUpgrade.cs          # Used for storing the price in resources [4 resources] of any operation
    |    |    |-- Robots
    |    |        |-- Robot.cs                          #
    |    |        |-- RobotLevel.cs                     #
    |    |        |-- StatusRobot.cs                    #
    |    |
    |    |-- Stores
    |    |    |-- ResourcesOperations.cs                # Operations of removing and adding with the resources [all 4]
    |    |    |-- StatBarsOperations.cs                 # Manage values and what is displayed on the stat bars of resources
    |    |    |-- StatsOperaions.cs                     # Manage operations of level and Xp
    |    |    |-- StoreDataPlayerStats.cs               #
    |    |    |-- StoreDaraResources.cs                 #
    |    |    
    |    |-- Tiles Data                        
    |    |    |-- DataOfTile.cs                         #
    |    |    |-- StoreAllTiles.cs                      #
    |    |    |-- TilesRule.cs                          #
    |    |
    |    |-- Timer
    |    |    |-- Timer.cs                              # Keeps track of the time during a process
    |    |
    |    |-- Train
    |    |    |-- BuildRobots.cs                        #
    |    |    |-- BuildRobotsOperations.cs              #
    |    |    |-- RobotsInBuilding.cs                   #
    |    |    |-- RobotsInBuildingOperations.cs         #
    |    |    |-- StoreRobots.cs                        #
    |    |    
    |    |-- Upgrade 
    |    |    |-- UpgradeRobots.cs                      #         
    |
    |-- Sounds
    |-- Tiles
    |-- *********************************************************************************************************************************************
```


## Used Plugins and Software

1. [Rainbow Folders 2](https://assetstore.unity.com/packages/tools/utilities/rainbow-folders-2-143526)
2. [DotWeen](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676)
3. [DarkRift Networking 2 - PRO - v2.10.1](https://assetstore.unity.com/packages/tools/network/darkrift-networking-2-pro-95399)


<!-- LICENSE -->
## License

[![CC BY-NC-SA 4.0][cc-by-nc-sa-shield]][cc-by-nc-sa]

This work is licensed under a
[Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License][cc-by-nc-sa].

[![CC BY-NC-SA 4.0][cc-by-nc-sa-image]][cc-by-nc-sa]

[cc-by-nc-sa]: http://creativecommons.org/licenses/by-nc-sa/4.0/
[cc-by-nc-sa-image]: https://licensebuttons.net/l/by-nc-sa/4.0/88x31.png
[cc-by-nc-sa-shield]: https://img.shields.io/badge/License-CC%20BY--NC--SA%204.0-lightgrey.svg



<!-- REFERENCES -->
## References

1. Darkrift example [Darkrift2_Boilerplate](https://github.com/mwage/DarkRift2_Boilerplate)
1. Project Template adapted from [Othneil Drew](https://github.com/othneildrew) / [Best-README-Template](https://github.com/othneildrew/Best-README-Template).
