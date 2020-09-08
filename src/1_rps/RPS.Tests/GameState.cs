namespace RPS.Tests
{
    public class GameState
    {
        public GameState When(IEvent @event) => this;

        public GameState When(GameCreated @event) => new GameState { Status = GameStatus.ReadyToStart };
        public GameState When(GameStarted @event) => new GameState { Status = GameStatus.Started };
        public GameState When(GameEnded @event) => new GameState { Status = GameStatus.Ended };

        public GameStatus Status { get; set; }
    }

}
