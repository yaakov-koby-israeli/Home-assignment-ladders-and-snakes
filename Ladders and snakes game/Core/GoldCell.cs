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
    internal class GoldCell : Cell
    {
        public GoldCell(int index, enumCellType cellType) : base(index, cellType){ }

        public int GetIndex()
        {
            return _index;
        }

        public override enumCellType GetCellType()
        {
            return _cellType;
        }

        public void OnLand(IPlayer firstPlayer, IPlayer luckyPlayer)
        {
            //modern C# feature to simplify this operation using tuple deconstruction.
            (firstPlayer.Position, luckyPlayer.Position) = (luckyPlayer.Position, firstPlayer.Position);

            //// my code before using tuple deconstruction
            //int pos = firstPlayer.Position;
            //firstPlayer.Position = luckyPlayer.Position;
            //luckyPlayer.Position = pos;
        }
    }
}
