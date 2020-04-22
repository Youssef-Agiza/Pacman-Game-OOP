#pragma once
#include "Character.h"


class Pacman :
    public Character {

private:

    const unsigned int mIntialColumn;
    const unsigned int mIntialRow;
    unsigned int mLives;
    //void getRow(); //moved to character
    //void getColumn();

    //void getDis();

public:
    Pacman(int intialRow, int intialColumn, float size, float posWindow);



    Pacman& setLives(unsigned int l);
    unsigned int getLives()const;
        
    void resetPosition()override;
     
};
