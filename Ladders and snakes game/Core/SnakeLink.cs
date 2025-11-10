using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladders_and_snakes_game.Players;

namespace Ladders_and_snakes_game.Core
{
    internal class SnakeLink
    {
        private readonly TopOrBottomCell _headCell;
        private readonly TopOrBottomCell _tailCell;
        public SnakeLink(TopOrBottomCell headIndex, TopOrBottomCell tailIndex)
        {
            this._headCell = headIndex;
            this._tailCell = tailIndex;
        }
        public TopOrBottomCell GetHeadCell()
        {
            return _headCell;
        }
        public TopOrBottomCell GetTailCell()
        {
            return _tailCell;
        }

        public void MovePlayerDown(Player player)
        {
            player.Position = _tailCell.GetIndex();
        }
        
    }
}
