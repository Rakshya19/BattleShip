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
        public ShipService(IFileService fileService, IBoardService boardService, IConfiguration config)
        {
            _fileService = fileService;
            _boardService = boardService;
            _config = config;

        }
        public ShipModel CreateShip(string shipType)
        {
            try
            {
                var shipList = validateShip(shipType);


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
                    return new ShipModel() { Message = "ShipType doesnot exist. Please select either " + ShipType.carrier + " or " + ShipType.battleship + " or " + ShipType.cruiser + " or " + ShipType.destroyer + " or " + ShipType.submarine };
                }
                _fileService.SetFileContent("ship.json", JsonConvert.SerializeObject(shipList));
                return model;

            }

            catch (Exception)
            {
                return new ShipModel { Message = "Error occured while creating ship!!" };
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
                BoardShipViewModel boardShipViewModel = new BoardShipViewModel();
                var listBoardModel = _boardService.GetBoard();
                var shipList = GetShipList();
                var shipAddedList = GetPlacedShip();

                var validatedData = ValidatePlacedShip(listBoardModel, shipList, shipPlacementModel.ShipType, shipPlacementModel.shipOrientation);
                if (!string.IsNullOrEmpty(validatedData.Message))
                {
                    boardShipViewModel.Message = validatedData.Message;
                    return boardShipViewModel;
                }

                if (shipAddedList == null)
                    shipAddedList = new List<ShipViewModel>();

                var shipModel = shipList.Where(x => x.shipType.ToLower() == shipPlacementModel.ShipType.ToLower()).FirstOrDefault();

                var checkeddata = CheckBoardOccupied(shipModel, shipPlacementModel.shipOrientation, shipPlacementModel.Row, shipPlacementModel.Column, listBoardModel);
                if (!string.IsNullOrEmpty(checkeddata.Message))
                {
                    boardShipViewModel.Message = checkeddata.Message;
                    return boardShipViewModel;
                }
                List<ShipViewModel> listshipViewModel = new List<ShipViewModel>();

                if (shipPlacementModel.shipOrientation.ToLower() == Enum.GetName(typeof(ShipOrientation), ShipOrientation.vertical).ToLower())
                    listshipViewModel = PlaceShipVertically(listBoardModel, shipPlacementModel, shipModel);

                if (shipPlacementModel.shipOrientation.ToLower() == Enum.GetName(typeof(ShipOrientation), ShipOrientation.horizontal).ToLower())
                    listshipViewModel = PlaceShipHorizontally(listBoardModel, shipPlacementModel, shipModel);

                boardShipViewModel.ListBoardModel = listBoardModel.Where(x => x.Occupied == true).ToList();
                boardShipViewModel.ListShipViewModel = listshipViewModel;
                if (boardShipViewModel != null)
                {
                    shipAddedList.AddRange(boardShipViewModel.ListShipViewModel);
                    foreach (var item in boardShipViewModel.ListBoardModel)
                        listBoardModel.Where(x => x.Row == item.Row && x.Column == item.Column).FirstOrDefault(x => x.Occupied = true);
                }

                _fileService.SetFileContent("board.json", JsonConvert.SerializeObject(listBoardModel));
                _fileService.SetFileContent("shipPlacement.json", JsonConvert.SerializeObject(shipAddedList));

                return boardShipViewModel;
            }
            catch (Exception)
            {
                return new BoardShipViewModel() { Message = "Error occured while adding ship to board" };
            }
           
        }



        private ErrorModel CheckBoardOccupied(ShipModel ship, string shipOrientation, int row, int column, List<BoardModel> ListBoardModel)
        {
            var errormessage1 = "Ship cannot be placed out of boards!!";
            var errormessage2 = "Cell is already occupied. Place the ship in another position!!";

            if (ship == null)
                return new ErrorModel() { Message = "Ship Doesnot exist!!" };

            int rows = _config.GetValue<int>("Rows");
            int columns = _config.GetValue<int>("Columns");

            if (row > rows || column > columns)
                return new ErrorModel() { Message = errormessage1 };

            if (shipOrientation.ToLower() == Enum.GetName(typeof(ShipOrientation), ShipOrientation.vertical).ToLower())
            {
                if (row + ship.size - 1 > rows)
                {
                    return new ErrorModel() { Message = errormessage1 };
                }
                for (int i = 0; i < ship.size; i++)
                {
                    bool occupied = ListBoardModel.Where(x => x.Row == row + i && x.Column == column).Select(x => x.Occupied).FirstOrDefault();
                    {
                        if (occupied)
                            return new ErrorModel() { Message = errormessage2 };

                    }
                }
            }
            if (shipOrientation.ToLower() == Enum.GetName(typeof(ShipOrientation), ShipOrientation.horizontal).ToLower())
            {
                if (column + ship.size - 1 > columns)
                    return new ErrorModel() { Message = errormessage1 };

                for (int i = 0; i < ship.size - 1; i++)
                {
                    bool occupied = ListBoardModel.Where(x => x.Row == row && x.Column == column + i).Select(x => x.Occupied).FirstOrDefault();
                    {
                        if (occupied)
                            return new ErrorModel() { Message = errormessage2 };

                    }
                }

            }
            return new ErrorModel();
        }


        public ShipBoardStatusModel AttackShip(Dimensions model)
        {
            try
            {
                var returnModel = new ShipBoardStatusModel();
                var errorModel = new ErrorModel();
                returnModel.errorModel = errorModel;

                var boardList = _boardService.GetBoard();

                var shipList = GetShipList();

                var shipAddedList = GetPlacedShip();

                if (shipAddedList == null)
                    shipAddedList = new List<ShipViewModel>();

                var boardShipNumber = shipAddedList.Where(x => x.Row == model.Row & x.Column == model.Column).Select(x => x.BoardShipNumber).FirstOrDefault();

                var data = shipAddedList.Where(x => x.Row == model.Row && x.Column == model.Column).FirstOrDefault();
                if (data == null)
                    return new ShipBoardStatusModel() { shipAndBoardstatus = ShipAndBoardstatus.Miss };

                ValidateAttackShip(boardList, model, data);

                shipAddedList.Where(x => x.Row == model.Row && x.Column == model.Column).FirstOrDefault(x => x.Hit = true);
                _fileService.SetFileContent("shipPlacement.json", JsonConvert.SerializeObject(shipAddedList));

                boardList.Where(x => x.Row == model.Row && x.Column == model.Column).FirstOrDefault(x => x.Occupied = false);
                _fileService.SetFileContent("board.json", JsonConvert.SerializeObject(boardList));

                if (boardList.Where(x => x.Occupied == true).Count() == 0)
                    return new ShipBoardStatusModel() { shipAndBoardstatus = ShipAndBoardstatus.GameOver };

                else
                {
                    var shipCellCount = shipAddedList.Where(x => x.BoardShipNumber == boardShipNumber).Count();
                    var hitCount = shipAddedList.Where(x => x.BoardShipNumber == boardShipNumber && x.Hit == true).Count();

                    if (hitCount == shipCellCount)
                        return new ShipBoardStatusModel() { shipAndBoardstatus = ShipAndBoardstatus.ShipSunk };

                    else
                        return new ShipBoardStatusModel() { shipAndBoardstatus = ShipAndBoardstatus.Hit };

                }

            }
            catch (Exception)
            {
                var returnModel = new ShipBoardStatusModel();
                var errorModel = new ErrorModel();

                returnModel.errorModel = errorModel;
                errorModel.Message = "Error in attacking ships!!";
                return returnModel;

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
                shipList = new List<ShipModel>();

            if (shipList.Where(x => x.shipType.ToLower() == shipType.ToLower()).Any())
                throw new Exception("Ship already exists!");

            if (shipType == null)
                throw new Exception("ShipType cannot be empty!!");
            return shipList;
        }

        private BoardShipViewModel ValidatePlacedShip(List<BoardModel> listBoardModel, List<ShipModel> shipList, string shipType, string shipOrientation)
        {
            var boardShipViewModel = new BoardShipViewModel();
            if (listBoardModel == null)
            {
                boardShipViewModel.Message = "Board Doesnot exists!! Create board first.";
                return boardShipViewModel;
            }

            if (shipList == null)
            {
                boardShipViewModel.Message = "Ship Doesnot exists!! Create Ship first.";
                return boardShipViewModel;
            }

            if (!Enum.IsDefined(typeof(ShipType), shipType.ToLower()))
            {
                boardShipViewModel.Message = "Ship Type must be either Carrier or Battlership or Cruiser, Destroyer or Submarine!!";
                return boardShipViewModel;
            }
            if (!Enum.IsDefined(typeof(ShipOrientation), shipOrientation.ToLower()))
            {

                boardShipViewModel.Message = "Ship Orientation can only be either Horizontal or Vertical!!";
                return boardShipViewModel;

            }

            return boardShipViewModel;

        }
        private ErrorModel ValidateAttackShip(List<BoardModel> boardList, Dimensions model, ShipViewModel shipData)
        {
            int rows = _config.GetValue<int>("Rows");
            int columns = _config.GetValue<int>("Columns");

            if (model.Row > rows || model.Column > columns)
                return new ErrorModel() { Message = "Attack cannot be out of boards!!" };

            if (boardList == null)
                return new ErrorModel() { Message = "Boards must have occupied ships in them!!" };

            if (shipData.Hit)
                return new ErrorModel() { Message = "This cell is already hit before!!" };

            return new ErrorModel();
        }
        private List<ShipViewModel> PlaceShipVertically(List<BoardModel> listBoardModel, ShipPlacementRequestModel shipPlacementModel, ShipModel shipModel)
        {
            List<ShipViewModel> listshipViewModel = new List<ShipViewModel>();
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
                    listshipViewModel.Add(model);
                }
            }
            return listshipViewModel;
        }

        private List<ShipViewModel> PlaceShipHorizontally(List<BoardModel> listBoardModel, ShipPlacementRequestModel shipPlacementModel, ShipModel shipModel)
        {
            List<ShipViewModel> listshipViewModel = new List<ShipViewModel>();
            var guid = Guid.NewGuid();
            for (int i = 0; i < shipModel.size; i++)
            {
                var data = listBoardModel.Where(x => x.Row == shipPlacementModel.Row && x.Column == shipPlacementModel.Column + i).FirstOrDefault();
                {
                    data.Occupied = true;
                    ShipViewModel model = new ShipViewModel()
                    {
                        BoardShipNumber = guid,
                        Row = shipPlacementModel.Row,
                        Column = shipPlacementModel.Column + i,
                        Hit = false,
                        ShipType = shipModel.shipType
                    };
                    listshipViewModel.Add(model);
                }
            }
            return listshipViewModel;
        }
    }
}
