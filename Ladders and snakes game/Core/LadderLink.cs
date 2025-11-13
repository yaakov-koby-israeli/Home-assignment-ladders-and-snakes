using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladders_and_snakes_game.Players;

namespace Ladders_and_snakes_game.Core
{
    internal sealed class LadderLink
    {
        private readonly TopOrBottomCell _bottomCell;
        private readonly TopOrBottomCell _topCell;
        public LadderLink(TopOrBottomCell bottomIndex, TopOrBottomCell topIndex)
        {
            _bottomCell = bottomIndex;
            _topCell = topIndex;
        }
        public TopOrBottomCell GetBottomCell()
        {
            return _bottomCell;
        }
        public TopOrBottomCell GetTopCell()
        {
            return _topCell;
        }
        public void MovePlayerUp(IPlayer player)
        {
            player.Position = _topCell.GetIndex();
        }
    }
}
