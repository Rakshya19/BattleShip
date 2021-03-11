using BattleShip.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Service.Board
{
    public class BoardService : IBoardService
    {

        public List<BoardModel> CreateBoard()
        {

            try
            {
                //Build board with 10 rows and 10 column. 
                //Can also be made dynamic by passing rows and columns as parameter to CreateBoard method if required. 
                //Since the task was to make  10 rows and 10 columns, I made it static 

                int rows = 10;
                int columns = 10;

                List<BoardModel> boardModelList = new List<BoardModel>();

                for (int i = 1; i <= rows; i++)
                {
                    for (int j = 1; j <= columns; j++)
                    {
                        BoardModel boardModel = new BoardModel();
                        boardModel.Row = i;
                        boardModel.Column = j;
                        boardModel.Occupied = false;

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
    }
}
