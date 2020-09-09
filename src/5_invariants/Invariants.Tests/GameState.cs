using System;

namespace Invariants.Tests
{
    public class GameState
    {
        public GameState When(IEvent @event) => this;

        public GameState When(GameCreated @event)
        {
            GameId = @event.GameId;
            Player1 = @event.PlayerId;

            return this;
        }

        public Guid GameId { get; private set; }
        public string Player1 { get; set; }
    }

}
