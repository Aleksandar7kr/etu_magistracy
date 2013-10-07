#!/bin/bash
gcc -c sender.c receiver.c
gcc -g -O -o sender sender.o -lrt
gcc -g -O -o receiver receiver.o -lrt

