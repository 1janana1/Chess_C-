using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Unit4.CollectionsLib;

namespace Chess
{
    public partial class Game : Form
    {
        Graphics g;
        Board board = new Board();

        Unit4.CollectionsLib.Stack<Cell> whereToMove = new Unit4.CollectionsLib.Stack<Cell>();

        int howHeMoves = 0; // 
        bool click = true;

        int t;
        // אם זה 0 אז שחקן 1
        // אם זה 1 אז שחקן  2 || המחשב

        int soldier = 0, iS = 0, jS = 0;

        public Game()
        {
            InitializeComponent();
        }
      
        private void Game_Load(object sender, EventArgs e)
        {
            board = new Board();
        }

        private void Game_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            Image pic = Image.FromFile("Chess_Board.png");
            Point p = new Point(300, 3);
            g.DrawImage(pic, p);
            board.PrintBoard(g);

            //MoveSoldier(g);// פעולה את ההאפשרויות התקדמות עלפי אותו חייל שלחצנו עליו
        }

        //ואיפה הוא נמצא פעולה שמחזירה על איזה חייל לחצנו
        private void Game_MouseClick(object sender, MouseEventArgs e)
        {
            Refresh();
            g = CreateGraphics();
            
            for(int i = 0; i < board.GetBoard().GetLength(0); i++)
            {
                for (int j = 0; j < board.GetBoard().GetLength(1); j++)
                {
                    if (e.X > board.GetBoard()[i, j].GetX() && e.X < board.GetBoard()[i, j].GetX() + 127 && e.Y > board.GetBoard()[i, j].GetY() && e.Y < board.GetBoard()[i, j].GetY() + 127)
                    {
                        // מזיז את החייל אם השחקן הזיז את השחקן
                        if (!whereToMove.IsEmpty() && FindCellStack(e))
                        {
                            int fill = board.GetBoard()[iS, jS].GetFill();

                            board.GetBoard()[iS, jS].SetFill(0);
                            board.GetBoard()[i, j].SetFill(fill);
                            board.GetBoard()[i, j].PaintPlayer(g);
                            ClearStack();
                            Refresh();
                            return;
                        }
                        // מראה את כל האשרויות שהחייל יכול לזוז ואיזה חייל
                        else if(board.GetBoard()[i, j].GetFill() != 0)
                        {
                            ClearStack();
                            iS = i;
                            jS = j;

                            soldier = board.GetBoard()[i, j].GetFill();
                            click = false;

                            MoveSoldier(g);// פעולה את ההאפשרויות התקדמות עלפי אותו חייe.ל שלחצנו עליו
                            return;
                        }
                        if (!whereToMove.IsEmpty())
                            whereToMove.Pop();
                    }
                }
            }
        }

