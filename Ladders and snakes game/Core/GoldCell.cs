using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Ladders_and_snakes_game.Players;

namespace Ladders_and_snakes_game.Core
{
    internal sealed class GoldCell : Cell
    {
        public GoldCell(int index, enumCellType cellType) : base(index, cellType){ }

        public void OnLand(IPlayer firstPlayer, IPlayer luckyPlayer)
        {
            (firstPlayer.Position, luckyPlayer.Position) = (luckyPlayer.Position, firstPlayer.Position);
        }
    }
}
