#pragma once
#include "Character.h"
#include "Board.h"
#include "../design patterns/IObserver.h"
class Ghost :
    public Character, public IObserver {

private:
    unsigned int mIntialRow;
    unsigned int mIntialCol;
    bool mFreight; 
    std::string mResource; //keeps resource for the texture

public:
    Ghost(int intialRow, int intialColumn, float size, sf::Vector2f position);
  
  
    unsigned int getIntialRow()const;
    unsigned int getIntialCol()const;
    const std::string& getResource()const;
    bool getFreight()const;


    Ghost& setIntialRow(unsigned int row);
    Ghost& setIntialCol(unsigned int col);
    Ghost& setFreight(bool f);
    Ghost& setResource(std::string resource);



    //Iobserver method
    void update(bool powerUp)override;
    
    // Inherited via Character
    virtual void resetPosition()override;
    virtual void die()override;
    virtual void animateMove() override;
    virtual void animateDie() override;
    virtual void move()override;
 
};

