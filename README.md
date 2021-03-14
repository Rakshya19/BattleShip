# BattleShip 

This project is created in visual studio 2019 community version. All the methods are tested on swagger. Internally data are saved on json file and are made empty on runtime. So, clearing cache might be required sometime for the new test.

## Create Board

Board is created with 10 rows and 10 columns. This value of row and column is saved on appsettings.json to avoid hard coading. The raw data is saved on board.json 

LocalUrl:https://localhost:44398/swagger/index.html#/Board/Board_CreateBoard

HostingUrl: http://rakshyabhattarai-001-site1.itempurl.com/swagger/index.html#/Board/Board_CreateBoard

## CreateShip

CreateShip requires ShipType as parameter. It takes 5 ship types Carrier, BattleShip, Cruiser, Destroyer and Submarine. Data is saved on ship.json

LocalUrl:https://localhost:44398/swagger/index.html#/Ship/Ship_CreateShip

http://rakshyabhattarai-001-site1.itempurl.com/swagger/index.html#/Ship/Ship_CreateShip

## PlaceShipInBoard

While placing ship to board we must pass shipType,shipOrientation which is either Horizontal or vertical, and starting which row column we want to place the ship. Data is saved on shipPlacement.json. we can only create the 5 ships one each but we can place as many number of ships we want on the board as long as they are not occupied.

LocalUrl: https://localhost:44398/swagger/index.html#/Ship/Ship_PlaceShipInBoard

HostingUrl: http://rakshyabhattarai-001-site1.itempurl.com/swagger/index.html#/Ship/Ship_PlaceShipInBoard

Paameters: 

    {
     "row": 0,
      "column": 0,
     "shipType": "string",
      "shipOrientation": "string"
    }

## AttackShip 

Attacking Ship takes attack row, column as a parameter. 

LocalUrl : https://localhost:44398/swagger/index.html#/Ship/Ship_AttackShip

HostingUrl: http://rakshyabhattarai-001-site1.itempurl.com/swagger/index.html#/Ship/Ship_AttackShip

Parameters:

    {
     "row": 0,
     "column": 0
    }
    
## Packages

We might need to install some packages for the program to rum if it is not already installed.

--> Microsoft.Extensions.Configuration.Abstraction(5.0.0)(BattleShip.Service)

--> Microsoft.Extensions.Configuration.Binder(5.0.0)(BattleShip.Service)

--> NSwag.AspNetCore(13.10.8)(BattleShip.webAPI)

