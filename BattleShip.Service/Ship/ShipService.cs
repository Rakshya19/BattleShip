using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleShip.Model;
using static BattleShip.Model.Enum.Enum;
using BattleShip.Model.Model;

namespace BattleShip.Service.Ship
{
    public class ShipService : IShipService
    {
        public ShipModel CreateShip(ShipType shipType)
        {
            try
            {
                ShipModel model = new ShipModel();

                if (shipType == ShipType.BattleShip)
                {
                    model.shipType = ShipType.BattleShip;
                    model.size = 4;
                    return model;

                }
                if (shipType == ShipType.Carrier)
                {
                    model.shipType = ShipType.Carrier;
                    model.size = 5;
                    return model;

                }
                if (shipType == ShipType.Cruiser)
                {
                    model.shipType = ShipType.Cruiser;
                    model.size = 3;
                    return model;

                }
                if (shipType == ShipType.Destroyer)
                {
                    model.shipType = ShipType.Destroyer;
                    model.size = 3;
                    return model;

                }
                if (shipType == ShipType.Submarine)
                {
                    model.shipType = ShipType.Submarine;
                    model.size = 1;
                    return model;

                }
                throw new Exception("ShipType doesnot exist. Please select either " + ShipType.Carrier + " or " + ShipType.BattleShip + " or " + ShipType.Cruiser + " or " + ShipType.Destroyer + " or " + ShipType.Submarine);
            }

            catch (Exception ex)
            {

                throw new Exception("Error in creating ship :"+ ex.Message);
            }
        }
    
           


       
        public BoardShipViewModel AddShipToBoard(ShipModel ship, ShipOrientation shipOrientation,int row, int column,List<BoardModel> ListBoardModel)
        {
            try
            {
                //here row and columns are cell where we want the ship to be positioned

                CheckBoardOccupied(ship, shipOrientation, row, column, ListBoardModel);

            BoardShipViewModel boardShipViewModel = new BoardShipViewModel();

            List<ShipViewModel> ListshipViewModel = new List<ShipViewModel>();

            if (shipOrientation == ShipOrientation.Horizontal)
            {
                ShipViewModel model = new ShipViewModel();
                for (int i = 0; i < ship.size; i++)
                {
                    var data = ListBoardModel.Where(x => x.Row == row+ i && x.Column == column).FirstOrDefault();
                    {
                        data.Occupied = true;

                        model.Row = row + i;
                        model.Column = column;
                        model.Hit = true;
                        model.ShipType = ship.shipType;

                        ListshipViewModel.Add(model);
                    }
                    
                }
            }
            if (shipOrientation == ShipOrientation.Vertical)
            {
                ShipViewModel model = new ShipViewModel();
                for (int i = 0; i < ship.size; i++)
                {
                    var data = ListBoardModel.Where(x => x.Row == row && x.Column == column+ i).FirstOrDefault();
                    {
                        data.Occupied = true;

                        model.Row = row ;
                        model.Column = column+i;
                        model.Hit = true;
                        model.ShipType = ship.shipType;


                        ListshipViewModel.Add(model);
                    }

                }

            }
            boardShipViewModel.ListBoardModel = ListBoardModel;
            boardShipViewModel.ListShipViewModel = ListshipViewModel;

            return boardShipViewModel;
        }
            catch (Exception ex)
            {

                throw new Exception("Error in adding ship to board :" + ex.Message);
            }
        }

        private void CheckBoardOccupied(ShipModel ship,ShipOrientation shipOrientation, int row, int column, List<BoardModel> ListBoardModel)
        {
            var errormessage1 = "Ship cannot be placed out of boards!!";
            var errormessage2 = "Cell is already occupied. Place the ship in another position!!";

            int boardSize = 10;
            if (shipOrientation == ShipOrientation.Horizontal)
            {
                if (row + ship.size - 1 > boardSize)
                {
                    throw new IndexOutOfRangeException(errormessage1);
                }
            }
            if (shipOrientation == ShipOrientation.Vertical)
            {
                if (column + ship.size > boardSize)
                {
                    throw new IndexOutOfRangeException(errormessage1);
                }
            }
            if (shipOrientation == ShipOrientation.Horizontal)
            {
                for (int i = 0; i < ship.size ; i++)
                {
                    bool occupied = ListBoardModel.Where(x => x.Row == row+i && x.Column == column).Select(x => x.Occupied).FirstOrDefault();
                    {
                        if (occupied)
                        {
                            throw new IndexOutOfRangeException(errormessage2);

                        }
                    }
                }
            }
            if (shipOrientation == ShipOrientation.Vertical)
            {
                for (int i = 0; i < ship.size - 1; i++)
                {
                    bool occupied = ListBoardModel.Where(x => x.Row == row && x.Column == column+i).Select(x => x.Occupied).FirstOrDefault();
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
            if(row>10 || column >10)
            {
                throw new IndexOutOfRangeException("Attack cannot be out of boards!!");
            }

           var data = shipViewModel.Where(x => x.Row == row && x.Column == column).FirstOrDefault();
            if(data != null)
            {
                if(!data.Hit)
                {
                    data.Hit = true;
                    var boardData = ListBoardModel.Where(x => x.Row == row && x.Column == column).FirstOrDefault();
                    boardData.Occupied = false;

                    if (ListBoardModel.Count==ListBoardModel.Where(x => x.Occupied == false).Count())
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

       
    }
}
