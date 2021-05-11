using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongProject
{
    class ControllerPhysics
    {
         public bool isCollide(FieldController field, Ball ball, Player player)
        {
            bool isColliding = false;
            if (ball.ballLocationY + ball.ballDirectionY > FieldController.fieldHeight - 1 || ball.ballLocationY + ball.ballDirectionY < 0)
            {
                ball.ballDirectionY *= -1;
                isColliding = true;
            }
            if (field.field[ball.ballLocationY, ball.ballLocationX + ball.ballDirectionX] != 0)
            {
                if(field.field[ball.ballLocationY, ball.ballLocationX + ball.ballDirectionX] == Player.playerPadBox || field.field[ball.ballLocationY, ball.ballLocationX + ball.ballDirectionX] == Player.playerPadLocation)
                {
                    player.score += 1;
                }
                ball.ballDirectionX *= -1;
                isColliding = true;
            }
            if (field.field[ball.ballLocationY + ball.ballDirectionY, ball.ballLocationX] != 0)
            {
                if (field.field[ball.ballLocationY, ball.ballLocationX + ball.ballDirectionX] == Player.playerPadBox || field.field[ball.ballLocationY, ball.ballLocationX + ball.ballDirectionX] == Player.playerPadLocation)
                {
                    player.score += 1;
                }
                ball.ballDirectionY *= -1;
                isColliding = true;
            }           
            return isColliding;
        }
    }
}
