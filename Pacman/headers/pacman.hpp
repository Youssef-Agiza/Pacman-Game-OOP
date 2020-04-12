//
//  pacman.hpp
//  character
//
//  Created by Nada Mohamed  on 4/11/20.
//  Copyright © 2020 Nada Mohamed . All rights reserved.
//

#ifndef pacman_hpp
#define pacman_hpp
#include "character.cpp"

#include <stdio.h>
class Pacman: public Character{
    
private:
  //  character paCman;
    int intialRow;
    int intialCol;
    int numLives;
    CircleShape c;
    
    
public:
    //void setLives(); for infinity mode
    //void getLives();//const if not infinity
    pacman( int intialRow, int intialColumn, string fileName);
    //bool Collide();
    void Dead();
    void newLife();
    void Points();
    bool touchFood();
    
    
};

#endif /* pacman_hpp */
