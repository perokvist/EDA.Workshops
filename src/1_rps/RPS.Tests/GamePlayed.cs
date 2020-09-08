using System;
using System.Collections.Generic;
using System.Linq;

namespace RPS.Tests
{
    public class GamePlayed : IEvent
    {
        public GamePlayed()
        {
            _highScore = new HighScoreView();
        }
        public GamePlayed When(IEvent @event)
        {
            _highScore.When((dynamic)@event);
            return this;
        }

        public GamePlayed When(RoundEnded @event)
        {
            _highScore.When(@event);
            Rounds++;
            return this;
        }
        public GamePlayed When(GameEnded @event)
        {
            _highScore.When(@event);
            Winner = _highScore.Rows.OrderBy(r => r.Rank).First().PlayerId;
            return this;
        }
        private HighScoreView _highScore;
        public Guid GameId { get; set; }
        public int Rounds { get; set; }
        public string Winner { get; set; }
        public string Looser { get; set; }
        public string SourceId => GameId.ToString();
        public IDictionary<string, string> Meta { get; set; }
    }

}
