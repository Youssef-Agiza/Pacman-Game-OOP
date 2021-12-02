CC= g++
CFLAGS=  -lsfml-graphics -lsfml-window -lsfml-system -lsfml-audio

OBJ_DIR= ./obj
HEADERS_DIR=./headers
SRC_DIR=./sources
BUILD_DIR=./build



pacman: $(SRC_DIR)/*.cpp $(HEADERS_DIR)/*.h
	$(CC) $(SRC_DIR)/*.cpp -I $(HEADERS_DIR) $(CFLAGS) -o $(BUILD_DIR)/pacman