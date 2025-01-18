using System;
using System.Drawing;
using System.Text;
using System.Collections.Generic;
using ConsoleApp5;

class Program
{
    public static void DrawBoard(Point chessman, Point[] possibleMoves)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                var currentPoint = new Point(j + 1, i + 1);
                if (chessman == currentPoint)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                }
                else
                {
                    bool isPossibleMove = false;
                    foreach (var move in possibleMoves)
                    {
                        if (move == currentPoint)
                        {
                            isPossibleMove = true;
                            break;
                        }
                    }

                    Console.BackgroundColor = isPossibleMove ? ConsoleColor.Green :
                        Convert.ToBoolean((j + i) % 2) ? ConsoleColor.DarkGray : ConsoleColor.Gray;
                }

                Console.Write("  ");
            }
            Console.WriteLine();
        }
        Console.ResetColor();
    }

    public static void Main(string[] args)
    {
        Console.OutputEncoding = UTF8Encoding.UTF8;
        Console.InputEncoding = UTF8Encoding.UTF8;

        Chessman chessman1 = new ChessPawn(4, 4, "white");

        List<Point> possibleMovesList1 = chessman1.PossibleMoves();

        Point[] possibleMoves = possibleMovesList1.ToArray();

        DrawBoard(chessman1.Position, possibleMoves);
    }
}




//drugoe




using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    abstract class Chessman
    {
        protected int x;
        protected int y;
        protected string team;

        public Chessman(int x, int y, string team)
        {
            this.x = x;
            this.y = y;
            this.team = team;
        }

        public Point Position => new Point(x, y);

        public abstract List<Point> PossibleMoves();

        public bool IsCanMove(int newX, int newY)
        {
            foreach (var move in PossibleMoves())
            {
                if (move.X == newX && move.Y == newY)
                    return true;
            }
            return false;
        }

        protected bool IsValidPosition(int x, int y)
        {
            return x >= 1 && x <= 8 && y >= 1 && y <= 8;
        }
    }

    class ChessPawn : Chessman
    {
        public ChessPawn(int x, int y, string team) : base(x, y, team) { }

        public override List<Point> PossibleMoves()
        {
            List<Point> moves = new List<Point>();
            int direction = team == "white" ? -1 : 1;

            if (IsValidPosition(x, y + direction))
                moves.Add(new Point(x, y + direction));

            return moves;
        }
    }

    class ChessRook : Chessman
    {
        public ChessRook(int x, int y, string team) : base(x, y, team) { }

        public override List<Point> PossibleMoves()
        {
            List<Point> moves = new List<Point>();
            for (int i = 1; i <= 8; i++)
            {
                if (i != y) moves.Add(new Point(x, i));
                if (i != x) moves.Add(new Point(i, y));
            }
            return moves;
        }
    }

    class ChessKnight : Chessman
    {
        public ChessKnight(int x, int y, string team) : base(x, y, team) { }

        public override List<Point> PossibleMoves()
        {
            List<Point> moves = new List<Point>
            {
                new Point(x - 2, y - 1), new Point(x - 1, y - 2), new Point(x + 1, y - 2), new Point(x + 2, y - 1),
                new Point(x + 2, y + 1), new Point(x + 1, y + 2), new Point(x - 1, y + 2), new Point(x - 2, y + 1)
            };

            List<Point> validMoves = new List<Point>();
            foreach (var move in moves)
            {
                if (IsValidPosition(move.X, move.Y))
                    validMoves.Add(move);
            }
            return validMoves;
        }
    }

    class ChessBishop : Chessman
    {
        public ChessBishop(int x, int y, string team) : base(x, y, team) { }

        public override List<Point> PossibleMoves()
        {
            List<Point> moves = new List<Point>();
            for (int i = 1; i <= 8; i++)
            {
                if (IsValidPosition(x + i, y + i)) moves.Add(new Point(x + i, y + i));
                if (IsValidPosition(x - i, y + i)) moves.Add(new Point(x - i, y + i));
                if (IsValidPosition(x + i, y - i)) moves.Add(new Point(x + i, y - i));
                if (IsValidPosition(x - i, y - i)) moves.Add(new Point(x - i, y - i));
            }
            return moves;
        }
    }

    class ChessQueen : Chessman
    {
        public ChessQueen(int x, int y, string team) : base(x, y, team) { }

        public override List<Point> PossibleMoves()
        {
            List<Point> moves = new ChessRook(x, y, team).PossibleMoves();
            foreach (var move in new ChessBishop(x, y, team).PossibleMoves())
            {
                bool exists = false;
                foreach (var m in moves)
                {
                    if (m.X == move.X && m.Y == move.Y)
                    {
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                    moves.Add(move);
            }
            return moves;
        }
    }

    class ChessKing : Chessman
    {
        public ChessKing(int x, int y, string team) : base(x, y, team) { }

        public override List<Point> PossibleMoves()
        {
            List<Point> moves = new List<Point>
            {
                new Point(x - 1, y - 1), new Point(x, y - 1), new Point(x + 1, y - 1),
                new Point(x - 1, y), new Point(x + 1, y), new Point(x - 1, y + 1), new Point(x, y + 1), new Point(x + 1, y + 1)
            };

            List<Point> validMoves = new List<Point>();
            foreach (var move in moves)
            {
                if (IsValidPosition(move.X, move.Y))
                    validMoves.Add(move);
            }
            return validMoves;
        }
    }
}
