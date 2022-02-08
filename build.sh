#!/bin/bash

docker-compose build

docker-compose up

start https://localhost:5005/swagger

$SHELL