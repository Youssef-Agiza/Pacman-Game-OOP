#pragma once
#include "Character.h"
#include "../design patterns/ISubject.h"
using namespace PacmanCS;
class Pacman :
    public Character, public ISubject {

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

    //observer functions
    void addObserver(IObserver* observer)override;
    void removeObserver(IObserver* observer)override;
    void notify()override;
};
