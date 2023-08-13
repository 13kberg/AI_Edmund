import java.util.*;

import static java.util.Collections.max;

/*
   AIplayer is a class that contains the methods for implementing the minimax search for playing the TicTacToe game.
*/
class AIplayer {
    List<Point> availablePoints; //an instance of the List class, a list of Point objects (equivalent to possible moves)
    List<PointsAndScores> rootsChildrenScores; //an instance of the List class, a list of PointsAndScores objects holding the available moves and their values at the root of the search tree, i.e., the current game board.
    Board b = new Board(); //an instance of the Board class

    //constructor
    public AIplayer() {
    }

    //method for returning the best move, the position that has the maximum value among all the available positions
    public Point returnBestMove() {
        int MAX = -100000; //The scores are not negative
        int best = -1; //The index for available positions are not negative

        for (int i = 0; i < rootsChildrenScores.size(); ++i) {
            if (MAX < rootsChildrenScores.get(i).score) {
                MAX = rootsChildrenScores.get(i).score;
                best = i;
            }
        }
        return rootsChildrenScores.get(best).point; //Return the position that has the maximum value among all the available positions
    }

    //method for returning the minimum value in a list
    public int returnMin(List<Integer> list) {
        int min = Integer.MAX_VALUE;
        int index = -1;

        for (int i = 0; i < list.size(); ++i) {
            if (list.get(i) < min) {
                min = list.get(i);
                index = i;
            }
        }
        return list.get(index);
    }

    //method for returning the maximum value in a list
    public int returnMax(List<Integer> list) {
        int max = Integer.MIN_VALUE;
        int index = -1;

        for (int i = 0; i < list.size(); ++i) {
            if (list.get(i) > max) {
                max = list.get(i);
                index = i;
            }
        }
        return list.get(index);
    }

    //method for calling minimax search, which takes depth, turn of play, and board as arguments.
    public void callMinimax(int depth, int turn, Board b) {
        rootsChildrenScores = new ArrayList<>();
        minimax(depth, turn, Integer.MIN_VALUE, Integer.MAX_VALUE, b); //method for implementing the minimax search algorithm with alpha-beta pruning
    }

