using System.Collections.Generic;

namespace RPS.Tests
{
    public class GameState
    {
        public GameState When(IEvent @event) => this;

        public GameState When(GameCreated @event)
        {
            Status = GameStatus.ReadyToStart;
            Player1 = @event.PlayerId;
            return this;
        }
        public GameState When(GameStarted @event)
        {
            Status = GameStatus.Started;
            Player2 = @event.PlayerId;
            return this;
        }

        public GameState When(RoundEnded @event)
        {
            Rounds.Add(new GameRound { Winner = @event.Winner, Looser = @event.Looser });
            return this;
        }

        public GameState When(GameEnded @event)
        {
            Status = GameStatus.Ended;
            Rounds.Clear();
            return this;
        }

        public GameStatus Status { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public List<GameRound> Rounds { get; } = new List<GameRound>();
    }
    public class GameRound
    {
        public string Winner { get; set; }
        public string Looser { get; set; }
    }

}
