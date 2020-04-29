#pragma once
#include <iostream>
#include <SFML/Audio.hpp>
#include <SFML/Graphics.hpp>
#include "Board.h"

using namespace std;
using namespace sf;
using namespace PacmanCS;

//enum { SCATTER, CHASE, FRIGHTEN, LEAVE, DEATH, PEN } movement; //state pattern 

enum Direction { UP, RIGHT, DOWN, LEFT,STOP };//case stop used to intialize the enum

class Character {

    protected:
      


         unsigned int mCurrentRow;
         unsigned int mCurrentColumn;
         float mSize;
         float mPositionOnWindow;
         float mSpeed;
         bool mAlive;
         RectangleShape mShape;
         Texture mTexture;
        Direction mDirection;
        Board* mBoard;

    public:


        Character(int intialRow, int intialColumn, float size,float posWindow);
        ~Character();

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
        Character& setBoard(Board* board);
        

        virtual void die()= 0;
       virtual void resetPosition()=0;
       virtual void move();
       virtual void drawOnWindow(RenderWindow& window);
       virtual void updateShape(); //updates new shape data
       virtual int checkDestination(Direction d)const; //returns 0 if block, 1 if valid direction, 2 if portal
    };

