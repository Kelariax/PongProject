using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongProject
{
    class Computer
    {
        public const int computerPadLocation = 2;
        public const int computerPadBox = 22;

        public int computerPadX;
        public int computerPadY;

        public const int computerPadWidth = 10;

        public void computerAI(FieldController field, Ball ball)
        {
            field.field[computerPadY, computerPadX] = 0;
            for (int i = 1; i < computerPadWidth - 1; i++)
            {
                field.field[computerPadY + i, computerPadX] = 0;
            }
            if (ball.ballLocationY < computerPadY && computerPadY > 0)
            {
                computerPadY--;
            }
            else if (ball.ballLocationY > computerPadY && computerPadY + computerPadWidth < FieldController.fieldHeight)
            {
                computerPadY++;
            }
            field.field[computerPadY, computerPadX] = computerPadLocation;
            for (int i = 1; i < computerPadWidth - 1; i++)
            {
                field.field[computerPadY + i, computerPadX] = computerPadBox;
            }
        }
    }
}
