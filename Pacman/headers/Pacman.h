#pragma once
#include "Character.h"
#include "Board.h"
using namespace PacmanCS;
class Pacman :
    public Character {

private:

    const unsigned int mIntialColumn;
    const unsigned int mIntialRow;
    unsigned int mLives;
    Board* mBoard;
    //void getRow(); //moved to character
    //void getColumn();

    //void getDis();

public:
    Pacman(int intialRow, int intialColumn, float size, float posWindow);


    Pacman& setBoard(Board* board);
    
    Pacman& setLives(unsigned int l);
    unsigned int getLives()const;
        
    bool isValidDirection(Direction d);
    void resetPosition()override;
    //void move()override; //overrides to check for blocks
     
};
