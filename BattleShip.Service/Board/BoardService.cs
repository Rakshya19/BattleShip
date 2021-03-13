using BattleShip.Model.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BattleShip.Service.Board
{
    public class BoardService : IBoardService
    {
        public List<BoardModel> CreateBoard( int rows, int columns)
        {

            try
            {

                List<BoardModel> boardModelList = new List<BoardModel>();

                for (int i = 1; i <= rows; i++)
                {
                    for (int j = 1; j <= columns; j++)
                    {
                        BoardModel boardModel = new BoardModel()
                        {
                            Row = i,
                            Column = j,
                            Occupied = false
                        };

                        boardModelList.Add(boardModel);
                    }
                }
                return boardModelList;


            }

            catch (Exception ex)
            {
                throw new Exception("Sorry!! Error has encountered while creating the board: " + ex.Message);
            }

            
        }
        public List<BoardModel> GetBoard()
        {
            try
            {
                var boardDetails = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "board.json");
                var board = System.IO.File.ReadAllText(boardDetails);
                var boardModelList = JsonConvert.DeserializeObject<List<BoardModel>>(board);
                return boardModelList;
            }
            catch (Exception ex)
            {

                throw new Exception("Error in fetching board!!"+ex.Message);
            }
            
        }
    }
}
