//using System;
//using System.Collections.Generic;
//using System.Text;
//using WIMSystem.Models.Contracts;

//namespace WIMSystem.Models
//{
//    public class BoardsCollecton : IBoardsCollection
//    {
//        private IDictionary<string, IBoard> boardsInTheTeam;

//        public BoardsCollecton()
//        {

//        }

//        public IDictionary<string, IBoard> BoardsInTheTeam
//        {
//            get
//            {
//                var boardsToGet = this.boardsInTheTeam;
//                return boardsToGet;
//            }
//            private set
//            {
//                this.boardsInTheTeam = value;
//            }
//        }
//        public void AddBoardToTeam(string newBoardName, IBoard newBoard)
//        {
//            try
//            {
//                this.boardsInTheTeam.Add(newBoardName, newBoard);
//            }
//            catch (Exception)
//            {

//                throw new Exception("The name of the board should be unique in the team.");
//            }
//        }


//    }
//}
