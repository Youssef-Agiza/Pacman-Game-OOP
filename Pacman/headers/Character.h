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
         float mSize;
         float mPositionOnWindow;
         float mSpeed;
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


        Character(int intialRow, int intialColumn, float size,float posWindow);


        //getters
        bool isAlive()const;
        int getDirection()const;
        unsigned int getRow()const;
        unsigned int getCol()const;
        float getSize()const;
        float getPosition()const;
        const Texture& getTexture()const;
        float getSpeed()const;

        //setters
        Character& setSize(float size);
        Character& setAlive(bool status);
        Character& setRow(unsigned int row);
        Character& setCol(unsigned int col);
        Character& setDirection(Direction dir);
        Character& setTexture(std::string fileName);
        Character& setPosition(float position);
        Character& setSpeed(float s);
        


       virtual void resetPosition();
       virtual void move();
       virtual void drawOnWindow(RenderWindow& window);
       virtual void updateShape(); //updates new shape data
    };

