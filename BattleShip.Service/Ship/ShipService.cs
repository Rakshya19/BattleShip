using BattleShip.Model.Model;
using BattleShip.Service.Board;
using BattleShip.Service.File;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static BattleShip.Model.Enum.Enum;

namespace BattleShip.Service.Ship
{
    public class ShipService : IShipService
    {
        private readonly IFileService _fileService;
        private readonly IBoardService _boardService;
        private readonly IConfiguration _config;
        public ShipService(IFileService fileService, IBoardService boardService , IConfiguration config)
        {
            _fileService = fileService;
            _boardService = boardService;
            _config = config;

        }
        public ShipModel CreateShip(string shipType)
        {
            try
            {
                var shipList=validateShip(shipType);


                ShipModel model = new ShipModel();

                if (shipType.ToLower() == Enum.GetName(typeof(ShipType), ShipType.carrier).ToLower())
                {
                    model.shipType = Enum.GetName(typeof(ShipType), ShipType.carrier);
                    model.size = 5;
                    shipList.Add(model);
                }
                else if (shipType.ToLower() == Enum.GetName(typeof(ShipType), ShipType.battleship).ToLower())
                {
                    model.shipType = Enum.GetName(typeof(ShipType), ShipType.battleship);
                    model.size = 4;
                    shipList.Add(model);

                }
                else if (shipType.ToLower() == Enum.GetName(typeof(ShipType), ShipType.cruiser).ToLower())
                {
                    model.shipType = Enum.GetName(typeof(ShipType), ShipType.cruiser);
                    model.size = 3;
                    shipList.Add(model);

                }
                else if (shipType.ToLower() == Enum.GetName(typeof(ShipType), ShipType.destroyer).ToLower())
                {
                    model.shipType = Enum.GetName(typeof(ShipType), ShipType.destroyer);
                    model.size = 2;
                    shipList.Add(model);

                }
                else if (shipType.ToLower() == Enum.GetName(typeof(ShipType), ShipType.submarine).ToLower())
                {
                    model.shipType = Enum.GetName(typeof(ShipType), ShipType.submarine);
                    model.size = 1;
                    shipList.Add(model);

                }
                else
                {
                    throw new Exception("ShipType doesnot exist. Please select either " + ShipType.carrier + " or " + ShipType.battleship + " or " + ShipType.cruiser + " or " + ShipType.destroyer + " or " + ShipType.submarine);
                }
                _fileService.SetFileContent("ship.json", JsonConvert.SerializeObject(shipList));
                return model;
                
            }

            catch (Exception ex)
            {

                throw new Exception("Error in creating ship :" + ex.Message);
            }
        }


        public List<ShipModel> GetShipList()
        {
            try
            {
                var ships = _fileService.GetFileContent("ship.json");
                return JsonConvert.DeserializeObject<List<ShipModel>>(ships);
            }

            catch (Exception ex)
            {

                throw new Exception("Couldnot fetch ship List :" + ex.Message);
            }
        }


