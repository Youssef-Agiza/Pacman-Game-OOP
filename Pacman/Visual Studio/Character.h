#pragma once
#include <iostream>
#include <SFML/Audio.hpp>
#include <SFML/Graphics.hpp>


using namespace std;
using namespace sf;

//enum { SCATTER, CHASE, FRIGHTEN, LEAVE, DEATH, PEN } movement; //state pattern 
enum Direction { UP, RIGHT, DOWN, LEFT };

class Character {

    protected:
      
  // const unsigned int mIntialRow;
//const unsigned int mIntialColumn;
   //  unsigned int mPosition;
//  Texture mTextures;

         unsigned int mCurrentRow;
         unsigned int mCurrentColumn;
         unsigned int mSize;
         bool mAlive;
         RectangleShape mShape;
         Texture mTexture;
        Direction mDirection;

    public:
        // when they die
         //and add the number of lives
         //unsigned int

       /* bool isEaten();
        bool dead();
        void cornerHandler();*/


        Character(int intialRow, int intialColumn, int size);


        //getters
        bool isAlive()const;
        int getDirection()const;
        unsigned int getRow()const;
        unsigned int getCol()const;
        unsigned int getSize()const;
        const Texture& getTexture()const;


        //setters
        Character& setAlive(bool);
        Character& setRow(unsigned int);
        Character& setCol(unsigned int);
        Character& setDirection(Direction);
        Character& setTexture(std::string);
        


       virtual void resetPosition();
       virtual void move(Direction);
       virtual void drawOnWindow(RenderWindow& window);
       virtual void updateShape(); //updates new shape data
    };

