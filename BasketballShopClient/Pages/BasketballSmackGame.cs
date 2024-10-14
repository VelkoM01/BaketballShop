using BasketballShopSharedLibrary.Models;

namespace BasketballShopClient.Pages
{
    public partial class BasketballSmackGame
    {
        int score = 0;
        int currentTime = 30;
        int hitPosition = 0;
        string message = "";
        int gameSpeed = 500;
        bool isGameRunning = false;

        public List<SquareModel> Squares { get; set; } = new List<SquareModel>();

        public BasketballSmackGame()
        {
            for(int i=0; i < 9; i++)
            {
                Squares.Add(new SquareModel { Id = i });
            }
        }
    }
}
