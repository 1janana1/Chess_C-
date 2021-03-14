using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Chess
{
    class Cell
    {
        // תכונות המחלקה
        private int x;
        private int y; // קואורדינטות של התא

        private int fill; // תכולת התא
        /*
            //-- שחור
                11 = מלך 
                12 = מלכה
                13 = צריח
                14 = סוס
                15 = רץ
                16 = חייל
 
            //-- לבן
                21 = מלך 
                22 = מלכה
                23 = צריח
                24 = סוס
                25 = רץ
                26 = חייל   
        */

        // פעולה בונה
        public Cell()
        {
            this.x = 0;
            this.y = 0;
            this.fill = 0;
        }

        // פעולות המחלקה
        public int GetX()
        {
            return (this.x);
        }

        public void SetX(int x)
        {
            this.x = x;
        }

        public int GetY()
        {
            return (this.y);
        }

        public void SetY(int y)
        {
            this.y = y;
        }

        public int GetFill()
        {
            return (this.fill);
        }

        public void SetFill(int fill)
        {
            this.fill = fill;
        }

        // פעולות המחלקה

        // פעולה זו מציגה את התא
        public void PrintCell(Graphics g)
        {
            Pen pen1 = new Pen(Color.Green, 7);
            g.DrawRectangle(pen1, this.x, this.y, 127, 127);
        }

        // פעולה זו משבצת דמות במשבצת...
        public void PaintPlayer(Graphics g)
        {
            Point p = new Point(this.x + 40, this.y + 10);
            Image pic;
            switch (this.fill)
            {
                //------------ שחור
                case 11:
                    pic = Image.FromFile("King11.png");
                    g.DrawImage(pic, p);
                    break;
                case 12:
                    pic = Image.FromFile("Queen12.png");
                    g.DrawImage(pic, p);
                    break;
                case 13:
                    pic = Image.FromFile("Rook13.png");
                    g.DrawImage(pic, p);
                    break;
                case 14:
                    pic = Image.FromFile("Knight14.png");
                    g.DrawImage(pic, p);
                    break;
                case 15:
                    pic = Image.FromFile("Bishop15.png");
                    g.DrawImage(pic, p);
                    break;
                case 16:
                    pic = Image.FromFile("Pawn16.png");
                    g.DrawImage(pic, p);
                    break;

                //------------- לבן
                case 21:
                    pic = Image.FromFile("King21.png");
                    g.DrawImage(pic, p);
                    break;
                case 22:
                    pic = Image.FromFile("Queen22.png");
                    g.DrawImage(pic, p);
                    break;
                case 23:
                    pic = Image.FromFile("Rook23.png");
                    g.DrawImage(pic, p);
                    break;
                case 24:
                    pic = Image.FromFile("Knight24.png");
                    g.DrawImage(pic, p);
                    break;
                case 25:
                    pic = Image.FromFile("Bishop25.png");
                    g.DrawImage(pic, p);
                    break;
                case 26:
                    pic = Image.FromFile("Pawn26.png");
                    g.DrawImage(pic, p);
                    break;
            }
        }
    }
}
