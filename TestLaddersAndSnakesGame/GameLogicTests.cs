using Ladders_and_snakes_game.Game_Logic;
using Xunit;

namespace TestLaddersAndSnakesGame
{
    public class GameLogicTests
    {
        [Fact]
        public void RollDices_ResultIsBetween2And12()
        {
            GameManager gameManager = new GameManager();
            int res = gameManager.RollDices();

            Assert.InRange(res, 2, 12);
        }

        [Fact]
        public void RollDicesLoopCheck()
        {
            GameManager gameManager = new GameManager();

            for (int  i=0 ; i < 100 ;i++)
            {
                int res = gameManager.RollDices();

                Assert.InRange(res, 2, 12);
            }
        }
    }
}
