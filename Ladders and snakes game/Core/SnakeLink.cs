using Ladders_and_snakes_game.Players;

namespace Ladders_and_snakes_game.Core
{
    internal sealed class SnakeLink
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
        
        public void MovePlayerDown(IPlayer player)
        {
            player.Position = _tailCell.GetIndex();
        }
    }
}
