namespace HorseAuction.Test
{
    internal class Auction
    {
        private AuctionDbContext @object;

        public Auction(AuctionDbContext @object)
        {
            this.@object = @object;
        }

        internal void DisplayHorses()
        {
            throw new NotImplementedException();
        }

        internal void PlaceBid(int v1, string v2, int v3)
        {
            throw new NotImplementedException();
        }

        internal void ViewHorseDetails(int v)
        {
            throw new NotImplementedException();
        }
    }
}