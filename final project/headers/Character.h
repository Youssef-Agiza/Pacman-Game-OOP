#pragma once
#include <iostream>
#include <SFML/Audio.hpp>
#include <SFML/Graphics.hpp>
#include "../headers/Board.h"

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
         sf::Clock currentTime;
         int speedTimer; //variable to keep track of timing for ghosts

         void adjustScale(int imagesPerRow,int imagesPerCol) ; //adjusts sprite scale and IntRect. Called in setTexture

    public:


        Character(int intialRow, int intialColumn, float size,Board* board);
        virtual  ~Character();

        //getters
        bool isAlive()const;
        Direction getDirection()const;
        unsigned int getRow()const;
        unsigned int getCol()const;
        float getSize()const;
        const sf::Vector2f& getPosition()const;
        const Texture& getTexture()const;
        int getVertex()const;
        const sf::Sprite& getSprite()const;
       

        //setters
        Character& setSize(float size);
        Character& setAlive(bool status);
        Character& setRow(unsigned int row);
        Character& setCol(unsigned int col);
        Character& setTexture(std::string fileName,int imagesPerRow=1,int imagesPerCol=1); //calls adjustScale
        Character& setPositionOnWindow(float x,float y);
        Character& setPositionOnWindow(sf::Vector2f position);
        Character& setBoard(Board* board);
        
       
        const sf::Time getCurrentTime()const;
        void resetTime();
        Character& setSpeedTimer(int s);
        int getSpeedTimer()const;

       //virtuals   
        virtual Character& setDirection(Direction dir);
       virtual void drawOnWindow(RenderWindow& window);
       virtual void updateShape(); //updates new shape data
       virtual int checkDestination(Direction d)const; //returns 0 if block, 1 if valid direction, 2 if portal
       virtual void move() = 0;
       virtual void die(sf::RenderWindow& w) = 0;
       virtual void resetPosition() = 0;
       virtual void animateMove() = 0;
     

    };

