using System;
using System.Collections.Generic;
using System.Text;

namespace RPS.Tests
{
    public static class Game
    {
        public static IEnumerable<IEvent> Handle(CreateGame command, GameState state)
            => new List<IEvent>
            {
                new GameCreated
                {
                    Created = DateTime.UtcNow,
                    GameId = command.GameId,
                    PlayerId = command.PlayerId,
                    Rounds = command.Rounds,
                    Title = command.Title
                }
            };


        public static IEnumerable<IEvent> Handle(JoinGame command, GameState state)
        {
            if (command.PlayerId == state.Player1)
            {
                return Array.Empty<IEvent>();
            }
            var gameStarted = new GameStarted
            {
                GameId = command.GameId,
                PlayerId = command.PlayerId,
            };
            var roundStarted = new RoundStarted
            {
                GameId = command.GameId,
                Round = 1
            };
            return new List<IEvent> { gameStarted, roundStarted };
        }

        public static IEnumerable<IEvent> Handle(PlayGame command, GameState state)
        {
            var handShown = new HandShown
            {
                GameId = command.GameId,
                Hand = command.Hand,
                PlayerId = command.PlayerId
            };
            if (state.Player1Hand == Hand.None && state.Player2Hand == Hand.None)
            {
                return new List<IEvent> { handShown };
            }

            Hand hand1 = Hand.None;
            Hand hand2 = Hand.None;

            if (command.PlayerId == state.Player1)
            {
                hand1 = command.Hand;
                hand2 = state.Player2Hand;
            }

            if (command.PlayerId == state.Player2)
            {
                hand1 = state.Player1Hand;
                hand2 = command.Hand;
            }


            var roundEnded = new RoundEnded
            {
                GameId = command.GameId,
                Winner = (hand1, hand2) switch
                {
                    (Hand.Paper, Hand.Rock) => state.Player1,
                    (Hand.Rock, Hand.Paper) => state.Player2,
                    (_, _) => throw new InvalidOperationException()
                }
            };

            var gamedEnded = new GameEnded { GameId = command.GameId };

            return new List<IEvent> { handShown, roundEnded, gamedEnded };

        }
    }
}