        public BoardShipViewModel PlaceShipInBoard(ShipPlacementRequestModel shipPlacementModel)
        {
       
        try
            {
                var listBoardModel = _boardService.GetBoard();
                if (listBoardModel == null)
                {
                    throw new Exception("Board Doesnot exists!! Create board first.");
                }
                var shipList = GetShipList();
                if (shipList == null)
                {
                    throw new Exception("Ship Doesnot exists!! Create Ship first.");
                }
                
                if (!Enum.IsDefined(typeof(ShipType), shipPlacementModel.ShipType.ToLower()))
                
                {
                    throw new Exception("Ship Type must be either Carrier or Battlership or Cruiser, Destroyer or Submarine!!");
                }

                var shipAddedList = GetPlacedShip();
                if (shipAddedList == null)
                {
                    shipAddedList = new List<ShipViewModel>();
                }

                var shipModel = shipList.Where(x => x.shipType.ToLower() == shipPlacementModel.ShipType.ToLower()).FirstOrDefault();


                if (!Enum.IsDefined(typeof(ShipOrientation), shipPlacementModel.shipOrientation.ToLower()))
                {
                    throw new Exception("Ship Orientation can only be either Horizontal or vertical!!");
                }


                CheckBoardOccupied(shipModel, shipPlacementModel.shipOrientation, shipPlacementModel.Row, shipPlacementModel.Column, listBoardModel);

                BoardShipViewModel boardShipViewModel = new BoardShipViewModel();

                List<ShipViewModel> ListshipViewModel = new List<ShipViewModel>();

                if (shipPlacementModel.shipOrientation.ToLower() == Enum.GetName(typeof(ShipOrientation), ShipOrientation.vertical).ToLower())
                {
                    var guid = Guid.NewGuid();
                    for (int i = 0; i < shipModel.size; i++)
                    {

                        var data = listBoardModel.Where(x => x.Row == shipPlacementModel.Row + i && x.Column == shipPlacementModel.Column).FirstOrDefault();
                        {
                            data.Occupied = true;
                            ShipViewModel model = new ShipViewModel()
                            {
                                BoardShipNumber = guid,
                                Row = shipPlacementModel.Row + i,
                                Column = shipPlacementModel.Column,
                                Hit = false,
                                ShipType = shipModel.shipType
                            };

                            ListshipViewModel.Add(model);
                        }

                    }
                }
                if (shipPlacementModel.shipOrientation.ToLower() == Enum.GetName(typeof(ShipOrientation), ShipOrientation.horizontal).ToLower())
                {

                    for (int i = 0; i < shipModel.size; i++)
                    {
                        var data = listBoardModel.Where(x => x.Row == shipPlacementModel.Row && x.Column == shipPlacementModel.Column + i).FirstOrDefault();
                        {
                            data.Occupied = true;

                            ShipViewModel model = new ShipViewModel();
                            model.Row = shipPlacementModel.Row;
                            model.Column = shipPlacementModel.Column + i;
                            model.Hit = false;
                            model.ShipType = shipModel.shipType;


                            ListshipViewModel.Add(model);
                        }

                    }

                }
               
                boardShipViewModel.ListBoardModel = listBoardModel.Where(x => x.Occupied == true).ToList();
                boardShipViewModel.ListShipViewModel = ListshipViewModel;
                if (boardShipViewModel != null)
                {
                    shipAddedList.AddRange(boardShipViewModel.ListShipViewModel);
                    foreach (var item in boardShipViewModel.ListBoardModel)
                    {
                        listBoardModel.Where(x => x.Row == item.Row && x.Column == item.Column).FirstOrDefault(x => x.Occupied = true);
                    }
                }

                _fileService.SetFileContent("board.json", JsonConvert.SerializeObject(listBoardModel));
                _fileService.SetFileContent("shipPlacement.json", JsonConvert.SerializeObject(shipAddedList));

                return boardShipViewModel;
            }
            catch (Exception ex)
            {

                throw new Exception("Error in adding ship to board :" + ex.Message);
            }
        }

       