    /*
       minimax is a method for implementing the minimax search algorithm with alpha-beta pruning in a recursive manner,
       which takes depth, turn of play, alpha, beta, and board as arguments.
       Set depth=0 for the current game position that is the root of the search tree.
       turn=1 represents the AI player's turn to play whilst turn=2 represents the user's turn to play.
       The AI player is player 'X' and the user is player 'O'.
       This method is the most important component of the TicTacToe program.
    */
    public int minimax(int depth, int turn, int alpha, int beta, Board b) { //added alpha, beta for alpha-beta pruning
        if (b.hasXWon())
            return 1; //If player 'X' has won, the minimax search reaches a win endgame and returns 1 as the value of this endgame position
        if (b.hasOWon())
            return -1; //If player 'O' has won, the minimax search reaches a loss endgame and returns -1 as the value of this endgame position
        List<Point> pointsAvailable = b.getAvailablePoints(); //Get a list of available moves on the current board
        if (pointsAvailable.isEmpty())
            return 0; //If there is no available move, the minimax search reaches a draw endgame and returns 0 as the value of this endgame position.

        List<Integer> scores = new ArrayList<>(); //an instance of the List class, a list of integer scores holding the values of all the game positions in the next depth

        int temp;
        if (turn == 1)
            temp = Integer.MAX_VALUE; //added for alpha-beta pruning    //SWAPPED VALUE SWAPPED AS TURN 1 = AI
        else temp = Integer.MIN_VALUE; //added for alpha-beta pruning


        for (int i = 0; i < pointsAvailable.size(); ++i) { //for each available move (position) ...
            Point point = pointsAvailable.get(i); //Choose an available position in natural order to make a move

            if (turn == 1) {//if it is the AI player's turn to play ...
                if (depth > 6) {

                    int heuristicScore = heuristic(b.board);
                    scores.add(heuristicScore); //ADD THE VALUE RETURNED FROM THE HEURISTIC FUNCTION TO THE SCORE LIST

                    temp = Math.max(temp, heuristicScore); //added for alpha-beta pruning
                    alpha = Math.max(alpha, temp); //added for alpha-beta pruning

//                    System.out.println("DEPTH HIGHER THAN 6");  //TESTING
//                    System.out.println("HEURISTIC TESTING: " + point +  heuristicScore);   //TESTING

                }

                    b.placeAMove(point, 1); //Make the chosen move to obtain a new board, i.e., a new game position in the next depth
                    int currentScore = minimax(depth + 1, 2, alpha, beta, b); //Recursively call the minimax method with the new board, the corresponding depth and the user's turn as arguments. The recursion ends when reaching an endgame position.
                    scores.add(currentScore); //Add the value of the game position to the list of scores
//                    temp = Math.max(temp, currentScore); //added for alpha-beta pruning
//                    alpha = Math.max(alpha, temp); //added for alpha-beta pruning

                    if (depth == 0) //If it is at the root, add the chosen move and the corresponding value (score) to the list rootsCheldrenScores
                        rootsChildrenScores.add(new PointsAndScores(currentScore, point));



            } else if (turn == 2) { //if it is the user's turn to play ...
                b.placeAMove(point, 2); //Make the chosen move to obtain a new board, i.e., a new game position in the next depth
                int currentScore = minimax(depth + 1, 1, alpha, beta, b); //Recursively call the minimax method with the new board, the corresponding depth and the user's turn as arguments. The recursion ends when reaching an endgame position.
                scores.add(currentScore);
                temp = Math.min(temp, currentScore); //added for alpha-beta pruning
                beta = Math.min(beta, temp); //added for alpha-beta pruning
                //Recursively call the minimax method with the new board, the corresponding depth and the AI player's turn as arguments. The recursion ends when reaching an endgame position.
                //Add the value of the game position to the list of scores
            }

            b.placeAMove(point, 0); //Clear the chosen position after its value has been obtained by the minimax search.
            if (alpha >= beta) { //Check whether alpha is equal to or greater than beta. If so, there is no need to valuate the remaining game positions at this depth.
//                temp = pointsAvailable.size() - i - 1;
//                System.out.println("Number of nodes that have not been evaluated here : " + temp);
                break;
            }
        }

        return turn == 1 ?

                returnMax(scores) :

                returnMin(scores); //If it is AI player's turn, return the maximum value. Otherwise, return the minimum value.

    }


