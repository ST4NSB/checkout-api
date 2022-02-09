#!/bin/bash

start https://localhost:5005/swagger

docker-compose build

docker-compose up

$SHELL