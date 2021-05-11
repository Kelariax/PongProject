using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PongProject
{
    class FieldController
    {
        public Image spritesSet;
        public const int fieldHeight = 60;
        public const int fieldWidth = 100;

        public int[,] field = new int[fieldHeight, fieldWidth];

        public FieldController()
        {

            spritesSet = new Bitmap("sprites.png");
        }
        public void DrawElements(Graphics graphics)
        {
            for (int i = 0; i < fieldHeight; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
                {
                    if (field[i, j] == Player.playerPadLocation)
                    {
                        graphics.DrawImage(spritesSet, new Rectangle(new Point(j * 10, i * 10), new Size(14, 112)), 69, 19, 12, 112, GraphicsUnit.Pixel);
                    }
                    if (field[i, j] == Computer.computerPadLocation)
                    {
                        graphics.DrawImage(spritesSet, new Rectangle(new Point(j * 10, i * 10), new Size(14, 112)), 19, 19, 12, 112, GraphicsUnit.Pixel);
                    }
                    if (field[i, j] == Ball.ballLocation)
                    {
                        graphics.DrawImage(spritesSet, new Rectangle(new Point(j * 10, i * 10), new Size(25, 25)), 113, 13, 25, 25, GraphicsUnit.Pixel);
                    }
                }
            }
        }
        public void DrawBorders(Graphics graphics)
        {
            graphics.DrawRectangle(Pens.HotPink, new Rectangle(0, 0, fieldWidth * 10 + 4, (fieldHeight + 1) * 10));
            graphics.DrawRectangle(Pens.HotPink, new Rectangle(1, 1, fieldWidth * 10 + 2, (fieldHeight + 1) * 10));

        }
    }
}
