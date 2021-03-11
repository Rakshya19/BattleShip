# BattleShip 

This project is created in visual studio 2015 in MVC 5. Initially I build a .net core api as I had visual studio 2019 installed in my machine but the free version lisence expired in the mean time and I had to create another project. All the methods were tested on postman, so I did not create the unit test case. If required can be done shortly. 

## Create Board
On page load it redirects to Board/CreateBoard which creates board with 10 rows and 10 columns.
LocalUrl: http://localhost:4222/Board/CreateBoard

## CreateShip
CreateShip requires ShipType as parameter. It takes 5 ship types Carrier, BattleShip, Cruiser, Destroyer and Submarine.
LocalUrl: http://localhost:4222/Ship/CreateShip?shipType=Carrier

## PlaceShipInBoard

While placing ship to board we must pass shipType,shipOrientation which is either Horizontal or vertical,  ShipStartingRow which is default 1 , ShipStartingColumn is also default 1.

LocalUrl: http://localhost:4222/Ship/PlaceShipInBoard?shipType=carrier&shipOrientation=vertical&shipRow=6&shipColumn=10

## Attack one ship on board
AttackOneShipOnBoard takes shipType,shipOrientation,shipRow,shipColumn,attackRow, attakColumn. Here board is created then ship is created and ship is placed on ship row and column and than attack is done in the attack row and column. this is a simple api to attack a ship which can give hit or miss.

LocalUrl:http://localhost:4222/Ship/AttackOneShipOnBoard?shipType=Cruiser&shipOrientation=Vertical&shipRow=3&shipColumn=5&AttackRow=5&attackColumn=5

## AttackShip (this is for hit, miss, sunk or game over)
Attacking Ship takes attack row, column, list of cells of board which are occupied, all shipcells where the particular ship is placed. This shipcell parameter is passed if the attacked cell has ship in it otherwise empty must be passed. We check for if it is empty. If empty then it is a Miss. If not than hit. If all ship cells list is hit then we say it is sunk. If the board occupied list is empty after it is hit then game is over. Passing parameter to attack ship is quiet tricky which I have explained below.

LocalUrl:http://localhost:4222/Ship/AttackShip

Parameter:
check for miss attack. (here ship in board will not have any data as that attack row and column provided has no ship)

    {
    "attackRow":5,
    "attackColumn":5,
    
        "boardCellList": [
            {
                "Occupied": true,
                "Row": 10,
                "Column": 6
            },
            {
                "Occupied": true,
                "Row": 10,
                "Column": 7
            },
            {
                "Occupied": true,
                "Row": 10,
                "Column": 8
            },
            {
                "Occupied": true,
                "Row": 10,
                "Column": 9
            },
            {
                "Occupied": true,
                "Row": 10,
                "Column": 10
            }]
        ,
        "shipInBoard": []
           
         }

This is a hit attack

    {
    "attackRow":2,
    "attackColumn":3,
    
        "boardCellList": [
            {
                "Occupied": true,
                "Row": 10,
                "Column": 6
            },
            {
                "Occupied": true,
                "Row": 10,
                "Column": 7
            },
            {
                "Occupied": true,
                "Row": 10,
                "Column": 8
            },
            {
                "Occupied": true,
                "Row": 10,
                "Column": 9
            },
            {
                "Occupied": true,
                "Row": 10,
                "Column": 10
            },
           
            {
                "Occupied": true,
                "Row": 1,
                "Column": 3
            },
            {
                "Occupied": true,
                "Row": 2,
                "Column": 3
            },
            {
                "Occupied": true,
                "Row": 3,
                "Column": 3
            },
            {
                "Occupied": true,
                "Row": 4,
                "Column": 3
            }
            ]
        ,
        "shipInBoard": [
        	 {
                "BoardShipNumber": 0,
                "ShipType": "BattleShip",
                "Hit": false,
                "Row": 1,
                "Column": 3
            },
            {
                "BoardShipNumber": 0,
                "ShipType": "BattleShip",
                "Hit": false,
                "Row": 2,
                "Column": 3
            },
            {
                "BoardShipNumber": 0,
                "ShipType": "BattleShip",
                "Hit": false,
                "Row": 3,
                "Column": 3
            },
            {
                "BoardShipNumber": 0,
                "ShipType": "Carrier",
                "Hit": false,
                "Row": 4,
                "Column": 3
            }
            ]
        }

There is only one cell occupied by ship in board which we attack now and that cell is hit than the game is over.

    {
    "attackRow":1,
    "attackColumn":3,
    
        "boardCellList": [
   
           
            {
                "Occupied": true,
                "Row": 1,
                "Column": 3
            }]
        ,
        "shipInBoard": [
        	 {
                "BoardShipNumber": 0,
                "ShipType": "BattleShip",
                "Hit": false,
                "Row": 1,
                "Column": 3
            },
            {
                "BoardShipNumber": 0,
                "ShipType": "BattleShip",
                "Hit": true,
                "Row": 2,
                "Column": 3
            },
            {
                "BoardShipNumber": 0,
                "ShipType": "BattleShip",
                "Hit": true,
                "Row": 3,
                "Column": 3
            },
            {
                "BoardShipNumber": 0,
                "ShipType": "Carrier",
                "Hit": true,
                "Row": 4,
                "Column": 3
            }
            ]
         }

Now I am targeting for attack row 2 column 3 and all other cells of this ship has already been hit. So the ship sinks in this case.

      {
        "attackRow":2,
         "attackColumn":3,
    
        "boardCellList": [
            {
                "Occupied": true,
                "Row": 10,
                "Column": 6
            },
            {
                "Occupied": true,
                "Row": 10,
                "Column": 7
            },
            {
                "Occupied": true,
                "Row": 10,
                "Column": 8
            },
            {
                "Occupied": true,
                "Row": 10,
                "Column": 9
            },
            {
                "Occupied": true,
                "Row": 10,
                "Column": 10
            },
           
            {
                "Occupied": true,
                "Row": 1,
                "Column": 3
            },
            {
                "Occupied": true,
                "Row": 2,
                "Column": 3
            },
            {
                "Occupied": true,
                "Row": 3,
                "Column": 3
            },
            {
                "Occupied": true,
                "Row": 4,
                "Column": 3
            }
            ]
        ,
        "shipInBoard": [
        	 {
                "BoardShipNumber": 0,
                "ShipType": "BattleShip",
                "Hit": true,
                "Row": 1,
                "Column": 3
            },
            {
                "BoardShipNumber": 0,
                "ShipType": "BattleShip",
                "Hit": false,
                "Row": 2,
                "Column": 3
            },
            {
                "BoardShipNumber": 0,
                "ShipType": "BattleShip",
                "Hit": true,
                "Row": 3,
                "Column": 3
            },
            {
                "BoardShipNumber": 0,
                "ShipType": "Carrier",
                "Hit": true,
                "Row": 4,
                "Column": 3
            }
            ]
         }
   
   
   
   
   
   
   
   


