using BattleShip.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BattleShip.Model.Enum.Enum;

namespace BattleShip.Service.Ship
{
    public class ShipService : IShipService
    {
        public ShipModel CreateShip(string shipType)
        {
            try
            {
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
                    model.size = 3;
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


        public ShipModel GetShip(string shipType)
        {
            try
            {
                ShipModel model = new ShipModel();
                model = CreateShip(shipType);
                return model;
            }

            catch (Exception ex)
            {

                throw new Exception("Error in creating ship :" + ex.Message);
            }
        }


        public BoardShipViewModel PlaceShipInBoard(ShipModel ship, string shipOrientation, int row, int column, List<BoardModel> ListBoardModel)
        {
            try
            {
                //here row and columns are cell where we want the ship to be positioned

                CheckBoardOccupied(ship, shipOrientation, row, column, ListBoardModel);

                BoardShipViewModel boardShipViewModel = new BoardShipViewModel();

                List<ShipViewModel> ListshipViewModel = new List<ShipViewModel>();

                if (shipOrientation.ToLower() == Enum.GetName(typeof(ShipOrientation), ShipOrientation.Vertical).ToLower())
                {
                    ShipViewModel model = new ShipViewModel();
                    for (int i = 0; i < ship.size; i++)
                    {
                        var data = ListBoardModel.Where(x => x.Row == row + i && x.Column == column).FirstOrDefault();
                        {
                            data.Occupied = true;

                            model.Row = row + i;
                            model.Column = column;
                            model.Hit = false;
                            model.ShipType = ship.shipType;

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
        public ShipAndBoardstatus AttackShip(int row, int column, List<BoardModel> ListBoardModel, List<ShipViewModel> shipViewModel)
        {
            try
            {

                if (row > 10 || column > 10)
                {
                    throw new IndexOutOfRangeException("Attack cannot be out of boards!!");
                }
                if (shipViewModel == null)
                {
                    return ShipAndBoardstatus.Miss;
                }
                if (ListBoardModel == null)
                {
                    throw new Exception("Boards must have occupied ships in them!!");
                }
                var data = shipViewModel.Where(x => x.Row == row && x.Column == column).FirstOrDefault();
                if (data != null)
                {
                    if (!data.Hit)
                    {
                        data.Hit = true;
                        var boardData = ListBoardModel.Where(x => x.Row == row && x.Column == column).FirstOrDefault();
                        boardData.Occupied = false;

                        if (ListBoardModel.Where(x => x.Occupied == true).Count() == 0)
                        {
                            return ShipAndBoardstatus.GameOver;
                        }
                        else
                        {
                            var shipCellCount = shipViewModel.Count;
                            var hitCount = shipViewModel.Where(x => x.Hit == true).Count();

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

                throw new Exception("Error in attacking ships: " +ex.Message);
            }


        }
    }
}
