import java.util.*;

/*
   Point is a simple class with a constructor and an int to string method
*/
class Point {
    int x, y;

    //constructor that takes two integers as arguments to define a point
    public Point(int x, int y) {
        this.x = x;
        this.y = y;
    }

    @Override
    //method for converting integer to string
    public String toString() {
        return "[" + (x + 1) + ", " + (y + 1) + "]";
    }
}

/*
   PointsAndScores is a simple class with a constructor
*/
class PointsAndScores {
    int score;
    Point point;

    //constructor that takes a Point object and its score as arguments
    PointsAndScores(int score, Point point) {
        this.score = score;
        this.point = point;
    }
}

/*
   Board is a class that contains methods for checking game status and position status,
   getting available moves (empty positions on the board), placing a player's move at a given position,
   and displaying the current board.
*/
class Board {
    List<Point> availablePoints; //an instance of the List class, list of Point objects
    Scanner scan = new Scanner(System.in); //an instance of the Scanner class
    int[][] board = new int[5][5]; //an integer array holding the 5x5 game positions. 0 - empty, 1 - Player 'X', 2 - Player 'O'.


    //constructor
    public Board() {
    }


    //method for checking whether the game is over
    public boolean isGameOver() {
        return (hasXWon() || hasOWon() || getAvailablePoints().isEmpty());
    }

    //method for checking whether player 'X' has won (Player 'X' is represented as 1)
    public boolean hasXWon() {
//         `X` = AI = 1
//        DIAGONAL LINES WIN CONDITIONS
        if ((board[0][0] == board[1][1] && board[0][0] == board[2][2] && board[0][0] == board[3][3] && board[0][0] == board[4][4] && board[0][0] == 1)
                || (board[0][4] == board[2][2] && board[0][4] == board[3][1] && board[0][4] == board[1][3] && board[0][4] == board[4][0] && board[0][4] == 1)) {
            return true; //AI WON
        }
//        ROWS AND COLUMNS WIN CONDITIONS
        for (int i = 0; i < 5; ++i) {
            if (((board[i][0] == board[i][1] && board[i][0] == board[i][2] && board[i][0] == board[i][3] && board[i][0] == board[i][4] && board[i][0] == 1)
                    || (board[0][i] == board[1][i] && board[0][i] == board[2][i] && board[0][i] == board[3][i] && board[0][i] == board[4][i] && board[0][i] == 1))) {
                return true; //AI WON
            }
        }
        return false;
    }

    //method for checking whether player 'O' has won (Player 'O' is represented as 2)
    public boolean hasOWon() {
//        `O` = HUMAN = 2

//        DIAGONAL LINES WIN CONDITIONS
        if ((board[0][0] == board[1][1] && board[0][0] == board[2][2] && board[0][0] == board[3][3] && board[0][0] == board[4][4] && board[0][0] == 2)
                || (board[0][4] == board[3][1] && board[0][4] == board[2][2] && board[0][4] == board[1][3] && board[0][4] == board[4][0] && board[0][4] == 2)) {
            return true; //HUMAN WON
        }
//        ROWS AND COLUMNS WIN CONDITIONS
        for (int i = 0; i < 5; ++i) {
            if ((board[i][0] == board[i][1] && board[i][0] == board[i][2] && board[i][0] == board[i][3] && board[i][0] == board[i][4] && board[i][0] == 2)
                    || (board[0][i] == board[1][i] && board[0][i] == board[2][i] && board[0][i] == board[3][i] && board[0][i] == board[4][i] && board[0][i] == 2)) {
                return true; //HUMAN WON
            }
        }
        return false;
    }

    //method for getting available moves on the current board (empty positions on the board)
    public List<Point> getAvailablePoints() {
        availablePoints = new ArrayList<>();
        for (int i = 0; i < 5; ++i) {       //FOR LOOP DETERMINES GRID SIZE ACKNOWLEDGE BY THE AI.
            for (int j = 0; j < 5; ++j) {
                if (board[i][j] == 0) {
                    availablePoints.add(new Point(i, j));  //Add an empty position to availablePoints
                }
            }
        }
        return availablePoints;
    }


    //method for getting the status of a position
    public int getState(Point point) {
        return board[point.x][point.y];
    }

    //method for placing a player's move at a given position
    public void placeAMove(Point point, int player) {
        board[point.x][point.y] = player;
    }

    //method for displaying the current board
    public void displayBoard() {
        System.out.println();

        for (int i = 0; i < 5; ++i) {     //FOR LOOP DETERMINES GRID SIZE DISPLAYED
            for (int j = 0; j < 5; ++j) {
                if (board[i][j] == 1)
                    System.out.print("X "); //AI
                else if (board[i][j] == 2)
                    System.out.print("O "); //PLAYER
                else
                    System.out.print(". ");
            }
            System.out.println();
        }
    }
}