#pragma once
#include "Character.h"
#include "Board.h"
using namespace PacmanCS;
class Pacman :
    public Character {

private:

    unsigned int mLives;
 
  
public:
    Pacman(int intialRow, int intialColumn, float size, float posWindow);



    
    Pacman& setLives(unsigned int l);
    unsigned int getLives()const;
        
    void resetPosition()override;
    void die();
};