        // פעולה שמציגה את האפשרויות של השחקן להתקדם עם החייל. מעבירה משתנה של אישור לחיצה
        public void MoveSoldier(Graphics g)
        {
            switch (soldier)
            {
                case 11:
                    Around(g, 1);
                    break;
                case 12:
                    // Up
                    Straight(g, 1, 0, 1, "Up");
                    //Down
                    Straight(g, -1, 0, 1, "Down");
                    //Left
                    Straight(g, 0, -1, 1, "Left");
                    //Right
                    Straight(g, 0, 1, 1, "Right");

                    // שמאל למעלה
                    Diagonals(g, -1, -1, 1, "leftUp");
                    // שמאל למטה
                    Diagonals(g, 1, -1, 1, "leftDown");

                    // ימין למעלה
                    Diagonals(g, -1, 1, 1, "rightUp");
                    // ימין למטה
                    Diagonals(g, 1, 1, 1, "rightDown");

                    Around(g, 1);
                    break;
                case 13:
                    // Up
                    Straight(g, 1, 0, 1, "Up");
                    //Down
                    Straight(g, -1, 0, 1, "Down");
                    //Left
                    Straight(g, 0, -1, 1, "Left");
                    //Right
                    Straight(g, 0, 1, 1, "Right");
                    break;
                case 14:
                    if (iS - 2 > -1 && jS - 1 > -1 && board.GetBoard()[iS - 2, jS - 1].GetFill() / 10 != 1)
                    {
                        // למעלה - שמאלה
                        board.GetBoard()[iS - 2, jS - 1].PrintCell(g);
                        whereToMove.Push(board.GetBoard()[iS - 2, jS - 1]);// הכנסה למחסנית
                    }
                    if (iS - 2 > -1 && jS + 1 < 8 && board.GetBoard()[iS - 2, jS + 1].GetFill() / 10 != 1)
                    {
                        // למעלה - ימינה
                        board.GetBoard()[iS - 2, jS + 1].PrintCell(g);
                        whereToMove.Push(board.GetBoard()[iS - 2, jS + 1]);// הכנסה למחסנית
                    }
                    //----------------------------------------------------------------------------
                    if (iS + 2 < 8 && jS - 1 > -1 && board.GetBoard()[iS + 2, jS - 1].GetFill() / 10 != 1)
                    {
                        // למטה - שמאלה
                        board.GetBoard()[iS + 2, jS - 1].PrintCell(g);
                        whereToMove.Push(board.GetBoard()[iS + 2, jS - 1]);// הכנסה למחסנית
                    }
                    if (iS + 2 < 8 && jS + 1 < 8 && board.GetBoard()[iS + 2, jS + 1].GetFill() / 10 != 1)
                    {
                        // למעלה - ימין
                        board.GetBoard()[iS + 2, jS + 1].PrintCell(g);
                        whereToMove.Push(board.GetBoard()[iS + 2, jS + 1]);// הכנסה למחסנית
                    }
                    //----------------------------------------------------------------------------
                    if (iS - 1 > -1 && jS - 2 > -1 && board.GetBoard()[iS - 1, jS - 2].GetFill() / 10 != 1)
                    {
                        // שמאל - למעלה
                        board.GetBoard()[iS - 1, jS - 2].PrintCell(g);
                        whereToMove.Push(board.GetBoard()[iS - 1, jS - 2]);// הכנסה למחסנית
                    }
                    if (iS + 1 < 8 && jS - 2 > -1 && board.GetBoard()[iS + 1, jS - 2].GetFill() / 10 != 1)
                    {
                        // שמאל - למטה
                        board.GetBoard()[iS + 1, jS - 2].PrintCell(g);
                        whereToMove.Push(board.GetBoard()[iS + 1, jS - 2]);// הכנסה למחסנית
                    }
                    //----------------------------------------------------------------------------
                    // ימין - למעלה
                    if (iS - 1 > -1 && jS + 2 < 8 && board.GetBoard()[iS - 1, jS + 2].GetFill() / 10 != 1)
                    {
                        board.GetBoard()[iS - 1, jS + 2].PrintCell(g);
                        whereToMove.Push(board.GetBoard()[iS - 1, jS + 2]);// הכנסה למחסנית
                    }
                    // ימין - למטה
                    if (iS + 1 < 8 && jS + 2 < 8 && board.GetBoard()[iS + 1, jS + 2].GetFill() / 10 != 1)
                    {
                        board.GetBoard()[iS + 1, jS + 2].PrintCell(g);
                        whereToMove.Push(board.GetBoard()[iS + 1, jS + 2]);// הכנסה למחסנית
                    }
                    break;
                case 15:
                    // שמאל למעלה
                    Diagonals(g, -1, -1, 1, "leftUp");
                    // שמאל למטה
                    Diagonals(g, 1, -1, 1, "leftDown");

                    // ימין למעלה
                    Diagonals(g, -1, 1, 1, "rightUp");
                    // ימין למטה
                    Diagonals(g, 1, 1, 1, "rightDown");
                    break;
                case 16:
                    // משבצת התחלתית
                    if (iS - 1 != -1 && board.GetBoard()[iS - 1, jS].GetFill() == 0)
                    {
                        board.GetBoard()[iS - 1, jS].PrintCell(g);
                        whereToMove.Push(board.GetBoard()[iS - 1, jS]);// הכנסה למחסנית

                        if (board.GetBoard()[iS - 2, jS].GetFill() == 0 && iS == 6)
                        {
                            board.GetBoard()[iS - 2, jS].PrintCell(g);
                            whereToMove.Push(board.GetBoard()[iS - 2, jS]);// הכנסה למחסנית
                        }
                            
                    }
                    // אכילה ימינה
                    if (iS - 1 != -1 && jS + 1 != 8 && board.GetBoard()[iS - 1, jS + 1].GetFill() != 0 && board.GetBoard()[iS - 1, jS + 1].GetFill() / 10 != 1)
                    {
                        board.GetBoard()[iS - 1, jS + 1].PrintCell(g);
                        whereToMove.Push(board.GetBoard()[iS - 1, jS + 1]);// הכנסה למחסנית
                    }
                    // אכילה שמאלה
                    if (iS - 1 != -1 && jS - 1 != -1 && board.GetBoard()[iS - 1, jS - 1].GetFill() != 0 && iS > 0 && jS > 0 && board.GetBoard()[iS - 1, jS - 1].GetFill() / 10 != 1)
                    {
                        board.GetBoard()[iS - 1, jS - 1].PrintCell(g);
                        whereToMove.Push(board.GetBoard()[iS - 1, jS - 1]);// הכנסה למחסנית
                    }
                    break;

                //------------- לבן
                case 21:
                    Around(g, 2);
                    break;
                case 22:
                    
                    break;
                case 23:
                    // Up
                    Straight(g, 1, 0, 2, "Up");
                    //Down
                    Straight(g, -1, 0, 2, "Down");
                    //Left
                    Straight(g, 0, -1, 2, "Left");
                    //Right
                    Straight(g, 0, 1, 2, "Right");
                    break;
                case 24:
                    if (iS - 2 > -1 && jS - 1 > -1 && board.GetBoard()[iS - 2, jS - 1].GetFill() / 10 != 2)
                    {
                        // למעלה - שמאלה
                        board.GetBoard()[iS - 2, jS - 1].PrintCell(g);
                        whereToMove.Push(board.GetBoard()[iS - 2, jS - 1]);// הכנסה למחסנית
                    }
                    if (iS - 2 > -1 && jS + 1 < 8 && board.GetBoard()[iS - 2, jS + 1].GetFill() / 10 != 2)
                    {
                        // למעלה - ימינה
                        board.GetBoard()[iS - 2, jS + 1].PrintCell(g);
                        whereToMove.Push(board.GetBoard()[iS - 2, jS + 1]);// הכנסה למחסנית
                    }
                    //----------------------------------------------------------------------------
                    if (iS + 2 < 8 && jS - 1 > -1 && board.GetBoard()[iS + 2, jS - 1].GetFill() / 10 != 2)
                    {
                        // למטה - שמאלה
                        board.GetBoard()[iS + 2, jS - 1].PrintCell(g);
                        whereToMove.Push(board.GetBoard()[iS + 2, jS - 1]);// הכנסה למחסנית
                    }
                    if (iS + 2 < 8 && jS + 1 < 8 && board.GetBoard()[iS + 2, jS + 1].GetFill() / 10 != 2)
                    {
                        // למעלה - ימין
                        board.GetBoard()[iS + 2, jS + 1].PrintCell(g);
                        whereToMove.Push(board.GetBoard()[iS + 2, jS + 1]);// הכנסה למחסנית
                    }
                    //----------------------------------------------------------------------------
                    if (iS - 1 > -1 && jS - 2 > -1 && board.GetBoard()[iS - 1, jS - 2].GetFill() / 10 != 2)
                    {
                        // שמאל - למעלה
                        board.GetBoard()[iS - 1, jS - 2].PrintCell(g);
                        whereToMove.Push(board.GetBoard()[iS - 1, jS - 2]);// הכנסה למחסנית
                    }
                    if (iS + 1 < 8 && jS - 2 > -1 && board.GetBoard()[iS + 1, jS - 2].GetFill() / 10 != 2)
                    {
                        // שמאל - למטה
                        board.GetBoard()[iS + 1, jS - 2].PrintCell(g);
                        whereToMove.Push(board.GetBoard()[iS + 1, jS - 2]);// הכנסה למחסנית
                    }
                    //----------------------------------------------------------------------------
                    // ימין - למעלה
                    if (iS - 1 > -1 && jS + 2 < 8 && board.GetBoard()[iS - 1, jS + 2].GetFill() / 10 != 2)
                    {
                        board.GetBoard()[iS - 1, jS + 2].PrintCell(g);
                        whereToMove.Push(board.GetBoard()[iS - 1, jS + 2]);// הכנסה למחסנית
                    }
                    // ימין - למטה
                    if (iS + 1 < 8 && jS + 2 < 8 && board.GetBoard()[iS + 1, jS + 2].GetFill() / 10 != 2)
                    {
                        board.GetBoard()[iS + 1, jS + 2].PrintCell(g);
                        whereToMove.Push(board.GetBoard()[iS + 1, jS + 2]);// הכנסה למחסנית
                    }
                    break;
                case 25:
                    // שמאל למעלה
                    Diagonals(g, -1, -1, 2, "leftUp");
                    // שמאל למטה
                    Diagonals(g, 1, -1, 2, "leftDown");

                    // ימין למעלה
                    Diagonals(g, -1, 1, 2, "rightUp");
                    // ימין למטה
                    Diagonals(g, 1, 1, 2, "rightDown");
                    break;
                case 26:
                    // כשהוא מגיע לסוף יש אפשרות להפוך לכל שחקן
                    // משבצת התחלתית
                    if (iS + 1 != 8 && board.GetBoard()[iS + 1, jS].GetFill() == 0)
                    {
                        board.GetBoard()[iS + 1, jS].PrintCell(g);
                        whereToMove.Push(board.GetBoard()[iS + 1, jS]);// הכנסה למחסנית

                        if (board.GetBoard()[iS + 2, jS].GetFill() == 0 && iS == 1)
                        {
                            board.GetBoard()[iS + 2, jS].PrintCell(g);
                            whereToMove.Push(board.GetBoard()[iS + 2, jS]);// הכנסה למחסנית
                        }
                    }
                    // אכילה ימינה
                    if (iS + 1 != 8 && jS + 1 != 8 && board.GetBoard()[iS + 1, jS + 1].GetFill() != 0 && board.GetBoard()[iS + 1, jS + 1].GetFill() / 10 != 2)
                    {
                        board.GetBoard()[iS + 1, jS + 1].PrintCell(g);
                        whereToMove.Push(board.GetBoard()[iS + 1, jS + 1]);// הכנסה למחסנית
                    }
                    // אכילה שמאלה
                    if (iS + 1 != 8 && jS - 1 != -1 && board.GetBoard()[iS + 1, jS - 1].GetFill() != 0 && board.GetBoard()[iS + 1, jS - 1].GetFill() / 10 != 2)
                    {
                        board.GetBoard()[iS + 1, jS - 1].PrintCell(g);
                        whereToMove.Push(board.GetBoard()[iS + 1, jS - 1]);// הכנסה למחסנית
                    }
                    break;
            }
        }

