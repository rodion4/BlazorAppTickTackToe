﻿namespace BlazorAppTickTackToe.Domain
{
    public enum CellState
    {
        Blank, X, O
    }

    public enum Gamer
    {
        X, O
    }

    public enum GameResult
    {
        WonX, 
        WonO,
        Unknown,
        NoWinner,

    }

    public class Board
    {
        public int ColumnCount => 3;
        public int RowCount => 3;

        public CellState[,] Cells { get; set; }
        public Gamer CurrentGamer { get; set; } = Gamer.X;

        public Board()
        {
            Cells = new CellState[RowCount, ColumnCount];
        }

        /// <summary>
        /// Сделать следующий ход
        /// </summary>
        public void NextTurn(int row, int column)
        {
            var gameResult = GetGameResult(out _);
            if (gameResult == GameResult.Unknown) 
            {
                if (Cells[row, column] == CellState.Blank)
                {
                    if (CurrentGamer == Gamer.X)
                    {
                        Cells[row, column] = CellState.X;
                        SwitchGamer();

                    }
                    else
                    {
                        Cells[row, column] = CellState.O;
                        CurrentGamer = Gamer.X;
                    }
                }
            }
            
        }

        private void SwitchGamer()
        {
            //Тернарный оператор
            CurrentGamer = CurrentGamer == Gamer.X ? Gamer.O : Gamer.X;
        }



        public GameResult GetGameResult(out CellPosition[] winCells)
        {
           
            if (CheckWin(Gamer.X, out winCells))
            { 
                return GameResult.WonX;
            }
            else if (CheckWin(Gamer.O, out winCells))
            {
                return GameResult.WonO;
            }
           /* else if(CheckNoWinner())
            {
                return GameResult.NoWinner;
            }*/
            else
            {
                return GameResult.Unknown;
            }
        }

        

        private bool CheckWin(Gamer gamer, out CellPosition[] winCells)
        {
            CellState expectedCellState;

            if (gamer == Gamer.X) 
                expectedCellState = CellState.X;
            else 
                expectedCellState = CellState.O;

            for (int row = 0; row < RowCount; row++)
            {
                var expectedCellsCount = 0;
                for (int column = 0; column < ColumnCount; column++)
                {
                    if (Cells[row, column] == expectedCellState)
                    {
                        expectedCellsCount++;
                    }
                }
                if (expectedCellsCount == 3)
                {
                    winCells = new CellPosition[]
                    {
                        new CellPosition(Row: row, Column: 0),  
                        new CellPosition(Row: row, Column: 1),
                        new CellPosition(Row: row, Column: 2),
                    };
                    return true;
                }

            }
            winCells = new CellPosition[0];
            return false;
        }
    }

    public record CellPosition(int Row, int Column);
}
