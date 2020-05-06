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
         float mSize; //=tileSize in the board
         sf::Vector2f mPositionOnWindow;
         bool mAlive;
         Texture* mTexture;
         Direction mDirection;
         Board* mBoard;
         Sprite mSprite;

         void adjustScale(float imagesPerRow,float imagesPerCol) ; //adjusts sprite scale and IntRect. Called in setTexture

    public:


        Character(int intialRow, int intialColumn, float size,sf::Vector2f posWindow);
        ~Character();

        //getters
        bool isAlive()const;
        Direction getDirection()const;
        unsigned int getRow()const;
        unsigned int getCol()const;
        float getSize()const;
        const sf::Vector2f& getPosition()const;
        const Texture& getTexture()const;
        int getVertex()const;


        //setters
        Character& setSize(float size);
        Character& setAlive(bool status);
        Character& setRow(unsigned int row);
        Character& setCol(unsigned int col);
        Character& setTexture(std::string fileName,float imagesPerRow=1.0f,float imagesPerCol=1.0f); //calls adjustScale
        Character& setPosition(float x,float y);
        Character& setPosition(sf::Vector2f position);
        Character& setBoard(Board* board);
        
       
  
       //virtuals   
        virtual Character& setDirection(Direction dir);
       virtual void drawOnWindow(RenderWindow& window);
       virtual void updateShape(); //updates new shape data
       virtual int checkDestination(Direction d)const; //returns 0 if block, 1 if valid direction, 2 if portal
       virtual void move() = 0;
       virtual void die() = 0;
       virtual void resetPosition() = 0;
       virtual void animateMove() = 0;
       virtual void animateDie() = 0;
    };

