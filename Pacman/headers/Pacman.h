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


    //inherited from character
    virtual void resetPosition()override;
    virtual void die()override;
    virtual void move()override;
    virtual void animateMove()override; //note: this function is not flexible as it is adujsted for certain values only.
                                          //i.e. if the sprite sheet changed it must be changed as well.
    virtual void animateDie()override{}



    //observer functions
    void addObserver(IObserver* observer)override;
    void removeObserver(IObserver* observer)override;
    void notify()override;
       
 

};