        // פעולה זו מרוקנת את המחסנית
        public void ClearStack()
        {
            if (!whereToMove.IsEmpty())
            {
                while (!whereToMove.IsEmpty())
                {
                    whereToMove.Pop();
                }
            }
        }

        // פעולה זו מוצאת את התא שלחצנו עליו במחסנית
        public bool FindCellStack(MouseEventArgs e)
        {
            while (!whereToMove.IsEmpty())
            {
                if (e.X > whereToMove.Top().GetX() && e.X < whereToMove.Top().GetX() + 127 && e.Y > whereToMove.Top().GetY() && e.Y < whereToMove.Top().GetY() + 127)
                    return true;
                whereToMove.Pop();
            }
            return false;
        }

        //פעולה שבודקת אלכסונים
        public void Diagonals(Graphics g, int numi, int numj, int colorSoldier, string direction)
        {
            int i = iS;
            int j = jS;
            while ((direction == "leftUp" && i + numi != -1 && j + numj != -1) || (direction == "leftDown" && i + numi != 8 && j + numj != -1) || (direction == "rightUp" && i + numi != -1 && j + numj != 8) || (direction == "rightDown" && i + numi != 8 && j + numj != 8))
            {
                if (board.GetBoard()[i + numi, j + numj].GetFill() / 10 == colorSoldier)
                {
                    return;
                }
                else if (board.GetBoard()[i + numi, j + numj].GetFill() != 0)
                {
                    board.GetBoard()[i + numi, j + numj].PrintCell(g);
                    whereToMove.Push(board.GetBoard()[i + numi, j + numj]);// הכנסה למחסנית
                    return;
                }
                else
                {
                    board.GetBoard()[i + numi, j + numj].PrintCell(g);
                    whereToMove.Push(board.GetBoard()[i + numi, j + numj]);// הכנסה למחסנית
                }
                i += numi;
                j += numj;
            }
        }

