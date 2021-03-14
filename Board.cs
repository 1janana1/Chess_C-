using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace Chess
{
    class Board
    {
        // תכונות המחלקה
        private Cell[,] board;


        // פעולה בונה
        public Board()
        {
            // מיקום הלוח על המסך
            int x = 300;
            int y = 3;

            this.board = new Cell[8, 8];
            for (int i = 0; i < this.board.GetLength(0); i++)
            {
                for (int j = 0; j < this.board.GetLength(1); j++)
                {
                    Cell c = new Cell();
                    // מיקום התא במסך
                    c.SetX(x);
                    c.SetY(y);
                    this.board[i, j] = c;
                    x += 127;
                }
                x = 300;
                y += 127;
            }
            DefaultPosition();
        }

        // פעולות המחלקה
        public Cell[,] GetBoard()
        {
            return (this.board);
        }

        //הפעולה מקבלת משתנה גרפי ומציירת את הלוח.
        public void PrintBoard(Graphics g)
        {
            for (int i = 0; i < this.board.GetLength(0); i++)
            {
                for (int j = 0; j < this.board.GetLength(1); j++)
                {
                    if (this.board[i, j].GetFill() != 0)
                    {
                        this.board[i, j].PaintPlayer(g);
                    }
                    // מצייר תתא
                    //this.board[i, j].PrintCell(g);
                }
            }
        }

        // פעולה זו ממקמת את החיילים במקום הדיפולט שלהם
        public void DefaultPosition()
        {
            // שחור
            this.board[7, 0].SetFill(13); // צריח שחור- שמאל
            this.board[7, 1].SetFill(14); // סוס שחור - שמאל
            this.board[7, 2].SetFill(15); // רץ שחור - שמאל
            this.board[7, 3].SetFill(12); // מלכה
            this.board[7, 4].SetFill(11); // מלך
            this.board[7, 5].SetFill(15); // רץ שחור - ימין
            this.board[7, 6].SetFill(14); // סוס שחור - ימין
            this.board[7, 7].SetFill(13); // צריח שחור- ימין

            // חיילים - שחורים
            this.board[6, 0].SetFill(16);
            this.board[6, 1].SetFill(16);
            this.board[6, 2].SetFill(16);
            this.board[6, 3].SetFill(16);
            this.board[6, 4].SetFill(16);
            this.board[6, 5].SetFill(16);
            this.board[6, 6].SetFill(16);
            this.board[6, 7].SetFill(16);

            // לבן
            this.board[0, 0].SetFill(23); // צריח לבן- שמאל
            this.board[0, 1].SetFill(24); // סוס לבן - שמאל
            this.board[0, 2].SetFill(25); // רץ לבן - שמאל
            this.board[0, 3].SetFill(22); // מלכה
            this.board[0, 4].SetFill(21); // מלך
            this.board[0, 5].SetFill(25); // רץ לבן - ימין
            this.board[0, 6].SetFill(24); // סוס לבן - ימין
            this.board[0, 7].SetFill(23); // צריח לבן - ימין

            // חיילים - לבנים
            this.board[1, 0].SetFill(26);
            this.board[1, 1].SetFill(26);
            this.board[1, 2].SetFill(26);
            this.board[1, 3].SetFill(26);
            this.board[1, 4].SetFill(26);
            this.board[1, 5].SetFill(26);
            this.board[1, 6].SetFill(26);
            this.board[1, 7].SetFill(26);
        }
    }
}