        private void CheckBoardOccupied(ShipModel ship, string shipOrientation, int row, int column, List<BoardModel> ListBoardModel)
        {
            var errormessage1 = "Ship cannot be placed out of boards!!";
            var errormessage2 = "Cell is already occupied. Place the ship in another position!!";

            int boardSize = 10;
            if (shipOrientation.ToLower() == Enum.GetName(typeof(ShipOrientation), ShipOrientation.vertical).ToLower())
            {
                if (row + ship.size - 1 > boardSize)
                {
                    throw new IndexOutOfRangeException(errormessage1);
                }
            }
            if (shipOrientation.ToLower() == Enum.GetName(typeof(ShipOrientation), ShipOrientation.horizontal).ToLower())
            {
                if (column + ship.size - 1 > boardSize)
                {
                    throw new IndexOutOfRangeException(errormessage1);
                }
            }
            if (shipOrientation.ToLower() == Enum.GetName(typeof(ShipOrientation), ShipOrientation.vertical).ToLower())
            {
                for (int i = 0; i < ship.size; i++)
                {
                    bool occupied = ListBoardModel.Where(x => x.Row == row + i && x.Column == column).Select(x => x.Occupied).FirstOrDefault();
                    {
                        if (occupied)
                        {
                            throw new IndexOutOfRangeException(errormessage2);

                        }
                    }
                }
            }
            if (shipOrientation == Enum.GetName(typeof(ShipOrientation), ShipOrientation.horizontal).ToLower())
            {
                for (int i = 0; i < ship.size - 1; i++)
                {
                    bool occupied = ListBoardModel.Where(x => x.Row == row && x.Column == column + i).Select(x => x.Occupied).FirstOrDefault();
                    {
                        if (occupied)
                        {

                            throw new IndexOutOfRangeException(errormessage2);

                        }
                    }

                }

            }

        }
        public ShipAndBoardstatus AttackShip(Dimensions model)
        {
            try
            {
                int rows = _config.GetValue<int>("Rows");
                int columns = _config.GetValue<int>("Columns");

                if (model.Row > rows || model.Column > columns)
                {
                    throw new IndexOutOfRangeException("Attack cannot be out of boards!!");
                }

                var boardList = _boardService.GetBoard();

                var shipList = GetShipList();

                var shipAddedList = GetPlacedShip();

                if (shipAddedList == null)
                {
                    shipAddedList = new List<ShipViewModel>();
                }
                if (shipAddedList == null)
                {
                    return ShipAndBoardstatus.Miss;
                }
                if (boardList == null)
                {
                    throw new Exception("Boards must have occupied ships in them!!");
                }
                var boardShipNumber = shipAddedList.Where(x => x.Row == model.Row & x.Column == model.Column).Select(x => x.BoardShipNumber).FirstOrDefault();

                var data = shipAddedList.Where(x => x.Row == model.Row && x.Column == model.Column).FirstOrDefault();
                if (data == null)
                {
                    return ShipAndBoardstatus.Miss;
                }
                 if (data.Hit)
                    {
                        throw new IndexOutOfRangeException("This cell is already hit before!!");
                    }
                    shipAddedList.Where(x => x.Row == model.Row && x.Column == model.Column).FirstOrDefault(x => x.Hit = true);

                        _fileService.SetFileContent("shipPlacement.json", JsonConvert.SerializeObject(shipAddedList));

                        boardList.Where(x => x.Row == model.Row && x.Column == model.Column).FirstOrDefault(x => x.Occupied = false);

                        _fileService.SetFileContent("board.json", JsonConvert.SerializeObject(boardList));

                        if (boardList.Where(x => x.Occupied == true).Count() == 0)
                        {
                            return ShipAndBoardstatus.GameOver;
                        }
                        else
                        {
                            var shipCellCount = shipAddedList.Where(x=>x.BoardShipNumber==boardShipNumber).Count();
                            var hitCount = shipAddedList.Where(x => x.BoardShipNumber == boardShipNumber && x.Hit == true).Count();

                            if (hitCount == shipCellCount)
                            {
                                return ShipAndBoardstatus.ShipSunk;

                            }
                            else
                            {
                                return ShipAndBoardstatus.Hit;
                            }

                        }                    

                       }
            catch (Exception ex)
            {

                throw new Exception("Error in attacking ships: " + ex.Message);
            }


        }
        public List<ShipViewModel> GetPlacedShip()
        {
            var shipsAddedOnBoard = _fileService.GetFileContent("shipPlacement.json");

            return JsonConvert.DeserializeObject<List<ShipViewModel>>(shipsAddedOnBoard);
        }

        private List<ShipModel> validateShip(string shipType)
        {
            var shipList = GetShipList();
            if (shipList == null)
            {
                shipList = new List<ShipModel>();
            }

            if (shipList.Where(x => x.shipType.ToLower() == shipType.ToLower()).Any())
            {
                throw new Exception("Ship already exists!");
            }

            if (shipType == null)
            {
                throw new Exception("ShipType cannot be empty!!");
            }
            return shipList;
        }
    }
}