        // פעולה שבודקת מסביב לשחקן 
        public void Around(Graphics g, int colorSoldier)
        {
            if (iS - 1 != -1 && (board.GetBoard()[iS - 1, jS].GetFill() == 0 || board.GetBoard()[iS - 1, jS].GetFill() / 10 != colorSoldier))// למעלה
            {
                board.GetBoard()[iS - 1, jS].PrintCell(g);
                whereToMove.Push(board.GetBoard()[iS - 1, jS]);// הכנסה למחסנית
            }
            if (iS + 1 != 8 && (board.GetBoard()[iS + 1, jS].GetFill() == 0 || board.GetBoard()[iS + 1, jS].GetFill() / 10 != colorSoldier))// למטה
            {
                board.GetBoard()[iS + 1, jS].PrintCell(g);
                whereToMove.Push(board.GetBoard()[iS + 1, jS]);// הכנסה למחסנית
            }


            if (jS - 1 != -1 && (board.GetBoard()[iS, jS - 1].GetFill() == 0 || board.GetBoard()[iS, jS - 1].GetFill() / 10 != colorSoldier))// שמאל
            {
                board.GetBoard()[iS, jS - 1].PrintCell(g);
                whereToMove.Push(board.GetBoard()[iS, jS - 1]);// הכנסה למחסנית
            }
            if (jS + 1 != 8 && (board.GetBoard()[iS, jS + 1].GetFill() == 0 || board.GetBoard()[iS, jS + 1].GetFill() / 10 != colorSoldier))// ימין
            {
                board.GetBoard()[iS, jS + 1].PrintCell(g);
                whereToMove.Push(board.GetBoard()[iS, jS + 1]);// הכנסה למחסנית
            }


            if (iS + 1 != 8 && jS - 1 != -1 && (board.GetBoard()[iS + 1, jS - 1].GetFill() == 0 || board.GetBoard()[iS + 1, jS - 1].GetFill() / 10 != colorSoldier))// אלכסון שמאלה למטה
            {
                board.GetBoard()[iS + 1, jS - 1].PrintCell(g);
                whereToMove.Push(board.GetBoard()[iS + 1, jS - 1]);// הכנסה למחסנית
            }
            if (iS - 1 != -1 && jS - 1 != -1 && (board.GetBoard()[iS - 1, jS - 1].GetFill() == 0 || board.GetBoard()[iS - 1, jS - 1].GetFill() / 10 != colorSoldier))// אלכסון שמאלה למעלה
            {
                board.GetBoard()[iS - 1, jS - 1].PrintCell(g);
                whereToMove.Push(board.GetBoard()[iS - 1, jS - 1]);// הכנסה למחסנית
            }
            if (iS + 1 != 8 && jS + 1 != 8 && (board.GetBoard()[iS + 1, jS + 1].GetFill() == 0 || board.GetBoard()[iS + 1, jS + 1].GetFill() / 10 != colorSoldier))// אלכסון ימין למטה
            {
                board.GetBoard()[iS + 1, jS + 1].PrintCell(g);
                whereToMove.Push(board.GetBoard()[iS + 1, jS + 1]);// הכנסה למחסנית
            }
            if (iS - 1 != -1 && jS + 1 != 8 && (board.GetBoard()[iS - 1, jS + 1].GetFill() == 0 || board.GetBoard()[iS - 1, jS + 1].GetFill() / 10 != colorSoldier))// אלכסון ימין למעלה
            {
                board.GetBoard()[iS - 1, jS + 1].PrintCell(g);
                whereToMove.Push(board.GetBoard()[iS - 1, jS + 1]);// הכנסה למחסנית
            }
        }

        // פעולה שבודקת קוים ישרים
        public void Straight(Graphics g, int numi, int numj, int colorSoldier, string direction)
        {
            int i = iS;
            int j = jS;
            while ((direction == "Up" && i + numi < 8) || (direction == "Down" && i + numi > -1) || (direction == "Left" && j + numj > -1) || (direction == "Right" && j + numj < 8))
            {
                if (board.GetBoard()[i + numi, j + numj].GetFill() / 10 == colorSoldier)
                {
                    return;
                }
                else if (board.GetBoard()[i + numi, j + numj].GetFill() != 0)
                {
                    board.GetBoard()[i + numi, j + numj].PrintCell(g);
                    whereToMove.Push(board.GetBoard()[i + numi, j + numj]);// הכנסה למחסנית
                    return;
                }
                else
                {
                    board.GetBoard()[i + numi, j + numj].PrintCell(g);
                    whereToMove.Push(board.GetBoard()[i + numi, j + numj]);// הכנסה למחסנית
                }
                i += numi;
                j += numj;
            }
        }
    }
}
