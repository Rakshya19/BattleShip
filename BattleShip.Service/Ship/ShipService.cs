using BattleShip.Model.Model;
using BattleShip.Service.File;
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
        public ShipService(IFileService fileService)
        {
            _fileService = fileService;
        }
        public ShipModel CreateShip(string shipType)
        {
            try
            {
                if (shipType == null)
                {
                    throw new Exception("ShipType cannot be empty!!");
                }
                ShipModel model = new ShipModel();

                if (shipType.ToLower() == Enum.GetName(typeof(ShipType), ShipType.Carrier).ToLower())
                {
                    model.shipType = Enum.GetName(typeof(ShipType), ShipType.Carrier);
                    model.size = 5;
                    return model;

                }
                if (shipType.ToLower() == Enum.GetName(typeof(ShipType), ShipType.BattleShip).ToLower())
                {
                    model.shipType = Enum.GetName(typeof(ShipType), ShipType.BattleShip);
                    model.size = 4;
                    return model;

                }
                if (shipType.ToLower() == Enum.GetName(typeof(ShipType), ShipType.Cruiser).ToLower())
                {
                    model.shipType = Enum.GetName(typeof(ShipType), ShipType.Cruiser);
                    model.size = 3;
                    return model;

                }
                if (shipType.ToLower() == Enum.GetName(typeof(ShipType), ShipType.Destroyer).ToLower())
                {
                    model.shipType = Enum.GetName(typeof(ShipType), ShipType.Destroyer);
                    model.size = 2;
                    return model;

                }
                if (shipType.ToLower() == Enum.GetName(typeof(ShipType), ShipType.Submarine).ToLower())
                {
                    model.shipType = Enum.GetName(typeof(ShipType), ShipType.Submarine);
                    model.size = 1;
                    return model;

                }
                throw new Exception("ShipType doesnot exist. Please select either " + ShipType.Carrier + " or " + ShipType.BattleShip + " or " + ShipType.Cruiser + " or " + ShipType.Destroyer + " or " + ShipType.Submarine);
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


        public BoardShipViewModel PlaceShipInBoard(ShipModel ship, string shipOrientation, int row, int column, List<BoardModel> ListBoardModel)
        {
            try
            {
                //if(shipOrientation.ToLower() != Enum.GetName(typeof(ShipOrientation),ShipOrientation.Horizontal) && shipOrientation != Enum.GetName(typeof(ShipOrientation), ShipOrientation.Vertical).ToLower())
                //    {

                //        throw new Exception("Ship Orientation can only be either Horizontal or vertical!!");

                //}

                if (!Enum.IsDefined(typeof(ShipOrientation), shipOrientation))
                {
                    throw new Exception("Ship Orientation can only be either Horizontal or vertical!!");
                }

                //here row and columns are cell where we want the ship to be positioned

                CheckBoardOccupied(ship, shipOrientation, row, column, ListBoardModel);

                BoardShipViewModel boardShipViewModel = new BoardShipViewModel();

                List<ShipViewModel> ListshipViewModel = new List<ShipViewModel>();

                if (shipOrientation.ToLower() == Enum.GetName(typeof(ShipOrientation), ShipOrientation.Vertical).ToLower())
                {
                    var guid = Guid.NewGuid();
                    for (int i = 0; i < ship.size; i++)
                    {

                        var data = ListBoardModel.Where(x => x.Row == row + i && x.Column == column).FirstOrDefault();
                        {
                            data.Occupied = true;
                            ShipViewModel model = new ShipViewModel()
                            {
                                BoardShipNumber = guid,
                                Row = row + i,
                                Column = column,
                                Hit = false,
                                ShipType = ship.shipType
                            };

                            ListshipViewModel.Add(model);
                        }

                    }
                }
                if (shipOrientation.ToLower() == Enum.GetName(typeof(ShipOrientation), ShipOrientation.Horizontal).ToLower())
                {

                    for (int i = 0; i < ship.size; i++)
                    {
                        var data = ListBoardModel.Where(x => x.Row == row && x.Column == column + i).FirstOrDefault();
                        {
                            data.Occupied = true;

                            ShipViewModel model = new ShipViewModel();
                            model.Row = row;
                            model.Column = column + i;
                            model.Hit = false;
                            model.ShipType = ship.shipType;


                            ListshipViewModel.Add(model);
                        }

                    }

                }
                boardShipViewModel.ListBoardModel = ListBoardModel.Where(x => x.Occupied == true).ToList();
                boardShipViewModel.ListShipViewModel = ListshipViewModel;

                return boardShipViewModel;
            }
            catch (Exception ex)
            {

                throw new Exception("Error in adding ship to board :" + ex.Message);
            }
        }

        //we can send those cells of board which are occupied in ListBoardModel parameter

        private void CheckBoardOccupied(ShipModel ship, string shipOrientation, int row, int column, List<BoardModel> ListBoardModel)
        {
            var errormessage1 = "Ship cannot be placed out of boards!!";
            var errormessage2 = "Cell is already occupied. Place the ship in another position!!";

            int boardSize = 10;
            if (shipOrientation.ToLower() == Enum.GetName(typeof(ShipOrientation), ShipOrientation.Vertical).ToLower())
            {
                if (row + ship.size - 1 > boardSize)
                {
                    throw new IndexOutOfRangeException(errormessage1);
                }
            }
            if (shipOrientation.ToLower() == Enum.GetName(typeof(ShipOrientation), ShipOrientation.Horizontal).ToLower())
            {
                if (column + ship.size - 1 > boardSize)
                {
                    throw new IndexOutOfRangeException(errormessage1);
                }
            }
            if (shipOrientation.ToLower() == Enum.GetName(typeof(ShipOrientation), ShipOrientation.Vertical).ToLower())
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
            if (shipOrientation == Enum.GetName(typeof(ShipOrientation), ShipOrientation.Horizontal).ToLower())
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
        //public ShipAndBoardstatus AttackShip(int row, int column, List<BoardModel> ListBoardModel, List<ShipViewModel> shipViewModel)
        public ShipAndBoardstatus AttackShip(AttackShipRequestModel model)
        {
            try
            {
                if (model.ShipInBoard == null)
                {
                    return ShipAndBoardstatus.Miss;
                }
                if (model.BoardCellList == null)
                {
                    throw new Exception("Boards must have occupied ships in them!!");
                }
                var boardShipNumber = model.ShipInBoard.Where(x => x.Row == model.Row & x.Column == model.Column).Select(x => x.BoardShipNumber).FirstOrDefault();
                //var ShipInBoard = model.ShipInBoard.Where(x => x.BoardShipNumber == boardShipNumber).ToList();

                var data = model.ShipInBoard.Where(x => x.Row == model.Row && x.Column == model.Column).FirstOrDefault();
                if (data != null)
                {
                    if (!data.Hit)
                    {
                        model.ShipInBoard.Where(x => x.Row == model.Row && x.Column == model.Column).FirstOrDefault(x => x.Hit = true);

                        _fileService.SetFileContent("shipPlacement.json", JsonConvert.SerializeObject(model.ShipInBoard));

                        model.BoardCellList.Where(x => x.Row == model.Row && x.Column == model.Column).FirstOrDefault(x => x.Occupied = false);

                        _fileService.SetFileContent("board.json", JsonConvert.SerializeObject(model.BoardCellList));

                        if (model.BoardCellList.Where(x => x.Occupied == true).Count() == 0)
                        {
                            return ShipAndBoardstatus.GameOver;
                        }
                        else
                        {
                            var shipCellCount = model.ShipInBoard.Count;
                            var hitCount = model.ShipInBoard.Where(x => x.Hit == true).Count();

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
                    else
                    {
                        throw new IndexOutOfRangeException("This cell is already hit before!!");
                    }

                }
                else
                {
                    return ShipAndBoardstatus.Miss;
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
    }
}
