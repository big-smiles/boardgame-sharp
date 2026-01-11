using boardgames_sharp;
using boardgames_sharp.Phases;
using boardgames_sharp.Player;
using examples;
using examples.Phases;

Console.WriteLine("Tic-Tac-Toe!");

var players = new HashSet<PlayerId>(){Constants.Player1,Constants.Player2};
var initializePhase = new InitializeGamePhase();
var player1Turn = new PlayerTurnPhase(Constants.ValuePlayer1);
var player2Turn = new PlayerTurnPhase(Constants.ValuePlayer2);
var playerTurnsPhase = new PhaseGroup([player1Turn, player2Turn], true);
var gamePhase = new PhaseGroup([initializePhase, playerTurnsPhase], false);
var engine = new Engine(players,gamePhase);
var board = new Board();
var input = new Input(board);
var observer = new StateObserver(board, input, engine);


engine.GameStateObservable.Subscribe(observer);
engine.Start();