    /*METHOD FOR EVALUATING THE HEURISTIC VALUE OF THE CURRENT GAME STATE
     *HEURISTIC FUNCTION USED:
     * - WHEN THERE IS BOTH AN `X` AND AN `O` IN A LINE THE VALUE TO THE AI IS 0
     * -- WHEN A ROW/COLUMN/DIAGONAL IS UNCONTESTED:
     *     ++AI PLAYER
     *       + 1 `X` IN THAT GIVEN ROW/COLUMN/DIAGONAL = 1 TO THE AI
     *       + 2 `X`'S IN THAT GIVEN ROW/COLUMN/DIAGONAL = 10 TO THE AI
     *       + 3 `X`'S IN THAT GIVEN ROW/COLUMN/DIAGONAL = 100 TO THE AI
     *       + 4 `X`'S IN THAT GIVEN ROW/COLUMN/DIAGONAL = 1000 TO THE AI
     *       + 5 `X`'S IN THAT GIVEN ROW/COLUMN/DIAGONAL = Integer.MAX_VALUE TO THE AI
     *     ++ HUMAN PLAYER
     *       + 1 `O` IN THAT GIVEN ROW/COLUMN/DIAGONAL = -1 TO THE AI
     *       + 2 `O`'S IN THAT GIVEN ROW/COLUMN/DIAGONAL = -10 TO THE AI
     *       + 3 `O`'S IN THAT GIVEN ROW/COLUMN/DIAGONAL = -100 TO THE AI
     *       + 4 `O`'S IN THAT GIVEN ROW/COLUMN/DIAGONAL = -1000 TO THE AI
     *       + 5 `O`'S IN THAT GIVEN ROW/COLUMN/DIAGONAL = Integer.MIN_VALUE TO THE AI
     */
    private static int heuristic(int[][] board) {
//        COUNT VARIABLES FOR THE ROW AND COLUMN DECLARATION
//        HUMAN
        int countRowX;
        int countRowO;
//        AI
        int countColX;
        int countColO;

//        DIAGONAL COUNT VARIABLES INSTANTIATED. 0 FOR BOTH AI/HUMAN
//        FIRST = (0, 0) to (4, 4)
        int xTopLeftDiagCount = 0;
        int oTopLeftDiagCount = 0;
//        SECOND = (0, 4) to (4, 0)
        int xTopRightDiagCount = 0;
        int oTopRightDiagCount = 0;

//        INSTANTIATED VARIABLE TO STORE THE SCORE
        int heuristicScore = 0;

//        ITERATE 0-4
        for (int i = 0; i < 5; i++) {
//            Reset the row and column counts as a new row and column is being evaluated on each iteration of i.
//            RESET ROW/COLUMN ON EACH ITERATION OF i AS ITS A NEW ROW/COLUMN BEING ASSESSED
            countRowX = 0;
            countRowO = 0;
            countColX = 0;
            countColO = 0;

//        ITERATE 0-4
            for (int j = 0; j < 5; j++) {

//              COLUMN CHECKED USING i AND j
//              e.g. (i=0, j=0), ..., (i=0, j=4)
                if (board[i][j] == 1) {
                    countRowX++;
                } else if (board[i][j] == 2) {
                    countRowO++;
                }

//              COLUMN CHECKED USING j AND i
//              e.g. (j=0, i=0), ..., (j=4, i=0)
                if (board[j][i] == 1) {
                    countColX++;
                } else if (board[j][i] == 2) {
                    countColO++;
                }
//              WHEN i = j IS EQUAL TO A POINT ON THE (FIRST (0, 0) to (4, 4)) DIAG LINE
                if (i == j) {
                    if (board[i][j] == 1) {
                        xTopLeftDiagCount++;
                    } else if (board[i][j] == 2) {
                        oTopLeftDiagCount++;
                    }
                }
//              WHEN i + j =4 IS EQUAL TO A POINT ON THE (SECOND (0, 4) to (4, 0)) DIAG LINE
                if (i + j == 4) {
                    if (board[i][j] == 1) {
                        xTopRightDiagCount++;
                    } else if (board[i][j] == 2) {
                        oTopRightDiagCount++;
                    }
                }
            }

//        IF countColX GREATER THAN 0 AND countColO EQUALS 0. `X`'S ARE IN AN UNCONTESTED ROW
            if (countRowX > 0 && countRowO == 0) {
//              SCORE DETERMINED BY NUMBER OF UNCONTESTED `X` IN ROW
                if (countRowX == 1) {
                    heuristicScore += 1;
                } else if (countRowX == 2) {
                    heuristicScore += 10;
                } else if (countRowX == 3) {
                    heuristicScore += 100;
                } else if (countRowX == 4) {
                    heuristicScore += 1000;
                } else {
                    return Integer.MAX_VALUE;
                }
            }
//        IF countColO GREATER THAN 0 AND countColX EQUALS 0. `O`'S ARE IN AN UNCONTESTED ROW
            else if (countRowO > 0 && countRowX == 0) {
//              SCORE DETERMINED BY NUMBER OF UNCONTESTED `O` IN ROW
                if (countRowO == 1) {
                    heuristicScore -= 1;
                } else if (countRowO == 2) {
                    heuristicScore -= 10;
                } else if (countRowO == 3) {
                    heuristicScore -= 100;
                } else if (countRowO == 4) {
                    heuristicScore -= 1000;
                } else {
                    return Integer.MIN_VALUE;
                }
            }
//        IF countColX GREATER THAN 0 AND countColO EQUALS 0. `X`'S ARE IN AN UNCONTESTED COL
            if (countColX > 0 && countColO == 0) {
//              SCORE DETERMINED BY NUMBER OF UNCONTESTED `X` IN COLUMN
                if (countColX == 1) {
                    heuristicScore += 1;
                } else if (countColX == 2) {
                    heuristicScore += 10;
                } else if (countColX == 3) {
                    heuristicScore += 100;
                } else if (countColX == 4) {
                    heuristicScore += 1000;
                } else {
                    return Integer.MAX_VALUE;
                }
            }
//        IF countColO GREATER THAN 0 AND countColX EQUALS 0. `O`'S ARE IN AN UNCONTESTED COL
            else if (countColO > 0 && countColX == 0) {
//              SCORE DETERMINED BY NUMBER OF UNCONTESTED `O` IN COLUMN
                if (countColO == 1) {
                    heuristicScore -= 1;
                } else if (countColO == 2) {
                    heuristicScore -= 10;
                } else if (countColO == 3) {
                    heuristicScore -= 100;
                } else if (countColO == 4) {
                    heuristicScore -= 1000;
                } else {
                    return Integer.MIN_VALUE;
                }
            }
        }

//        IF xTopRightDiagCount GREATER THAN 0 AND oTopRightDiagCount EQUALS 0. `X`'S ARE IN AN UNCONTESTED FIRST DIAG
        if (xTopLeftDiagCount > 0 && oTopLeftDiagCount == 0) {
//          SCORE DETERMINED BY NUMBER OF UNCONTESTED `X` IN FIRST DIAGONAL FOLLOWING THE RULES OF THE HEURISTIC FUNCTION
            if (xTopLeftDiagCount == 1) {
                heuristicScore += 1;
            } else if (xTopLeftDiagCount == 2) {
                heuristicScore += 10;
            } else if (xTopLeftDiagCount == 3) {
                heuristicScore += 100;
            } else if (xTopLeftDiagCount == 4) {
                heuristicScore += 1000;
            } else {
                return Integer.MAX_VALUE;
            }
        }
//        IF oTopRightDiagCount GREATER THAN 0 AND xTopRightDiagCount EQUALS 0. `O`'S ARE IN AN UNCONTESTED FIRST DIAG
        else if (oTopLeftDiagCount > 0 && xTopLeftDiagCount == 0) {

//          SCORE DETERMINED BY NUMBER OF UNCONTESTED `O` IN FIRST DIAGONAL FOLLOWING THE RULES OF THE HEURISTIC FUNCTION
            if (oTopLeftDiagCount == 1) {
                heuristicScore -= 1;
            } else if (oTopLeftDiagCount == 2) {
                heuristicScore -= 10;
            } else if (oTopLeftDiagCount == 3) {
                heuristicScore -= 100;
            } else if (oTopLeftDiagCount == 4) {
                heuristicScore -= 1000;
            } else {
                return Integer.MIN_VALUE;
            }
        }
//        IF xTopRightDiagCount GREATER THAN 0 AND oTopRightDiagCount EQUALS 0. `X`'S ARE IN AN UNCONTESTED SECOND DIAG
        if (xTopRightDiagCount > 0 && oTopRightDiagCount == 0) {
//          SCORE DETERMINED BY NUMBER OF UNCONTESTED `X` IN SECOND DIAGONAL FOLLOWING THE RULES OF THE HEURISTIC FUNCTION
            if (xTopRightDiagCount == 1) {
                heuristicScore += 1;
            } else if (xTopRightDiagCount == 2) {
                heuristicScore += 10;
            } else if (xTopRightDiagCount == 3) {
                heuristicScore += 100;
            } else if (xTopRightDiagCount == 4) {
                heuristicScore += 1000;
            } else {
                return Integer.MAX_VALUE;
            }
        }
//        IF oTopRightDiagCount GREATER THAN 0 AND xTopRightDiagCount EQUALS 0. `O`'S ARE IN AN UNCONTESTED SECOND DIAG
        else if (oTopRightDiagCount > 0 && xTopRightDiagCount == 0) {

//          SCORE DETERMINED BY NUMBER OF UNCONTESTED `O` IN SECOND DIAGONAL FOLLOWING THE RULES OF THE HEURISTIC FUNCTION
            if (oTopRightDiagCount == 1) {
                heuristicScore -= 1;
            } else if (oTopRightDiagCount == 2) {
                heuristicScore -= 10;
            } else if (oTopRightDiagCount == 3) {
                heuristicScore -= 100;
            } else if (oTopRightDiagCount == 4) {
                heuristicScore -= 1000;
            } else {
                return Integer.MIN_VALUE;
            }
        }

//        RETURNS THE HEURISTICS SCORE AS AN INT
        return heuristicScore;
    }
}



