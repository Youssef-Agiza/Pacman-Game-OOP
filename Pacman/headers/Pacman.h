#pragma once
#include "Character.h"
using namespace PacmanCS;
class Pacman :
    public Character {

private:

    unsigned int mLives;
    bool mPowerUp; //ate big pellet

  
public:
    Pacman(int intialRow, int intialColumn, float size, float posWindow);



    Pacman& setPowerUp(bool p);
    bool getPowerUp()const;
    Pacman& setLives(unsigned int l);
    unsigned int getLives()const;
        
    void resetPosition()override;
    void die();
};
