#!/usr/bin/env bash

echo 'clean publish folder'
cd ../../
pwd
# rm -r publish/*
echo 'webpack build'
cd Vue.Core/ClientApp
npm run build:prod
cd ../../
dotnet publish -r linux-x64 -c Release -o ./publish/ -p:PublishSingleFile=true
docker build . -f ./Dockerfile -t zlinebot:production
cd Vue.Core/docker
docker-compose -f app.yml down
docker-compose -f app.yml up
#清除Temp images
#echo y|docker system prune --all

cmd /k