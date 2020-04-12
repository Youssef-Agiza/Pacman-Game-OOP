//
//  character.hpp
//  character
//
//  Created by Nada Mohamed  on 4/11/20.
//  Copyright © 2020 Nada Mohamed . All rights reserved.
//

#ifndef character_hpp
#define character_hpp
#include <stdio.h>
#include <SFML/Graphics.hpp>
#include <iostream>
#include <fstream>
using namespace std;
using namespace sf;
class Character{
    
protected:
    int mIntialRow;
    int mIntialColumn;
    int mCurrentRow;
    int mCurrentColumn;
    Texture mTextures;
    Shape* mShape;
public:
   
    
    Character( int intialRow, int intialColumn, string imageFile);
    void Move(char direction);
   // bool resetPosition();
    void drawOnWindow(RenderWindow &window);

};

#endif /* character_hpp */
