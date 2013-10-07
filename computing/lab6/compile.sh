#!/bin/bash
gcc -c client.c 
gcc -c server.c
gcc -g -O -o client client.o
gcc -g -O -o server server.o
