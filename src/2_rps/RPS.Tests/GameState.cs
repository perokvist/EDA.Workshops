using System;

namespace RPS.Tests
{
    public class GameState
    {
        public GameState When(IEvent @event) => this;
        public GameState When(GameCreated @event)
        {
            Rounds = @event.Rounds;
            Player1 = @event.PlayerId;
            GameId = @event.GameId;

            return this;
        }

        public GameState When(GameStarted @event)
        {
            Player2 = @event.PlayerId;
            return this;
        }
        public GameState When(RoundStarted @event)
        {
            return this;
        }

        public GameState When(HandShown @event)
        {
            if (@event.PlayerId == Player1)
            {
                Player1Hand = @event.Hand;
            }
            
            if (@event.PlayerId == Player2)
            {
                Player2Hand = @event.Hand;
            }
            return this;
        }

        public Guid GameId { get; private set; }
        public int Rounds { get; private set; }
        public string Player1 { get; private set; }
        public string Player2 { get; private set; }
        public Hand Player1Hand { get; private set; }
        public Hand Player2Hand { get; private set; }

        public enum GameStatus
        {
            None = 0,
            ReadyToStart = 10,
            Started = 20,
            Ended = 50
        }
    }

}
