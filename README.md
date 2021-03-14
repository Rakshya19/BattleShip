# BattleShip 

This project is created in visual studio 2019 community version. All the methods are tested on swagger. Internally data are saved on json file and are made empty on runtime.

## Create Board

Board is created with 10 rows and 10 columns. This value of row and column is saved on appsettings.json to avoid hard coading. 

LocalUrl:https://localhost:44398/swagger/index.html#/Board/Board_CreateBoard

HostingUrl: http://rakshyabhattarai-001-site1.itempurl.com/swagger/index.html#/Board/Board_CreateBoard

## CreateShip

CreateShip requires ShipType as parameter. It takes 5 ship types Carrier, BattleShip, Cruiser, Destroyer and Submarine.

LocalUrl:https://localhost:44398/swagger/index.html#/Ship/Ship_CreateShip

http://rakshyabhattarai-001-site1.itempurl.com/swagger/index.html#/Ship/Ship_CreateShip

## PlaceShipInBoard

While placing ship to board we must pass shipType,shipOrientation which is either Horizontal or vertical, and starting which row column we want to place the ship.

LocalUrl: https://localhost:44398/swagger/index.html#/Ship/Ship_PlaceShipInBoard

HostingUrl: http://rakshyabhattarai-001-site1.itempurl.com/swagger/index.html#/Ship/Ship_PlaceShipInBoard

Paameters: 
{
  "row": 0,
  "column": 0,
  "shipType": "string",
  "shipOrientation": "string"
}

## AttackShip (this is for hit, miss, sunk or game over)

Attacking Ship takes attack row, column as a parameter. 

http://rakshyabhattarai-001-site1.itempurl.com/swagger/index.html#/Ship/Ship_AttackShip

http://rakshyabhattarai-001-site1.itempurl.com/swagger/index.html#/Ship/Ship_AttackShip

Parameters:

{
  "row": 0,
  "column": 0
}

